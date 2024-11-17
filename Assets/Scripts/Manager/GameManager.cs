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
    /// ���� �� ĵ������ Ʈ������
    /// </summary>
    private Transform m_canvasTrans = null;
    /// <summary>
    /// �������� �ڵ� ����Ʈ
    /// </summary>
    private List<int> m_stageCodeList = new List<int>();
    /// <summary>
    /// �������� ���� ��ġ
    /// </summary>
    private string m_stageFolderPath = "Assets/Scenes/Stage";
    /// <summary>
    /// ���� �������� �ڵ�
    /// </summary>
    private int m_stageCode = 0;

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
            SceneManager.sceneLoaded -= SceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        //�� �ε��ϴ� ���
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void OnEnable()
    {
        OnEnableSetting();
    }

    private void OnEnableSetting()
    {
        FileManager _fileManager = new FileManager();

        m_stageCodeList = _fileManager.GetFileNum(m_stageFolderPath);
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
    /// <param name="argStageCode">�������� �ڵ�</param>
    public void EnterStage(int argStageCode)
    {
        m_stageCode = argStageCode;
        MoveSceneAsName(argStageCode.ToString());
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
    public List<int> StageCodeList => m_stageCodeList;

    public int StageCode
    {
        get { return m_stageCode; }
        set
        {
            if(m_stageCode <= 0)
            {
                return;
            }

            m_stageCode = value;
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
    /// �������� �ڵ� ����Ʈ
    /// </summary>
    List<int> StageCodeList { get; }
    /// <summary>
    /// ���� ���� ĵ���� Ʈ������
    /// </summary>
    Transform CanvasTrans { get; }
    /// <summary>
    /// ���� �������� �ڵ�
    /// </summary>
    int StageCode { get; set; }
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
}

