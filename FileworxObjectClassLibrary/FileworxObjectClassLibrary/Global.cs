using FileworxDTOsLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = FileworxDTOsLibrary.DTOs.Type;

namespace FileworxObjectClassLibrary
{
    public delegate Task AsyncHandler();
    public static class Global
    {
        public static AsyncHandler UIAction;
        public static clsNews MapNewsDtoToNews(clsNewsDto newsDto)
        {
            var news = new clsNews()
            {
                Id = Guid.NewGuid(),
                Description = newsDto.Description,
                CreationDate = newsDto.CreationDate,
                ModificationDate = newsDto.ModificationDate,
                CreatorId = newsDto.CreatorId,
                CreatorName = newsDto.CreatorName,
                LastModifierId = newsDto.LastModifierId,
                Name = newsDto.Name,
                Class = (Type)(int)newsDto.Class,
                Body = newsDto.Body,
                Category = newsDto.Category,
            };

            return news;
        }

        public static clsPhoto MapPhotoDtoToPhoto(clsPhotoDto photoDto)
        {
            var photo = new clsPhoto()
            {
                Id = photoDto.Id,
                Description = photoDto.Description,
                CreationDate = photoDto.CreationDate,
                ModificationDate = photoDto.ModificationDate,
                CreatorId = photoDto.CreatorId,
                CreatorName = photoDto.CreatorName,
                LastModifierId = photoDto.LastModifierId,
                Name = photoDto.Name,
                Class = (Type)(int)photoDto.Class,
                Body = photoDto.Body,
                Location = photoDto.Location,
            };

            return photo;
        }

        public static clsContact MapContactDtoToContact(FileworxDTOsLibrary.DTOs.clsContactDto contactDto)
        {
            var contact = new clsContact()
            {
                Id = contactDto.Id,
                Description = contactDto.Description,
                CreationDate = contactDto.CreationDate,
                ModificationDate = contactDto.ModificationDate,
                CreatorId = contactDto.CreatorId,
                CreatorName = contactDto.CreatorName,
                LastModifierId = contactDto.LastModifierId,
                Name = contactDto.Name,
                Class = (Type)(int)contactDto.Class,
                TransmitLocation = contactDto.TransmitLocation,
                ReceiveLocation = contactDto.ReceiveLocation,
                Direction = (ContactDirection)(int)contactDto.Direction,
                LastReceiveDate = contactDto.LastReceiveDate,
                Enabled = contactDto.Enabled,
            };

            return contact;
        }

        public static FileworxDTOsLibrary.DTOs.clsContactDto MapContactToContactDto(clsContact contact)
        {
            var contactDto = new FileworxDTOsLibrary.DTOs.clsContactDto()
            {
                Id = contact.Id,
                Description = contact.Description,
                CreationDate = contact.CreationDate,
                ModificationDate = contact.ModificationDate,
                CreatorId = contact.CreatorId,
                CreatorName = contact.CreatorName,
                LastModifierId = contact.LastModifierId,
                Name = contact.Name,
                Class = (FileworxDTOsLibrary.DTOs.Type)(int)contact.Class,
                TransmitLocation = contact.TransmitLocation,
                ReceiveLocation = contact.ReceiveLocation,
                Direction = (FileworxDTOsLibrary.DTOs.Direction)(int)contact.Direction,
                LastReceiveDate = contact.LastReceiveDate,
                Enabled = contact.Enabled,
            };

            return contactDto;
        }

        public static clsPhotoDto MapPhotoToPhotoDto(clsPhoto photo)
        {
            var photoDto = new clsPhotoDto()
            {
                Id = photo.Id,
                Description = photo.Description,
                CreationDate = photo.CreationDate,
                ModificationDate = photo.ModificationDate,
                CreatorId = photo.CreatorId,
                CreatorName = photo.CreatorName,
                LastModifierId = photo.LastModifierId,
                Name = photo.Name,
                Class = (FileworxDTOsLibrary.DTOs.Type)(int)photo.Class,
                Body = photo.Body,
                Location = photo.Location,
            };

            return photoDto;
        }

        public static clsNewsDto MapNewsToNewsDto(clsNews news)
        {
            var newsDto = new clsNewsDto()
            {
                Id = news.Id,
                Description = news.Description,
                CreationDate = news.CreationDate,
                ModificationDate = news.ModificationDate,
                CreatorId = news.CreatorId,
                CreatorName = news.CreatorName,
                LastModifierId = news.LastModifierId,
                Name = news.Name,
                Class = (FileworxDTOsLibrary.DTOs.Type)(int)news.Class,
                Body = news.Body,
                Category = news.Category,
            };

            return newsDto;
        }
    }
}
