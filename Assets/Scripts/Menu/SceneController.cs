using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void ChangeSeceneToLose(string _sceneName)
    {
        StartCoroutine(ChangeSceneDelay(2f, _sceneName));
    }
    public IEnumerator ChangeSceneDelay(float _time,string _sceneName)
    {
        yield return new WaitForSeconds(_time);
        SceneManager.LoadScene(_sceneName);
    }
}
