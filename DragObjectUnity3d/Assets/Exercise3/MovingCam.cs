using UnityEngine;
using UnityEngine.UIElements;

public class MovingCam : MonoBehaviour
{
    [SerializeField] private Vector3 _mousePos, _mouseWorldPos;
    [SerializeField] private bool _isDraging;
    [SerializeField] private float _angle;

    public float minLimit, maxLimit;
    // Start is called before the first frame update
    void Start()
    {
        _isDraging = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Get angle beetween mouse and word mouse;
        _mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0f, 10f));
        Vector3 targetDir = _mouseWorldPos - transform.position;
        Vector3 forward = transform.forward;
        _angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);


        if (Input.GetMouseButtonDown(0))
        {
            _mousePos = Input.mousePosition;
            _isDraging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_isDraging == true)
            {
                _isDraging = false;
            }
        }

        //Rotate cam
        if (_isDraging)
        {
            //Rotate follow mouse pos
            Vector3 currentMousePos = Input.mousePosition - _mousePos;
            _mousePos = Input.mousePosition;

            if(_angle < -5.0f)
            {
                transform.Rotate(Vector3.up, - currentMousePos.x * Time.deltaTime * -_angle);
            }
            else if(_angle > 5.0f)
            {
                transform.Rotate(Vector3.up, - currentMousePos.x * Time.deltaTime * _angle );
            }
            else
            {
                transform.Rotate(Vector3.up, -currentMousePos.x * Time.deltaTime);
            }

        }

        LimitRot();
    }

    //Source: https://www.youtube.com/watch?v=dU_6Z3WKdtg
    private void LimitRot()
    {
        Vector3 camEulerAngle = Camera.main.transform.eulerAngles;
        camEulerAngle.y = (camEulerAngle.y > 180) ? camEulerAngle.y - 360 : camEulerAngle.y;
        camEulerAngle.y = Mathf.Clamp(camEulerAngle.y, minLimit, maxLimit);
        transform.rotation = Quaternion.Euler(camEulerAngle);
    }
}
