using Alura.LeilaoOnline.Core;
using System;
using Xunit;


namespace Alura.LeilaoOnline.Testes
{
    public class LanceConstrutor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            var valor = -100;

            Assert.Throws<ArgumentException>(
                () => new Lance(null, valor)
                );
        }
    }
}
