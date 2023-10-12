using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnemyType_Script : MonoBehaviour
{
    public EnemiesData enemies;
    //SUBIR RANGO PARA PODER ELEGIR!!!!
    [Range(0,6)]
    public int enemySelector;
    public GameObject enemyType = null;


    private void Awake()
    {
        enemyType = enemies.enemiesList[enemySelector].gameObject;

        this.gameObject.name = enemyType.name.ToString() + "_SpawnLocation";
    }
    private void Update()
    {
      
        if((Application.platform == RuntimePlatform.WindowsEditor) && (Application.isPlaying == false))
        { //ESTO SE LLAMA PERO SOLO DURANTE EL EDIT

            enemyType = enemies.enemiesList[enemySelector].gameObject;

            this.gameObject.name = enemyType.name.ToString() + "_SpawnLocation";


            //Debug.Log("ENEMYTIPE UPDATE");

        }


        
    }

}


