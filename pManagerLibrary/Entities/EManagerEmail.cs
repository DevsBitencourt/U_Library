namespace pManagerLibrary.Entities
{
    /// <summary>
    /// Configurações do servidor de e-mail
    /// </summary>
    public class EManagerEmail
    {
        private string UserRemetente;
        private string UserPasswd;
        private string HostEmail;
        private int PortEmail;
        private bool AutenticacaoEmail;
        private bool SSLEmail;
        /// <summary>
        /// Remetente do e-mail
        /// </summary>
        public string Remetente { get => UserRemetente; set => UserRemetente = value; }
        /// <summary>
        /// Senha do e-mail do <see cref="Remetente"/>
        /// </summary>
        public string Password { get => UserPasswd; set => UserPasswd = value; }
        /// <summary>
        /// Host SMTP do servidor de e-mail
        /// </summary>
        public string Host { get => HostEmail; set => HostEmail = value; }
        /// <summary>
        /// Porta do servidor SMTP
        /// </summary>
        public int Port { get => PortEmail; set => PortEmail = value; }
        /// <summary>
        /// Informar se é obrigatório a autenticação ao e-mail antes de enviar
        /// </summary>
        public bool Autenticacao { get => AutenticacaoEmail; set => AutenticacaoEmail = value; }
        /// <summary>
        /// Informar se é obrigatório o uso do SSL no envio do e-mail 
        /// </summary>
        public bool SSL { get => SSLEmail; set => SSLEmail = value; }
    }
}
