using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchItem : MonoBehaviour
{
    [SerializeField] private GameObject _itemToLunch;
    [SerializeField] private Camera _camera;

    private bool _asItem = true;


    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private Transform _curveControl;



    void Update()   
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && _asItem)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)){
                _pointB.position = hit.point;

            }

            GameObject item = Instantiate(_itemToLunch, _pointA.position, transform.rotation);

            Item itemScript = item.GetComponent<Item>();

            itemScript.pointA = _pointA;
            itemScript.pointB = _pointB;
            itemScript.curveControl = _curveControl;

            //itemScript._isBeingLucnch = true;

            itemScript.Seek();
        }

       
    }

}
