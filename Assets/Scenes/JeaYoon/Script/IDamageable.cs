using UnityEngine;

/* [0] ���� : IDamageable
		- �������� ���õ� ��� ������ ������.
*/

public interface IDamageable
{
    public void TakeDamage(float damage);
    public void Slow(float rate);
}
