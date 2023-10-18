using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndRotate : MonoBehaviour
{
    [SerializeField] private LayerMask _rotateLayer;
    [SerializeField] private Vector3 _mousePos, _direction;
    [SerializeField] private GameObject _rotatedGameObject;
    [SerializeField] private bool _isRotating;
    public float _mouseAndScreenAngle;
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
            _mouseAndScreenAngle = Vector3.SignedAngle(_direction, hit.point, Vector3.forward); 
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
            if (_isRotating)
            {
                RotateObject(_rotatedGameObject);
            }
        }


        if (Input.GetMouseButtonUp(0) && _rotatedGameObject != null)
        {
            _isRotating = false;
        }
    }

    void RotateObject(GameObject rotatedObject)
    {
        rotatedObject.transform.Rotate(0f, 0f, 0f, Space.Self);
    }
}
