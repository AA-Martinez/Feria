using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ThirdPerson;
    public GameObject FirstPerson;
    public bool State;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (State)
            {
                ThirdPerson.SetActive(true);
                FirstPerson.SetActive(false);
                State = false;
            }
            else
            {
                ThirdPerson.SetActive(false);
                FirstPerson.SetActive(true);
                State = true;
            }
            
        }
    }
}
