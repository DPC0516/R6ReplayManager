using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Text.RegularExpressions;

public class Save : MonoBehaviour
{
    [SerializeField]
    private GameObject menu1;
    [SerializeField]
    private GameObject menu2;

    [SerializeField]
    private ScrollViewManager selectReplay;

    [SerializeField]
    private GameObject selectReplayButtonUI;
    [SerializeField]
    private InputField nameUI;

    private string[] replays;

    private int selectedReplayId;

    private void Start()
    {
        menu1.SetActive(true);
        menu2.SetActive(false);
        replays = Directory.GetDirectories(Paths.R6ReplayPath);
        selectReplay.ResetMenu();
        for (int i = 0; i < replays.Length; i++)
        {
            int index = i;
            GameObject selectReplayButton = selectReplay.AddUI(i.ToString(), selectReplayButtonUI);
            selectReplayButton.GetComponentInChildren<Text>().text = Path.GetFileName(replays[i]);
            selectReplayButton.GetComponent<Button>().onClick.AddListener(new UnityAction(() => OnSelectReplayButton(index)));
        }
    }

    private void OnSelectReplayButton(int index)
    {
        selectedReplayId = index;
        string[] savedReplays = Directory.GetDirectories(Paths.R6ReplaySavePath);
        for (int i = 0; i < savedReplays.Length; i++)
        {
            if (Directory.GetDirectories(savedReplays[i]).Length == 1)
            {
                if (Path.GetFileName(Directory.GetDirectories(savedReplays[i])[0]) == Path.GetFileName(replays[selectedReplayId]))
                {
                    Public.logManager.AddLog("Already saved replay with name " + Path.GetFileName(savedReplays[i]));
                    Start();
                    return;
                }
            }
        }
        menu1.SetActive(false);
        menu2.SetActive(true);
    }

    public void OnSave()
    {
        string[] savedReplays = Directory.GetDirectories(Paths.R6ReplaySavePath);
        for (int i = 0; i < savedReplays.Length; i++)
        {
            if(Path.GetFileName(savedReplays[i]) == nameUI.text)
            {
                Public.logManager.AddLog("The title already exists.");
                return;
            }
        }
        if(!Regex.IsMatch(nameUI.text, @"[^a-zA-Z0-9가-힣 ]"))
        {
            string dir = Paths.R6ReplaySavePath + "/" + nameUI.text + "/" + Path.GetFileName(replays[selectedReplayId]);
            DirectoryInfo info = new DirectoryInfo(dir);
            if (!info.Exists)
            {
                info.Create();
            }
            string[] files = Directory.GetFiles(replays[selectedReplayId]);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(dir, name);
                File.Copy(file, dest);
            }
            Public.logManager.AddLog("Replay saved");
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            nameUI.text = "";
            Public.logManager.AddLog("Please exclude special characters.");
        }
    }

    public void OnBack1()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnBack2()
    {
        Start();
    }
}
