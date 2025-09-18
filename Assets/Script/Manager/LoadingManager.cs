using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    public static string NEXT_SCENE = "Menu";
    [SerializeField] private GameObject processBar;
    [SerializeField] private TextMeshProUGUI textPercent;
    [SerializeField] float fixedLoadingTime = 3f;

    private void Start()
    {
        StartCoroutine(LoadSceneFixedTime(NEXT_SCENE));
    }
    public IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            processBar.GetComponent<Image>().fillAmount = progress;
            textPercent.text = (progress * 100).ToString("0") + "%";
            yield return null;
        }
    }

    public IEnumerator LoadSceneFixedTime(string sceneName)
    {
        float elapsedTime = 0f;
        while(elapsedTime < fixedLoadingTime)
        {
            float process = Mathf.Clamp01(elapsedTime / fixedLoadingTime);
            processBar.GetComponent<Image>().fillAmount = process;
            textPercent.text = (process * 100).ToString("0") + "%";
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
        AudioManager.Instance.PlayVFX("Click UI");
    }
}
