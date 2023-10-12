using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool active;
    public GameObject gameCamera;
    public GameObject extraCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(active == active)
            {
                active = !active;
                GameCamera(active);
            }
            else if(active == !active)
            {
                active = active;
                GameCamera(active);
            }
            //active = !active;
            //GameCamera(active);
        }
    }

    void GameCamera(bool state)
    {
        gameCamera.SetActive(!state);
        extraCamera.SetActive(state);
    }
}
