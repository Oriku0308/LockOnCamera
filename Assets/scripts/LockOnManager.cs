using System.Collections.Generic;
using UnityEngine;

public class LockOnManager : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;//プレイヤーのTransform
    private List<Transform> _lockOnEnemyList = new List<Transform>();//ロックオン対象のList

    /// <summary>
    /// Playerに一番近いエネミーを返すメソッド
    /// </summary>
    public Transform NearEnemySearch()
    {
        Transform nearestEnemy = null;

        if (_lockOnEnemyList.Count == 0)
        {
            return nearestEnemy;
        }
        else
        {
            nearestEnemy = _lockOnEnemyList[0];//一番近いエネミーを保存する変数、ここでは仮のエネミーを代入
            float minDistance = Vector3.Distance(_playerTransform.position, nearestEnemy.position);

            foreach (Transform enemy in _lockOnEnemyList)
            {
                float distance = Vector3.Distance(_playerTransform.position, enemy.position);

                if (distance < minDistance)
                {
                    minDistance = distance;//一番近いエネミーの距離を更新
                    nearestEnemy = enemy;//一番近いエネミーを更新
                }
            }
            return nearestEnemy;
        }
    }

    public void LockOn(Transform LockOnTarget)
    {
        if (LockOnTarget == null)
        {
        }
        else
        {
            _playerTransform.LookAt(LockOnTarget);
        }
    }
    #region エネミーリストに追加、削除
    /// <param name="collision"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _lockOnEnemyList.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _lockOnEnemyList.Remove(other.transform);
        }
    }
    #endregion
}
