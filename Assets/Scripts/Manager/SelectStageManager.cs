using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStageManager : MonoBehaviour
{
    /// <summary>
    /// �������� ���� �г�
    /// </summary>
    [SerializeField]
    GameObject m_selectStagePanelPrefeb = null;

    /// <summary>
    /// �������� ���� ��ư
    /// </summary>
    [SerializeField]
    GameObject m_selectStageBtnPrefeb = null;

    /// <summary>
    /// ���Ӹ޴��� �������̽�
    /// </summary>
    private IGameManager m_gameManager;

    /// <summary>
    /// �������� ���� �г� ������ ��ġ
    /// </summary>
    private Transform m_stageSelectPanelContentTrans = null;

    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameManager.Instance;

        m_stageSelectPanelContentTrans = Instantiate(m_selectStagePanelPrefeb, m_gameManager.CanvasTrans)
            .GetComponent<ScrollRect>().content.transform;
        ResetSelectStageBtn();
    }

    /// <summary>
    /// �������� ��ư���� ����
    /// </summary>
    void ResetSelectStageBtn()
    {
        List<int> stageCodeList = m_gameManager.StageCodeList;
        for (int i = 0; i < stageCodeList.Count; i++)
        {
            Instantiate(m_selectStageBtnPrefeb, m_stageSelectPanelContentTrans).GetComponent<SelectStageBtn>()
                .ResetBtn(stageCodeList[i], this);
        }
    }
}
