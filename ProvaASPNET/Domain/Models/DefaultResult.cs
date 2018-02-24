namespace Domain.Models
{
    public class DefaultResult<T>
    {
        public T Dados { get; set; }
        public string Mensagem { get; set; }
        public bool Sucesso  { get; set; }

        public DefaultResult(T dados, bool sucesso, string mensagem = "")
        {
            Dados = dados;
            Mensagem = mensagem;
            Sucesso = sucesso;
        }
    }
}
