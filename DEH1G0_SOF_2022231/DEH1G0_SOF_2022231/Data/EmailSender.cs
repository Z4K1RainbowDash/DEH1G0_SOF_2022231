using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace DEH1G0_SOF_2022231.Data
{

    /// <summary>
    /// Class responsible for sending emails.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        /// <summary>
        /// The configuration used to send emails.
        /// </summary>
        private readonly IConfiguration Configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/> used to send emails.</param>
        public EmailSender(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }


        /// <summary>
        /// Asynchronously sends an email to the specified address.
        /// </summary>
        /// <param name="email">The email address to send the message to.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="htmlMessage">The HTML message to send in the email.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            string host = this.Configuration["Email:Host"];
            int port = int.Parse(this.Configuration["Email:Port"]);      
            string userEmail = this.Configuration["Email:UserEmail"];
            string userPw = this.Configuration["Email:UserPassword"];
            string targetName = this.Configuration["Email:TargetName"];

            using (SmtpClient client = new SmtpClient()
            {
                
                Host = host,
                Port = port,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(userEmail, userPw),
                TargetName = targetName,
                EnableSsl = true

            })
            {
                MailMessage message = new MailMessage()
                {
                    From = new MailAddress(userEmail),
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = htmlMessage,
                    BodyEncoding = System.Text.Encoding.UTF8,
                    SubjectEncoding = System.Text.Encoding.UTF8,
                };
                message.To.Add(email);

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    // TODO
                }
            }
            return Task.CompletedTask;
        }
    }
}
