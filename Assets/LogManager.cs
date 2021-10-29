using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour
{
    [SerializeField]
    private GameObject logPrefab;

    [HideInInspector]
    public Dictionary<int, RectTransform> logTransforms = new Dictionary<int, RectTransform>();
    [HideInInspector]
    public List<int> ids = new List<int>();

    [SerializeField]
    private ScrollRect ScrollView;

    private const float space = 0f;

    int count = 0;

    void Start()
    {
        if(Public.logManager == null)
        {
            DontDestroyOnLoad(gameObject);
            Public.logManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddLog(string _msg)
    {        
        RectTransform newLog = Instantiate(logPrefab, ScrollView.content).GetComponent<RectTransform>();
        logTransforms.Add(count, newLog);
        ids.Add(count);
        newLog.GetComponent<Log>().logManager = this;
        newLog.GetComponent<Text>().text = _msg;
        newLog.GetComponent<Log>().id = count;
        newLog.GetComponent<Log>().destroyTime = 2f;
        newLog.GetComponent<Log>().startCounter();
        count++;

        InitLogTransform();
    }

    public void AddLog(string _msg, float _delayTime)
    {
        RectTransform newLog = Instantiate(logPrefab, ScrollView.content).GetComponent<RectTransform>();
        logTransforms.Add(count, newLog);
        ids.Add(count);
        newLog.GetComponent<Log>().logManager = this;
        newLog.GetComponent<Text>().text = _msg;
        newLog.GetComponent<Log>().id = count;
        newLog.GetComponent<Log>().destroyTime = _delayTime;
        newLog.GetComponent<Log>().startCounter();
        count++;

        InitLogTransform();
    }

    public void RemoveLog(int _id)
    {
        logTransforms.Remove(_id);
        ids.Remove(_id);

        InitLogTransform();
    }

    private void InitLogTransform()
    {
        float y = 0f;
        for (int i = 0; i < logTransforms.Count; i++)
        {
            logTransforms[ids[i]].anchoredPosition = new Vector2(0f, y);
            y += logTransforms[ids[i]].sizeDelta.y + space;
        }
    }
}
