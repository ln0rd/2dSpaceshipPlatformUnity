using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "New Dialogue", menuName = "_Dialogue")]
public class Dialogue : ScriptableObject
{
    public Sprite NPCAvatar;
    public string NPCName;
    public string[] Dialogues;
    public bool[] NPCLine;


}
