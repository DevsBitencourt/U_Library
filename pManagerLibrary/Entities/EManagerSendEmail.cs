namespace pManagerLibrary.Entities
{
    public class EManagerSendEmail
    {
        private int IDEmail;
        private string AssuntoEmail;
        private string DestinatarioEmail;
        private string CopiaEmail;
        private string AnexoEmail;

        public int ID { get => IDEmail; set => IDEmail = value; }
        public string Assunto { get => AssuntoEmail; set => AssuntoEmail = value; }
        public string Destinatario { get => DestinatarioEmail; set => DestinatarioEmail = value; }
        public string Copia { get => CopiaEmail; set => CopiaEmail = value; }
        public string Anexo { get => AnexoEmail; set=> AnexoEmail = value; }

    }
}
