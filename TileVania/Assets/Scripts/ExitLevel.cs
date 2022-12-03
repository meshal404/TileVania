using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    IEnumerator NextLevel()
    {
        yield return new WaitForSecondsRealtime(3);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(NextLevel());
    }
}
