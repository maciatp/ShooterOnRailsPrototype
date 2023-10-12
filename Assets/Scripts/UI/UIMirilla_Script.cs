using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMirilla_Script : MonoBehaviour
{
    public Transform mirillaCercaInWorld;
    public Transform mirillaLejosInWorld;
    public GameObject playerInScene;

    [Header("Settings")]
    public bool joystick = true;

    public float xyMirillaSpeed = 100;


    // Start is called before the first frame update
    void Start()
    {
        playerInScene = GameObject.Find("Player");     
    }

    // Update is called once per frame
    void Update()
    {
        mirillaCercaInWorld.position = Camera.main.ScreenToWorldPoint(this.transform.position + new Vector3(0, 0, 20));
        mirillaLejosInWorld.position = Camera.main.ScreenToWorldPoint(this.transform.position + new Vector3(0, 0, 23));
        Debug.DrawLine(playerInScene.transform.position, mirillaCercaInWorld.transform.position);

        float h = joystick ? Input.GetAxis("Horizontal") : Input.GetAxis("Mouse X");
        float v = joystick ? Input.GetAxis("Vertical") : Input.GetAxis("Mouse Y");

        if(h != 0 || v != 0)
        {
            LocalMove(h, v, xyMirillaSpeed);
        }
       if(( h == 0 )  && transform.position.x != 0)
        {
            transform.localPosition += new Vector3((0 - transform.localPosition.x), 0)* xyMirillaSpeed * Time.deltaTime;
            
        }
     
       
       /* if ((v == 0) && transform.position.y != 0)
        {
            transform.localPosition += new Vector3(0, (0 - transform.localPosition.y)) * xyMirillaSpeed * Time.deltaTime;
        } */
    }

    void LocalMove(float x, float y, float speed)  //MOVIMIENTO DEL JUGADOR EN LA PANTALLA
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        Debug.Log(new Vector3(x, y, 0) * speed * Time.deltaTime);



     // ClampPosition();
    }

    void ClampPosition() //LÍMITES DE LA PANTALLA
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(playerInScene.transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
