using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Setting : MonoBehaviour
{
    [SerializeField]
    private InputField R6ReplayPath;
    [SerializeField]
    private InputField R6ReplaySavePath;

    private void Start()
    {
        R6ReplayPath.text = Paths.R6ReplayPath;
        R6ReplaySavePath.text = Paths.R6ReplaySavePath;
    }

    public void OnSave()
    {
        if (Public.CheckPathSetting(R6ReplayPath.text, R6ReplaySavePath.text))
        {
            StreamWriter initWriter = new StreamWriter("setting.ini");
            initWriter.WriteLine(R6ReplayPath.text);
            initWriter.WriteLine(R6ReplaySavePath.text);
            initWriter.Close();
        }
    }

    public void OnBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
