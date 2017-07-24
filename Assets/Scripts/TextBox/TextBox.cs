using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour {

    GameObject textBoxGameObject;
    Text textBoxText;

    [TextArea]
    public string currentPage;
    public int Pos;    

    void Awake ()
    {
        textBoxGameObject = this.gameObject;
        textBoxText = textBoxGameObject.transform.Find("Text").gameObject.GetComponent<Text>();
    }

    public IEnumerator Tick ()
    {
        TextBoxManager.instance.isRunning = true;
        while (TextBoxManager.instance.pages.Count > 0 && TextBoxManager.instance.isRunning)
        {
            if (string.IsNullOrEmpty(currentPage))
            {
                Pos = 0;
                currentPage = TextBoxManager.instance.pages.Peek();
            }

            if (Pos < currentPage.Length)
            {
                Pos++;
                textBoxText.text = currentPage.Substring(0, Pos);
            }
            else if (Input.GetButton("Fire1"))
            {
                TextBoxManager.instance.pages.Dequeue();
                currentPage = null;
            }

            float delay;
            if (currentPage != null && Pos < currentPage.Length)
            {
                delay = char.IsPunctuation(currentPage[Pos - 1]) ? TextBoxManager.instance.puncuationDelay : TextBoxManager.instance.charDelay;
            }
            else
            {
                delay = 0f;
            }

            yield return new WaitForSeconds(delay);

        }

        TextBoxManager.instance.Hide();
        TextBoxManager.instance.isRunning = false;
    }
}
