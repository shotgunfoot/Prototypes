using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameConsole : MonoBehaviour
{
    Animator anim;
    bool open = false;
    public TMP_InputField InputText;
    public TextMeshProUGUI OutputText;
    public ScrollRect scroll; //the content of the scrollview

    public string[] scenes;

    private void Start()
    {
        anim = GetComponent<Animator>();
        scenes = new string[SceneManager.sceneCountInBuildSettings];
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            scenes[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Console"))
        {
            if (open)
            {
                anim.Play("ConsoleClose");
                InputText.DeactivateInputField();
                EventSystem.current.SetSelectedGameObject(null);
                MouseLook mouse = FindObjectOfType<MouseLook>();
                if (mouse != null)
                {
                    mouse.lockCursor = true;
                }
            }
            else
            {
                anim.Play("ConsoleOpen");
                InputText.ActivateInputField();
                MouseLook mouse = FindObjectOfType<MouseLook>();
                if (mouse != null)
                {
                    mouse.lockCursor = false;
                }
            }
            open = !open;
        }
    }

    public void ValidateString(string s)
    {
        s = s.ToLower();
        if (s == "help")
        {
            Help();
        }

        if (s == "list")
        {
            ListScenes();
        }

        if (s == "clear")
        {
            Clear();
        }

        string[] words = s.Split(' ');

        if (words[0].ToLower() == "load")
        {
            if (words.Length > 1)
            {
                Load(words[1]);
            }
        }

        InputText.text = "";

        StartCoroutine(ForceToBottom());
        InputText.ActivateInputField();
    }

    IEnumerator ForceToBottom()
    {
        yield return new WaitForEndOfFrame();
        scroll.normalizedPosition = new Vector2(0, 0);
        Canvas.ForceUpdateCanvases();
    }

    private void Clear()
    {
        OutputText.text = "";
    }

    private void Help()
    {
        OutputText.text += "Helpful commands: list, load, help\n";
    }

    private void ListScenes()
    {
        foreach (string scene in scenes)
        {
            OutputText.text += scene + "\n";
        }

        OutputText.text += "Type, without quotations: load 'scenename'\n";
    }

    private void Load(string scene)
    {
        if (SceneManager.GetSceneByName(scene) != null)
        {
            SceneManager.LoadScene(scene);
        }
        else
        {
            OutputText.text += "The scene '" + scene + "' does not exist\n";
        }
    }
}
