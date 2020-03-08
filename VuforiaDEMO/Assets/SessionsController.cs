using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionsController : MonoBehaviour
{

    public Button Button;
    public Transform Panel;
    public TestLoader Loader;

    private List<int> Sessions;

    // Start is called before the first frame update
    void Start()
    {
        Sessions = Database.GetSessions();
        Loader = new TestLoader();

        for (int i = 0; i < Sessions.Count; i++)
        {
            var button = Instantiate(Button, Panel);
           
            var text = button.GetComponentInChildren<Text>();
            text.text = Sessions[i] + "";
            int index = i;

            button.onClick.AddListener(() => Choose(Sessions[index]));
        }
    }

    private void Choose(int id)
    {
        PlayerPrefs.SetInt("session", id);
        SceneManager.LoadScene(2);
    }
}
