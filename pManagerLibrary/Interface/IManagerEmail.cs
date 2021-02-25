using System;
using System.Collections.Generic;
using pManagerLibrary.Entities;

namespace pManagerLibrary.Interface
{
    public interface IManagerEmail
    {
        /// <summary>
        /// Envia o e-mail
        /// </summary>
        /// <param name="MsgReturn"></param>
        /// <returns></returns>
        bool SendEmail(out string MsgReturn);
        /// <summary>
        /// Envia e-mail 
        /// </summary>
        /// <param name="eManager">Representa informações de dados a serem enviados</param>
        /// <param name="MsgReturn">Retorna o status do email</param>
        /// <returns></returns>
        bool SendEmail(EManagerSendEmail eManager, out string MsgReturn);
        /// <summary>
        /// Envia lista de e-mails
        /// </summary>
        /// <param name="eManagers">Representa uma lista da classe EManagerSendEmail</param>
        /// <returns>Retorna Dicionario dos itens, informando statues de cada e-mail</returns>
        Dictionary<int, string> SendEmail(List<EManagerSendEmail> eManagers);
        /// <summary>
        /// Adiciona cópias ao e-mail
        /// </summary>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento está vázio ou nulo </exception>
        /// <param name="copy">Representa as cópias. Pode ser separados anexos utilizando ";" ou ",".</param>
        /// <returns></returns>
        IManagerEmail AddCopy(string copy);
        /// <summary>
        /// Adiciona cópias ao e-mail
        /// </summary>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento <see cref="copy"/>está vázio ou nulo </exception>
        /// <param name="copy">Representa as cópias. Argumento não pode ser vázio ou nulo.</param>
        /// <returns></returns>
        IManagerEmail AddCopy(params string[] copy);
        /// <summary>
        /// Adiciona o destinatário ao e-mail
        /// </summary>
        /// <exception cref="Exception">Exceção lançada quando localizado informação incorreta</exception>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento está vázio ou nulo </exception>
        /// <param name="recipient">Representa o destinatário do email. Pode ser separados anexos utilizando ";" ou ",".</param>
        /// <returns></returns>
        IManagerEmail AddRecipient(string recipient);
        /// <summary>
        /// Adiociona Destinatários ao email
        /// </summary>
        /// <exception cref="Exception">Exceção lançada quando encontrado parametro vazio ou null</exception>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento está vázio ou nulo </exception>
        /// <param name="recipient">Representa os destinatários. Argumento não pode ser vázio ou nulo</param>
        /// <returns></returns>
        IManagerEmail AddRecipient(params string[] recipient);
        /// <summary>
        /// Adicionar anexos ao e-mail
        /// </summary>
        /// <exception cref="Exception">Exceção lançada quando não for possível localizar o arquivo para anexo.</exception>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento está vázio ou nulo </exception>
        /// <param name="attach">Representa o caminho do anexo. Argumento não pode ser vázio ou nulo. Pode ser separados anexos utilizando ";" ou ",".</param>
        /// <returns></returns>
        IManagerEmail AddAttachment(string attach);
        /// <summary>
        /// Adiciona os anexos ao e-mail
        /// </summary>
        /// <exception cref="Exception">Exceção lançada quando não for possível localizar o arquivo para anexo</exception>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento está vázio ou nulo </exception>
        /// <param name="attach">Representa o caminho do anexo. Argumento não pode ser vázio ou nulo</param>
        /// <returns></returns>
        IManagerEmail AddAttachment(params string[] attach);
        /// <summary>
        /// Adiciona o assunto no email
        /// </summary>
        /// <exception cref="Exception">Exceção lançada quando parametro  já informado</exception>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento está vázio ou nulo </exception>
        /// <param name="subject">Representa o assunto do email. Argumento não pode ser vazio ou nulo</param>
        /// <returns></returns>
        IManagerEmail AddSubject(string subject);
        /// <summary>
        /// Adiciona o corpo do email.
        /// </summary>
        /// <exception cref="Exception">Exceção lançada quando parametro já informado</exception>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento está vázio ou nulo </exception>
        /// <param name="str">Representa o corpo do email. Argumento não pode ser vázio ou nulo</param>
        /// <returns></returns>
        IManagerEmail AddBodyEmail(string str);
     

    }
}
