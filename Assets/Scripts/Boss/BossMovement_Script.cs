using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement_Script : MonoBehaviour
{
    bool isAtGoalPosition = false;
    UIBossHealth_Script uIBossHealth_Script_;
    Transform goalPosition;


    [SerializeField] float speed = 10;
    [SerializeField] Animator bossChildAnimator;

    public UIBossHealth_Script UIBossHealthScript
    {
        get { return uIBossHealth_Script_; }
        set { uIBossHealth_Script_ = value; }
    }

    private void Awake()
    {
        uIBossHealth_Script_ = GameObject.Find("UIBossHealth").gameObject.GetComponent<UIBossHealth_Script>();
        goalPosition = GameObject.Find("GameplayPlane").transform.GetChild(6);
        
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
                uIBossHealth_Script_.EnableBossUIHealth();
                isAtGoalPosition = true;
                this.gameObject.GetComponent<Animator>().enabled = true;
                bossChildAnimator.enabled = true;
            }
        }

        
    }


    
}
