using UnityEngine;

/* [0] ���� : Tower
		- �Ʊ� ���� �� Ÿ���� ���� ������.
		- Ÿ���� ���Ÿ� / ����
		- ����ü �� ����Ƽ�� Ÿ�� �ʿ�.
		- Ÿ���� �θ�Ŭ����.
*/

public class Tower : MonoBehaviour
{
    // [1] Variable.
    #region ������ Variable ������
    // [��] - ������ ����.
    [SerializeField] private float attackPerSecond = 1.0f;      // ) ���ݼӵ�.
    private float shootCountdown = 0;                         // ) ���� ���� ���� ���ݱ����� ī��Ʈ.
    [SerializeField] private float attackRange = 10f;           // ) ���ݻ�Ÿ�.


    // [��] - ������ ����.
    protected Transform target;                 // ) ���� ����� ���� ã��.
    protected IDamageable targetEnemy;      // ) IDamageable ��ũ��Ʈ���� 
    public float searchTimer = 0.5f;             // ) ???.
    private float countdown = 0f;               // ) ???.


    // [��] - ������ ����ü.
    public GameObject projectilePrefab;         // ) ����ü ������.
    public Transform firePoint;                // ) ����ü�� �߻� ��ġ.


    // [��] - ������ ���(�Ʊ�����).
    [SerializeField] private int cost;      // ) �Ʊ� ���ź��.


    // [��] - ������ ETC.
    public string enemyTag = "Enemy";       // ) Enemy �±�.
    #endregion ������ Variable ������





    // [2] Unity Event Method.
    #region ������ Unity Event Method ������
    // [��] - ������ Start.
    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f);      // ) UpdateTarget�� 0.1�ʸ��� �ݺ��Ͽ� ȣ����.
    }


    // [��] - ������ UpdateTarget.
    private void UpdateTarget()
    {
        // [��] - [��] - ) Enemy �±׸� ���� ��� ������Ʈ�� �迭 ���·� ��ȯ.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        // [��] - [��] - ) �ּҰŸ��� ���� ��.
        float minDistance = float.MaxValue;
        GameObject nearEnemy = null;
        // [��] - [��] - ) �迭�� ���Ե� ��� �� ������Ʈ(GameObject) �� �ϳ��� ������ enemy��� �̸����� �ݺ� ����.
        foreach (var enemy in enemies)
        {
            // [��] - [��] - [��] ) ������ ������ Enemy Ž������ ����.
            EnemyBase arriveEnemy = enemy.GetComponent<EnemyBase>();
            // [��] - [��] - [��] ) �̹� �������� ������ ���� �����ϰ�, �� �̻� Ÿ������ ������� ����.
            if (arriveEnemy != null && arriveEnemy.IsArrive == true)
            {
                continue;
            }
            // [��] - [��] - [��] ) ???. Ž��
            float distance = Vector3.Distance(this.transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearEnemy = enemy;
            }
        }
        // [��] - [��] - ) ���� �ȿ� ���� �ִ��� Ȯ���ϰ�, ���� ����� ���� ��Ÿ� �ȿ� ������ ���, ������.
        if (nearEnemy != null && minDistance <= attackRange)
        {
            target = nearEnemy.transform;
            targetEnemy = target.GetComponent<IDamageable>();
        }
        else
        {
            target = null;
            targetEnemy = null;
        }
    }


    // [��] - ������ Update.
    private void Update()
    {
        // [��] - [��] - ) Ÿ���� ���� ��� ��� �ݺ���.
        if (target == null)
            return;
        // [��] - [��] - ) Ÿ���� ã���� �� ����.
        LockOn();
        // [��] - [��] - ) Ÿ������ �ϸ� Ÿ���� 1�ʸ��� 1�߾� ���.
        shootCountdown += Time.deltaTime;
        if (shootCountdown >= attackPerSecond)
        {
            // [��] - [��] - [��] ) Ÿ�̸� ��� - 1�߾� ���.
            Shoot();
            // [��] - [��] - [��] ) Ÿ�̸� �ʱ�ȭ.
            shootCountdown = 0f;
        }
    }
    #endregion ������ Unity Event Method ������





    // [4] Custom Method.
    #region ������ Custom Method ������
    // [��] - ������ Shoot.
    private void Shoot()
    {
        // [��] - [��] - ) ����ü�� ����
        GameObject projectileGo = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        // [��] - [��] - ) ProjectileBase ��ũ��Ʈ�� ������.
        ProjectileBase projectile = projectileGo.GetComponent<ProjectileBase>();
        // [��] - [��] - ) Projectile ��ũ��Ʈ���� SetTarget�� ������.
        if (projectile != null)
        {
            projectile.SetTarget(target);
        }
    }


    // [��] - ������ LockOn.
    private void LockOn()
    {
        // [��] - [��] - ) ���� ������Ʈ���� Ÿ���� ���ϴ� ���� ����(���Ⱚ)�� ���ϴ� ��.
        Vector3 dir = target.position - this.transform.position;
        // [��] - [��] - ) Ÿ���� ���� ������Ʈ�� ȸ����Ű�ų� ������ ��.
        // ) Quaternion targetRotation = Quaternion.LookRotation(dir);
    }


    // [��] - ������ OnDrawGizmosSelected.
    private void OnDrawGizmosSelected()
    {
        // [��] - [��] - ) Ÿ���� ��Ÿ� ����.
        Gizmos.color = Color.red;
        // [��] - [��] - ) ������ Ÿ���� ��Ÿ� ǥ��.
        Gizmos.DrawWireSphere(this.transform.position, attackRange);
    }
    #endregion ������ Custom Method ������
}