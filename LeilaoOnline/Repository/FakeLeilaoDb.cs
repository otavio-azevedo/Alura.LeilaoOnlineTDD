using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeilaoOnline.Repository
{
    public class FakeLeilaoDb : IFakeLeilaoDb
    {
        public Leilao GetLeilaoRandom()
        {
            var modalidade = new MaiorValor();
            var tableLeilao = new List<Leilao>()
            {
                new Leilao("The piece of Da Vinci",modalidade),
                new Leilao("The piece of Michelangelo",modalidade),
                new Leilao("The piece of Van Gogh",modalidade),
                new Leilao("The piece of Picasso",modalidade),
            };

            //Random trick
            return tableLeilao.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }
    }
}
