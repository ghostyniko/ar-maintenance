using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLoader : MonoBehaviour
{
    // Start is called before the first frame update
   public void Load1()
    {
        SceneManager.LoadScene(1);
    }

    public void Load2()
    {
        var sId = Database.AddNewSession();
        SessionData.sessionID = sId;
        SceneManager.LoadScene(2);
    }

    public void Load3()
    {
        SceneManager.LoadScene(3);
    }
}
