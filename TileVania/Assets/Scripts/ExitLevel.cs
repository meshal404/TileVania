using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    public bool hasEnd;

    IEnumerator NextLevel()
    {
        playerAnimator.SetTrigger("hasEnd");
        hasEnd = true;

        yield return new WaitForSecondsRealtime(3);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;

        SceneManager.LoadScene(nextSceneIndex);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(NextLevel());
    }
}
