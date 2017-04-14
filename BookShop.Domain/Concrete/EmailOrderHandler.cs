using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using BookShop.Domain.Abstract;
using BookShop.Domain.Entities;

namespace BookShop.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "bookshop@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"D:\Programming\Projects\BookShop\bookshopemails";
    }

    public class EmailOrderHandler : IOrderHandler
    {
        private EmailSettings emailSettings;

        public EmailOrderHandler(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void HandleOrder(Basket basket, DeliveryInfo deliveryInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Новый заказ обработан")
                    .AppendLine("-----")
                    .AppendLine("Книги:");

                foreach (var type in basket.Types)
                {
                    var subtotal = type.Book.Price * type.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2:c}",
                        type.Quantity, type.Book.BookName, subtotal);
                }

                body.AppendFormat("Общая стоимость: {0:c}", basket.ComputeOrderValue())
                    .AppendLine("-----")
                    .AppendLine("Доставка:")
                    .AppendLine(deliveryInfo.Name)
                    .AppendLine(deliveryInfo.Phone)
                    .AppendLine(deliveryInfo.Country)
                    .AppendLine(deliveryInfo.City)
                    .AppendLine(deliveryInfo.Street)
                    .AppendLine(deliveryInfo.House)
                    .AppendLine(deliveryInfo.Flat)
                    .AppendLine("-----")
                    .AppendFormat("Подарочная упаковка: {0}",
                        deliveryInfo.GiftWrap ? "Да" : "Нет");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	// From who
                                       emailSettings.MailToAddress,		// For who
                                       "Новый заказ отправлен!",		// Topic
                                       body.ToString()); 				// Message body

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}
