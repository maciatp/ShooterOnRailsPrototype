using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBombs : MonoBehaviour
{
    PlayerShooting_Script playerShooting_Script_;
    [Header("Component References")]
    [SerializeField] Image bombImage1;
    [SerializeField] Image bombImage2;
    [SerializeField] Image bombImage3;
    [SerializeField] Image bombImage4;
    [SerializeField] TMPro.TextMeshProUGUI playerBombs_Text;

    [Space]
    [Header("UI Parameters")]
    [SerializeField] GameObject uIBombSpawnLocation;
    [SerializeField] Transform bombImage1NewLocation;
    [SerializeField] float uIBombLerpMultiplier = 3f;

    bool uIBomb1HasToMove = false;
    bool uIBomb2HasToMove = false;
    bool uIBomb3HasToMove = false;
    bool uIBomb4HasToMove = false;

    Vector3 bombImage1OriginalLocalPosition;
    Vector3 bombImage2OriginalLocalPosition;
    Vector3 bombImage3OriginalLocalPosition;
    Vector3 bombImage4OriginalLocalPosition;

    private void Start()
    {
        bombImage1OriginalLocalPosition = bombImage1.transform.localPosition;
        bombImage2OriginalLocalPosition = bombImage2.transform.localPosition;
        bombImage3OriginalLocalPosition = bombImage3.transform.localPosition;
        bombImage4OriginalLocalPosition = bombImage4.transform.localPosition;

        playerShooting_Script_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting_Script>();
        SetUIBombs(playerShooting_Script_.Bombs);
    }

    private void Update()
    {
        playerBombs_Text.text = (playerShooting_Script_.Bombs.ToString("x 0"));
        if (playerShooting_Script_.Bombs < 1)
        {
            playerBombs_Text.color = Color.red;
        }
        else if ((playerShooting_Script_.Bombs > 0) && (playerBombs_Text.color == Color.red))
        {
            playerBombs_Text.color = Color.white;
        }
        if (playerShooting_Script_.IsBombShot == true)
        {
            bombImage1.color = new Color(1, 1, 1, 0.5f);
            bombImage2.color = new Color(1, 1, 1, 0.5f);
            bombImage3.color = new Color(1, 1, 1, 0.5f);
            bombImage4.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            bombImage1.color = new Color(1, 1, 1, 0.75f);
            bombImage2.color = new Color(1, 1, 1, 0.75f);
            bombImage3.color = new Color(1, 1, 1, 0.75f);
            bombImage4.color = new Color(1, 1, 1, 0.75f);
        }


        if (playerShooting_Script_.Bombs <= 4)
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
            bombImage1.transform.localPosition = new Vector2(Mathf.Lerp(bombImage1.transform.localPosition.x, bombImage1NewLocation.localPosition.x, Time.unscaledDeltaTime * uIBombLerpMultiplier), bombImage1.transform.localPosition.y);
            bombImage2.transform.localPosition = new Vector2(Mathf.Lerp(bombImage2.transform.localPosition.x, bombImage1NewLocation.localPosition.x, Time.unscaledDeltaTime * uIBombLerpMultiplier), bombImage2.transform.localPosition.y);
            bombImage3.transform.localPosition = new Vector2(Mathf.Lerp(bombImage3.transform.localPosition.x, bombImage1NewLocation.localPosition.x, Time.unscaledDeltaTime * uIBombLerpMultiplier), bombImage3.transform.localPosition.y);
            bombImage4.transform.localPosition = new Vector2(Mathf.Lerp(bombImage4.transform.localPosition.x, bombImage1NewLocation.localPosition.x, Time.unscaledDeltaTime * uIBombLerpMultiplier), bombImage4.transform.localPosition.y);

        }
    }
    public void SetUIBombs(int _currentBombs)
    {


        if (_currentBombs == 0)
        {
            bombImage1.gameObject.SetActive(false);
            bombImage2.gameObject.SetActive(false);
            bombImage3.gameObject.SetActive(false);
            bombImage4.gameObject.SetActive(false);
            playerBombs_Text.gameObject.SetActive(false);

        }

        else if (_currentBombs == 1)
        {
            bombImage1.gameObject.SetActive(true);
            bombImage2.gameObject.SetActive(false);
            bombImage3.gameObject.SetActive(false);
            bombImage4.gameObject.SetActive(false);
            playerBombs_Text.gameObject.SetActive(false);

            //MoveUIBomb(bombImage1, uIBombSpawnLocation.transform, bombImage1OriginalLocalPosition);

        }
        else if (_currentBombs == 2)
        {
            bombImage1.gameObject.SetActive(true);
            bombImage2.gameObject.SetActive(true);
            bombImage3.gameObject.SetActive(false);
            bombImage4.gameObject.SetActive(false);
            playerBombs_Text.gameObject.SetActive(false);

        }
        else if (_currentBombs == 3)
        {
            bombImage1.gameObject.SetActive(true);
            bombImage2.gameObject.SetActive(true);
            bombImage3.gameObject.SetActive(true);
            bombImage4.gameObject.SetActive(false);
            playerBombs_Text.gameObject.SetActive(false);

        }
        else if (_currentBombs == 4)
        {
            bombImage1.gameObject.SetActive(true);
            bombImage2.gameObject.SetActive(true);
            bombImage3.gameObject.SetActive(true);
            bombImage4.gameObject.SetActive(true);
            playerBombs_Text.gameObject.SetActive(false);

        }
        else if (_currentBombs > 4)
        {
            bombImage1.gameObject.SetActive(true);
            bombImage1.transform.localPosition = bombImage1NewLocation.localPosition;
            bombImage2.gameObject.SetActive(false);
            bombImage3.gameObject.SetActive(false);
            bombImage4.gameObject.SetActive(false);
            playerBombs_Text.gameObject.SetActive(true);
            playerBombs_Text.text = (_currentBombs.ToString("x 0"));


        }
    }

    public void AddUIBomb(int _currentBombs, int addedOrRemoved)
    {
        if (addedOrRemoved > 0) // falta preparar lanzar bomba
        {
            if (_currentBombs == 1)
            {
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(false);
                bombImage3.gameObject.SetActive(false);
                bombImage4.gameObject.SetActive(false);
                playerBombs_Text.gameObject.SetActive(false);

                bombImage1.transform.position = new Vector3(uIBombSpawnLocation.transform.position.x, bombImage1.transform.position.y, bombImage1.transform.position.z);
                uIBomb1HasToMove = false;
                uIBomb2HasToMove = true;
                uIBomb3HasToMove = true;
                uIBomb4HasToMove = true;

                //MoveUIBomb(bombImage1, uIBombSpawnLocation.transform, bombImage1OriginalLocalPosition);

            }
            else if (_currentBombs == 2)
            {
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(true);
                bombImage3.gameObject.SetActive(false);
                bombImage4.gameObject.SetActive(false);
                playerBombs_Text.gameObject.SetActive(false);

                bombImage2.transform.position = new Vector3(uIBombSpawnLocation.transform.position.x, bombImage2.transform.position.y, bombImage2.transform.position.z);
                //MoveUIBomb(bombImage1, uIBombSpawnLocation.transform, bombImage1OriginalLocalPosition);

                uIBomb1HasToMove = false;
                uIBomb2HasToMove = false;
                uIBomb3HasToMove = true;
                uIBomb4HasToMove = true;

            }
            else if (_currentBombs == 3)
            {
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(true);
                bombImage3.gameObject.SetActive(true);
                bombImage4.gameObject.SetActive(false);
                playerBombs_Text.gameObject.SetActive(false);

                bombImage3.transform.position = new Vector3(uIBombSpawnLocation.transform.position.x, bombImage3.transform.position.y, bombImage3.transform.position.z);
                //MoveUIBomb(bombImage1, uIBombSpawnLocation.transform, bombImage1OriginalLocalPosition);

                uIBomb1HasToMove = false;
                uIBomb2HasToMove = false;
                uIBomb3HasToMove = false;
                uIBomb4HasToMove = true;

            }
            else if (_currentBombs == 4)
            {
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(true);
                bombImage3.gameObject.SetActive(true);

                bombImage4.gameObject.SetActive(true);
                playerBombs_Text.gameObject.SetActive(false);

                bombImage4.transform.position = new Vector3(uIBombSpawnLocation.transform.position.x, bombImage4.transform.position.y, bombImage4.transform.position.z);
                //MoveUIBomb(bombImage1, uIBombSpawnLocation.transform, bombImage1OriginalLocalPosition);
                uIBomb1HasToMove = false;
                uIBomb2HasToMove = false;
                uIBomb3HasToMove = false;
                uIBomb4HasToMove = false;

            }
            else if (_currentBombs > 4)
            {
                //bombImage1.gameObject.SetActive(false);
                //bombImage2.gameObject.SetActive(false);
                bombImage1.gameObject.SetActive(true);
                bombImage1.transform.localPosition = bombImage1NewLocation.localPosition;
                //bombImage4.gameObject.SetActive(false);
                playerBombs_Text.gameObject.SetActive(true);
                playerBombs_Text.text = (_currentBombs.ToString("x 0"));


            }
        }
        else
        {
            if (_currentBombs == 0)
            {
                uIBomb1HasToMove = true;
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(false);
                bombImage3.gameObject.SetActive(false);
                bombImage4.gameObject.SetActive(false);
                playerBombs_Text.gameObject.SetActive(false);

            }
            else if (_currentBombs == 1)
            {
                uIBomb2HasToMove = true;
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(true);
                bombImage3.gameObject.SetActive(false);
                bombImage4.gameObject.SetActive(false);
                playerBombs_Text.gameObject.SetActive(false);
            }
            else if (_currentBombs == 2)
            {
                uIBomb3HasToMove = true;
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(true);
                bombImage3.gameObject.SetActive(true);
                bombImage4.gameObject.SetActive(false);
                playerBombs_Text.gameObject.SetActive(false);
            }
            else if (_currentBombs == 3)
            {
                uIBomb4HasToMove = true;
                bombImage1.gameObject.SetActive(true);
                bombImage2.gameObject.SetActive(true);
                bombImage3.gameObject.SetActive(true);
                bombImage4.gameObject.SetActive(true);
                playerBombs_Text.gameObject.SetActive(false);
            }
            else if (_currentBombs == 4)
            {
                bombImage1.gameObject.SetActive(true);
                bombImage1.transform.localPosition = bombImage3OriginalLocalPosition;
                bombImage2.gameObject.SetActive(true);
                bombImage3.gameObject.SetActive(true);
                bombImage4.gameObject.SetActive(true);
                playerBombs_Text.gameObject.SetActive(false);
                
            }
            else if (_currentBombs > 4)
            {
                bombImage1.gameObject.SetActive(true);                
                bombImage2.gameObject.SetActive(false);
                bombImage3.gameObject.SetActive(false);
                bombImage4.gameObject.SetActive(false);
                playerBombs_Text.gameObject.SetActive(true);
                playerBombs_Text.text = (_currentBombs.ToString("x 0"));
            }
        }

    }
    public void MoveUIBomb(GameObject uIBombToMove, Transform spawnLocation, Vector3 goalPosition)
    {

        uIBombToMove.transform.position = spawnLocation.transform.position;


    }
}
