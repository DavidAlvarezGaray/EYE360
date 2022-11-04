using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointEnter : MonoBehaviour
{

    public GameObject CheckpointUi;

 

    // Start is called before the first frame update
    void Start()
    {
        CheckpointUi.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            CheckpointUi.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            CheckpointUi.gameObject.SetActive(false);
        }
    }
}


/*
   void OnTriggerEnter(Collider other)
    {
        canvas.enabled = true;
    }

    void OnTriggerExit()
    {
        canvas.enabled = false;
    }
     */
