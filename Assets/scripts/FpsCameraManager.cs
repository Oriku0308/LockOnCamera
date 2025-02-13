using Unity.Cinemachine;
using UnityEngine;

public class FpsCameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform _tpsCamTransform;
    [SerializeField]
    private Transform _switchTarget;

    private CinemachineCamera _fpsCamera;
    private CameraSwitch _cameraSwitch;
    private PlayerMovement _playerMovement;

    private float _mouseY;
    private float _rotationX = 0f;
    private Vector3 _direction;

    private void Start()
    {
        _playerMovement = FindAnyObjectByType<PlayerMovement>();
        _fpsCamera = GetComponent<CinemachineCamera>();
        _cameraSwitch = FindAnyObjectByType<CameraSwitch>();
    }

    private void Update()
    {
        if (_cameraSwitch.IsFirstPerson)
        {
            _mouseY = Input.GetAxis("Mouse Y") * _playerMovement.MouseSensitivity * Time.deltaTime;

            //カメラのX回転
            Vector3 _rotate = _playerMovement.transform.rotation.eulerAngles;
            _rotationX -= _mouseY;
            _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

            _fpsCamera.transform.rotation = Quaternion.Euler(_rotationX, _rotate.y, 0f);
        }
    }
    /// <summary>
    /// FPSカメラに切り替えたタイミングでプレイヤーの向きを変更する
    /// </summary>
    public void FpsCamInit()
    {
        _direction = (_switchTarget.position - _tpsCamTransform.position).normalized;
        _direction.y = 0;//Y軸の回転はさせない
        _playerMovement.transform.rotation = Quaternion.LookRotation(_direction, Vector3.up);
    }
}
 