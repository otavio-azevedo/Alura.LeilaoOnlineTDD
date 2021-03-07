using Xunit;
using System.Linq;
using LeilaoOnline.Repository;
using Moq;

namespace LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Theory]
        [InlineData(2,new double[] { 800,900})]
        public void NaoPermiteLancesDadoLeilaoFinalizado(int expectedResult, double[] offers)
        {
            //Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var joaoInteressado = new Interessada("Joao", leilao);
            var mariaInteressada = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < offers.Length; i++)
            {
                var valor = offers[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(joaoInteressado, valor);
                }
                else
                {
                    leilao.RecebeLance(mariaInteressada, valor);
                }
            }
            

            leilao.TerminaPregao();

            //Act
            leilao.RecebeLance(joaoInteressado, 802);

            //Assert
            Assert.Equal(expectedResult, leilao.Lances.Count());
        }

        [Fact]
        public void NaoPermiteLancesDeUmMesmoInteressadoConsecutivamente()
        {
            //Arrange
            var modalidade = new MaiorValor();

            //Exemplo MOCK (interfaces ou classes com metódos que podem ser sobreescritos)
            Mock<IFakeLeilaoDb> mock = new Mock<IFakeLeilaoDb>();
            mock
                .Setup(x => x.GetLeilaoRandom())
                .Returns(new Leilao("The piece", new MaiorValor()));
            
            var leilao = mock.Object.GetLeilaoRandom();
            //

            var joaoInteressado = new Interessada("Joao", leilao);

            leilao.IniciaPregao();

            leilao.RecebeLance(joaoInteressado, 500);
            //Act
            leilao.RecebeLance(joaoInteressado, 802);

            //leilao.TerminaPregao();
            
            //Assert
            Assert.Single(leilao.Lances);
        }

    }
}
