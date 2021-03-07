using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace LeilaoOnline
{
    public class OfertaSuperiorMaisProxima : IModalidadeAvaliacao
    {
        public double ValorEstimadoDoLeilao { get; }

        public OfertaSuperiorMaisProxima(double valorEstimadoDoLeilao)
        {
            ValorEstimadoDoLeilao = valorEstimadoDoLeilao;
        }


        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                .DefaultIfEmpty(new Lance(null, 0))
                .Where(x => x.Valor > ValorEstimadoDoLeilao)
                .OrderBy(x => x.Valor).FirstOrDefault();

        }
    }
}
