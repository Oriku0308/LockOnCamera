using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera _tpsCamera;//TPSカメラ

    [SerializeField]
    private GameObject _fpsCamObj;//FPSカメラ
    private CinemachineCamera _fpsCam;
    private Transform _fpsCamTransform;
    private FpsCameraManager _fpsCamMana;

    [SerializeField]
    private CinemachineCamera _lockOnCam;//LockOnカメラ

    public Transform FpsCamTransform => _fpsCamTransform;//カメラのTransformを取得するためのプロパティ

    private bool isFirstPerson = false;
    public bool IsFirstPerson => isFirstPerson;

    private void Start()
    {
        _fpsCam = _fpsCamObj.GetComponent<CinemachineCamera>();
        _fpsCamMana = _fpsCamObj.GetComponent<FpsCameraManager>();
        _fpsCamTransform = _fpsCamObj.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))//デバック用処理
        {
            ToggleCamera();
        }
    }

    /// <summary>
    /// カメラのプライオリティを切り替えて、カメラを切り替える
    /// </summary>
    void ToggleCamera()
    {
        isFirstPerson = !isFirstPerson;

        if (isFirstPerson)
        {
            _fpsCamMana.FpsCamInit();
            _tpsCamera.Priority = 0;
            _fpsCam.Priority = 10;
            _lockOnCam.Priority = 0;
        }
        else
        {
            _tpsCamera.Priority = 10;
            _fpsCam.Priority = 0;
            _lockOnCam.Priority = 0;
        }
    }
}
