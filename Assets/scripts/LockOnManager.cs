using System.Collections.Generic;
using UnityEngine;

public class LockOnManager : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;//�v���C���[��Transform
    private List<Transform> _lockOnEnemyList = new List<Transform>();//���b�N�I���Ώۂ�List

    /// <summary>
    /// Player�Ɉ�ԋ߂��G�l�~�[��Ԃ����\�b�h
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
            nearestEnemy = _lockOnEnemyList[0];//��ԋ߂��G�l�~�[��ۑ�����ϐ��A�����ł͉��̃G�l�~�[����
            float minDistance = Vector3.Distance(_playerTransform.position, nearestEnemy.position);

            foreach (Transform enemy in _lockOnEnemyList)
            {
                float distance = Vector3.Distance(_playerTransform.position, enemy.position);

                if (distance < minDistance)
                {
                    minDistance = distance;//��ԋ߂��G�l�~�[�̋������X�V
                    nearestEnemy = enemy;//��ԋ߂��G�l�~�[���X�V
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
    #region �G�l�~�[���X�g�ɒǉ��A�폜
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
