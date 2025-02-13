using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera _tpsCamera;//TPS�J����

    [SerializeField]
    private GameObject _fpsCamObj;//FPS�J����
    private CinemachineCamera _fpsCam;
    private Transform _fpsCamTransform;
    private FpsCameraManager _fpsCamMana;

    [SerializeField]
    private CinemachineCamera _lockOnCam;//LockOn�J����

    public Transform FpsCamTransform => _fpsCamTransform;//�J������Transform���擾���邽�߂̃v���p�e�B

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
        if (Input.GetKeyDown(KeyCode.Mouse1))//�f�o�b�N�p����
        {
            ToggleCamera();
        }
    }

    /// <summary>
    /// �J�����̃v���C�I���e�B��؂�ւ��āA�J������؂�ւ���
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
