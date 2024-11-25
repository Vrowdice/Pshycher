using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    private int m_damage = 0;
    private float m_lifetime = 0.0f;
    private PlayerController m_playerController = null;
    private Camera m_mainCamera;

    private void Update()
    {
        CheckOutOfBounds();
    }

    /// <summary>
    /// �Ѿ��� ���� �ʱ�ȭ
    /// </summary>
    /// <param name="argDamage">�Ѿ��� ������</param>
    /// <param name="argLifeTime">�Ѿ��� ���� ������ �ð�</param>
    public void ResetState(int argDamage, float argLifeTime, PlayerController argManager)
    {
        m_damage = argDamage;
        m_lifetime = argLifeTime;
        m_playerController = argManager;

        m_mainCamera = Camera.main;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController _enemy = other.gameObject.GetComponent<EnemyController>();
        if (_enemy != null && other.gameObject.CompareTag("Enemy"))
        {
            _enemy.TakeDamage(m_damage);

            m_playerController.DestroyBullet(this);
        }
    }

    /// <summary>
    /// ī�޶� ����Ʈ�� ����� ȭ�� ��� ����
    /// </summary>
    void CheckOutOfBounds()
    {
        if (m_mainCamera != null)
        {
            Vector3 viewportPosition = m_mainCamera.WorldToViewportPoint(transform.position);

            // ����Ʈ ��ǥ���� ȭ�� ������ �������� Ȯ��
            if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
            {
                if (m_playerController != null)
                {
                    m_playerController.DestroyBullet(this);
                }
            }
        }
    }
}
