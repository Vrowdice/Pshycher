using UnityEngine;
using UnityEngine.UI;

public class SelectStageBtn : MonoBehaviour
{
    /// <summary>
    /// �� ��ư�� �ؽ�Ʈ
    /// </summary>
    [SerializeField]
    Text m_text = null;

    /// <summary>
    /// �������� �ڵ�
    /// </summary>
    private int m_stageCode = 0;

    /// <summary>
    /// �������� ���� �Ŵ���
    /// </summary>
    private SelectStageManager m_selectStageManager = null;

    /// <summary>
    /// �� ��ư �ʱ�ȭ
    /// </summary>
    /// <param name="argStageCode">�������� �ڵ�</param>
    /// <param name="argManager">�������� ���� �Ŵ���</param>
    public void ResetBtn(int argStageCode, SelectStageManager argManager)
    {
        m_stageCode = argStageCode;
        m_selectStageManager = argManager;
        //������ ���� ����
        m_text.text = (argStageCode % 10000).ToString();
    }

    /// <summary>
    /// Ŭ�� ��
    /// </summary>
    public void Click()
    {
        GameManager.Instance.EnterStage(m_stageCode);
    }
}
