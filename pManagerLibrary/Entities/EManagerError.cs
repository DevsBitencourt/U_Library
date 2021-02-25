namespace pManagerLibrary.Entities
{
    public class EManagerError
    {
        private int IDError;
        private string MensagemErro;

        public int ID { get => IDError; set => IDError = value; }
        public string Mensagem { get => MensagemErro; set => MensagemErro = value; }

    }
}
