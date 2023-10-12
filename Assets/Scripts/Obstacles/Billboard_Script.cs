using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_Script : MonoBehaviour
{

   // public Rigidbody rb_Billboard;
    //public BoxCollider triggerCollider;
    public MeshRenderer billboardMeshRenderer;
    public ScoreManager_Script scoreManager_Script_;
    public bool isBillBoardActivated = false;
    public BillboardTrigger_Script billboardTrigger_Script_;

    public Texture[] myTextures = new Texture[13]; //CREO ARRAY DE TEXTURAS
   

    



    private void Awake()
    {
        scoreManager_Script_ = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager_Script>();
        billboardTrigger_Script_ = this.transform.GetChild(0).GetComponent<BillboardTrigger_Script>();
       
    }
    // Start is called before the first frame update
    void Start()
    {
       // triggerCollider = this.transform.GetChild(0).gameObject.GetComponent<BoxCollider>();
        //rb_Billboard = this.gameObject.GetComponent<Rigidbody>();
        billboardMeshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        billboardMeshRenderer.material.color = Color.red; //QUITAR COMENTARIO CUANDO TERMINE CON MELICOTO

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


   


   

    public void ActivateBillBoard()
    {
        isBillBoardActivated = true;
        // billboardMeshRenderer.material.color = Color.green; QUITAR COMENTARIO CUANDO TERMINE CON MELICOTO
        billboardMeshRenderer.material.color = Color.white;
        scoreManager_Script_.AddHits(1);
        SelectMelicotoTexture();
        

    }

    void SelectMelicotoTexture()
    {
        int i = Random.Range(0, myTextures.Length);
        billboardMeshRenderer.material.mainTexture = myTextures[i];
        
        

    }
}
