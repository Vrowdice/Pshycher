using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    private int m_damage = 0;
    private float m_lifetime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, m_lifetime);
    }

    /// <summary>
    /// �Ѿ��� ���� �ʱ�ȭ
    /// </summary>
    /// <param name="argDamage">�Ѿ��� ������</param>
    /// <param name="argLifeTime">�Ѿ��� ���� ������ �ð�</param>
    public void ResetState(int argDamage, float argLifeTime)
    {
        m_damage = argDamage;
        m_lifetime = argLifeTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController _enemy = other.gameObject.GetComponent<EnemyController>();
        if (_enemy != null && other.gameObject.CompareTag("Enemy"))
        {
            _enemy.TakeDamage(m_damage);

            Destroy(gameObject);
        }
    }
}
