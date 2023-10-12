using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnTrigger_Script : MonoBehaviour
{

    public BoxCollider triggerCollider;

    public ColumnPrefab_Script column_Script_;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        triggerCollider = this.gameObject.GetComponent<BoxCollider>();
        column_Script_ = this.gameObject.transform.GetComponentInParent<ColumnPrefab_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            column_Script_.PlayAnimationColumn();
        }
    }

}
