using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SheepFinance.Model
{
    public class Dollar
    {
        public Dollar(DateTime dataHoraCotacao, double cotacaoCompra, double cotacaoVenda)
        {
            DataHoraCotacao = dataHoraCotacao;
            CotacaoCompra = cotacaoCompra;
            CotacaoVenda = cotacaoVenda;
        }

        public DateTime DataHoraCotacao { get; private set; }
        public double CotacaoCompra { get; private set; }
        public double CotacaoVenda { get; private set; }
    }
}
