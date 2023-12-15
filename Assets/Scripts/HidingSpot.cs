using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HidingSpot : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _canvas;

    [SerializeField] private GameObject _TextCanvas;
    [SerializeField] private Camera _playerCam;

    [SerializeField] private Slider _slider;
    [SerializeField] private float _airTime;
    [SerializeField] private float _cooldown;

    private float _currentAirTime = 0;

    private bool _isHiding = false;


    private bool _playerInRange = false;

    private GameObject _playerGO;

    private void Start()
    {
        _slider.minValue = 0;

        _currentAirTime = _airTime;

        _slider.maxValue = _airTime;
        _slider.value = _airTime;
    }

    private void LateUpdate()
    {
        _TextCanvas.transform.LookAt(transform.position + _playerCam.transform.rotation * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameController"))
        {
            _playerInRange = true;
            _playerGO = other.gameObject;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GameController"))
        {
            _playerInRange = false;
            _playerGO = null;
        }

    }

    private void Update()
    {
        if (!_playerInRange) return;
        if (!_playerGO) return;


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!_isHiding)
            {
                _isHiding = true;

                _slider.value = _currentAirTime;


                _camera.SetActive(true);
                _canvas.SetActive(true);

                _playerGO.SetActive(false);

                _TextCanvas.SetActive(false);
            }
            else if (_isHiding)
            {
                _isHiding = false;

                _camera.SetActive(false);
                _canvas.SetActive(false);

                _playerGO.SetActive(true);

                _TextCanvas.SetActive(true);

            }
        }

        if (_isHiding) { PlayerWantToHide(); }
        if (!_isHiding) { PlayerWantToLeave(); }
    }




    private void PlayerWantToHide()
    {
        if (_currentAirTime > _slider.minValue)
        {
            _currentAirTime -= Time.deltaTime;
            _slider.value = _currentAirTime;
        }


        if (_slider.value <= _slider.minValue)
        {
            PlayerWantToLeave();
        }
    }


    private void PlayerWantToLeave()
    {
        _isHiding = false;

        _camera.SetActive(false);
        _canvas.SetActive(false);

        _playerGO.SetActive(true);

        _TextCanvas.SetActive(true);

        StartCoroutine(PlayerLeftCoroutine());
    }


    private IEnumerator PlayerLeftCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        if (_currentAirTime <= _slider.maxValue)
        {
            _currentAirTime += Time.deltaTime;
        }
    }
}
