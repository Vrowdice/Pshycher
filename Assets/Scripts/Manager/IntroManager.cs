using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroManager : MonoBehaviour
{
    /// <summary>
    /// ȭ�� ���̵�� �̹���
    /// </summary>
    public CanvasGroup fadeImage;
    /// <summary>
    /// �ؽ�Ʈ �ڽ� �г�
    /// </summary>
    public CanvasGroup textBoxPanel;
    /// <summary>
    /// ��ȭ �ؽ�Ʈ
    /// </summary>
    public TextMeshProUGUI dialogueText;
    /// <summary>
    /// ���̵� ��/�ƿ� ���� �ð�
    /// </summary>
    public float fadeDuration = 1.0f;
    /// <summary>
    /// ��� �迭
    /// </summary>
    public string[] dialogueLines;

    private int currentLineIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOutScene());
    }
    IEnumerator FadeOutScene()
    {
        yield return FadeCanvasGroup(fadeImage, 0, 1, fadeDuration); // ���̵� �ƿ� (���� ȭ��)
        yield return new WaitForSeconds(0.5f);
        yield return ShowDialogueBox(); // ��ȭ ���� ǥ��
    }
    IEnumerator ShowDialogueBox()
    {
        yield return FadeCanvasGroup(textBoxPanel, 0, 1, fadeDuration); // �ؽ�Ʈ �ڽ� ���̵� ��

        // ��ȭ ����
        while (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
            currentLineIndex++;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space)); // �����̽� Ű�� ��ȭ �ѱ��
        }

        yield return new WaitForSeconds(0.5f);
        yield return FadeCanvasGroup(textBoxPanel, 1, 0, fadeDuration); // �ؽ�Ʈ �ڽ� ���̵� �ƿ�
        yield return FadeCanvasGroup(fadeImage, 1, 0, fadeDuration); // ȭ�� ���̵� �ƿ� (���� ����)
    }

    IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
