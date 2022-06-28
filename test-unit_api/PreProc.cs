using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using mycaller.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime;

namespace mycaller.preproc
{
    //Class with static method for each endpoint 

    public static class PreProc
    {
        public static HttpClient? Client { get; set; }

        public static async Task<IPO?> GetConnection_IPO(string dateFrom, string dateTo)
        {
            dateFrom = DateCherker(dateFrom);
            dateTo = DateCherker(dateTo);
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = $"https://api.benzinga.com/api/v2.1/calendar/ipos?parameters%5Bdate_from%5D={dateFrom}&parameters%5Bdate_to%5D={dateTo}&token=a255db84b80243f79a120c8122daaedb";
            using (HttpResponseMessage response = await Client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    IPO? model;
                    if (responseBody != "[]")
                    {
                        model = JsonConvert.DeserializeObject<IPO>(responseBody);
                    }
                    else
                    {
                        model = new IPO();
                    }
                    return model;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<MA?> GetConnection_MA(string dateFrom, string dateTo)
        {
            dateFrom = DateCherker(dateFrom);
            dateTo = DateCherker(dateTo);
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = $"https://api.benzinga.com/api/v2.1/calendar/ma?parameters%5Bdate_from%5D={dateFrom}&parameters%5Bdate_to%5D={dateTo}&token=a255db84b80243f79a120c8122daaedb";
            using (HttpResponseMessage response = await Client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    MA? model;
                    if (JsonConvert.DeserializeObject<MA>(responseBody) != null)
                    {
                        model = JsonConvert.DeserializeObject<MA>(responseBody);
                    }
                    else
                    {
                        model = new MA();
                    }
                    return model;
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }

        }

        public static async Task<Earnings?> GetConncetion_Earnings(string dateFrom, string dateTo)
        {
            dateFrom = DateCherker(dateFrom);
            dateTo = DateCherker(dateTo);
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = $"https://api.benzinga.com/api/v2.1/calendar/earnings?parameters%5Bdate_from%5D={dateFrom}&parameters%5Bdate_to%5D={dateTo}&token=a255db84b80243f79a120c8122daaedb";
            using (HttpResponseMessage response = await Client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Earnings? model;
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (JsonConvert.DeserializeObject<Earnings>(responseBody) != null)
                    {
                        model = JsonConvert.DeserializeObject<Earnings>(responseBody);
                    }
                    else
                    {
                        model = new Earnings();
                    }
                    return model;
                }
                else
                    throw new Exception(response.ReasonPhrase);

            }
        }

        public static async Task<Dividends?> GetConnection_Div(string dateFrom, string dateTo)
        {
            dateFrom = DateCherker(dateFrom);
            dateTo = DateCherker(dateTo);
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = $"https://api.benzinga.com/api/v2.1/calendar/dividends?parameters%5Bdate_from%5D={dateFrom}&parameters%5Bdate_to%5D={dateTo}&token=a255db84b80243f79a120c8122daaedb";
            using (HttpResponseMessage response = await Client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Dividends? model = new();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (responseBody != null)
                    {

                        model = JsonConvert.DeserializeObject<Dividends>(responseBody);
                    }
                    else
                    {
                        model = new Dividends();
                    }
                    return model;
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }
        }
        public static async Task<Dividends?> GetConnection_Div(string dateFrom, string dateTo, string tickers)
        {
            dateFrom = DateCherker(dateFrom);
            dateTo = DateCherker(dateTo);
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = $"https://api.benzinga.com/api/v2.1/calendar/dividends?parameters[date_from]={dateFrom}&parameters[date_to]={dateTo}&parameters[tickers]={tickers}&token=a255db84b80243f79a120c8122daaedb";
            using (HttpResponseMessage response = await Client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Dividends? model;
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (JsonConvert.DeserializeObject<Dividends>(responseBody) != null)
                    {
                        model = JsonConvert.DeserializeObject<Dividends>(responseBody);
                    }
                    else
                    {
                        model = new Dividends();
                    }
                    return model;
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }
        }
        public static async Task<MA?> GetConnection_MA(string dateFrom, string dateTo, string tickers)
        {
            dateFrom = DateCherker(dateFrom);
            dateTo = DateCherker(dateTo);
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = $"https://api.benzinga.com/api/v2.1/calendar/ma?parameters%5Bdate_from%5D={dateFrom}&parameters%5Bdate_to%5D={dateTo}&token=a255db84b80243f79a120c8122daaedb";
            using (HttpResponseMessage response = await Client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    MA? model;
                    if (JsonConvert.DeserializeObject<MA>(responseBody) != null)
                    {
                        model = JsonConvert.DeserializeObject<MA>(responseBody);
                    }
                    else
                    {
                        model = new MA();
                    }
                    return model;
                }
                else
                    throw new Exception(response.ReasonPhrase);
            }

        }
        public static async Task<Earnings?> GetConncetion_Earnings(string dateFrom, string dateTo, string tickers)
        {
            dateFrom = DateCherker(dateFrom);
            dateTo = DateCherker(dateTo);
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string url = $"https://api.benzinga.com/api/v2.1/calendar/earnings?parameters%5Bdate_from%5D=2020-06-12&parameters%5Bdate_to%5D=2022-06-15&parameters%5Btickers%5D={tickers}&token=a255db84b80243f79a120c8122daaedb";
            using (HttpResponseMessage response = await Client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Earnings? model;
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (JsonConvert.DeserializeObject<Earnings>(responseBody) != null)
                    {
                        model = JsonConvert.DeserializeObject<Earnings>(responseBody);
                    }
                    else
                    {
                        model = new Earnings();
                    }
                    return model;
                }
                else
                    throw new Exception(response.ReasonPhrase);

            }
        }
            //Cheks whether date is correct or not
            static string DateCherker(string date)
            {
                string[] s = date.Split('-');
                DateTime outDate = new DateTime(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]), Convert.ToInt32(s[2]));
                if (DateTime.Now > outDate && !(outDate.Year < DateTime.Now.Year - 5))
                    return date;
                else
                    return DateTime.Now.ToString("yyyy-mm-dd");
            }

        
    }
}
