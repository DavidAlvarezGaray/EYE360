using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
// using System;

public class RadioEstaciones : MonoBehaviour
{

    public TMP_Text nombre_estacion;
    public Image imagen;

    public Sprite imagen1;
    public Sprite imagen2;
    public Sprite imagen3;
    public Sprite imagen4;
    public Sprite imagen5;
    public Sprite imagen6;
    public string tremenda = "LA TREMENDA 96.5FM";
    public string lobosfm = "LOBOS DURANGO 94.1 FM";
    public string lalupe = "LA LUPE";
    public string spotify = "AUXILIAR";
    public string exafm = "EXA 101.3 FM";
    public string radioapagado = "APAGADO";
    public AudioSource audiosource_radio;
    public AudioClip tremenda_audiosource;
    public AudioClip lobosfm_audiosource;
    public AudioClip spotify_audiosource;
    public AudioClip lalupe_audiosource;
    public AudioClip exafm_audiosource;
    public int radioactual;
    public int siguiente;
    public int anterior;
    public bool haschanged;

    // Start is called before the first frame update
    void Start()
    {
        radioactual = Random.Range(0, 5);
        if (radioactual == 0)
        {
            anterior = 3;
        }
        else {
            anterior = anterior - 1;
        }
        siguiente = radioactual + 1;
        haschanged = false;
        //nombre_estacion.text = "LA TREMENDA 96.5 FM";
        //nombre_estacion.text = datos_estaciones[0,0];
        // imagen.sprite = imagen1;
        //imagen.sprite = datos_estaciones[0,2];
        // audiosource_radio.clip = tremenda_audiosource;
        //audiosource_radio.clip = datos_estaciones[0,1];

        //Random.Range(0, 10);
    }

    // Update is called once per frame
    void Update()
    {

        




        if (Input.GetKeyDown(KeyCode.L))
        {
            radioactual = siguiente;
            haschanged = true;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            radioactual = anterior;
            haschanged = true;
        }

        switch (radioactual)
        {
            case 0:
                nombre_estacion.text = "LA TREMENDA 96.5 FM";
                imagen.sprite = imagen1;
                audiosource_radio.clip = tremenda_audiosource;
                siguiente = 1;
                anterior = 5;
                radioactual = 0;
                break;
            case 1:
                nombre_estacion.text = "LOBOS DURANGO 96.5 FM";
                imagen.sprite = imagen2;
                audiosource_radio.clip = lobosfm_audiosource;
                siguiente = 2;
                anterior = 0;
                radioactual = 1;
                break;
            case 2:
                nombre_estacion.text = "LA LUPE 96.5 FM";
                imagen.sprite = imagen3;
                audiosource_radio.clip = lalupe_audiosource;
                siguiente = 3;
                anterior = 1;
                radioactual = 2;
                break;
            case 3:
                nombre_estacion.text = "AUXILIAR";
                imagen.sprite = imagen4;
                audiosource_radio.clip = spotify_audiosource;
                siguiente = 4;
                anterior = 2;
                radioactual = 3;
                break;
            case 4:
                nombre_estacion.text = "EXA FM 101.3 FM";
                imagen.sprite = imagen5;
                audiosource_radio.clip = exafm_audiosource;
                siguiente = 5;
                anterior = 3;
                radioactual = 4;
                break;
            case 5:
                nombre_estacion.text = "APAGADO";
                imagen.sprite = imagen6;
                audiosource_radio.Stop();
                siguiente = 0;
                anterior = 4;
                radioactual = 5;
                break;
        }

        if (haschanged)
        {
            Debug.Log(audiosource_radio.time);
            audiosource_radio.time = Random.Range(0, audiosource_radio.clip.length);
            //
            audiosource_radio.Stop();
            audiosource_radio.Play();
            haschanged = false;
        }




    }
}
