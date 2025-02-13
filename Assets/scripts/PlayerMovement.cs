using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 2f;
    private Rigidbody _rb;
    private float _horizontal, _vertical;
    private float _rotationSpeed = 10f;

    private CameraSwitch cameraSwitch;

    //マウス関連
    [Header("マウス関連")]
    [SerializeField]
    private float _mouseSensitivity = 100f;
    public float MouseSensitivity => _mouseSensitivity;
    private float _mouseX;
    [SerializeField]
    private float _ySensitivity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // カーソルを中央に固定
        _rb = GetComponent<Rigidbody>();
        cameraSwitch = FindAnyObjectByType<CameraSwitch>();
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        if (cameraSwitch.IsFirstPerson)
        {
            _mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        }

        //移動
        Vector3 _direction = new Vector3(_horizontal, 0, _vertical);
        _direction = Camera.main.transform.TransformDirection(_direction);
        _direction.y = 0;
        _rb.linearVelocity = _direction * _moveSpeed;

        //モデルの回転
        if (!cameraSwitch.IsFirstPerson)
        {
            var moveDirection = new Vector3(_horizontal, 0, _vertical);
            if (moveDirection != Vector3.zero)
            {
                Quaternion _rotate = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, _rotate, Time.deltaTime * _rotationSpeed);
            }
        }
        else
        {
            transform.Rotate(Vector3.up * _mouseX * _ySensitivity);
        }
    }
}
