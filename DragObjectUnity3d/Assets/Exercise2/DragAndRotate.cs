using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class DragAndRotate : MonoBehaviour
{
    [SerializeField] private LayerMask _rotateLayer;
    [SerializeField] private Vector3 _mousePos, _direction;
    [SerializeField] private GameObject _rotatedGameObject;
    [SerializeField] private bool _isRotating;
    public float angle;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        _isRotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Draw ray
        _mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        _direction = _mousePos - Camera.main.transform.position;
        Debug.DrawRay(transform.position, _direction, Color.red);

        //check Quad surface
        if (Physics.Raycast(transform.position, _direction, out hit, Mathf.Infinity, _rotateLayer))
        {
            Debug.DrawRay(transform.position, _direction, Color.green);
            _rotatedGameObject = hit.transform.gameObject;
            GetAngle();
        }
        else
        {
            _rotatedGameObject = null;
            _isRotating = false;
        }

        //check input button
        if (Input.GetMouseButtonDown(0) && _rotatedGameObject != null)
        {
            _isRotating = true;
        }

        if (Input.GetMouseButtonUp(0) && _rotatedGameObject != null)
        {
            _isRotating = false;
        }

        if (_isRotating)
        {
            if (angle < -5.0f)
            {
                _rotatedGameObject.transform.Rotate(0f, 0f, -1f, Space.Self);
            }
            else if (angle > 5.0f)
            {
                _rotatedGameObject.transform.Rotate(0f, 0f, 1f, Space.Self);
            }
            else
            {
                _rotatedGameObject.transform.Rotate(0f, 0f, 0f, Space.Self);
            }
        }
    }

    void GetAngle()
    {
        Vector3 TargetDir = transform.position - hit.point;
        Vector3 forward = transform.forward;
        angle = Vector3.SignedAngle(TargetDir, forward, Vector3.up);
    }
}
