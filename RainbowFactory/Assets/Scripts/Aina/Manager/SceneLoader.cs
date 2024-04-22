using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int sceneToLoad;
    [SerializeField] private Animator loadingAnim;

    private void Start()
    {
        StartCoroutine(LoadALevelASync());
    }

    public void ChangeSceneBtn()
    {
        SceneManager.LoadScene(GameManager.instance.sceneToLoad);
    }

    IEnumerator LoadALevelASync()
    {
        yield return new WaitForSeconds(7.5f);
        loadingAnim.SetTrigger("Loaded");
    }
}
