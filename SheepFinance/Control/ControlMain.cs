using SheepFinance.Data;
using SheepFinance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Control
{
    public class ControlMain
    {
        LocalDatabase database;
        Endpoints endpoints;

        public ControlMain()
        {
            database = LocalDatabase.GetInstance();
            endpoints = new Endpoints();
        }

        public List<MenuItem> GetMenuList()
        {
            List<MenuItem> menu = new List<MenuItem>
            {
                new MenuItem("DASHBOARD", MaterialDesignThemes.Wpf.PackIconKind.ViewDashboard),
                new MenuItem("CONTAS", MaterialDesignThemes.Wpf.PackIconKind.AccountCardDetails),
                new MenuItem("ENTRADAS", MaterialDesignThemes.Wpf.PackIconKind.PlusCircleOutline),
                new MenuItem("SAÍDAS", MaterialDesignThemes.Wpf.PackIconKind.MinusCircleOutline),
                new MenuItem("TRANSFERÊNCIAS", MaterialDesignThemes.Wpf.PackIconKind.SwapHorizontal),
                new MenuItem("CAIXINHAS", MaterialDesignThemes.Wpf.PackIconKind.Dropbox)
            };
            return menu;
        }

        public void GetDollaresAPI()
        {
            DateTime StartDate = DateTime.Now.AddDays(-10);
            DateTime EndDate = DateTime.Now;

            var dollares = database.GetDollares();

            if (dollares.Count > 0)
            {
                StartDate = dollares.OrderByDescending(d=>d.DataHoraCotacao).FirstOrDefault().DataHoraCotacao.AddDays(1);
                EndDate = DateTime.Today;
            }

            var result = endpoints.GetDollars(StartDate, EndDate);

            foreach (var item in result)
            {
                if (dollares
                    .Where(d => 
                        d.DataHoraCotacao.ToShortDateString()
                        .Equals(item.DataHoraCotacao.ToShortDateString()))
                    .Count()
                    .Equals(0))
                {
                    database.AddDollar(item.CotacaoCompra, item.CotacaoVenda, item.DataHoraCotacao);
                }
            }
        }
    }
}
