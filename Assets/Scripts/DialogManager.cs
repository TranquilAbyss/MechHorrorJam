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
    [SerializeField] AudioSource audioSource;
    public static System.Action<DialogSequence> OnTextEnd;
    public bool IsPlaying { get; private set; }

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
        IsPlaying = true;
        while (sequenceIndex < sequence.segment.Length)
        {
            blackImage.SetActive(sequence.segment[sequenceIndex].blackScreen);
            name.text= sequence.segment[sequenceIndex].name;
            Dialog.text = sequence.segment[sequenceIndex].dialog;
            float time = sequence.segment[sequenceIndex].time;
           
            if(sequence.segment[sequenceIndex].audioClip != null)
            {
                audioSource.clip = sequence.segment[sequenceIndex].audioClip;
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }

            sequenceIndex++;
            Debug.Log("run" + sequence.name);
            yield return new WaitForSeconds(time);
        }
        container.SetActive(false);
        blackImage.SetActive(false);
        IsPlaying = false;
        OnTextEnd.Invoke(sequence);
    }
}
