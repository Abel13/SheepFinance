using Newtonsoft.Json;
using SheepFinance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SheepFinance.Data
{
    public class Endpoints
    {
        private HttpResponseMessage GET(string url)
        {
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                try
                {
                    var result = client.GetAsync(url);
                    result.Wait();

                    return result.Result;
                }
                catch (AggregateException)
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable);
                }
            }
        }

        public List<Dollar> GetDollars(DateTime StartDate, DateTime EndDate)
        {
            string url = @"https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoDolarPeriodo(dataInicial=@dataInicial,dataFinalCotacao=@dataFinalCotacao)?";

            url += "@dataInicial='" + StartDate.ToString("MM-dd-yyyy") + "'&@dataFinalCotacao='" + EndDate.ToString("MM-dd-yyyy") + "'&$top=100&$format=json";

            var retorno = GET(url);

            if (retorno.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string ret = retorno.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<BacenDollar>(ret).Value;
            }

            return new List<Dollar>();
        }
    }
}