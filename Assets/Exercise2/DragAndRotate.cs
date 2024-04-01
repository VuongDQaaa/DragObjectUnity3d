using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;

public class DragAndRotate : MonoBehaviour
{
    [SerializeField] private LayerMask _rotateLayer;
    [SerializeField] private Vector3 _mouseWorldPos, _mousePos;
    [SerializeField] private GameObject _rotatedGameObject;
    [SerializeField] private bool _isRotating;
    [SerializeField] private float angle;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        _isRotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        _mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        Vector3 dir = _mouseWorldPos - transform.position;

        //Draw ray
        Debug.DrawRay(transform.position, dir, Color.red);

        //check Quad surface
        if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity, _rotateLayer))
        {
            Debug.DrawRay(transform.position, dir, Color.green);
            _rotatedGameObject = hit.transform.gameObject;
            //get angle
            angle = Vector3.SignedAngle(dir, hit.point, Vector3.up);
        }
        else
        {
            _rotatedGameObject = null;
            _isRotating = false;
        }

        //check input button
        if (Input.GetMouseButtonDown(0) && _rotatedGameObject != null)
        {
            _mousePos = Input.mousePosition;
            _isRotating = true;
        }

        if (Input.GetMouseButtonUp(0) && _rotatedGameObject != null)
        {
            if (_isRotating == true)
            {
                _isRotating = false;
            }
        }

        if (_isRotating)
        {
            Vector3 currentMousePos = Input.mousePosition - _mousePos;
            _mousePos = Input.mousePosition;

            //first corner
            if (angle < -5.0f && hit.point.y > 0)
            {
                _rotatedGameObject.transform.Rotate(Vector3.forward, -currentMousePos.x * -angle * Time.deltaTime);
            }
            //second corner
            else if (angle < -5.0f && hit.point.y < 0)
            {
                _rotatedGameObject.transform.Rotate(Vector3.forward, -currentMousePos.x * angle * Time.deltaTime);
            }
            //third corner
            else if (angle > 5.0f && hit.point.y < 0)
            {
                _rotatedGameObject.transform.Rotate(Vector3.forward, -currentMousePos.x * -angle * Time.deltaTime);
            }
            //fourd corner
            else if (angle > 5.0f && hit.point.y > 0)
            {
                _rotatedGameObject.transform.Rotate(Vector3.forward, -currentMousePos.x * angle * Time.deltaTime);
            }
            else
            {
                _rotatedGameObject.transform.Rotate(Vector3.forward, -currentMousePos.x * Time.deltaTime);
            }
        }
    }
}
