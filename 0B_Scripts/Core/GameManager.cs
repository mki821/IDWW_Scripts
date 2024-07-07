using System.Collections;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public void StopTime(float time) {
        StartCoroutine(StopCoroutine(time));
    }

    private IEnumerator StopCoroutine(float time) {
        Time.timeScale = 0.001f;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1f;
    }
}
