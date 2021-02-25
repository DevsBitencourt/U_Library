namespace pManagerLibrary
{
    /// <summary>
    /// Classe por armazenar constantes
    /// </summary>
    public static class BiblioConstante
    {
        private static int MegaByteMax = 5;
        private static int SmtpTimeOut = 5000;
        private static string GetEncoding = "UTF-8";
        /// <summary>
        /// MegaByte máximo a ser permitido
        /// </summary>
        public static int Megabytemax { get => MegaByteMax; set => MegaByteMax = value; }
        /// <summary>
        /// TimeOut da conexão do SMTP com o servidor de E-mail
        /// </summary>
        public static int Smtptimeout { get => SmtpTimeOut; set => SmtpTimeOut = value; }
        /// <summary>
        /// Caracter encoding
        /// </summary>
        public static string Getencoding { get => GetEncoding; set => GetEncoding = value; }
    }
}
