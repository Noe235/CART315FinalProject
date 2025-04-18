using TMPro;
using UnityEngine;
using System.Collections;

public class GameMessageUI : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private float defaultDuration = 2f;
    
    public static GameMessageUI Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);      // keep HUD between scene loads
        messageText.text = "";
    }

    public void Show(string msg, float? duration = null)
    {
        StopAllCoroutines();                // replace any existing message
        StartCoroutine(ShowRoutine(msg, duration ?? defaultDuration));
    }

    private IEnumerator ShowRoutine(string msg, float duration)
    {
        messageText.text = msg;
        yield return new WaitForSeconds(duration);
        messageText.text = "";
    }
}
