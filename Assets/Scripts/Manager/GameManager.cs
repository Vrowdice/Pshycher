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
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// �ڱ� �ڽ�
    /// </summary>
    static GameManager g_gameDataManager = null;

    /// <summary>
    /// �ɼ� �Ŵ��� ������
    /// </summary>
    [SerializeField]
    GameObject m_optionManagerPrefeb = null;
    /// <summary>
    /// �� Ȯ�� �г� ������
    /// </summary>
    [SerializeField]
    GameObject m_moneyPanelPrefeb = null;
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
    /// ��
    /// 
    /// ���� �ʿ�
    /// </summary>
    private long m_money = 10000;

    private void Awake()
    {
        //�̱��� ����
        if (g_gameDataManager == null)
        {
            g_gameDataManager = this;
            SceneManager.sceneLoaded -= SceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        //�� �ε��ϴ� ���
        SceneManager.sceneLoaded += SceneLoaded;
        //Ŀ�� ���� ����
        ChangeCursorState(true);
    }

    private void OnEnable()
    {
        OnEnableSetting();
    }

    private void OnEnableSetting()
    {

    }

    /// <summary>
    /// ���� �ε� �Ǿ��� ��
    /// gamedatamanager �ܵ� ��� ����
    /// </summary>
    /// <param name="scene">��</param>
    /// <param name="mode">���</param>
    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        m_canvasTrans = GameObject.Find("Canvas").transform;
        if (m_canvasTrans != null)
        {

        }
    }

    /// <summary>
    /// �̸����� �� �̵�
    /// </summary>
    /// <param name="argStr">�̵��� ���� �̸�</param>
    public void MoveSceneAsName(string argStr, bool argCursorState)
    {
        SceneManager.LoadScene(argStr);
        ChangeCursorState(argCursorState);
    }
    /// <summary>
    /// ���� ���� ������ �̵�
    /// </summary>
    public void GoMainScene(int argRoundIndex)
    {
        MoveSceneAsName("Main", false);
    }
    /// <summary>
    /// Ÿ��Ʋ ������ �̵�
    /// </summary>
    public void GoTitleScene()
    {
        MoveSceneAsName("Title", true);
    }
    /// <summary>
    /// ���� ������ �̵�
    /// </summary>
    public void GoShopScene()
    {
        MoveSceneAsName("Shop", true);
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

    /// <summary>
    /// Ŀ�� ���� Ȱ��ȭ ��Ȱ��ȭ
    /// </summary>
    /// <param name="argActive">����</param>
    public void ChangeCursorState(bool argActive)
    {
        if (SceneManager.GetActiveScene().name != "Main")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            return;
        }

        if (argActive == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public static GameManager Instance
    {
        get { return g_gameDataManager; }
    }
    public Transform GetCanvasTrans
    {
        get { return m_canvasTrans; }
    }

    public long SetMoney
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

