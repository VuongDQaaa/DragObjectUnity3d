using UnityEngine;

public class DragUsingRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask _draggableLayer, _QuadLayer;
    [SerializeField] private Vector3 _worldMousePosition, _direction;
    [SerializeField] private GameObject _selectedObject;
    [SerializeField] private bool _isDragging;
    float targetObjectHeight;
    RaycastHit hit;

    private void Start()
    {
        _isDragging = false;
    }

    private void FixedUpdate()
    {
        //Draw ray
        _worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 21f));
        _direction = _worldMousePosition - Camera.main.transform.position;
        Debug.DrawRay(transform.position, _direction, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            //get selected object
            if (Physics.Raycast(transform.position, _direction, out hit, Mathf.Infinity, _draggableLayer))
            {
                Debug.Log(hit.transform.name);
                Debug.DrawRay(transform.position, _direction, Color.red);
                _selectedObject = hit.transform.gameObject;
                targetObjectHeight = _selectedObject.GetComponent<MeshRenderer>().bounds.size.y;
                _isDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_isDragging)
            {
                _isDragging = false;
            }
            _selectedObject = null;
        }

        //Dragging
        if (_isDragging)
        {
            Vector3 pos = mousePos();
            _selectedObject.transform.position = pos;
        }
    }

    //new coordinate
    Vector3 mousePos()
    {
        Vector3 newPos = new Vector3();
        if (Physics.Raycast(transform.position, _direction, out hit, Mathf.Infinity, _QuadLayer))
        {
            Debug.Log("drag");
            newPos = hit.point + new Vector3(0f, targetObjectHeight / 2f, 0f);
        }
        else
        {
            _isDragging = false;
        }
        return newPos;
    }
}
