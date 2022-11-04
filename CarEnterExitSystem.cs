using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnterExitSystem : MonoBehaviour
{

    public MonoBehaviour CarController;
    public MonoBehaviour RadioEstaciones;
    //public MonoBehaviour CarSounds;
    public Transform Car;
    public Transform Player;

   // [Header(“Cameras”)]
    public GameObject PlayerCam;
    public GameObject CarCam;

    public GameObject DriveUi;
    public GameObject RadioUi;
    public AudioSource audiosource_radio;

    bool Candrive;



    // Start is called before the first frame update
    void Start()
    {
        CarController.enabled = false;
        RadioEstaciones.enabled = false;
        //CarSounds.enabled = false;
        DriveUi.gameObject.SetActive(false);
        RadioUi.gameObject.SetActive(false);
        CarCam.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && Candrive)  // Here After Click F button and trigger is true player is driving
        {

            CarController.enabled = true; // After Click F button Car Controller Script is enabled

            audiosource_radio.Play();
            RadioEstaciones.enabled = true;
            //  CarSounds.enabled = true;

            DriveUi.gameObject.SetActive(false);

            // Here we parent Car with player
            Player.transform.SetParent(Car);
            Player.gameObject.SetActive(false);

            // Camera
            PlayerCam.gameObject.SetActive(false);
            CarCam.gameObject.SetActive(true);
        }
        


            if (Input.GetKeyDown(KeyCode.G))
        {


            CarController.enabled = false; // After Click G button Car Controller Script is disable
            RadioEstaciones.enabled = false;
            //  CarSounds.enabled = false;

            // Here We Unparent the Player with Car
            Player.transform.SetParent(null);
            Player.gameObject.SetActive(true);

            // Here If Player Is Not Driving So PlayerCamera turn On and Car Camera turn off

            PlayerCam.gameObject.SetActive(true);
            CarCam.gameObject.SetActive(false);
        }
    }


    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            DriveUi.gameObject.SetActive(true);
            RadioUi.gameObject.SetActive(true);
            Candrive = true;

           
            RadioEstaciones.enabled = true;
            
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            DriveUi.gameObject.SetActive(false);
            RadioUi.gameObject.SetActive(false);
            Candrive = false;
            RadioEstaciones.enabled = false;
            audiosource_radio.Stop();
        }
    }
}