using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyLightSelf : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 20);
	}

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Light>().intensity > 0)
        {
            GetComponent<Light>().intensity -= .003f;
        }
    }
}
