using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    /// <summary>
    /// �ɼ� �����ϴ� ������Ʈ ������
    /// </summary>
    [SerializeField]
    GameObject m_optionSetupPrefeb = null;

    /// <summary>
    /// ���Ӹ޴��� �������̽�
    /// </summary>
    private IGameManager m_gameManager;

    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameManager.Instance;

        if (m_gameManager.CanvasTrans != null)
        {
            Instantiate(m_optionSetupPrefeb, m_gameManager.CanvasTrans);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
