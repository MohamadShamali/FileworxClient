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

namespace FileworxObjectClassLibrary
{
    public delegate Task AsyncHandler();
    public class MessagesHandling
    {
        // RabbitMQ
        private static IConnection connection;
        private static IModel channel;

        public event AsyncHandler UIAction;

        public MessagesHandling()
        {
            rabbitMQInit();
        }

        private static void rabbitMQInit()
        {
            var factory = new ConnectionFactory { HostName = EditBeforeRun.HostName };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        public void StartListeningToRxFileMessages()
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += onMessageReceived;

            channel.BasicConsume(queue: EditBeforeRun.RxFileQueue, autoAck: false, consumer: consumer);
        }

        private async void onMessageReceived(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var messageString = Encoding.UTF8.GetString(body);

            // Deserialize the JSON response
            clsMessage message = JsonConvert.DeserializeObject<clsMessage>(messageString);

            if (message.Command == MessagesCommands.RxFile)
            {
                await processRxFileMessage(message);
            }

            if(UIAction != null)
            {
                await UIAction();
            }

            // Acknowledge the message to remove it from the queue
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        }

        private async Task processRxFileMessage(clsMessage rxFileMessage)
        {
            if (rxFileMessage.Command == MessagesCommands.RxFile)
            {
                // Write txt file
                await ReceiveFile(rxFileMessage);
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

        public void SendTxFileMessage(clsMessage txMessage)
        {
            channel.QueueDeclare(queue: EditBeforeRun.TxFileQueue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // Serialize the rxMessage object to JSON
            var jsonMessage = JsonConvert.SerializeObject(txMessage);

            // Convert the JSON string to bytes
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            // Publish the JSON message
            channel.BasicPublish(exchange: string.Empty,
                                    routingKey: EditBeforeRun.TxFileQueue,
                                    basicProperties: null,
                                    body: body);

        }
    }
}
