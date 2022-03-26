using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ticker : MonoBehaviour
{
    public Text tickText;
    private RestRouter router;
    private Agent agent;
    // Start is called before the first frame update
    void Start()
    {
        tickText = GetComponent<Text>();
        router = GameObject.Find("RestRouter").GetComponent<RestRouter>();
        agent = new Agent();
        agent.router = router;
        Time.fixedDeltaTime = 2;

        router.GetQuote("F");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        agent.RequestCryptoQuote(this, "btcusd");
         
    }

    public void UpdateTicker(string data)
    {
        tickText.text = data;
    }
}
