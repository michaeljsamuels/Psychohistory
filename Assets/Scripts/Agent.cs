using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using SimpleJSON;


public class Agent
{
    
    public GameObject parent;
    public RestRouter router;

    struct StockQuote
    {
        public float open, high, low, close, volume;

        public StockQuote(float open, float high, float low, float close, float volume)
        {
            this.open = open;
            this.high = high;
            this.low = low;
            this.close = close;
            this.volume = volume;
        }
    }

    struct CryptoQuote
    {
        public float bid, ask, volume;

        public CryptoQuote(float bid, float ask, float volume)
        {
            this.bid = bid;
            this.ask = ask;
            this.volume = volume;
        }
    }

    public void RequestCryptoQuote(Ticker client, string pair)
    {
        router.GeminiTimeSeries(client, this, pair);
    }

    public void ReceiveCryptoQuote(Agent agent, Ticker client, JSONNode data) {

        List<CryptoQuote> quotes = new List<CryptoQuote>();

        JSONNode jsonQuotes = data;

        quotes.Add(new CryptoQuote(
            data["bid"],
            data["ask"],
            data["volume"]["BTC"]
            ));

        Debug.Log(quotes[0].bid);
        client.UpdateTicker(quotes[0].bid.ToString());
       
    }

    public void RequestStockQuote(Ticker client, string symbol)
    {
        router.AlphaVantageTimeSeries(client, this, symbol);
    }

    public void ReceiveStockQuote(Agent agent, Ticker client, JSONNode data)
    {
        List<StockQuote> quotes = new List<StockQuote>();

        JSONNode jsonQuotes = data["Time Series (5min)"];

        foreach (JSONNode quote in jsonQuotes)
        {
         //   Debug.Log(quote); //see raw
;           quotes.Add(new StockQuote(
                quote["1. open"],
                quote["2. high"],
                quote["3. low"],
                quote["4. close"],
                quote["5. volume"]
            ));
        }

      //  Debug.Log(quotes[0].volume);
    }

   
}
