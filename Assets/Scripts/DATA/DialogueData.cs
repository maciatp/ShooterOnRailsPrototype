using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Data", menuName = "Dialogue Data")]
public class DialogueData : ScriptableObject
{
    [TextArea(6,6)]
    public List<string> conversationBlock;
    public List<AudioClip> audioBlock;
}
