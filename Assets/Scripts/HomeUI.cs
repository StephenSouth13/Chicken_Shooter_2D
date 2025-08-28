using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HomeUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 3f;
    [SerializeField] private AudioClip homeMusic;


    public void StartGame()
    {
        StartCoroutine(FadeOutAndLoad());
        AudioManager.Instance.PlayMusic(homeMusic);

    }

    private IEnumerator FadeOutAndLoad()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = 1 - (timer / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0;
        SceneManager.LoadSceneAsync("Scene_1");
    }
}