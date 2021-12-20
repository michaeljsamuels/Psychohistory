using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Trader: Agent
{

    public Trader(GameObject parent)
    {
        this.parent = parent;
        this.router = parent.GetComponent<RestRouter>();
        
    }


}
