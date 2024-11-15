using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 돈 아이템 저장, 다음 씬에 데이터 이동 등을 담당합니다
/// 단독으로 실행이 가능해야합니다
/// 라운드 데이터 리스트의 인덱스는 스크립터블 오브젝트 안의 라운드 데이터 인덱스와 일치해야합니다
/// 
/// 부가 기능
/// 
/// 경고 패널 생성 기능
/// 사운드 관리 기능
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 자기 자신
    /// </summary>
    static GameManager g_gameDataManager = null;

    /// <summary>
    /// 옵션 매니저 프리펩
    /// </summary>
    [SerializeField]
    GameObject m_optionManagerPrefeb = null;
    /// <summary>
    /// 돈 확인 패널 프리펩
    /// </summary>
    [SerializeField]
    GameObject m_moneyPanelPrefeb = null;
    /// <summary>
    /// 경고 오브젝트 프리펩
    /// </summary>
    [SerializeField]
    GameObject m_alertObjPrefeb = null;

    /// <summary>
    /// 현재 씬 캔버스의 트렌스폼
    /// </summary>
    private Transform m_canvasTrans = null;

    /// <summary>
    /// 돈
    /// 
    /// 저장 필요
    /// </summary>
    private long m_money = 10000;

    private void Awake()
    {
        //싱글톤 세팅
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

        //씬 로드하는 경우
        SceneManager.sceneLoaded += SceneLoaded;
        //커서 상태 변경
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
    /// 씬이 로드 되었을 때
    /// gamedatamanager 단독 사용 권장
    /// </summary>
    /// <param name="scene">씬</param>
    /// <param name="mode">모드</param>
    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        m_canvasTrans = GameObject.Find("Canvas").transform;
        if (m_canvasTrans != null)
        {

        }
    }

    /// <summary>
    /// 이름으로 씬 이동
    /// </summary>
    /// <param name="argStr">이동할 씬의 이름</param>
    public void MoveSceneAsName(string argStr, bool argCursorState)
    {
        SceneManager.LoadScene(argStr);
        ChangeCursorState(argCursorState);
    }
    /// <summary>
    /// 메인 게임 씬으로 이동
    /// </summary>
    public void GoMainScene(int argRoundIndex)
    {
        MoveSceneAsName("Main", false);
    }
    /// <summary>
    /// 타이틀 씬으로 이동
    /// </summary>
    public void GoTitleScene()
    {
        MoveSceneAsName("Title", true);
    }
    /// <summary>
    /// 상점 씬으로 이동
    /// </summary>
    public void GoShopScene()
    {
        MoveSceneAsName("Shop", true);
    }

    /// <summary>
    /// 경고 패널 생성
    /// </summary>
    public void Alert(string argAlertStr)
    {
        if (m_canvasTrans != null)
        {
            Instantiate(m_alertObjPrefeb, m_canvasTrans).GetComponent<AlertPanel>().Alert(argAlertStr);
        }
    }

    /// <summary>
    /// 커서 상태 활성화 비활성화
    /// </summary>
    /// <param name="argActive">여부</param>
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

