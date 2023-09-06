using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FileworxObjectClassLibrary;
using Type = FileworxObjectClassLibrary.Type;

namespace FileworxObjectTester
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Index Creation
            //await IndexCreation.CreateFilesIndex();
            //await IndexCreation.CreateUsersIndex();
            //await IndexCreation.CreateBusinessObjectAlias();
            //await IndexCreation.CreateContactsIndex();
            //await IndexCreation.CreateAdmin();
            //____________________________________________________________________________________________________
            //// Insert Contact
            //var contact = new clsContact();
            //contact.Id = new Guid("62a7b071-45f4-48f5-b494-b860da0b5c2f");
            //contact.Description = "contact";
            //contact.Name = "contact";
            //contact.Enabled = true;
            //contact.Direction = ContactDirection.Transmit;
            //await contact.InsertAsync();

            // Read Contact
            var unknown = new clsContact();
            unknown.Id = new Guid("62a7b071-45f4-48f5-b494-b860da0b5c2f");
            unknown.Read();
            Console.WriteLine(unknown.ReceiveLocation);
            var news = new clsNews();
            news.Id = new Guid("64a7b071-45f4-48f5-b494-b860da0b5c2f");
            news.Read();
            unknown.TransmitFile(news);

            //// Update Contact
            //var toupdate = new clsContact();
            //toupdate.Id = new Guid("62a7b071-45f4-48f5-b494-b860da0b5c2f");
            //toupdate.Read();
            //toupdate.Description = "Updated Contact";
            //toupdate.Name = "Updated Contact";
            //await toupdate.UpdateAsync();
            //Console.WriteLine(toupdate.Name);

            // Delete Contact
            //var toDelete = new clsContact();
            //toDelete.Id = new Guid("62a7b071-45f4-48f5-b494-b860da0b5c2f");
            //toDelete.Read();
            //await toDelete.DeleteAsync();

            //____________________________________________________________________________________________________
            //// Insert Photo
            //var photo = new clsPhoto();
            //photo.Id = new Guid("52a7b071-45f4-48f5-b494-b860da0b5c2f");
            //photo.Description = "photo";
            //photo.Name = "photo";
            //photo.Body = "photo";
            //photo.Location = @"D:\skysports-trent-alexander-arnold_6214990.jpg";
            //await photo.InsertAsync();

            // Read Photo
            //var unknown = new clsPhoto();
            //unknown.Id = new Guid("52a7b071-45f4-48f5-b494-b860da0b5c2f");
            //unknown.Read();
            //Console.WriteLine(unknown.Name);

            //// Update Photo
            //var toupdate = new clsPhoto();
            //toupdate.Id = new Guid("52a7b071-45f4-48f5-b494-b860da0b5c2f");
            //toupdate.Read();
            //toupdate.Description = "Updated photo";
            //toupdate.Name = "Updated photo";
            //toupdate.Body = "Updated photo";
            //toupdate.Location = @"D:\skysports-novak-djokovic-tennis_6214595.jpg";
            //await toupdate.UpdateAsync();
            //Console.WriteLine(toupdate.Name);

            // Delete Photo
            //var toDelete = new clsPhoto();
            //toDelete.Id = new Guid("52a7b071-45f4-48f5-b494-b860da0b5c2f");
            //toDelete.Read();
            //await toDelete.DeleteAsync();

            //____________________________________________________________________________________________________

            // Insert News
            //var news = new clsNews();
            //news.Id = new Guid("64a7b071-45f4-48f5-b494-b860da0b5c2f");
            //news.Description = "news";
            //news.Name = "news";
            //news.Body = "news";
            //news.Category = "news";
            //await news.InsertAsync();

            // Read News
            //var unknown = new clsNews();
            //unknown.Id = new Guid("64a7b071-45f4-48f5-b494-b860da0b5c2f");
            //unknown.Read();
            //Console.WriteLine(unknown.Name);

            //// Update News
            //var toupdate = new clsNews();
            //toupdate.Id = new Guid("64a7b071-45f4-48f5-b494-b860da0b5c2f");
            //toupdate.Read();
            //toupdate.Description = "Updated news";
            //toupdate.Name = "Updated news";
            //toupdate.Body = "Updated news";
            //toupdate.Category = "Updated news";
            //await toupdate.UpdateAsync();
            //toupdate.Read();
            //Console.WriteLine(toupdate.Body);

            // Delete News
            //var toDelete = new clsNews();
            //toDelete.Id = new Guid("64a7b071-45f4-48f5-b494-b860da0b5c2f");
            //await toDelete.DeleteAsync();

            //____________________________________________________________________________________________________

            // Insert User
            //var user = new clsUser();
            //user.Id = new Guid("77a7b071-45f4-48f5-b494-b860da0b5c2f");
            //user.Description = "user";
            //user.Name = "user";
            //user.Username = "user";
            //user.Password = "user";
            //user.IsAdmin = false;
            //await user.InsertAsync();

            // Read User
            //var unknown = new clsUser();
            //unknown.Id = new Guid("77a7b071-45f4-48f5-b494-b860da0b5c2f");
            //unknown.Read();
            //Console.WriteLine(unknown.Name);

            //// Update News
            //var toupdate = new clsUser();
            //toupdate.Id = new Guid("77a7b071-45f4-48f5-b494-b860da0b5c2f");
            //toupdate.Read();
            //toupdate.Description = "Updated user";
            //toupdate.Name = "Updated user";
            //toupdate.Username = "Updated user";
            //toupdate.Password = "Updated user";
            //await toupdate.UpdateAsync();
            //Console.WriteLine(toupdate.Name);

            // Delete News
            //var toDelete = new clsUser();
            //toDelete.Id = new Guid("77a7b071-45f4-48f5-b494-b860da0b5c2f");
            //await toDelete.DeleteAsync();

            //____________________________________________________________________________________________________

            //Contact Query
            //var query = new clsContactQuery() { Source = QuerySource.ES };
            ////query.QDirection = new ContactDirection[] { ContactDirection.Receive };
            //var result = await query.Run();
            //foreach (var c in result)
            //{
            //    Console.WriteLine(c.Name);
            //}

            //____________________________________________________________________________________________________

            //Buisiness Object Query
            //var query = new clsBusinessObjectQuery() { Source = QuerySource.ES };
            //query.QClasses = new FileworxObjectClassLibrary.Type[] { FileworxObjectClassLibrary.Type.User };
            //var result = await query.Run();
            //foreach (var c in result)
            //{
            //    Console.WriteLine(c.Name); 
            //}
            //____________________________________________________________________________________________________

            //File Query
            //var query = new clsFileQuery();
            //query.Source = QuerySource.ES;
            //query.QClasses = new clsFileQuery.ClassIds[] { clsFileQuery.ClassIds.Photos };
            //var result = await query.Run();
            //foreach (var c in result)
            //{
            //    Console.WriteLine(c.Name);
            //}

            //____________________________________________________________________________________________________

            //User Query
            //var query = new clsUserQuery();
            //query.Source = QuerySource.ES;
            //var result = await query.Run();
            //foreach (var c in result)
            //{
            //    Console.WriteLine(c.Username);
            //}

            //____________________________________________________________________________________________________

            //News Query
            //var query = new clsNewsQuery();
            //query.Source = QuerySource.ES;
            //var result = await query.Run();
            //foreach (var c in result)
            //{
            //    Console.WriteLine(c.Name);
            //}

            //____________________________________________________________________________________________________

            //Photos Query
            //var query = new clsPhotoQuery();
            //query.Source = QuerySource.ES;
            //var result = await query.Run();
            //foreach (var c in result)
            //{
            //    Console.WriteLine(c.Name);
            //}

            //____________________________________________________________________________________________________

            //User Validation
            //var user = new clsUser() { Username = "a", Password = "a" };

            //Console.WriteLine(user.ValidateLogin());

            //var user = new clsUser() { Username = "admin" };
            //user.Read();
            //Console.WriteLine(user.Name);

            Console.ReadLine();
        }
    }
}
