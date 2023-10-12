using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow_Script : MonoBehaviour
{

    public PlayerMovement_Script playerMovement_Script_;

    private void Awake()
    {
        playerMovement_Script_ = GameObject.Find("Player").GetComponent<PlayerMovement_Script>();
    }
    

    private void FixedUpdate()
    {
        this.transform.localPosition = new Vector3(playerMovement_Script_.transform.localPosition.x, playerMovement_Script_.transform.localPosition.y, this.transform.localPosition.z);
    }
}
