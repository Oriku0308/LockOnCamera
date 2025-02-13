using Unity.Cinemachine;
using UnityEngine;
/// <summary>
/// ���b�N�I���J�����̊Ǘ��N���X
/// �C�ɓ���Ȃ��A�������s�{�ӂȎ���
/// </summary>
public class LockOnCameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;
    private LockOnManager _lockOnManager;
    private CinemachineCamera _tpsCamera;

    private CinemachineCamera _thisCamera;
    private Transform _nearEnemy;

    bool _isLockOn = false;

    void Start()
    {
        _lockOnManager = FindAnyObjectByType<LockOnManager>();
        _tpsCamera = FindAnyObjectByType<CinemachineCamera>();
        _thisCamera = GetComponent<CinemachineCamera>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse3))
        {
            _isLockOn = !_isLockOn;
            _tpsCamera.Priority = _isLockOn ? 0 : 10;
            _thisCamera.Priority = _isLockOn ? 10 : 0;
        }
        if (_isLockOn)
        {
            OnLockOn();
        }
    }
    /// <summary>
    /// �C�ɓ���Ȃ������Ղ�A��蒼���\��
    /// </summary>
    public void OnLockOn()
    {
        _nearEnemy = _lockOnManager.NearEnemySearch();
        if (_nearEnemy == null)
        {
            return;//�G�l�~�[�����Ȃ��ꍇ�͎��_���Z�b�g�������������������A������
        }
        else
        {
            _playerTransform.LookAt(_nearEnemy);
        }
    }
}
