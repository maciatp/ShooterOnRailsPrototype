using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement_Script : MonoBehaviour
{
    public bool mustFollowRoute = true;

    public Rigidbody enemyShip_RB;

    [SerializeField] EnemyHealth_Script enemyHealth_Script;

    public float speed = 10;

    public float currentDistanceToNextCheckpoint = 1000;

    
    public Transform beginShootingLocation;
    public Transform lastChanceLocation;
    public Transform outOfViewLocation;

    public int currentCheckpointInRoute = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentCheckpointInRoute = 0;
        enemyShip_RB = gameObject.GetComponent<Rigidbody>();
        beginShootingLocation.transform.parent = null;
        lastChanceLocation.transform.parent = null;
        outOfViewLocation.transform.parent = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, beginShootingLocation.transform.position);
        Gizmos.DrawLine(beginShootingLocation.transform.position, lastChanceLocation.transform.position);
        Gizmos.DrawLine(lastChanceLocation.transform.position, outOfViewLocation.transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!enemyHealth_Script.isShotDown)
        {
            if (mustFollowRoute == true)
            {
                if (currentCheckpointInRoute == 0)
                {
                    enemyShip_RB.transform.position += (beginShootingLocation.position - enemyShip_RB.transform.position).normalized * Time.deltaTime * speed;
                    currentDistanceToNextCheckpoint = (beginShootingLocation.position - enemyShip_RB.transform.position).magnitude;
                }

                else if (currentCheckpointInRoute == 1)
                {
                    enemyShip_RB.transform.position += (lastChanceLocation.position - enemyShip_RB.transform.position).normalized * Time.deltaTime * speed;
                    currentDistanceToNextCheckpoint = (lastChanceLocation.position - enemyShip_RB.transform.position).magnitude;
                }

                else if (currentCheckpointInRoute == 2)
                {
                    enemyShip_RB.transform.position += (outOfViewLocation.position - enemyShip_RB.transform.position).normalized * Time.deltaTime * speed;
                    currentDistanceToNextCheckpoint = (outOfViewLocation.position - enemyShip_RB.transform.position).magnitude;
                }
                else if (currentCheckpointInRoute == 3)
                {
                    DestroyWayPoints();
                    Destroy(gameObject);
                }

                if (currentDistanceToNextCheckpoint <= 1)
                {
                    currentCheckpointInRoute += 1;
                }

            }
            else //NO ROUTE
            {
                enemyShip_RB.transform.position += enemyShip_RB.transform.forward.normalized * speed * Time.deltaTime;
            }
        }
        else //SHOTDOWN
        {
            enemyShip_RB.transform.position += enemyShip_RB.transform.forward.normalized * (20/3) * Time.deltaTime;
            enemyShip_RB.mass *= 10;
        }
        
    }

    public void DestroyWayPoints()
    {
        if(beginShootingLocation != null)
        {
            Destroy(beginShootingLocation.gameObject);
        }
        if(lastChanceLocation != null)
        {
            Destroy(lastChanceLocation.gameObject);
        }
        if(outOfViewLocation != null)
        {
            Destroy(outOfViewLocation.gameObject);
        }
        
       
       
    }
}
