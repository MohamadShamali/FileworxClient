using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FileworxDTOsLibrary.DTOs;
using Type = FileworxDTOsLibrary.DTOs.Type;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft;
using Newtonsoft.Json;
using FileworxDTOsLibrary;
using FileworxDTOsLibrary.RabbitMQMessages;
using Elastic.Clients.Elasticsearch.Graph;
using System.Threading.Channels;
using System;
using MassTransit;

namespace FileworxObjectClassLibrary
{
    public class MessagesHandling: IConsumer<clsMessage>
    {
        // MassTransit
        private IBusControl busControl;
        public string name = "default";

        public MessagesHandling()
        {
            configRabbitMQ();
        }

        private void configRabbitMQ()
        {
            busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(EditBeforeRun.HostAddress), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        }

        public async void StartListeningToRxFileMessages()
        {
            busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(EditBeforeRun.HostAddress), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(EditBeforeRun.RxFileQueue, endpoint =>
                {
                    endpoint.Consumer<MessagesHandling>();
                });
            });

            await busControl.StartAsync();
        }

        private async Task processRxFileMessage(clsMessage rxFileMessage)
        {
            if (rxFileMessage.Command == MessagesCommands.RxFile)
            {
                // Write txt file
                await ReceiveFile(rxFileMessage);
            }

            if(Global.UIAction != null)
            {
                await Global.UIAction();
            }
        }

        private async Task ReceiveFile(clsMessage rxFileMessage)
        {
            Guid TxGuid = Guid.NewGuid();

            if (rxFileMessage.NewsDto != null)
            {
                clsNews news = Global.MapNewsDtoToNews(rxFileMessage.NewsDto);
                await news.InsertAsync();
            }

            if (rxFileMessage.PhotoDto != null)
            {
                clsPhoto photo = Global.MapPhotoDtoToPhoto(rxFileMessage.PhotoDto);
                await photo.InsertAsync();
            }

            rxFileMessage.Contact.LastReceiveDate = rxFileMessage.ActionDate;
            await Global.MapContactDtoToContact(rxFileMessage.Contact).UpdateAsync();
        }

        public async Task SendTxFileMessage(clsMessage txMessage)
        {
            // Publishing a message to RxFile Queue 
            var sendEndpoint = await busControl.GetSendEndpoint(new Uri($"{EditBeforeRun.HostAddress}/{EditBeforeRun.TxFileQueue}"));
            await sendEndpoint.Send(txMessage);

        }

        public async Task Consume(ConsumeContext<clsMessage> context)
        {
            var message = context.Message;

            // Handle the received message here
            if (message.Command == MessagesCommands.RxFile)
            {
                await processRxFileMessage(message);
            }

            else
            {
                throw new Exception($"Unsupported command: {message.Command}");
            }
        }     
    }
}
