using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �� ������ ����, ���� ���� ������ �̵� ���� ����մϴ�
/// �ܵ����� ������ �����ؾ��մϴ�
/// ���� ������ ����Ʈ�� �ε����� ��ũ���ͺ� ������Ʈ ���� ���� ������ �ε����� ��ġ�ؾ��մϴ�
/// 
/// �ΰ� ���
/// 
/// ��� �г� ���� ���
/// ���� ���� ���
/// </summary>
public class GameManager : MonoBehaviour, IGameManager
{
    /// <summary>
    /// �ڱ� �ڽ�
    /// </summary>
    static GameManager g_gameManager = null;

    /// <summary>
    /// ��� ������Ʈ ������
    /// </summary>
    [SerializeField]
    GameObject m_alertObjPrefeb = null;
    /// <summary>
    /// �ɼ� �Ŵ���
    /// </summary>
    [SerializeField]
    OptionManager m_optionManager = null;

    /// <summary>
    /// �������� ���� ��ġ
    /// </summary>
    private const string m_stageFolderPath = "Assets/Scenes/Stage/";

    /// <summary>
    /// ���� �� ĵ������ Ʈ������
    /// </summary>
    private Transform m_canvasTrans = null;
    /// <summary>
    /// �������� �ڵ� ����Ʈ
    /// ����� ������ ������ ����
    /// </summary>
    private List<List<int>> m_stageIndexList = new List<List<int>>();
    /// <summary>
    /// �������� Ŭ���� ����Ʈ
    /// ���� Ŭ���� �� ���������� ǥ��
    /// </summary>
    private List<int> m_stageClearList = new List<int>();
    /// <summary>
    /// ���� ���� �ε���
    /// �������� ���� ����
    /// </summary>
    private int m_levelIndex = 0;
    /// <summary>
    /// ���� �������� �ε���
    /// </summary>
    private int m_stageIndex = 0;

    /// <summary>
    /// ��
    /// 
    /// ���� �ʿ�
    /// </summary>
    private long m_money = 10000;

    private void Awake()
    {
        //�̱��� ����
        if (g_gameManager == null)
        {
            g_gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //�� �ε��ϴ� ���
        SceneManager.sceneLoaded -= SceneLoaded;
        SceneManager.sceneLoaded += SceneLoaded;

        //�� �̸� �ҷ��ͼ� ����Ʈ�� ����
        FileManager _fileManager = new FileManager();
        m_stageIndexList.Add(_fileManager.GetFileNum(m_stageFolderPath + m_levelIndex.ToString()));

        //Ŭ���� �� �������� �ʱ�ȭ
        for(int i = 0; i < m_stageIndexList.Count; i++)
        {
            m_stageClearList.Add(-1);
        }
    }

    /// <summary>
    /// ���� �ε� �Ǿ��� ��
    /// gamedatamanager �ܵ� ��� ����
    /// </summary>
    /// <param name="scene">��</param>
    /// <param name="mode">���</param>
    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        try
        {
            m_canvasTrans = GameObject.Find("Canvas").transform;

            if(m_canvasTrans != null)
            {
                Instantiate(m_optionManager);
            }
        }
        catch
        {
            m_canvasTrans = null;
            Debug.Log("no canvas");
        }
    }

    /// <summary>
    /// �̸����� �� �̵�
    /// </summary>
    /// <param name="argStr">�̵��� ���� �̸�</param>
    public void MoveSceneAsName(string argStr)
    {
        SceneManager.LoadScene(argStr);
    }

    /// <summary>
    /// �������� ����
    /// </summary>
    /// <param name="argStageIndex">�������� �ڵ�</param>
    public void EnterStage(int argStageIndex)
    {
        StageIndex = argStageIndex;
        MoveSceneAsName(argStageIndex.ToString());
    }

    /// <summary>
    /// ���� �������� Ŭ����
    /// </summary>
    public void ClearStage(bool argIsNextStage)
    {
        m_stageClearList[m_levelIndex] += 1;

        if (argIsNextStage == true)
        {
            EnterStage(StageIndex + 1);
        }
        else
        {
            MoveSceneAsName("SelectStage");
        }
    }

    /// <summary>
    /// ��� �г� ����
    /// </summary>
    public void Alert(string argAlertStr)
    {
        if (m_canvasTrans != null)
        {
            Instantiate(m_alertObjPrefeb, m_canvasTrans).GetComponent<AlertPanel>().Alert(argAlertStr);
        }
    }

    public static GameManager Instance => g_gameManager;
    public Transform CanvasTrans => m_canvasTrans;
    public List<int> StageIndexList => m_stageIndexList[m_levelIndex];
    public int ClearStageCount => m_stageClearList[m_levelIndex];

    public int LevelIndex
    {
        get { return m_levelIndex; }
        set
        {
            if (m_levelIndex <= 0)
            {
                return;
            }

            m_levelIndex = value;
        }
    }
    public int StageIndex
    {
        get { return m_stageIndex; }
        set
        {
            if(m_stageIndex <= 0)
            {
                return;
            }

            m_stageIndex = value;
        }
    }
    public long Money
    {
        get { return m_money; }
        set
        {
            m_money = value;
            if (m_money <= 0)
            {
                m_money = 0;
            }
        }
    }
}

public interface IGameManager
{
    /// <summary>
    /// ���� ������ �������� �ε��� ����Ʈ ��ȯ
    /// </summary>
    List<int> StageIndexList { get; }
    /// <summary>
    /// ���� ���� ĵ���� Ʈ������
    /// </summary>
    Transform CanvasTrans { get; }
    /// <summary>
    /// Ŭ������ ��������
    /// </summary>
    int ClearStageCount { get; }
    /// <summary>
    /// ���� ���� �ε���
    /// </summary>
    int LevelIndex { get; set; }
    /// <summary>
    /// ���� �������� �ڵ�
    /// </summary>
    int StageIndex { get; set; }
    /// <summary>
    /// ��
    /// </summary>
    long Money { get; set; }
    /// <summary>
    /// �������� ����
    /// </summary>
    void EnterStage(int argStageCode);
    /// <summary>
    /// �̸����� �� �̵�
    /// </summary>
    void MoveSceneAsName(string argStr);
    /// <summary>
    /// ��� �г� ����
    /// </summary>
    void Alert(string argAlertStr);
    /// <summary>
    /// �������� Ŭ����
    /// </summary>
    void ClearStage(bool argIsNextStage);
}