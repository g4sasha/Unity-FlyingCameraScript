using UnityEngine;

public class FlyingCamera : MonoBehaviour  {
    
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _sensitivity = 300f;
    [SerializeField] private float _upDownSpeed = 7f;
    [SerializeField] private float _slowFactor = 0.3f;
    [SerializeField] private float _fastFactor = 3f;
    [SerializeField] private float _angleMin = -90f;
    [SerializeField] private float _angleMax = 90f;

    private float _rotX;
    private float _rotY;

    private void Start()
    {
        var angles = transform.eulerAngles;
        _rotX = angles.y;
        _rotY = angles.x;

        if (_rotY >= 270.0f)
        {
            _rotY = 360.0f - _rotY;
        }
        else if (_rotY > 0.0f && _rotY <= 90.0f)
        {
            _rotY = -_rotY;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            _rotX += Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
            _rotY += Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;
            _rotY = Mathf.Clamp(_rotY, _angleMin, _angleMax);

            transform.localRotation = Quaternion.AngleAxis(_rotX, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(_rotY, Vector3.left);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.position += transform.forward * (_moveSpeed * _fastFactor * Input.GetAxis("Vertical") * Time.deltaTime);
                transform.position += transform.right * (_moveSpeed * _fastFactor * Input.GetAxis("Horizontal") * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.position += transform.forward * (_moveSpeed * _slowFactor * Input.GetAxis("Vertical") * Time.deltaTime);
                transform.position += transform.right * (_moveSpeed * _slowFactor * Input.GetAxis("Horizontal") * Time.deltaTime);
            }
            else
            {
                transform.position += transform.forward * (_moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
                transform.position += transform.right * (_moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.E))
            {
                transform.position += transform.up * (_upDownSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                transform.position -= transform.up * (_upDownSpeed * Time.deltaTime);
            }
        }
    }
}
