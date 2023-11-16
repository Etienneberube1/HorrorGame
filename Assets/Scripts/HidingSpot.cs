using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HidingSpot : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _airTime;
    [SerializeField] private float _cooldown;

    private bool _isHiding = false;
    private bool _playerInRange = false;

    private GameObject _playerGO;

    private void Start()
    {
        _slider.minValue = 0;
        _slider.maxValue = _airTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GameController")) { 
            _playerInRange = true;
            _playerGO = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("GameController"))
        {
            _playerInRange = false;
            _playerGO = null;
        }
    }


    private void Update()
    {
        if (!_playerInRange) return;


        if (!_isHiding) PlayerNotHiding();


        if (_isHiding) PlayerIsHiding();
    }




    private void PlayerNotHiding() {
        // if the player is not already hiding 

        if (Input.GetKeyDown(KeyCode.E))
        {

            Debug.Log("player want to hide");

            if (_playerGO)
            {

                _isHiding = true;

                // making the player transparent
                _playerGO.SetActive(false);

                // activating the hiding spot visual 
                _canvas.SetActive(true);
                _camera.SetActive(true);
            }
        }

    }


    private void PlayerIsHiding()
    {
        // if the player is already hiding 

        if (Input.GetKeyDown(KeyCode.E))
        {

            Debug.Log("player want to leave");

            if (_playerGO)
            {

                _isHiding = false;

                // making the player transparent
                _playerGO.SetActive(true);

                // activating the hiding spot visual 
                _canvas.SetActive(false);
                _camera.SetActive(false);
            }
        }

    }

}
