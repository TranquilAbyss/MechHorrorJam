using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "ScriptableObjects/DialogSequence", order = 1)]
public class DialogSequence : ScriptableObject
{
    public DialogSegment[] segment;

}

[System.Serializable]
public class DialogSegment
{
    public string name;
    public string dialog;
    public float time = 2;
    public bool blackScreen;
}
