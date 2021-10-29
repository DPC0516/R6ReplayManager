using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(1280, 720, false);

        //설정 파일이 없을 시 생성
        StreamWriter initWriter = new StreamWriter("setting.ini", true);
        initWriter.Close();

        //설정 파일 열기
        StreamReader initReader = new StreamReader("setting.ini");
        //리플레이 폴더
        string R6ReplayPath = initReader.ReadLine();
        Paths.R6ReplayPath = R6ReplayPath;

        //리플레이 저장 폴더
        string R6ReplaySavePath = initReader.ReadLine();
        Paths.R6ReplaySavePath = R6ReplaySavePath;

        initReader.Close();
    }

    public void OnSave()
    {
        if (Public.CheckPathSetting())
        {
            SceneManager.LoadScene("Save");
        }
    }

    public void OnWatch()
    {
        if (Public.CheckPathSetting()) 
        { 
            SceneManager.LoadScene("Watch");
        }
    }

    public void OnSetting()
    {
        SceneManager.LoadScene("Setting");
    }
}
