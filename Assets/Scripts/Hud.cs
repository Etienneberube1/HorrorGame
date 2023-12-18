using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Hud : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI m_TextMeshProUGUI;

    [SerializeField] private float _time;



    private void Update()
    {
        if (_time > 0)
        {
            Timer();
        }
        else if (_time < 0)
        {
            _time = 0;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            SceneManager.LoadScene("WonMenu");
        }
    }

    private void Timer()
    {
        _time -= Time.deltaTime;
        int min = Mathf.FloorToInt(_time / 60);
        int sec = Mathf.FloorToInt(_time % 60);
        m_TextMeshProUGUI.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
