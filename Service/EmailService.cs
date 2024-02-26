using System.Net.Mail;
using System.Net;

namespace Background.Service;
public class EmailService
{
    SmtpClient _smtpClient;
    public EmailService()
    {
        _smtpClient = new SmtpClient("smtp.gmail.com", 587)
        {
            Port = 587,
            Credentials = new NetworkCredential("murodilov853@gmail.com", "your_key"),
            //your_key means your google account key for App.
            //You can find that key from your google account's.
            //Firstly open settings and click security and you can create your key
            EnableSsl = true,
            UseDefaultCredentials = false
        };
    }

    public void SendEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            Console.WriteLine("Email address is empty or null. Skipping email sending.");
            return;
        }
        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress("murodilov853@gmail.com");
            mail.To.Add(email);
            mail.Subject = "Reminder: Deadline Exceeded";
            mail.Body = @"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Deadline Reminder</title>
            </head>
            <body>
                <h1>Hi, Do you remember your todo's deadline ?</h1>
                        <p> This is a reminder that your deadline has been exceeded.</p >
                        <p> Please take appropriate action.</p>
            </body>
            </html>";
            mail.IsBodyHtml = true;

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.Credentials = new NetworkCredential("murodilov853@gmail.com", "your_key");
                smtpClient.EnableSsl = true;
                // Send the email
                smtpClient.Send(mail);
            }
        }
    }
}
