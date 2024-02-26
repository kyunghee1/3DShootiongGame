using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ����: ���� ������
// -> ���� ��ü�� ���¸� �˸���, ���۰� ���� �ؽ�Ʈ�� ��Ÿ����.
public enum GameState
{
    Ready, // �غ�
    Go, // ����
    Over,  // ����
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // ������ ���´� ó���� "�غ�" ����
    public GameState State { get; private set; } = GameState.Ready;

    public TextMeshProUGUI StateTextMeshProUI;

    public Color GoStateColor;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(Start_Coroutine());
    }

    private IEnumerator Start_Coroutine()
    {
        // ���� ����
        // 1. ���� "�غ�" ���� (Ready...)
        State = GameState.Ready;
        StateTextMeshProUI.gameObject.SetActive(true);
        Refresh();

        // 2. 1.6�� �Ŀ� ���� "����" ���� (Start!)
        yield return new WaitForSeconds(1.6f);
        State = GameState.Go;
        Refresh();

        // 3. 0.4�� �Ŀ� �ؽ�Ʈ �������...
        yield return new WaitForSeconds(0.4f);
        StateTextMeshProUI.gameObject.SetActive(false);
    }

    // 4. �÷��̸� �ϴٰ�
    // 5. �÷��̾� ü���� 0�� �Ǹ� "���� ����" ����
    public void GameOver()
    {
        State = GameState.Over;
        StateTextMeshProUI.gameObject.SetActive(true);
        Refresh();
    }




    public void Refresh()
    {
        switch (State)
        {
            case GameState.Ready:
            {
                StateTextMeshProUI.text = "Ready...";
                StateTextMeshProUI.color = new Color32(0, 253, 181, 255);
                break;
            }

            case GameState.Go:
            {
                StateTextMeshProUI.text = "Go!";
                StateTextMeshProUI.color = GoStateColor;
                break;
            }

            case GameState.Over:
            {
                StateTextMeshProUI.text = "Game Over";
                StateTextMeshProUI.color = Color.red;
                break;
            }

        }
    }
}