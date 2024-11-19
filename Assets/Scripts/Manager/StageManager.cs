using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    /// <summary>
    /// �÷��̾� ������
    /// </summary>
    [SerializeField]
    GameObject m_playerPrefeb = null;

    /// <summary>
    /// �������� �����ϸ� �������� Ŭ����
    /// </summary>
    [SerializeField]
    bool m_isReachDestination = false;
    /// <summary>
    /// ������
    /// </summary>
    [SerializeField]
    Transform m_destination;
    /// <summary>
    /// ��� ���� ���̸� �������� Ŭ����
    /// </summary>
    [SerializeField]
    bool m_isKillAllEnemy = false;

    /// <summary>
    /// ���� �Ŵ��� �������̽�
    /// </summary>
    private IGameManager m_gameManager = null;

    /// <summary>
    /// �� ����Ʈ
    /// </summary>
    List<GameObject> m_enemyList = new List<GameObject>();

    /// <summary>
    /// �÷��̾� ���� ����Ʈ
    /// </summary>
    Transform m_playerSpawnPosition = null;

    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameManager.Instance;
        
        m_playerPrefeb = Instantiate(m_playerPrefeb);

        m_playerSpawnPosition = GameObject.Find("PlayerSpawnPosition").transform;
        m_playerPrefeb.transform.position = m_playerSpawnPosition.position;

        m_playerSpawnPosition.GetComponent<SpriteRenderer>().enabled = false;

        // Enemy �±׸� ���� ��� ���� ��������
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        m_enemyList = new List<GameObject>(_enemies);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isReachDestination == true && m_destination != null)
        {
            CheckDestination();
        }
    }

    /// <summary>
    /// ���� ������ ��
    /// </summary>
    /// <param name="enemy">�� ����</param>
    public void RemoveEnemy(GameObject enemy)
    {
        if (m_enemyList.Contains(enemy))
        {
            m_enemyList.Remove(enemy);
        }

        if (m_isKillAllEnemy == true)
        {
            if (m_enemyList.Count <= 0)
            {
                m_gameManager.ClearStage();
            }
        }
    }

    /// <summary>
    /// �÷��̾ �������� �����ߴ��� Ȯ��
    /// </summary>
    void CheckDestination()
    {
        if (m_playerPrefeb != null)
        {
            float distance = Vector3.Distance(m_playerPrefeb.transform.position, m_destination.position);
            if (distance < 1.0f)
            {
                m_gameManager.ClearStage();
            }
        }
    }
}
