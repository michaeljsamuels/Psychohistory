using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SimpleJSON;


public class RestRouter :  MonoBehaviour
{
  
    public void GeminiTimeSeries(Ticker client, Agent agent, string pair)
    {
        string baseURL = "https://api.gemini.com/v1";
        string query = "/pubticker/";

        StartCoroutine(ProcessRequest(1, client, agent, baseURL + query + pair));
    }


    public void AlphaVantageTimeSeries(Ticker client, Agent agent, string sym)
    {
        string baseURL = "https://www.alphavantage.co/";
        string query = "query?function=TIME_SERIES_INTRADAY";
        string symbol = "&symbol=" + sym;
        string interval = "&interval=5min";
        string key = "&apikey=" + apiKeymaster.keys["AlphaVantage"];


        StartCoroutine(ProcessRequest(2, client, agent, baseURL + query + symbol + interval + key));
      
    }

    public void GetQuote(string symbol)
    {
        string baseURL = "https://api.tdameritrade.com/v1/marketdata/";

        string quotes = "/quotes";

        StartCoroutine(ProcessRequest(baseURL + symbol + quotes));

    }

    private IEnumerator ProcessRequest(string uri)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {

            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {

                Debug.Log(request.error);
            }
            else
            {
                JSONNode data = JSON.Parse(request.downloadHandler.text);

                Debug.Log(data);
            }
        }
    }

    private IEnumerator ProcessRequest(int quoteType, Ticker client, Agent caller, string uri)
    {

        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {

                Debug.Log(request.error);
            }
            else
            {
                JSONNode data = JSON.Parse(request.downloadHandler.text);

                switch (quoteType)
                {
                    case 1:
                        caller.ReceiveCryptoQuote(caller, client, data);
                        break;

                    case 2:
                        caller.ReceiveStockQuote(caller, client, data);
                        break;
                }
    
            }
        }


    }
}
