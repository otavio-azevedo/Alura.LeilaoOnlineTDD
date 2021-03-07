using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeilaoOnline.Tests
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            //Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var joaoInteressado = new Interessada("Joao", leilao);
            var valorNegativo = -1;

            //Assert
            Assert.Throws<ArgumentException>(
                    ()=> new Lance(joaoInteressado, valorNegativo)
                );
        }

    }
}
