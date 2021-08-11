using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public const int lettersPerSecond = 3;
    public const float inverseLettersPerSecond = 1f / lettersPerSecond;

    public GameObject dialogGameObject;
    public Text dialogText;

    public void ShowDialog(in string dialog)
    {
        StartCoroutine(TypeDialog(dialog));
    }

    public IEnumerator TypeDialog(string dialog)
    {
        string displayedDialog = "";

        foreach(char c in dialog)
        {
            displayedDialog += c;
            dialogText.text = displayedDialog;
            yield return new WaitForSeconds(inverseLettersPerSecond);
        }
    }
}
