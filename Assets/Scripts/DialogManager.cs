using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text Dialog;
    [SerializeField] GameObject container;
    ProgressionManager progressionManager;
    [SerializeField] GameObject blackImage;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        container.SetActive(false);

    }
    public void PlayDialog(DialogSequence dialogSequence)
    {
        StartCoroutine(StartDialogSequence(dialogSequence));
        Debug.Log("Plaay" + dialogSequence.name);
    }

    IEnumerator StartDialogSequence(DialogSequence sequence)
    {
        container.SetActive(true);
        int sequenceIndex = 0;
      
        while (sequenceIndex < sequence.segment.Length)
        {
            blackImage.SetActive(sequence.segment[sequenceIndex].blackScreen);
            name.text= sequence.segment[sequenceIndex].name;
            Dialog.text = sequence.segment[sequenceIndex].dialog;
            float time = sequence.segment[sequenceIndex].time;
            sequenceIndex++;
            Debug.Log("run" + sequence.name);
            yield return new WaitForSeconds(time);
        }
        container.SetActive(false);
        blackImage.SetActive(false);
    }
}
