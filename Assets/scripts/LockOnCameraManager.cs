using Unity.Cinemachine;
using UnityEngine;
/// <summary>
/// ���b�N�I���J�����̊Ǘ��N���X
/// </summary>
public class LockOnCameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;
    private LockOnTargetManager _lockOnManager;
    private CinemachineOrbitalFollow _orbitalFollow;//���ꂽ�̂���

    private CinemachineCamera _thisCamera;
    private Transform _nearEnemy;
    private Vector3 _initTransform;

    private bool _isLockOn = false;
    public bool IsLockOn => _isLockOn;

    void Start()
    {
        _lockOnManager = FindAnyObjectByType<LockOnTargetManager>();
        _thisCamera = GetComponent<CinemachineCamera>();
        _orbitalFollow = GetComponent<CinemachineOrbitalFollow>();
        _initTransform = new(0, 0, 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            _isLockOn = !_isLockOn;
            _thisCamera.Priority = _isLockOn ? 10 : 0;
        }
        if (_isLockOn)
        {
            OnLockOn();
        }
    }
    /// <summary>
    /// ���b�N�I����Ԃɂ��鏈��
    /// ���b�N�I���Ώۂ����Ȃ��ꍇ�͎��_���Z�b�g�������������������A������
    /// </summary>
    public void OnLockOn()
    {
        _nearEnemy = _lockOnManager.NearEnemySearch();
        if (_nearEnemy == null)
        {
            _orbitalFollow.HorizontalAxis.Value = Vector3.SignedAngle(_initTransform, _playerTransform.forward, Vector3.up);
            _isLockOn = !_isLockOn;
        }
        else
        {
            Vector3 _direction = (_nearEnemy.position - _playerTransform.position).normalized;//Enemy����Player�ւ̕���
            _direction.y = 0;//Y���̉�]�𖳌���\
            _playerTransform.LookAt(_nearEnemy);
            _orbitalFollow.HorizontalAxis.Value = Vector3.SignedAngle(_initTransform, _direction, Vector3.up);
            _thisCamera.transform.LookAt(_nearEnemy);
        }
    }
}
