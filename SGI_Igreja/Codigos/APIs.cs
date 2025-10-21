using System;
using System.Net.Http;

using Newtonsoft.Json;

namespace ECTurbo.Codigos
{
    public class APIs
    {
        public static dynamic Requisicao(string url)
        {

            dynamic json;

            try
            {
                using (HttpClient http = new HttpClient())
                {
                    using (HttpResponseMessage Resposta = http.GetAsync(url).Result)
                    {
                        json = JsonConvert.DeserializeObject(Resposta.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception)
            {
                json = null;
            }

            return json;
        }
    }
}
