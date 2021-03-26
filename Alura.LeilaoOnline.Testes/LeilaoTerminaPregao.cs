using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Testes
{
    public class LeilaoTerminaPregao
    {
        //Teste orientado por dados.
        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                {

                    //Act - método sob teste
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            leilao.TerminaPregao();

            var valorObtido = leilao.Ganhador.Valor;

            //Assert
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDAdoPregaoNaoIniciado()
        {
            var leilao = new Leilao("Van Gogh");

            var e = Assert.Throws<InvalidOperationException> (() => leilao.TerminaPregao());

            var MsgEsperada = "Não é possível terminar sem que ele tenha começado.";
            Assert.Equal(MsgEsperada, e.Message);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLance()
        {
            var leilao = new Leilao("Van Gogh");
            leilao.IniciaPregao();

            leilao.TerminaPregao();

            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
