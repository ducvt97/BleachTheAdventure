using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private Stat health;
	[SerializeField]
	private Stat energy;
    // Start is called before the first frame update
    private void Awake()
    {
    	health.Init();
    	energy.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
