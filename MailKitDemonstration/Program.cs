// This is the code that I want to push to the repo if needed

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace MailKitDemonstration
{
    class Program
    {
        public static void SendMessage(MimeMessage message)
        {
            using (var client = new SmtpClient(new ProtocolLogger("smtp.log")))
            {
                message.From.Add(new MailboxAddress("Input Name", "InputGmail@gmail.com"));
                message.To.Add(new MailboxAddress("Input Name", "InputGmail@gmail.com"));
                message.Subject = "Enter Subject Line Here";

                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

                // Go to Google Profile to generate an app password rather than using your real password here
                client.Authenticate("User Name Here", "Input App Password");

                client.Send(message);

                client.Disconnect(true);
            }
        }

        // Use the below statements for debugging purposes if needed
        public static void PrintCapabilities()
        {
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

                if (client.Capabilities.HasFlag(SmtpCapabilities.Authentication))
                {
                    var mechanisms = string.Join(", ", client.AuthenticationMechanisms);
                    Console.WriteLine("The SMTP server supports the following SASL mechanisms: {0}", mechanisms);

                    // Use your Google profile to generate an App Password
                    client.Authenticate("User Name Here", "Input App Password");
                }

                if (client.Capabilities.HasFlag(SmtpCapabilities.Size))
                    Console.WriteLine("The SMTP server has a size restriction on messages: {0}.", client.MaxSize);

                if (client.Capabilities.HasFlag(SmtpCapabilities.Dsn))
                    Console.WriteLine("The SMTP server supports delivery-status notifications.");

                if (client.Capabilities.HasFlag(SmtpCapabilities.EightBitMime))
                    Console.WriteLine("The SMTP server supports Content-Transfer-Encoding: 8bit");

                if (client.Capabilities.HasFlag(SmtpCapabilities.BinaryMime))
                    Console.WriteLine("The SMTP server supports Content-Transfer-Encoding: binary");

                if (client.Capabilities.HasFlag(SmtpCapabilities.UTF8))
                    Console.WriteLine("The SMTP server supports UTF-8 in message headers.");

                client.Disconnect(true);
            }
        }
        public static void Main()
        {
            PrintCapabilities();
            SendMessage(new MimeMessage());
        }
    }
}