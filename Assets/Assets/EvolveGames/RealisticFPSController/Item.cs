using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] private float _speed = 5.0f;
    private float _sampleTime = 0f;


    public bool _isBeingLucnch = false;

    public Transform pointA;
    public Transform pointB;
    public Transform curveControl;



    private void Update()
    {
        //if (_isBeingLucnch) { Seek(); }
    }

    public Vector3 evaluate(float t)
    {
        Vector3 ac = Vector3.Lerp(pointA.position, curveControl.position, t);
        Vector3 cb = Vector3.Lerp(curveControl.position, pointB.position, t);
        return Vector3.Lerp(ac, cb, t);
    }


    public void Seek( )
    {
        _sampleTime += Time.deltaTime * _speed;
        transform.position = evaluate(_sampleTime);
        transform.forward = evaluate(_sampleTime + 0.001f) - transform.position;

        if (_sampleTime >= 1f)
        {
            Destroy(this);
        }

    }
}
