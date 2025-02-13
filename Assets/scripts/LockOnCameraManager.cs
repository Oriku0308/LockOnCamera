using Unity.Cinemachine;
using UnityEngine;
/// <summary>
/// ロックオンカメラの管理クラス
/// 気に入らない、すごく不本意な実装
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
    /// 気に入らない実装祭り、作り直す予定
    /// </summary>
    public void OnLockOn()
    {
        _nearEnemy = _lockOnManager.NearEnemySearch();
        if (_nearEnemy == null)
        {
            return;//エネミーがいない場合は視点リセットを実装したかったが、未実装
        }
        else
        {
            _playerTransform.LookAt(_nearEnemy);
        }
    }
}
