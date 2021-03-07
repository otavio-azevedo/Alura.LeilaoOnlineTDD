using System;
using System.Linq;
namespace LeilaoOnline
{
    public class MaiorValor : IModalidadeAvaliacao
    {
        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                    .DefaultIfEmpty(new Lance(null, 0))
                    .OrderBy(x => x.Valor).LastOrDefault();
        }
    }
}
