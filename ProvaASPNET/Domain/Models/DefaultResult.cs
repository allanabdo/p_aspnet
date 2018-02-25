using System.Net;

namespace Domain.Models
{
    public class DefaultResult<T>
    {
        public T Dados { get; set; }
        public string Mensagem { get; set; }
        public HttpStatusCode Status  { get; set; }

        public DefaultResult(T dados, HttpStatusCode status, string mensagem = "")
        {
            Dados = dados;
            Mensagem = mensagem;
            Status = status;
        }
    }
}
