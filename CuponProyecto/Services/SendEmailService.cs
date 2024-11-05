using CuponProyecto.Interfaces;
using System.Net;
using System.Net.Mail;

namespace CuponProyecto.Services
{
    public class SendEmailService : ISendEmailService
    {
        
        public async Task EnviarEmailCliente(string emailCliente, string nroCupon)
        {
            string emailDesde = "julianbentancor27@gmail.com";
            string emailClave = "dyst cwjp pymx oqkd";
            string servicioGoogle = "smtp.gmail.com";

            try 
            {
                SmtpClient smtpClient = new SmtpClient(servicioGoogle);
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(emailDesde, emailClave);
                smtpClient.EnableSsl = true;

                MailMessage message = new MailMessage();
                message.From = new MailAddress(emailDesde, "ProgramacionIV");
                message.To.Add(emailCliente);
                message.Subject = "Número de cupón";
                message.Body = $"Su número de cupón es: {nroCupon}.";

                await smtpClient.SendMailAsync(message);
            
            }catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
