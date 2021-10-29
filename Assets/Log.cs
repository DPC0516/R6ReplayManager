using System.Collections;
using UnityEngine;

public class Log : MonoBehaviour
{
    public LogManager logManager;
    public int id;
    public float destroyTime;

    public void startCounter()
    {
        StartCoroutine(Destroy(destroyTime));
    }
    
    private IEnumerator Destroy(float _delaytime)
    {
        yield return new WaitForSeconds(_delaytime);
        logManager.RemoveLog(id);
        Destroy(gameObject);
    }
}