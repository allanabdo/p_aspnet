using System.Collections.Generic;

namespace Domain.Models
{
    public class PageResult<T>
    {
        public List<T> Dados { get;  set; }
        public int Total { get; set; }
        public int Pagina { get; set; }


        public PageResult(List<T> dados, int total, int pagina)
        {
            Dados = dados;
            Total = total;
            Pagina = pagina;
        }
    }
}
