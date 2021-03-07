using System;
using Xunit;

namespace LeilaoOnline.Tests
{
    #region Info & References
    /*
     Defini��o de Teste: http://softwaretestingfundamentals.com/definition-of-test/
     Padr�o AAA (Arrange, Act, Assert): http://wiki.c2.com/?ArrangeActAssert
     Padr�o Given/When/Then: https://martinfowler.com/bliki/GivenWhenThen.html
     xUnit: https://xunit.github.io/
     MSTests: https://github.com/Microsoft/testfx-docs
     NUnit: https://nunit.org/
     Comparativo entre os frameworks de Teste: https://xunit.github.io/docs/comparisons
     Porque xUnit? https://www.martin-brennan.com/why-xunit/
     Microsoft est� usando o xUnit: https://dev.to/hatsrumandcode/net-core-2-why-xunit-and-not-nunit-or-mstest--aei
    
    Diferen�a entre [Fact] e [Theory] https://xunit.github.io/docs/getting-started/netfx/visual-studio#write-first-theory
        [Fact] � quando testamos algo sem depender de valores de entrada 
        [Theory] � quando testamos o mesmmo m�todo para condi��es diferentes de entrada (InlineData)

    Nomenclatura de testes
    https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices#best-practices
    https://docs.microsoft.com/pt-br/dotnet/standard/modern-web-apps-azure-architecture/test-asp-net-core-mvc-apps#test-naming

    Como utilizar tipos de dados complexos (diferentes dos tipos nativos: double,int, string e etc...) nos parametros dos testes?
    https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/

    Testes de regress�o: http://softwaretestingfundamentals.com/regression-testing/
    Livro TDD By Example, de Kent Beck: https://www.amazon.com.br/Test-Driven-Development-Kent-Beck/dp/0321146530/
    Testes de m�todos privados: https://docs.microsoft.com/pt-br/dotnet/core/testing/unit-testing-best-practices#validate-private-methods-by-unit-testing-public-methods
    Martin Fowler debate o real prop�sito da cobertura de c�digo: https://www.martinfowler.com/bliki/TestCoverage.html
    
    Tutorial para verificar a cobertura de c�digo: https://docs.microsoft.com/pt-br/dotnet/core/testing/unit-testing-code-coverage?tabs=windows
        Exemplo cmd para gerar o relat�rio (a partir da raiz do projeto):
            reportgenerator "-reports:C:\Users\lotav\Documents\Cursos\Alura\Cursos\Testes Unitarios\LeilaoOnline\LeilaoOnline.Tests\TestResults\91281b5f-8d46-45b3-960d-b3b2b2c04702\coverage.cobertura.xml" "-targetdir:coveragereport" -reporttypes:Html"
    
    
    
    Talk de Michael Feathers sobre como testes melhoram o design: https://www.youtube.com/watch?v=4cVZvoFGJTU
     */

    #endregion

    //Nome = Classe+M�todoASerTestado
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1101, 1100, new double[] { 800, 1101, 1500, 999 })]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(double valorEsperado, double valorEstimadoDoLeilao, double[] offers)
        {
            //Arrange
            var modalidade = new OfertaSuperiorMaisProxima(valorEstimadoDoLeilao);
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
            //Act
            leilao.TerminaPregao();

            //Assert
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemlance()
        {
            //Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh",modalidade);
            var joaoInteressado = new Interessada("Joao", leilao);
            var mariaInteressada = new Interessada("Maria", leilao);
            var otavioInteressada = new Interessada("Otavio", leilao);
            leilao.IniciaPregao();
            //Act
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Theory]
        [InlineData(1000, new double[] { 800, 900, 1000 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 999 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoNoLeilao(double expectedValue, double[] offers)
        {
            //Arrange
            IModalidadeAvaliacao modalidade = new MaiorValor();
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

            //Act
            leilao.TerminaPregao();

            //Assert
            Assert.Equal(expectedValue, leilao.Ganhador.Valor);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciao()
        {
            //Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);

            //Assert Exception
            var exception = Assert.Throws<InvalidOperationException>(
                    //Act
                    () => leilao.TerminaPregao()
                );

            //Assert Message
            string msgExpected = "N�o � poss�vel terminar o preg�o sem que ele tenha come�ado. Para isso, utilize o m�todo IniciaPregao()";
            Assert.Equal(msgExpected, exception.Message);
        }


    }


}
