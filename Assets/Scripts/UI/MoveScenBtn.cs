using UnityEngine;

public class MoveScenBtn : MonoBehaviour
{
    /// <summary>
    /// Ŭ�� ��
    /// </summary>
    /// <param name="argScenName">�̵��� �� �̸�</param>
    public void Click(string argScenName)
    {
        GameManager.Instance.MoveSceneAsName(argScenName);
    }
}
