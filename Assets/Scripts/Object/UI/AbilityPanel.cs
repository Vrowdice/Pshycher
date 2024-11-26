using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    /// <summary>
    /// �� �гε��� ��ġ
    /// </summary>
    [SerializeField]
    Transform m_skillPanelTrans = null;
    [SerializeField]
    Transform m_toolPanelTrans = null;
    /// <summary>
    /// ���� ǥ�� ��ũ ��ġ
    /// </summary>
    [SerializeField]
    Transform m_selectMarkTrans = null;
    /// <summary>
    /// ���� �ֱ�
    /// </summary>
    [SerializeField]
    float m_changeInterval = 0.5f;

    /// <summary>
    /// ��ų �ε����� �� �ε���
    /// �ѹ��� �ϳ����� ���� �����ѵ�
    /// </summary>
    private int m_skillIndex = 0;
    private int m_toolIndex = 0;
    /// <summary>
    /// �ִ� �ε���
    /// ���߿� ��ų �����͸� �����Ұ�
    /// </summary>
    private int m_maxSkillIndex = 5;
    private int m_maxToolIndex = 5;
    /// <summary>
    /// ���� ��ų�� �������� ��� true
    /// </summary>
    private bool m_isSkill = true;
    /// <summary>
    /// ��ų ���� �� ���� �� �� �ִ� ���
    /// </summary>
    private bool m_isCanChange = true;

    // Start is called before the first frame update
    void Start()
    {
        //�ӽ� ����!!!!!
        m_maxSkillIndex = m_skillPanelTrans.childCount - 1;
        m_maxToolIndex = m_toolPanelTrans.childCount - 1;
    }

    void IsCanChange()
    {
        m_isCanChange = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_isCanChange == false)
        {
            return;
        }

        if (Input.GetAxisRaw("Vertical") == -1)
        {
            m_isSkill = false;
            ToolIndex = SkillIndex;
        }
        else if (Input.GetAxisRaw("Vertical") == 1)
        {
            m_isSkill = true;
            SkillIndex = ToolIndex;
        }

        if (m_isSkill == true)
        {
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                SkillIndex -= 1;
            }
            else if (Input.GetAxisRaw("Horizontal") == 1)
            {
                SkillIndex += 1;
            }
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                ToolIndex -= 1;
            }
            else if (Input.GetAxisRaw("Horizontal") == 1)
            {
                ToolIndex += 1;
            }
        }

        m_isCanChange = false;
        Invoke("IsCanChange", m_changeInterval);
    }

    public int SkillIndex
    {
        get
        { return m_skillIndex; }
        private set
        {
            m_skillIndex = value;

            if(m_skillIndex < 0)
            {
                m_skillIndex = m_maxSkillIndex;
            }
            else if(m_skillIndex > m_maxSkillIndex)
            {
                m_skillIndex = 0;
            }

            m_selectMarkTrans.position = m_skillPanelTrans.GetChild(m_skillIndex).position;
        }
    }

    public int ToolIndex
    {
        get
        { return m_toolIndex; }
        private set
        {
            m_toolIndex = value;

            if (m_toolIndex < 0)
            {
                m_toolIndex = m_maxToolIndex;
            }
            else if (m_toolIndex > m_maxToolIndex)
            {
                m_toolIndex = 0;
            }

            m_selectMarkTrans.position = m_toolPanelTrans.GetChild(m_toolIndex).position;
        }
    }
}
