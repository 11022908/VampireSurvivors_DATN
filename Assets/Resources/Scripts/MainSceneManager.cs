using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] private Button singleModeButton;
    [SerializeField] private Button multiModeButton;

    private GameObject Sound;

    void Start()
    {
        singleModeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX("button_click");
            LoadSingleMode();
        });
        multiModeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX("button_click");
            LoadMultiMode();
        });
    }

    void LoadSingleMode()
    {
        SceneManager.LoadScene("Offline");
    }
    void LoadMultiMode()
    {
        SceneManager.LoadScene("Online");
    }
}
