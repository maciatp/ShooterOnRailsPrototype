using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner_Script : MonoBehaviour
{
    public float secondsForBoss = 3;
    public GameObject boss;
    public Transform spawnLocation;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false; //desactivo trigger

          GameObject bossInScene = (GameObject)Instantiate(boss, spawnLocation.transform.position, new Quaternion(0, 0, 0, 0));
            bossInScene.transform.parent = GameObject.Find("GameplayPlane").gameObject.transform;
            Camera.main.GetComponent<MusicSelector_Script>().StartCoroutine("PlayBossMusic",secondsForBoss);
        }
        

    }
}
