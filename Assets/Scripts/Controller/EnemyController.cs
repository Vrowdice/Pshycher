using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField]
    private int m_health = 3;

    [Header("Damage Settings")]
    [SerializeField]
    private GameObject m_hitEffectPrefab;

    // �������� �Ŵ����� �����ϴ� ����
    private StageManager m_stageManager;

    void Start()
    {
        m_stageManager = FindObjectOfType<StageManager>();
    }

    // ���� �ı��� �� ȣ��Ǵ� �޼���
    void OnDestroy()
    {
        if (m_stageManager != null)
        {
            m_stageManager.RemoveEnemy(gameObject);
        }
    }

    /// <summary>
    /// �Ѿ˿� ���� �� ȣ��˴ϴ�.
    /// </summary>
    /// <param name="damage">�޴� ���ط�</param>
    public void TakeDamage(int argDamage)
    {
        m_health -= argDamage;

        // �ǰ� ȿ�� ���� (�ɼ�)
        if (m_hitEffectPrefab != null)
        {
            Instantiate(m_hitEffectPrefab, transform.position, Quaternion.identity);
        }

        // ü���� 0 ���϶�� �� ����
        if (m_health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// �� ���� ����
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }
}
