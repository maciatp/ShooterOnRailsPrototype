using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement_Script : MonoBehaviour
{
    public Transform goalPosition;

    public bool isAtGoalPosition = false;
    public float speed = 10;

    public Animator bossChildAnimator;
    public UIBossHealth_Script uIBossHealth_Script_;

    private void Awake()
    {
        uIBossHealth_Script_ = GameObject.Find("UIBossHealth").gameObject.GetComponent<UIBossHealth_Script>();
        goalPosition = GameObject.Find("GameplayPlane").transform.GetChild(6);
        bossChildAnimator = this.transform.GetChild(0).GetComponent<Animator>();
        bossChildAnimator.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isAtGoalPosition != true)
        {
            this.transform.position += new Vector3(goalPosition.transform.position.x - this.transform.position.x, goalPosition.transform.position.y - this.transform.position.y, goalPosition.transform.position.z - this.transform.position.z).normalized * speed * Time.deltaTime;
            
            if ((goalPosition.transform.position - this.transform.position).magnitude <= 3)
            {
                uIBossHealth_Script_.StartCoroutine(uIBossHealth_Script_.coroutineName);
                isAtGoalPosition = true;
                this.gameObject.GetComponent<Animator>().enabled = true;
                bossChildAnimator.enabled = true;
            }
        }

        
    }


    
}
