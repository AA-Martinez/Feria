using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ThirdPerson;
    public GameObject FirstPerson;
    public bool State;
    public Image Crosshair;

    void Start()
    {
        Crosshair.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (State)
            {
                ThirdPerson.SetActive(true);
                FirstPerson.SetActive(false);
                Crosshair.enabled = false;
                State = false;
            }
            else
            {
                ThirdPerson.SetActive(false);
                FirstPerson.SetActive(true);
                Crosshair.enabled = true;
                State = true;
            }
            
        }
    }
}
