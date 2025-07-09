using UnityEngine;
using UnityEngine.UI;

/* [0] ���� : UnitBase
		- �Ʊ�, ���� ��� �������� ���Ǵ� �ɷ�ġ.
        - ���� ����Ʈ�� ���� �̵���
*/

public class UnitBase : MonoBehaviour
{
    // [1] Variable.
    #region ������ Variable ������
    // [��] - ������ �ڵ� �� �̸�.
    [SerializeField] private string id;                           // ) ���� �ڵ�.
    [SerializeField] private string name;                      // ) ���� ��.


    // [��] - ������ ü��.
    [SerializeField] private float health = 100f;             // ) ü��.
    [SerializeField] private float startHealth = 100f;      // ) ������ ���� ü���ʱⰪ.
    public Image healthBarImage;                         // ) HP ��.


    // [��] - ������ ����.
    [SerializeField] private float damage = 10f;            // ) ������.
    [SerializeField] private int[] rangeOfAttack;             // ) ���ݹ���.
    [SerializeField] private float attackSpeed = 10f;        // ) �ʴ� ���ݼӵ�.


    // [��] - ������ �̵�.
    private EnemyMove Move;                        // ) �̵�.
    [SerializeField] private float moveSpeed = 10f;         // ) �̵��ӵ�.


    // [��] - ������ ���.
    private bool isDeath = false;       // ) ���� üũ.
    [SerializeField] public GameObject deathEffectPrefab;        // ) ���� ������.


    // [��] - ������ ETC.
    [SerializeField] private int[] size;                          // ) ���� ũ��.
    #endregion ������ Variable ������





    // [2] Property.
    #region ������ Property ������
    // [��] - ������ Move�� IsArrive�� �̿��Ͽ����� ��ǥ������ �����Ͽ����� �˷���.
    public bool IsArrive => Move.IsArrive;
    #endregion ������ Property ������





    // [3] Unity Event Method.
    #region ������ Unity Event Method ������
    // [��] - ������ Start.
    private void Start()
    {
        // [��] - [��] - ) EnemyMove�� ������.
        Move = this.GetComponent<EnemyMove>();

        // [��] - [��] - ) ���� ���� ü���� ü���ʱⰪ���� ������.
        health = startHealth;
    }
    #endregion ������ Unity Event Method ������





    // [4] Custom Method.
    #region ������ Custom Method ������
    // [��] - ������ TakeDamage.
    public void TakeDamage(float damage)
    {
        // [��] - [��] - ) �������� ������ ���� �������� ��������.
        if (Move.IsArrive)
            return;
        // [��] - [��] - ) ������ ���� ��ŭ health�� ������.
        health -= damage;
        // [��] - [��] - ) ü�¹� UI(�����) = ����ü��(health) / ü���ʱⰪ(startHealth).
        healthBarImage.fillAmount = health / startHealth;
        // [��] - [��] - ) ���� ��� Ȯ�� �� ü���� 0���ϰ� �Ǹ� �������� ó��.
        if (health <= 0f && isDeath == false)
        {
            Die();
        }
    }


    // [��] - ������ Die.
    private void Die()
    {
        // [��] - [��] - ) ���.
        isDeath = true;
        // [��] - [��] - ) ���� �׾��� ��, ����Ʈ ����.
        GameObject effectGo = Instantiate(deathEffectPrefab, this.transform.position, Quaternion.identity);
        Destroy(effectGo, 2f);
        // [��] - [��] - ) ���� ������ ���� ���� ī����.
        WaveManager.enemyAlive--;
        Debug.Log($"enemyAlive: {WaveManager.enemyAlive}");
        // [��] - [��] - ) �׾��� ��� ������Ʈ ����.
        Destroy(this.gameObject, 0f);
    }


    // [��] - ������ Slow.
    public void Slow(float rate)
    {
        // [��] - [��] - ) ���ӷ���ŭ ���� �ӵ��� ����.
        Move.moveSpeed = Move.StartMoveSpeed * (1 - rate);
    }
    #endregion ������ Custom Method ������

}
