using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.CodeDom;


namespace CapaNegocio
{
    public class CN_Recursos
    {
        public static string ConvertirSHA256(string text)
        {
            StringBuilder sb = new StringBuilder();
            using(SHA256 hash =  SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] res = hash.ComputeHash(enc.GetBytes(text));
                foreach(byte b in res)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();
        }

        public static string GenerarClave()
        {
            string claveNueva = Guid.NewGuid().ToString("N").Substring(0, 8);
            return claveNueva;
        }

        public static bool EnviarCorreo(string correoDestino, string asunto, string mensaje, out string MensajeRes)
        {
            bool resultado = false;
            MensajeRes = string.Empty;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correoDestino);
                mail.From = new MailAddress("jacortessa@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                SmtpClient smpt = new SmtpClient()
                {
                    Credentials = new NetworkCredential("jacortessa@gmail.com", "coysabbncvodrchr"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };

                smpt.Send(mail);
                resultado = true;
            }
            catch (Exception ex)
            {
                MensajeRes = $"{ex.Message} {ex.InnerException}";
                resultado = false;
            }
            return resultado;
        }
    }
}
