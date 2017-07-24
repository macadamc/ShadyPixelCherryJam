using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TextBoxManager : MonoBehaviour {

    public static TextBoxManager instance;
    public TextBox textBox;
    public Queue<string> pages;

    public float charDelay;
    public float puncuationDelay;

    //Test if the Tick coroutine for the TextBox is running
    public bool isRunning;

    // Use this to add a page to the queue to be displayed.
    public void Display(string text)
    {
        pages.Enqueue(text);
    }
    public void Display(string[] pages)
    {
        for(int i = 0;i< pages.Length;i++)
        {
            this.pages.Enqueue(pages[i]);
        }
    }

    private void Show()
    {
        SceneManager.LoadScene("TextBox", LoadSceneMode.Additive);
    }
    // force close the textbox.
    public void Hide()
    {
        if (isRunning)
        {
            pages.Clear();
            isRunning = false;//  should kill the coroutinne.
            textBox = null;
            SceneManager.UnloadSceneAsync("TextBox");
        }
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            pages = new Queue<string>();

            SceneManager.sceneLoaded += sceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    private void sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "TextBox")
        {
            textBox = GameObject.Find("TextBox").GetComponent<TextBox>();
            StartCoroutine(textBox.Tick());
        }           
    }

    private void Update ()
    {
        if (pages.Count > 0 && textBox == null)
        {
            Show();
        }
    }
}
