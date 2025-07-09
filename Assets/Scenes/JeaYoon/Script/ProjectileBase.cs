using UnityEngine;

/* [0] ���� : TowerProjectile
		- Ÿ���� ���Ÿ� ����(�Ʊ�, ���� ����) ��� �������� �����.
*/

public class ProjectileBase : MonoBehaviour
{
    // [1] Variable.
    #region ������ Variable ������
    // [��] - ������ ����.
    [SerializeField] private float attackDamage = 10f;             // ) ���ݷ�.
    [SerializeField] private float projectileSpeed = 10f;            // ) ����ü�� ���󰡴� �ӵ�.

    // [��] - ������ ETC.
    public Transform target;                              // ) Ÿ�� ������Ʈ.
    public GameObject projectileImpactPrefab;        // ) ����ü Ÿ�� ����Ʈ ������.
    #endregion ������ Variable ������





    // [2] Unity Event Method.
    #region ������ Unity Event Method ������
    // [��] - ������ SetTarget.
    public void SetTarget(Transform _target)
    {
        // [��] - [��] - ) �ܺο��� ���޹��� '_target'���� Ŭ���� �ȿ� �ִ� 'target'�� ����.
        this.target = _target;
    }


    // [��] - ������ Update.
    private void Update()
    {
        // [��] - [��] - ) Ÿ���� �׾��� ��� ����ü�� ������.
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        // [��] - [��] - ) Ÿ�ٰ� ����ü���� �Ÿ�.
        Vector3 dir = target.position - this.transform.position;          // ) Ÿ����ġ - ����ü��ġ.
        float ditanceThisFrame = Time.deltaTime * projectileSpeed;    // ) �̹� �����ӿ� ����ü�� �̵��ϴ� �Ÿ�.
        // [��] - [��] - ) ����ü�� Ÿ�ٿ��� �浹�ߴ��� Ȯ��.
        if (dir.magnitude <= ditanceThisFrame)
        {
            HitTarget();
            return;
        }
        // [��] - [��] - ) ����ü�� �� �����Ӹ��� Ÿ�ٹ������� �̵�
        transform.Translate(dir.normalized * Time.deltaTime * projectileSpeed, Space.World);
        // [��] - [��] - ) ����ü�� Ÿ���� �ٶ󺸸� ���󰡱�
        transform.LookAt(target);
    }
    #endregion ������ Unity Event Method ������





    // [3] Custom Method.
    #region ������ Custom Method ������
    // [��] - ������ HitTarget.
    protected virtual void HitTarget()
    {
        // [��] - [��] - ) Ÿ�ݽ� ����Ʈ ȿ��.
        GameObject effectGo = Instantiate(projectileImpactPrefab, this.transform.position, Quaternion.identity);        // ) ����ü�� ������ �������� �� ����Ʈ ȿ���� �߻�.
        Destroy(effectGo, 2f);                                                                                                          // ) 2�� �� ����Ʈ�� �����.
        // [��] - [��] - ) Ÿ�ٿ��� ������ �ֱ�.
        Damage(target);
        // [��] - [��] - ) ����ü�� ���� ���� ������Ʈ ����.
        Destroy(this.gameObject);
    }


    // [��] - ������ Damage �� Ÿ�ٿ��� ������ �ֱ�.
    protected void Damage(Transform _target)
    {
        // [��] - [��] - ) ���ݷ�(attackDamgae)��ŭ Ÿ���� ü��(maxHealth)�� ���.
        IDamageable enemy = _target.GetComponent<IDamageable>();
        if (enemy != null)
        {
            // [��] - [��] - ) ������ 'attackDamage'��ŭ �������� ����.
            enemy.TakeDamage(attackDamage);
        }
    }
        #endregion ������ Custom Method ������
}
