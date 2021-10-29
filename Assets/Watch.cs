using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Watch : MonoBehaviour
{
    [SerializeField]
    private GameObject menu1;
    [SerializeField]
    private GameObject menu2;

    [SerializeField]
    private ScrollViewManager selectReplayToWatch;
    [SerializeField]
    private ScrollViewManager selectReplayToDelete;

    [SerializeField]
    private GameObject selectReplayButtonUI;

    private string[] savedReplays;
    private string[] replaysToDelete;

    private int selectedReplayId;
    
    private void Start()
    {
        menu1.SetActive(true);
        menu2.SetActive(false);
        savedReplays = Directory.GetDirectories(Paths.R6ReplaySavePath);
        selectReplayToWatch.ResetMenu();
        for (int i = 0; i < savedReplays.Length; i++)
        {
            int index = i;
            GameObject selectReplayButton = selectReplayToWatch.AddUI(i.ToString(), selectReplayButtonUI);
            selectReplayButton.GetComponentInChildren<Text>().text = Path.GetFileName(savedReplays[i]);
            selectReplayButton.GetComponent<Button>().onClick.AddListener(new UnityAction(() => OnSelectReplay(index)));
        }
    }

    private void OnSelectReplay(int _index)
    {
        selectedReplayId = _index;
        if (Directory.GetDirectories(savedReplays[selectedReplayId]).Length != 1)
        {
            Public.logManager.AddLog("Replay folder is damaged or some folder is in it.");
            return;
        }
        if (Directory.GetDirectories(Paths.R6ReplayPath).Length >= 12)
        {
            menu1.SetActive(false);
            menu2.SetActive(true);

            Public.logManager.AddLog("Too many replays. Select replay to delete.");
            menu2.SetActive(true);
            replaysToDelete = Directory.GetDirectories(Paths.R6ReplayPath);
            selectReplayToDelete.ResetMenu();
            for (int i = 0; i < replaysToDelete.Length; i++)
            {
                int index = i;
                GameObject selectReplayButton = selectReplayToDelete.AddUI(i.ToString(), selectReplayButtonUI);
                selectReplayButton.GetComponentInChildren<Text>().text = Path.GetFileName(replaysToDelete[i]);
                selectReplayButton.GetComponent<Button>().onClick.AddListener(new UnityAction(() => OnSelectReplayToDelete(index)));
            }
        }
        else
        {
            string replayFullPath = Directory.GetDirectories(savedReplays[selectedReplayId])[0];
            string dir = Paths.R6ReplayPath + "/" + Path.GetFileName(replayFullPath);
            string[] files = Directory.GetFiles(replayFullPath);
            DirectoryInfo info = new DirectoryInfo(dir);
            if (!info.Exists)
            {
                info.Create();
                foreach (string file in files)
                {
                    string name = Path.GetFileName(file);
                    string dest = Path.Combine(dir, name);
                    File.Copy(file, dest);
                }
            }
            Public.logManager.AddLog("Replay copied to R6 match replay folder");
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void OnSelectReplayToDelete(int _index)
    {
        DeleteDirectory(replaysToDelete[_index]);

        string replayFullPath = Directory.GetDirectories(savedReplays[selectedReplayId])[0];
        string dir = Paths.R6ReplayPath + "/" + Path.GetFileName(replayFullPath);
        string[] files = Directory.GetFiles(replayFullPath);
        DirectoryInfo info = new DirectoryInfo(dir);
        if (!info.Exists)
        {
            info.Create();
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(dir, name);
                File.Copy(file, dest);
            }
        }
        Public.logManager.AddLog("Replay copied to R6 match replay folder");
        SceneManager.LoadScene("MainMenu");
    }
    
    public static void DeleteDirectory(string path)
    {
        foreach (string directory in Directory.GetDirectories(path))
        {
            DeleteDirectory(directory);
        }

        try
        {
            Directory.Delete(path, true);
        }
        catch (IOException)
        {
            Directory.Delete(path, true);
        }
        catch (UnauthorizedAccessException)
        {
            Directory.Delete(path, true);
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
