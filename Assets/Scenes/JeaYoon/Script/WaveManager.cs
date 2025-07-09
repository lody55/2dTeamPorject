using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;

/* [0] ���� : WaveManager
		- Enemy�� ���� �� ���̺긦 �����ϴ� ��ũ��Ʈ.
*/

public class WaveManager : MonoBehaviour
{
    // [1] Variable.
    #region ������ Variable ������
    // [��] - ������ ���̺�.
    public WaveManager[] waves;          // ) ���̺� ������ ���� : Enemy ������, Enemy ���� ����, Enemy �� ������ Ÿ��.
    public float waveTimer = 5f;            // ) Ÿ�̸�.
    private int waveCount = 0;              // ) ���̺� ī��Ʈ.
    public GameObject startButton;        // ) ���̺� ��ŸƮ ��ư.
    public GameObject waveInfo;           // ) ���̺� ����.
    private int enemyCount;                 // ) ���̺꿡�� ������ Enemy ����.


    // [��] - ������ ETC.
    public static int enemyAlive = 0;       // ) ���� ����ȭ�鿡�� ����ִ� Enemy�� ����.
    public Transform startPoint;            // ) Enemy ���� ��ġ.
    private float countdown = 0f;
    public TextMeshProUGUI countText;       // ) Text.
    public GameManager gameManager;         // ) ���ӸŴ���.
    #endregion ������ Variable ������





    // [2] Unity Event Method.
    #region ������ Unity Event Method ������
    // [��] - ������ Start.
    private void Start()
    {
        // [��] - [��] - ) ������ ���� �� �� ��� ������ �ʱ�ȭ�ؼ� �������� ù ���̺���� ���������� �۵��� �� �ְ� ��.
        countdown = 3f;
        waveCount = 0;
        enemyAlive = 0;
        enemyCount = 0;
    }


    // [��] - ������ Update.
    private void Update()
    {
        // [��] - [��] - ) ���� �ʿ� Enemy�� �����ִ��� ���θ� üũ�Ͽ� �� �� ī��Ʈ�ٿ��� ����.
        if (enemyAlive > 0)
        {
            // [��] - [��] - [��] ) 147.

        }
    }

    // [��] - [��] - ) 789.
    // [��] - [��] - [��] ) 147.
    // [��] - [��] - [��] - [��] ) 258.
    // [��] - [��] - [��] - [��] - [��] ) 369.
    #endregion ������ Unity Event Method ������





    // [4] Custom Method.
    #region ������ Custom Method ������
    // [��] - ������ 123.


    // [��] - ������ 456.


    // [��] - [��] - ) 789.
    // [��] - [��] - [��] ) 147.
    // [��] - [��] - [��] - [��] ) 258.
    // [��] - [��] - [��] - [��] - [��] ) 369.
    #endregion ������ Custom Method ������
}
