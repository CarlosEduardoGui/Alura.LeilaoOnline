using Alura.LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Testes
{
    public class LeilaoRecebeOferta
    {
        [Theory]
        [InlineData(2, new double[] { 800, 900 })]
        [InlineData(4, new double[] { 100, 1200, 1400, 1300 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdeEsperada, double[] ofertas)
        {
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);
            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i%2) ==0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            leilao.TerminaPregao();


            leilao.RecebeLance(fulano, 1000);

            var qtdeObtida = leilao.Lances.Count();

            Assert.Equal(qtdeEsperada, qtdeObtida);
        }

        [Fact]
        public void NaoAceitaProximoDadoMesmoClienteRealizouUltimoLance()
        {
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            leilao.IniciaPregao();


            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(fulano, 1000);


            leilao.TerminaPregao();


            leilao.RecebeLance(fulano, 1000);

            var qtdeEsperada = 1;
            var qtdeObtida = leilao.Lances.Count();

            Assert.Equal(qtdeEsperada, qtdeObtida);
        }
    }
}
