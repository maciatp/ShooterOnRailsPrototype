using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnemySpawner_Script : MonoBehaviour
{
    [Header("Add SpawnLocations As Childs to DollyTrack to Add Enemies")]
   
   
    public float speed = 0;

   
    public int childrenSpawnPoints;
    [Header("Name tells enemyToSpawn")]
    public List<Transform> spawnLocations = new List<Transform>();
    

    private void Awake()
    {
        spawnLocations.Clear();

        childrenSpawnPoints = this.transform.GetChild(0).transform.childCount;

        //Debug.Log(childrenSpawnPoints + "CHILDREN SPAWN POINTS");

        for (int i = 0; i < childrenSpawnPoints; i++)
        {
            spawnLocations.Add(this.transform.GetChild(0).transform.GetChild(i).transform);
        }
    }

    private void Update()
    {
        if ((Application.platform == RuntimePlatform.WindowsEditor) && (Application.isPlaying == false))
        {
            spawnLocations.Clear();

            childrenSpawnPoints = this.transform.GetChild(0).transform.childCount;

            //Debug.Log(childrenSpawnPoints + "CHILDREN SPAWN POINTS");

            for(int i = 0; i < childrenSpawnPoints; i++)
            {
                spawnLocations.Add(this.transform.GetChild(0).transform.GetChild(i).transform);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SpawnEnemies();
        }
    }

   



    public void SpawnEnemies()
    {
        foreach(Transform spawnlocation in spawnLocations)
        {
            //OFFSET = track  position - spawnlocation
            Vector3 offset = spawnlocation.transform.position - this.gameObject.transform.GetChild(0).gameObject.transform.position; //GetComponent<Cinemachine.CinemachineSmoothPath>().m_Waypoints[0].position
           


            GameObject _enemySpawned = Instantiate<GameObject>(spawnlocation.gameObject.GetComponent<EnemyType_Script>().enemyType, spawnlocation.transform) as GameObject;
            
            _enemySpawned.gameObject.GetComponent<EnemyUpdatePosition_Script>().offset = offset;

            _enemySpawned.name = spawnlocation.gameObject.GetComponent<EnemyType_Script>().enemyType.name.ToString();

            //enemy path = trigger_path
            _enemySpawned.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Path = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Cinemachine.CinemachineSmoothPath>();
            _enemySpawned.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = speed;
            _enemySpawned.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position = 0;
            _enemySpawned.gameObject.GetComponent<EnemyUpdatePosition_Script>().OverWriteEnemyPosition();


        }


        //if(enemySelector == 0)
        //{
        //     int rand = Random.Range(0, enemies_GO.Count);
        //    //int rand = 1;
        //    int randLocation = Random.Range(1, 2);
        //    if (rand == 0)
        //    {
        //        //Instantiate<GameObject>(enemyFighter, spawnLocation1);
        //        //Instantiate<GameObject>(enemyFighter, spawnLocation2);

        //        ////USAR POOLS PARA ENEMIGOS
        //        //GameObject _enemyFighter = Instantiate<GameObject>(enemyFighter, spawnLocation1) as GameObject;
        //        ////enemy path = trigger_path
        //        //_enemyFighter.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Path = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Cinemachine.CinemachineSmoothPath>();
        //        //_enemyFighter.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = speed;
        //        //_enemyFighter.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position = 0;


        //        Debug.Log("He instanciado Fighters con Random = " + rand);
        //    }
        //    else if (rand == 1)
        //    {
        //        Instantiate<GameObject>(enemyBandit, spawnLocation1);
        //        Instantiate<GameObject>(enemyBandit, spawnLocation2);
        //        Debug.Log("He instanciado Bandits con Random = " + rand);
        //    }
        //    else if (rand == 2)
        //    {
        //        Instantiate<GameObject>(enemyTank, spawnLocation3);
        //        Instantiate<GameObject>(enemyTank, spawnLocation4);
        //        Debug.Log("He instanciado Tanks con Random = " + rand);
        //    }
        //    else if (rand == 3)
        //    {
        //        Instantiate<GameObject>(enemySaucer, spawnLocation1);
        //        Instantiate<GameObject>(enemySaucer, spawnLocation2);
        //        Debug.Log("He instanciado Saucers con Random = " + rand);
        //    }
        //    else if (rand == 4)
        //    {
        //        Instantiate<GameObject>(gangaFighter, spawnLocation1);
        //        Instantiate<GameObject>(gangaFighter, spawnLocation2);
        //        Debug.Log("He instanciado gangas con Random = " + rand);
        //    }
        //    else if (rand == 5)
        //    {
        //        Instantiate<GameObject>(dragonFighter, spawnLocation1);
        //        Instantiate<GameObject>(dragonFighter, spawnLocation2);
        //        Debug.Log("He instanciado dragons con Random = " + rand);
        //    }
        //}
        //else if(enemySelector == 1)
        //{
        //    //USAR POOLS PARA ENEMIGOS
        //    GameObject _enemyFighter = Instantiate<GameObject>(fighter, spawnLocation1) as GameObject;
        //    //enemy path = trigger_path
        //    _enemyFighter.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Path = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Cinemachine.CinemachineSmoothPath>();
        //    _enemyFighter.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Speed = speed;
        //    _enemyFighter.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position = 0;
        //}
        //else if (enemySelector == 2)
        //{
        //    Instantiate<GameObject>(enemyBandit, spawnLocation1);
        //    Instantiate<GameObject>(enemyBandit, spawnLocation2);
        //    Debug.Log("He instanciado Bandits con Selector = " + enemySelector);
        //}
        //else if (enemySelector == 3)
        //{
        //    Instantiate<GameObject>(enemyTank, spawnLocation3);
        //    Instantiate<GameObject>(enemyTank, spawnLocation4);
        //    Debug.Log("He instanciado Tanks con Selector = " + enemySelector);
        //}
        //else if (enemySelector == 4)
        //{
        //    Instantiate<GameObject>(enemySaucer, spawnLocation1);
        //    Instantiate<GameObject>(enemySaucer, spawnLocation2);
        //    Debug.Log("He instanciado Saucers con Selector = " + enemySelector);
        //}

        //else if (enemySelector == 5)
        //{
        //    Instantiate<GameObject>(gangaFighter, spawnLocation1);
        //    Instantiate<GameObject>(gangaFighter, spawnLocation2);
        //    Debug.Log("He instanciado Gangas con Selector = " + enemySelector);
        //}
        //else if (enemySelector == 6)
        //{
        //    Instantiate<GameObject>(dragonFighter, spawnLocation1);
        //    Instantiate<GameObject>(dragonFighter, spawnLocation2);
        //    Debug.Log("He instanciado Dragons con Selector = " + enemySelector);
        //}

    }

}
