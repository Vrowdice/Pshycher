using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField]
    private int m_health = 3;

    [Header("Damage Settings")]
    [SerializeField]
    private GameObject m_hitEffectPrefab;
    [SerializeField]
    private float m_flashDuration = 0.2f;

    [Header("View Settings")]
    [SerializeField]
    private GameObject m_viewSprite = null;

    // �������� �Ŵ����� �����ϴ� ����
    private StageManager m_stageManager;
    private SpriteRenderer m_viewSpriteRenderer;
    private Color m_originalColor;

    void Start()
    {
        m_stageManager = FindObjectOfType<StageManager>();
        m_viewSpriteRenderer = m_viewSprite.GetComponent<SpriteRenderer>();
        m_originalColor = m_viewSpriteRenderer.color;
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

        // ���� �������� �޾��� �� ������ ���ϵ��� Coroutine ȣ��
        StartCoroutine(FlashRed());

        // ü���� 0 ���϶�� �� ����
        if (m_health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// ���� ������ ���ߴٰ� ���� ������ ���ƿ��� �Լ�
    /// </summary>
    private IEnumerator FlashRed()
    {
        // ���������� ����
        m_viewSpriteRenderer.color = Color.red;

        // ������ �ð� ���� ������ ����
        yield return new WaitForSeconds(m_flashDuration);

        // ���� ������ ����
        m_viewSpriteRenderer.color = m_originalColor;
    }

    /// <summary>
    /// �� ���� ����
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }
}
