using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class Chat_Trigger_Script : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] DialogueData dialogue;
    //SUBIR RANGO PARA PODER ELEGIR!!!!
    [Range(0, 20)] 
    // [Range(0,dialogue.conversationBlock.Count - 1)]
    [SerializeField] int intConversationBlock;
    
    [Header("PREVIEW ONLY - DO NOT MODIFY HERE")]
    [TextArea(6, 6)]
    [SerializeField] string dialogueString = null;
    [SerializeField] AudioClip audioDialogue;
    UIChat_Script uIChat_Script_;
    TMP_Animated animatedText;
    bool isActive = false;

    string animatorTrigger = string.Empty;





    private void Start()    
    {
        uIChat_Script_ = GameObject.Find("UI").transform.GetChild(0).GetChild(0).Find("UIChat").GetComponent<UIChat_Script>();        
        
        audioDialogue = dialogue.audioBlock[intConversationBlock];


        //Preparo para que envíe el trigger para el animator según el nombre
        animatorTrigger = string.Empty;
        if (dialogue.name.Contains("Fox"))
        {
            //Debug.Log("CAMBIO A FOX");
            animatorTrigger = "Fox";
            //Debug.Log(animatorTrigger);
        }
        else if (dialogue.name.Contains("Falco"))
        {
            //Debug.Log("CAMBIO A FALCO");
            animatorTrigger = "Falco";

        }
        else if (dialogue.name.Contains("Slippy"))
        {
            //Debug.Log("CAMBIO A FALCO");
            animatorTrigger = "Slippy";

        }
        else if (dialogue.name.Contains("Peppy"))
        {
            //Debug.Log("CAMBIO A FALCO");
            animatorTrigger = "Peppy";

        }
    }

    // Update is called once per frame
    void Update()
    {
        //PARA QUE SE LLAME EN EL EDITOR (Y HE PUESTO EXECUTEinEditMode sobre la declaración de la clase)
        if ((Application.platform == RuntimePlatform.WindowsEditor) && (Application.isPlaying == false))
        {

            dialogueString = dialogue.conversationBlock[intConversationBlock];
            audioDialogue = dialogue.audioBlock[intConversationBlock];

            if (dialogue.name.Contains("Fox"))
            {
                //Debug.Log("CAMBIO A FOX");
                animatorTrigger = "Fox";
                //Debug.Log(animatorTrigger);
            }
            else if (dialogue.name.Contains("Falco"))
            {
                //Debug.Log("CAMBIO A FALCO");
                animatorTrigger = "Falco";

            }
            else if (dialogue.name.Contains("Slippy"))
            {
                //Debug.Log("CAMBIO A FALCO");
                animatorTrigger = "Slippy";

            }
            else if (dialogue.name.Contains("Peppy"))
            {
                //Debug.Log("CAMBIO A FALCO");
                animatorTrigger = "Peppy";

            }
        }


        if ((isActive == true) && (uIChat_Script_.gameObject.activeSelf == false))
        {
            ActivateUIChat();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ActivateUIChat();
            
        }
    }

    private void ActivateUIChat()
    {
        isActive = true;
       
        uIChat_Script_.ClearText();
        //uIChat_Script_.chatImageAnimator.ResetTrigger("CloseChatBox");
        uIChat_Script_.EnableChat();
        //uIChat_Script_.gameObject.GetComponent<Animator>().Play("UIChatBox_Opening");
        animatedText = uIChat_Script_.AnimatedChatText;
        uIChat_Script_.gameObject.GetComponent<AudioSource>().clip = (dialogue.audioBlock[intConversationBlock]);

        //SE LO PASO AL BEHAVIOUR DE LA ANIMACIÓN DE UIchatImage para que se empiece a reproducir DESPUÉS de que termine la animación de transmisión.
        uIChat_Script_.ChatImageAnimator.GetBehaviour<ChatImageBehaviour>().dialogue = dialogue;
        uIChat_Script_.ChatImageAnimator.GetBehaviour<ChatImageBehaviour>().conversationBlockint = intConversationBlock;
        uIChat_Script_.ChatImageAnimator.Play("UIChatImage_Opening_Anim");
       
        //EStÁ COMENTADO PORQUE SE ACTIVA DESDE EL BEHAVIOUR DE LA ANIMACIÓN UNA VEZ TERMINA
        //uIChat_Script_.ReadDialogue(dialogue.conversationBlock[intConversationBlock]);
        
        
        //animatedText.ReadText(dialogue.conversationBlock[conversationBlock]);

        //LE PASO EL TRIGGER PARA QUE SALGA LA CARA (DEL PJ CON EL NOMBRE DEL TRIGGER)
        uIChat_Script_.ChatImageAnimator.SetTrigger(animatorTrigger);
        this.gameObject.SetActive(false);
    }
}
