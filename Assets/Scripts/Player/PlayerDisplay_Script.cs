using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay_Script : MonoBehaviour
{
    public GameObject playerInScene;
    public PlayerHealth_Script playerHealth_Script_;
    public PlayerMovement_Script playerMovement_Script_;
    public PlayerShooting_Script playerShooting_Script_;

    public GameObject scoreManager_GO;
    public ScoreManager_Script scoreManager_Script_;

   
    public TMPro.TextMeshProUGUI playerHits_Text;
    
    public TMPro.TextMeshProUGUI playerBombs_Text;

    //public List<GameObject> bombImages_GO = new List<GameObject>();
    public Image bombImage1;
    public Image bombImage2;
    public Image bombImage3;
    public Image bombImage4;
    public GameObject bombTextGameObject;
    public GameObject uIBombSpawnLocation;
    public float uIBombLerpMultiplier = 3f;

    public bool uIBomb1HasToMove = false;
    public bool uIBomb2HasToMove = false;
    public bool uIBomb3HasToMove = false;
    public bool uIBomb4HasToMove = false;

    public Vector3 bombImage1OriginalLocalPosition;
    public Vector3 bombImage2OriginalLocalPosition;
    public Vector3 bombImage3OriginalLocalPosition;
    public Vector3 bombImage4OriginalLocalPosition;



    private void Awake()
    {
        playerInScene = GameObject.FindGameObjectWithTag("Player");
        playerHealth_Script_ = playerInScene.gameObject.GetComponent<PlayerHealth_Script>();

        playerMovement_Script_ = playerInScene.gameObject.GetComponent<PlayerMovement_Script>();
        playerShooting_Script_ = playerInScene.gameObject.GetComponent<PlayerShooting_Script>();

        scoreManager_GO = GameObject.Find("ScoreManager");
        scoreManager_Script_ = scoreManager_GO.GetComponent<ScoreManager_Script>();

        if (this.gameObject.name == "HitsText")
        {
            playerHits_Text = this.gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        }

       
        if (this.gameObject.name == "UIBombs")
        {
            bombImage1 = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
            bombImage2 = this.gameObject.transform.GetChild(1).gameObject.GetComponent<Image>(); 
            bombImage3 = this.gameObject.transform.GetChild(2).gameObject.GetComponent<Image>(); 
            bombImage4 = this.gameObject.transform.GetChild(3).gameObject.GetComponent<Image>();
            bombTextGameObject = this.gameObject.transform.GetChild(4).gameObject;
            playerBombs_Text = this.gameObject.transform.GetChild(4).GetComponent<TMPro.TextMeshProUGUI>();
            uIBombSpawnLocation = this.gameObject.transform.GetChild(5).gameObject;
            bombImage1OriginalLocalPosition = bombImage1.gameObject.transform.localPosition;
            bombImage2OriginalLocalPosition = bombImage2.gameObject.transform.localPosition;
            bombImage3OriginalLocalPosition = bombImage3.gameObject.transform.localPosition;
            bombImage4OriginalLocalPosition = bombImage4.gameObject.transform.localPosition;

        }



    }
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.name == "HitsText")
        {
            playerHits_Text.text = (scoreManager_Script_.actualHits.ToString("000"));
        }

       
       
        if(this.gameObject.name == "UIBombs")
        {
            SetUIBombs(playerShooting_Script_.actualBombs);
        }
        

    }

    // Update is called once per frame
    void Update()
    {

        

        if (this.gameObject.name == "HitsText")
        {
            playerHits_Text.text = (scoreManager_Script_.actualHits.ToString("000"));
        }

      

        //ESTO NO DEBERÍA FUNCIONAR. COMPROBAR LUEGO
        if (this.gameObject.name == "BombsText")
        {
            playerBombs_Text.text = (playerShooting_Script_.actualBombs.ToString("x 0"));
            if (playerShooting_Script_.actualBombs < 1)
            {
                playerBombs_Text.color = Color.red;
            }
            else if ((playerShooting_Script_.actualBombs > 0) && (playerBombs_Text.color == Color.red))
            {
                playerBombs_Text.color = Color.white;
            }
        }

        if(this.gameObject.name == "UIBombs")
        {

            if(playerShooting_Script_.isBombShot == true)
            {
                bombImage1.color = new Color(1,1,1, 0.5f);
                bombImage2.color = new Color(1,1,1, 0.5f);
                bombImage3.color = new Color(1,1,1, 0.5f);
                bombImage4.color = new Color(1,1,1, 0.5f);
            }
            else
            {
                bombImage1.color = new Color(1, 1, 1, 0.75f);
                bombImage2.color = new Color(1, 1, 1, 0.75f);
                bombImage3.color = new Color(1, 1, 1, 0.75f);
                bombImage4.color = new Color(1, 1, 1, 0.75f);
            }


            if(playerShooting_Script_.actualBombs <= 4)
            {
                


                if (uIBomb1HasToMove == false)
                {
                    if (bombImage1.transform.localPosition.x != bombImage1OriginalLocalPosition.x)
                    {
                        bombImage1.transform.localPosition = new Vector2(Mathf.Lerp(bombImage1.transform.localPosition.x, bombImage1OriginalLocalPosition.x, Time.unscaledDeltaTime * uIBombLerpMultiplier), bombImage1.transform.localPosition.y);
                    }

                }
                else
                {
                    bombImage1.transform.localPosition = new Vector2(Mathf.Lerp(bombImage1.transform.localPosition.x, uIBombSpawnLocation.transform.localPosition.x, Time.unscaledDeltaTime * uIBombLerpMultiplier), bombImage1.transform.localPosition.y);
                    if (bombImage1.transform.localPosition.x >= uIBombSpawnLocation.transform.position.x)
                    {
                        uIBomb1HasToMove = false;
                    }
                }

                if (uIBomb2HasToMove == false)
                {
                    if (bombImage2.transform.localPosition.x != bombImage2OriginalLocalPosition.x)
                    {
                        bombImage2.transform.localPosition = new Vector2(Mathf.Lerp(bombImage2.transform.localPosition.x, bombImage2OriginalLocalPosition.x, Time.unscaledDeltaTime * uIBombLerpMultiplier), bombImage2.transform.localPosition.y);
                    }
                }
                else
                {
                    bombImage2.transform.localPosition = new Vector2(Mathf.Lerp(bombImage2.transform.localPosition.x, uIBombSpawnLocation.transform.localPosition.x, Time.unscaledDeltaTime * uIBombLerpMultiplier), bombImage2.transform.localPosition.y);
                    if (bombImage2.transform.localPosition.x >= uIBombSpawnLocation.transform.position.x)
                    {
                        uIBomb2HasToMove = false;
                    }
                }


                if (uIBomb3HasToMove == false)
                {
                    if (bombImage3.transform.localPosition.x != bombImage3OriginalLocalPosition.x)
                    {
                        bombImage3.transform.localPosition = new Vector2(Mathf.Lerp(bombImage3.transform.localPosition.x, bombImage3OriginalLocalPosition.x, Time.unscaledDeltaTime * uIBombLerpMultiplier), bombImage3.transform.localPosition.y);
                    }
                }
                else
                {
                    bombImage3.transform.localPosition = new Vector2(Mathf.Lerp(bombImage3.transform.localPosition.x, uIBombSpawnLocation.transform.localPosition.x, Time.unscaledDeltaTime * uIBombLerpMultiplier), bombImage3.transform.localPosition.y);
                    if (bombImage3.transform.localPosition.x >= uIBombSpawnLocation.transform.position.x)
                    {
                        uIBomb3HasToMove = false;
                    }
                }


                if (uIBomb4HasToMove == false)
                {
                    if (bombImage4.transform.localPosition.x != bombImage4OriginalLocalPosition.x)
                    {
                        bombImage4.transform.localPosition = new Vector2(Mathf.Lerp(bombImage4.transform.localPosition.x, bombImage4OriginalLocalPosition.x, Time.unscaledDeltaTime * uIBombLerpMultiplier), bombImage4.transform.localPosition.y);
                    }
                }
                else
                {
                    bombImage4.transform.localPosition = new Vector2(Mathf.Lerp(bombImage4.transform.localPosition.x, uIBombSpawnLocation.transform.localPosition.x, Time.unscaledDeltaTime * uIBombLerpMultiplier), bombImage4.transform.localPosition.y);
                    if (bombImage4.transform.localPosition.x >= uIBombSpawnLocation.transform.position.x)
                    {
                        uIBomb4HasToMove = false;
                    }
                }
            }
            else
            {
                bombImage1.transform.localPosition = new Vector2(Mathf.Lerp(bombImage1.transform.localPosition.x, bombImage3OriginalLocalPosition.x, Time.unscaledDeltaTime * uIBombLerpMultiplier), bombImage1.transform.localPosition.y);
                bombImage2.transform.localPosition = new Vector2(Mathf.Lerp(bombImage2.transform.localPosition.x, bombImage3OriginalLocalPosition.x, Time.unscaledDeltaTime * uIBombLerpMultiplier), bombImage2.transform.localPosition.y);
                bombImage4.transform.localPosition = new Vector2(Mathf.Lerp(bombImage4.transform.localPosition.x, bombImage3OriginalLocalPosition.x, Time.unscaledDeltaTime * uIBombLerpMultiplier), bombImage4.transform.localPosition.y);
                
            }
            
            
        }
    }
    public void SetUIBombs(int actualBombs)
    {


        if (actualBombs == 0)
        {
            bombImage1.gameObject.SetActive(false);
            bombImage2.gameObject.SetActive(false);
            bombImage3.gameObject.SetActive(false);
            bombImage4.gameObject.SetActive(false);
            bombTextGameObject.gameObject.SetActive(false);

        }

        else if (actualBombs == 1)
        {
            bombImage1.gameObject.SetActive(true);
            bombImage2.gameObject.SetActive(false);
            bombImage3.gameObject.SetActive(false);
            bombImage4.gameObject.SetActive(false);
            bombTextGameObject.gameObject.SetActive(false);

            //MoveUIBomb(bombImage1, uIBombSpawnLocation.transform, bombImage1OriginalLocalPosition);
            
        }
       else if (actualBombs == 2)
        {
            bombImage1.gameObject.SetActive(true);
            bombImage2.gameObject.SetActive(true);
            bombImage3.gameObject.SetActive(false);
            bombImage4.gameObject.SetActive(false);
            bombTextGameObject.gameObject.SetActive(false);

        }
       else if (actualBombs == 3)
        {
            bombImage1.gameObject.SetActive(true);
            bombImage2.gameObject.SetActive(true);
            bombImage3.gameObject.SetActive(true);
            bombImage4.gameObject.SetActive(false);
            bombTextGameObject.gameObject.SetActive(false);

        }
       else if (actualBombs == 4)
        {
            bombImage1.gameObject.SetActive(true);
            bombImage2.gameObject.SetActive(true);
            bombImage3.gameObject.SetActive(true);
            bombImage4.gameObject.SetActive(true);
            bombTextGameObject.gameObject.SetActive(false);

        }
       else if(actualBombs > 4)
        {
            bombImage1.gameObject.SetActive(false);
            bombImage2.gameObject.SetActive(false);
            bombImage3.gameObject.SetActive(true);
            bombImage4.gameObject.SetActive(false);
            bombTextGameObject.gameObject.SetActive(true);
            playerBombs_Text.text = (actualBombs.ToString());


        }
    }

    public void AddUIBomb(int _actualBombs, int addedOrRemoved)
    {
        if(addedOrRemoved > 0) // falta preparar lanzar bomba
        {
            if (_actualBombs == 1)
            {
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(false);
                bombImage3.gameObject.SetActive(false);
                bombImage4.gameObject.SetActive(false);
                bombTextGameObject.gameObject.SetActive(false);

                bombImage1.transform.position = new Vector3(uIBombSpawnLocation.transform.position.x, bombImage1.transform.position.y,bombImage1.transform.position.z);
                uIBomb1HasToMove = false;
                uIBomb2HasToMove = true;
                uIBomb3HasToMove = true;
                uIBomb4HasToMove = true;

                //MoveUIBomb(bombImage1, uIBombSpawnLocation.transform, bombImage1OriginalLocalPosition);

            }
            else if (_actualBombs == 2)
            {
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(true);
                bombImage3.gameObject.SetActive(false);
                bombImage4.gameObject.SetActive(false);
                bombTextGameObject.gameObject.SetActive(false);

                bombImage2.transform.position = new Vector3(uIBombSpawnLocation.transform.position.x, bombImage2.transform.position.y, bombImage2.transform.position.z);
                //MoveUIBomb(bombImage1, uIBombSpawnLocation.transform, bombImage1OriginalLocalPosition);

                uIBomb1HasToMove = false;
                uIBomb2HasToMove = false;
                uIBomb3HasToMove = true;
                uIBomb4HasToMove = true;

            }
            else if (_actualBombs == 3)
            {
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(true);
                bombImage3.gameObject.SetActive(true);
                bombImage4.gameObject.SetActive(false);
                bombTextGameObject.gameObject.SetActive(false);

                bombImage3.transform.position = new Vector3(uIBombSpawnLocation.transform.position.x, bombImage3.transform.position.y, bombImage3.transform.position.z);
                //MoveUIBomb(bombImage1, uIBombSpawnLocation.transform, bombImage1OriginalLocalPosition);

                uIBomb1HasToMove = false;
                uIBomb2HasToMove = false;
                uIBomb3HasToMove = false;
                uIBomb4HasToMove = true;

            }
            else if (_actualBombs == 4)
            {
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(true);
                bombImage3.gameObject.SetActive(true);
                bombImage4.gameObject.SetActive(true);
                bombTextGameObject.gameObject.SetActive(false);

                bombImage4.transform.position = new Vector3(uIBombSpawnLocation.transform.position.x, bombImage4.transform.position.y, bombImage4.transform.position.z);
                //MoveUIBomb(bombImage1, uIBombSpawnLocation.transform, bombImage1OriginalLocalPosition);
                uIBomb1HasToMove = false;
                uIBomb2HasToMove = false;
                uIBomb3HasToMove = false;
                uIBomb4HasToMove = false;

            }
            else if (_actualBombs > 4)
            {
                //bombImage1.gameObject.SetActive(false);
                //bombImage2.gameObject.SetActive(false);
                bombImage3.gameObject.SetActive(true);
                //bombImage4.gameObject.SetActive(false);
                bombTextGameObject.gameObject.SetActive(true);
                playerBombs_Text.text = (_actualBombs.ToString());


            }
        }
        else
        {
            if(_actualBombs == 0)
            {
                uIBomb1HasToMove = true;
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(false);
                bombImage3.gameObject.SetActive(false);
                bombImage4.gameObject.SetActive(false);
                bombTextGameObject.gameObject.SetActive(false);

            }
            else if(_actualBombs == 1)
            {
                uIBomb2HasToMove = true;
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(true);
                bombImage3.gameObject.SetActive(false);
                bombImage4.gameObject.SetActive(false);
                bombTextGameObject.gameObject.SetActive(false);
            }
            else if (_actualBombs == 2)
            {
                uIBomb3HasToMove = true;
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(true);
                bombImage3.gameObject.SetActive(true);
                bombImage4.gameObject.SetActive(false);
                bombTextGameObject.gameObject.SetActive(false);
            }
            else if (_actualBombs == 3)
            {
                uIBomb4HasToMove = true;
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(true);
                bombImage3.gameObject.SetActive(true);
                bombImage4.gameObject.SetActive(true);
                bombTextGameObject.gameObject.SetActive(false);
            }
            else if (_actualBombs == 4)
            {
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(true);
                bombImage3.gameObject.SetActive(true);
                bombImage4.gameObject.SetActive(true);
                bombTextGameObject.gameObject.SetActive(false);
                //bombTextGameObject.gameObject.SetActive(true);
                //playerBombs_Text.text = (_actualBombs.ToString());
            }
            else if( _actualBombs > 4)
            {
                bombImage1.gameObject.SetActive(false);
                bombImage2.gameObject.SetActive(false);
                bombImage3.gameObject.SetActive(true);
                bombImage4.gameObject.SetActive(false);
                bombTextGameObject.gameObject.SetActive(true);
                playerBombs_Text.text = (_actualBombs.ToString());
            }
        }
      
    }

    public void MoveUIBomb(GameObject uIBombToMove, Transform spawnLocation, Vector3 goalPosition)
    {

        uIBombToMove.transform.position = spawnLocation.transform.position;


    }

}
