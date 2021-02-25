using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Mail;
using pManagerLibrary.Entities;
using pManagerLibrary.Interface;
using System.Collections.Generic;

namespace pManagerLibrary
{
    /// <summary>
    /// Gerenciamento de envio de e-mails
    /// </summary>
    public sealed class ManagerEmail : IManagerEmail
    {

        private EManagerEmail EManager;
        private List<string> Copy;
        private List<string> Recipient;
        private List<string> Attachment;
        private string Subject;
        private string BodyEmail;
        private MailMessage mail;

        private ManagerEmail(EManagerEmail eManager)
        {
            this.EManager = eManager;
            Copy = new List<string>();
            Recipient = new List<string>();
            Attachment = new List<string>();
        }
        /// <summary>
        /// Configuração de Parametros do servidor de E-mail <paramref name="eManager"/>
        /// </summary>
        /// <param name="eManager">Representa uma classe com as configurações necessarias para o envio de email</param>
        /// <returns></returns>
        public static ManagerEmail Connect(EManagerEmail eManager)
        {
            return new ManagerEmail(eManager);
        }
        /// <summary>
        /// Envia o e-mail
        /// </summary>
        /// <param name="MsgReturn"></param>
        /// <returns></returns>
        public bool SendEmail(out string MsgReturn)
        {
            MsgReturn = "";

            SmtpClient ClientSMTP = new SmtpClient(EManager.Host, EManager.Port);
            try
            {
                ClientSMTP.EnableSsl = EManager.SSL;
                ClientSMTP.UseDefaultCredentials = EManager.Autenticacao;
                ClientSMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
                ClientSMTP.Credentials = new NetworkCredential(EManager.Remetente, EManager.Password);

                mail = new MailMessage();
                try
                {
                    mail.Sender = new MailAddress(EManager.Remetente);
                    mail.From = new MailAddress(EManager.Remetente);
                    GetRecipient()
                        .GetSubject()
                        .GetCopy()
                        .GetBody();

                    mail.IsBodyHtml = false;
                    mail.Priority = MailPriority.High;
                    mail.SubjectEncoding = Encoding.GetEncoding(BiblioConstante.Getencoding);
                    mail.BodyEncoding = Encoding.GetEncoding(BiblioConstante.Getencoding);
                    GetAttachment();

                    try
                    {
                        ClientSMTP.Timeout = BiblioConstante.Smtptimeout;
                        ClientSMTP.Send(mail);
                        MsgReturn = "E-mail enviado com sucesso";
                    }
                    catch (SmtpException e)
                    {
                        MsgReturn = e.Message;
                        return false;
                    }
                }
                catch (Exception e)
                {
                    MsgReturn = e.Message;
                    return false;
                }
                finally
                {
                    mail = null;
                }
            }
            catch (SmtpException e)
            {
                MsgReturn = e.Message;
                return false;
            }
            finally
            {
                ClientSMTP = null;
                Copy.Clear();
                Attachment.Clear();
                Recipient.Clear();
                Subject = "";
                BodyEmail = "";
            }

            return true;
        }
        /// <summary>
        /// Envia e-mail 
        /// </summary>
        /// <param name="eManager">Representa informações de dados a serem enviados</param>
        /// <param name="MsgReturn">Retorna o status do email</param>
        /// <returns></returns>
        public bool SendEmail(EManagerSendEmail eManager, out string MsgReturn)
        {
            if (eManager == null)
            {
                throw new ArgumentNullException("Classe de gerenciamento de email não pode ser nula.", nameof(eManager));
            }
            MsgReturn = "";
            try
            {
                AddRecipient(eManager.Destinatario)
                    .AddCopy(eManager.Copia)
                    .AddSubject(eManager.Assunto)
                    .AddAttachment(eManager.Anexo);

                bool result = SendEmail(out MsgReturn);

                if (!String.IsNullOrEmpty(MsgReturn) && !result)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                MsgReturn = e.Message;
                return false;
            }
            return true;
        }
        /// <summary>
        /// Envia lista de e-mails
        /// </summary>
        /// <param name="eManagers">Representa uma lista da classe EManagerSendEmail</param>
        /// <returns>Retorna Dicionario dos itens, informando statues de cada e-mail</returns>
        public Dictionary<int, string> SendEmail(List<EManagerSendEmail> eManagers)
        {
            if (eManagers.Count == 0)
            {
                throw new ArgumentNullException("Listas de e-mails não pode ser null!", nameof(eManagers));
            }

            Dictionary<int, string> keys = new Dictionary<int, string>();
            string MsgReturn = "";
            foreach (EManagerSendEmail email in eManagers)
            {
                try
                {
                    AddSubject(email.Assunto)
                        .AddRecipient(email.Destinatario)
                        .AddCopy(email.Copia)
                        .AddAttachment(email.Anexo);

                    bool result = SendEmail(out MsgReturn);

                    if ((!String.IsNullOrEmpty(MsgReturn)) && !result)
                        keys.Add(email.ID, MsgReturn);

                }
                catch (Exception e)
                {
                    keys.Add(email.ID, e.Message);
                }
            }
            return keys;
        }
        private ManagerEmail GetRecipient()
        {
            if (Recipient.Count == 0)
            {
                throw new Exception("Destinatário não informado!");
            }

            foreach (string str in Recipient)
            {
                mail.To.Add(new MailAddress(str));
            }

            return this;
        }
        private ManagerEmail GetSubject()
        {          
            if (String.IsNullOrEmpty(Subject))
            {
                throw new Exception("Assunto do e-mail não informado!");
            }

            mail.Subject = Subject;
            return this;
        }
        private ManagerEmail GetCopy()
        {
            foreach (string str in Copy)
            {
                mail.CC.Add(new MailAddress(str));
            }
            return this;
        }
        private ManagerEmail GetAttachment()
        {
            if (Attachment.Count > 0)
            {
                foreach (string str in Attachment)
                {
                    mail.Attachments.Add(new Attachment(str));
                }
            }
            return this;
        }
        private ManagerEmail GetBody()
        {
            if (String.IsNullOrEmpty(BodyEmail))
            {
                throw new Exception("Corpo do e-mail não informado!");
            }
            
            mail.Body = BodyEmail;
            return this;
        }
        /// <summary>
        /// Adiciona cópias ao e-mail
        /// </summary>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento está vázio ou nulo </exception>
        /// <param name="copy">Representa as cópias. Pode ser separados anexos utilizando ";" ou ",".</param>
        /// <returns></returns>
        public IManagerEmail AddCopy(string copy)
        {
            if (String.IsNullOrEmpty(copy))
            {
                throw new ArgumentException("Argumento cópia não pode ser vázio ou null", nameof(copy));
            }

            string[] vsCopy = copy.Split(';', ',');

            foreach (string str in vsCopy)
            {
                if (String.IsNullOrEmpty(str))
                {
                    throw new ArgumentException("Argumento cópia não pode ser vázio ou null", nameof(copy));
                }
                Copy.Add(str);
            }

            return this;
        }
        /// <summary>
        /// Adiciona cópias ao e-mail
        /// </summary>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento <see cref="copy"/>está vázio ou nulo </exception>
        /// <param name="copy">Representa as cópias. Argumento não pode ser vázio ou nulo.</param>
        /// <returns></returns>
        public IManagerEmail AddCopy(params string[] copy)
        {
            if (copy.Length == 0)
            {
                throw new ArgumentException("Argumento cópia não pode ser vázio ou null", nameof(copy));
            }

            foreach (string str in copy)
            {
                if (String.IsNullOrEmpty(str))
                {
                    throw new ArgumentException("Argumento cópia não pode ser vázio ou null", nameof(copy));
                }
                Copy.Add(str);
            }

            return this;
        }
        /// <summary>
        /// Adiciona o destinatário ao e-mail
        /// </summary>
        /// <exception cref="Exception">Exceção lançada quando localizado informação incorreta</exception>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento está vázio ou nulo </exception>
        /// <param name="recipient">Representa o destinatário do email. Pode ser separados anexos utilizando ";" ou ",".</param>
        /// <returns></returns>
        public IManagerEmail AddRecipient(string recipient)
        {
            if (String.IsNullOrEmpty(recipient))
            {
                throw new ArgumentNullException("Argumento destinatário não pode ser vázio ou null", nameof(recipient));
            }

            string[] recipientArray = recipient.Split(';', ',');

            foreach (string str in recipientArray)
            {
                if (String.IsNullOrEmpty(str))
                    throw new Exception("Encontrado um argumento vázio ou em branco na lista");

                Recipient.Add(str);
            }
            return this;
        }
        /// <summary>
        /// Adiociona Destinatários ao email
        /// </summary>
        /// <exception cref="Exception">Exceção lançada quando encontrado parametro vazio ou null</exception>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento está vázio ou nulo </exception>
        /// <param name="recipient">Representa os destinatários. Argumento não pode ser vázio ou nulo</param>
        /// <returns></returns>
        public IManagerEmail AddRecipient(params string[] recipient)
        {
            if (recipient.Length == 0)
            {
                throw new ArgumentNullException("Argumento destinatário não pode ser vázio ou null", nameof(recipient));
            }

            foreach (string str in recipient)
            {
                if (String.IsNullOrEmpty(str))
                    throw new Exception("Encontrado um argumento vázio ou em branco na lista");

                Recipient.Add(str);
            }
            return this;
        }
        /// <summary>
        /// Adicionar anexos ao e-mail
        /// </summary>
        /// <exception cref="Exception">Exceção lançada quando não for possível localizar o arquivo para anexo.</exception>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento está vázio ou nulo </exception>
        /// <param name="attach">Representa o caminho do anexo. Argumento não pode ser vázio ou nulo. Pode ser separados anexos utilizando ";" ou ",".</param>
        /// <returns></returns>
        public IManagerEmail AddAttachment(string attach)
        {
            if (String.IsNullOrEmpty(attach))
            {
                throw new ArgumentNullException("Argumento de anexo não pode ser vázio ou null", nameof(attach));
            }

            string[] Att = attach.Split(';', ',');

            foreach (string str in Att)
            {
                if (String.IsNullOrEmpty(str))
                {
                    throw new ArgumentNullException("Um dos argumentos do anexo se encontra vázio ou null", nameof(attach));
                }

                if (!File.Exists(str))
                {
                    throw new Exception("Não foi possível encontrar o arquivo para anexo");
                }
                Attachment.Add(attach);
            }

            return this;
        }
        /// <summary>
        /// Adiciona os anexos ao e-mail
        /// </summary>
        /// <exception cref="Exception">Exceção lançada quando não for possível localizar o arquivo para anexo</exception>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento está vázio ou nulo </exception>
        /// <param name="attach">Representa o caminho do anexo. Argumento não pode ser vázio ou nulo</param>
        /// <returns></returns>
        public IManagerEmail AddAttachment(params string[] attach)
        {
            if (attach.Length == 0)
            {
                throw new ArgumentNullException("Argumento de anexo não pode ser vázio ou null", nameof(attach));
            }

            foreach (string str in attach)
            {
                if (!File.Exists(str))
                {
                    throw new Exception("Não foi possível encontrar o arquivo para anexo");
                }

                Attachment.Add(str);
            }

            return this;
        }
        /// <summary>
        /// Adiciona o assunto no email
        /// </summary>
        /// <exception cref="Exception">Exceção lançada quando <see cref="Subject"/> já informado</exception>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento está vázio ou nulo </exception>
        /// <param name="subject">Representa o assunto do email. Argumento não pode ser vazio ou nulo</param>
        /// <returns></returns>
        public IManagerEmail AddSubject(string subject)
        {
            if (!String.IsNullOrEmpty(Subject))
            {
                throw new Exception("Argumento assunto já informado!");
            }
            else if (String.IsNullOrEmpty(subject))
            {
                throw new ArgumentNullException("Argumento assunto não pode ser vázio ou null!", nameof(Subject));
            }
            Subject = subject;

            return this;
        }
        /// <summary>
        /// Adiciona o corpo do email.
        /// </summary>
        /// <exception cref="Exception">Exceção lançada quando <see cref="BodyEmail"/> já informado</exception>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento está vázio ou nulo </exception>
        /// <param name="str">Representa o corpo do email. Argumento não pode ser vázio ou nulo</param>
        /// <returns></returns>
        public IManagerEmail AddBodyEmail(string str)
        {
            if (!String.IsNullOrEmpty(BodyEmail))
            {
                throw new Exception("Corpo do email já informado");
            }
            else if (String.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException("Corpo do e-mail não pode ser vázio ou null", nameof(str));
            }
            BodyEmail = str;

            return this;
        }
    }
}
