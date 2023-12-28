using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_Script : MonoBehaviour
{

   
   
    ScoreManager_Script scoreManager_Script_;
    bool isBillBoardActivated = false;
    BillboardTrigger_Script billboardTrigger_Script_;

    [SerializeField] Texture[] myTextures = new Texture[13]; //CREO ARRAY DE TEXTURAS
   

    public bool IsBillboardActivated
    {
        get { return isBillBoardActivated; }
        set { isBillBoardActivated = value; }
    }



    private void Awake()
    {
        scoreManager_Script_ = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager_Script>();
        billboardTrigger_Script_ = this.transform.GetChild(0).GetComponent<BillboardTrigger_Script>();
       
    }
    // Start is called before the first frame update
    void Start()
    {

        GetComponent<MeshRenderer>().material.color = Color.red; //QUITAR COMENTARIO CUANDO TERMINE CON MELICOTO
    }


    public void ActivateBillBoard()
    {
        isBillBoardActivated = true;
        // billboardMeshRenderer.material.color = Color.green; QUITAR COMENTARIO CUANDO TERMINE CON MELICOTO
        GetComponent<MeshRenderer>().material.color = Color.white;
        scoreManager_Script_.AddHits(1);
        SelectMelicotoTexture();
        

    }

    void SelectMelicotoTexture()
    {
        int i = Random.Range(0, myTextures.Length);
        GetComponent<MeshRenderer>().material.mainTexture = myTextures[i];
        
        

    }
}
