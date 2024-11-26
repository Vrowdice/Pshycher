using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroManager : MonoBehaviour
{
    /// <summary>
    /// ȭ�� ���̵�� �̹��� (���� ȭ���� ���� CanvasGroup)
    /// </summary>
    public CanvasGroup FadeImage;

    /// <summary>
    /// �ؽ�Ʈ �ڽ� �г� (��ȭ ���ڰ� ���Ե� CanvasGroup)
    /// </summary>
    public CanvasGroup TextBoxPanel;

    /// <summary>
    /// ��ȭ �ؽ�Ʈ (TextMeshPro�� �̿��� �ؽ�Ʈ ������Ʈ)
    /// </summary>
    public TextMeshProUGUI DialogueText;

    /// <summary>
    /// ���̵� ��/�ƿ� ȿ���� ����Ǵ� �ð� (�� ����)
    /// </summary>
    public float fadeDuration = 1.0f;

    /// <summary>
    /// ��� �迭 (ǥ�õ� ��ȭ�� �ؽ�Ʈ ���)
    /// </summary>
    public string[] dialogueLines;

    /// <summary>
    /// ���� ǥ�� ���� ����� �ε���
    /// </summary>
    private int currentLineIndex = 0;

    /// <summary>
    /// ���̵� ���� ����
    /// </summary>
    private bool isFading = false; 


    // Start �޼���� ��ũ��Ʈ�� Ȱ��ȭ�� �� ó�� ����˴ϴ�.
    void Start()
    {
        // �ؽ�Ʈ �ڽ��� ó���� ��Ȱ��ȭ
        TextBoxPanel.SetActive(false);

        // ���� ȭ�� ���̵� �ƿ� �ڷ�ƾ ����
        StartCoroutine(FadeOutScene());

    }

    /// <summary>
    /// ���� ȭ���� ���̵� �ƿ���Ű�� ��ȭ ���ڸ� ǥ���ϴ� �ڷ�ƾ
    /// </summary>
    IEnumerator FadeOutScene()
    {
        // ȭ���� ���������� �������� ���̵� �ƿ�
        yield return FadeCanvasGroup(FadeImage, 1, 0, fadeDuration);

        // �ణ�� ��� �ð� (0.5��)
        yield return new WaitForSeconds(0.5f);

        // ��ȭ ���ڸ� ǥ���ϴ� �ڷ�ƾ ����
        yield return ShowDialogueBox();
    }
    
    /// <summary>
    /// ��ȭ ���ڸ� ���̵� ���Ͽ� ǥ���ϰ� ��ȭ�� �����ϴ� �ڷ�ƾ
    /// </summary>
    IEnumerator ShowDialogueBox()
    {
        // ��ȭ ���� ���̵� ��
        TextBoxPanel.SetActive(true);


        // ��ȭ ����
        while (currentLineIndex < dialogueLines.Length)
        {
            // ���� ��� �迭�� ������ �ؽ�Ʈ �ڽ��� ǥ��
            DialogueText.text = dialogueLines[currentLineIndex];

            // ���� ��縦 �غ��ϱ� ���� �ε��� ����
            currentLineIndex++;

            // ����ڰ� �����̽� Ű�� ���� ������ ���
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        // ��ȭ�� ���� �� �ణ�� ��� �ð� (0.5��)
        yield return new WaitForSeconds(0.5f);
        TextBoxPanel.SetActive(false);

        // ȭ���� ������ ���������� ���̵� �� (����)
        yield return FadeCanvasGroup(FadeImage, 0, 1, fadeDuration);
    }

    /// <summary>
    /// CanvasGroup�� Alpha ���� ���������� �����Ͽ� ���̵� ��/�ƿ� ȿ���� �ִ� �ڷ�ƾ
    /// </summary>
    /// <param name="canvasGroup">���̵� ȿ���� ������ CanvasGroup</param>
    /// <param name="startAlpha">���� Alpha �� (0: ������ ����, 1: ������ ������)</param>
    /// <param name="endAlpha">�� Alpha ��</param>
    /// <param name="duration">��ȭ�� �ɸ��� �ð� (��)</param>
    IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float time = 0; // ��� �ð� �ʱ�ȭ

        // Alpha ���� ���������� ����
        while (time < duration)
        {
            // ��� �ð� ����
            time += Time.deltaTime;

            // Lerp �Լ��� Alpha ���� ���������� ����
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);

            // �� ������ ���
            yield return null;
        }

        // ���� �� ��Ȯ�� Alpha �� ���� (���� ����)
        canvasGroup.alpha = endAlpha;
    }


    void Update()
    {

    }
}
