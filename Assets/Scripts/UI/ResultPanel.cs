using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    /// <summary>
    /// ���� �Ŵ���
    /// </summary>
    IGameManager m_gameManager = null;

    private void Start()
    {
        m_gameManager = GameManager.Instance;
    }

    public void NextStage()
    {
        m_gameManager.ClearStage(true);
    }

    public void BackSelectStage()
    {
        m_gameManager.ClearStage(false);
    }
}
