using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingObject : MonoBehaviour
{
    [SerializeField] private float _force = 5f;
    [SerializeField] private List<GameObject> _toysList = new List<GameObject>();
    [SerializeField] private Transform _hand;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("toy"))
        {
            collision.gameObject.GetComponent<Collider>().enabled = false;
            _toysList.Add(collision.gameObject);
        }
    }

    private void Update()
    {
        if (_toysList.Count > 0)
        {
            GameObject toy = _toysList[0];
            toy.transform.position = _hand.position;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ThrowObject(toy);
            }
        }
    }

    private void ThrowObject(GameObject toy)
    {

        GameObject item = Instantiate(_toysList[0], _hand.position, Quaternion.identity);


        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * _force, ForceMode.Impulse);
        }


        _toysList.RemoveAt(0);
        StartCoroutine(waitForCollider(item));


    }


    private IEnumerator waitForCollider(GameObject item)
    {
        yield return new WaitForSeconds(0.1f);
        item.gameObject.GetComponent<Collider>().enabled = true;

        _toysList.Remove(item);
    }
}