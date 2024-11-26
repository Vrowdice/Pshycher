using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("First Setting")]
    /// <summary>
    /// �÷��̾� ������
    /// </summary>
    [SerializeField]
    GameObject m_playerPrefeb = null;
    /// <summary>
    /// ��ų �г� ������
    /// </summary>
    [SerializeField]
    GameObject m_abilityPanelPrefeb = null;

    [Header("Stage Setting")]
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
    /// �÷��̾� ��Ʈ�ѷ�
    /// </summary>
    private PlayerController m_playerController = null;
    /// <summary>
    /// �� ����Ʈ
    /// </summary>
    private List<GameObject> m_enemyList = new List<GameObject>();
    /// <summary>
    /// �÷��̾� ���� ����Ʈ
    /// </summary>
    private Transform m_playerSpawnPosition = null;

    // Start is called before the first frame update
    void Start()
    {
        //�������̽� ����
        m_gameManager = GameManager.Instance;

        //�÷��̾� ���� ����Ʈ ã�Ƽ� �����
        m_playerSpawnPosition = GameObject.Find("PlayerSpawnPosition").transform;
        m_playerSpawnPosition.GetComponent<SpriteRenderer>().enabled = false;

        //�÷��̾� ���� ����Ʈ�� �÷��̾� ����
        m_playerPrefeb = Instantiate(m_playerPrefeb);
        m_playerPrefeb.transform.position = m_playerSpawnPosition.position;
        m_playerController = m_playerPrefeb.GetComponent<PlayerController>();

        //�ɷ� �г� ����
        m_abilityPanelPrefeb = Instantiate(m_abilityPanelPrefeb, m_gameManager.CanvasTrans);
        m_abilityPanelPrefeb.SetActive(false);

        // Enemy �±׸� ���� ��� ���� ��������
        GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        m_enemyList = new List<GameObject>(_enemies);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) == true)
        {
            if (m_playerController.IsCanControll == true)
            {
                m_playerController.IsCanControll = false;
                m_abilityPanelPrefeb.SetActive(true);
            }
            else
            {
                m_playerController.IsCanControll = true;
                m_abilityPanelPrefeb.SetActive(false);
            }
        }

        if (m_isReachDestination == true && m_destination != null)
        {
            CheckDestination();
        }
    }

    /// <summary>
    /// ���� ������ ��
    /// ���� ������ ȣ����
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
