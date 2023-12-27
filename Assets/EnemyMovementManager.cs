using Cinemachine;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour
{
    [SerializeField] bool mustFollowTrack;
    [Tooltip("Speed when NOT following track. i.e. Enemy from Boss")]
    [SerializeField] float speed = 10f;
    

    public bool MustFollowTrack
    {
        get { return mustFollowTrack; }
        set { mustFollowTrack = value; }
    }
    private void Start()
    {
        if(!mustFollowTrack)
        {
            GetComponent<CinemachineDollyCart>().enabled = false;
        }
    }
    private void Update()
    {
        if(!mustFollowTrack)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (-speed * Time.deltaTime));
        }
        
    }
}
