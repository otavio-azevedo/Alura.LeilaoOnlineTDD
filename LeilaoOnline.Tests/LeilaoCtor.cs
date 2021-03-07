using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace LeilaoOnline.Tests
{
    public class LeilaoCtor
    {
        [Theory]
        [ClassData(typeof(LeilaoData))]
        public void VerificaEstadoIncialDoLeilao(Leilao leilaoA, Leilao leilaoB)
        {
            Assert.Equal(EstadoLeilao.LeilaoAntesDoPregao, leilaoA.Estado);
            Assert.Equal(EstadoLeilao.LeilaoAntesDoPregao, leilaoB.Estado);
        }

        /// <summary>
        /// Necessário para passar um ou mais objetos de um tipo de dado diferente de uma constante
        /// </summary>
        public class LeilaoData : IEnumerable<object[]>
        {
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            public IEnumerator<object[]> GetEnumerator()
            {
                var modalidade = new MaiorValor();
                
                yield return new object[] {
                new Leilao("LeilaoA",modalidade),
                new Leilao("LeilaoB",modalidade)
                };
            }
        }
    }
}
