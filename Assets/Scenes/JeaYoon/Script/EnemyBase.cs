using UnityEngine;
using UnityEngine.UI;

namespace MainGame.Units
{

}

/* [0] 개요 : EnemyBase
		- 적과 관련된 내용을 관리하는 클래스.
*/

public class EnemyBase : UnitBase
{
    // [1] Variable.
    #region ▼▼▼▼▼ Variable ▼▼▼▼▼
    // [◆] - ▶▶▶ 이동.
    private EnemyMove enemyMove;        // ) 이동.


    // [◆] - ▶▶▶ 체력.
    private float health;                                      // ) 체력.
    [SerializeField] private float startHealth = 100f;      // ) 시작할 때의 체력초기값.
    public Image healthBarImage;                         // ) 체력바 UI.


    // [◇] - [◆] - ) 사망.
    private bool isDeath = false;                   // ) 죽음 체크.
    public GameObject deathEffectPrefab;        // ) 죽었을 때의 이팩트 프리팹.


    // [◇] - [◆] - ) 보상.
    [SerializeField] private int rewardGold = 50;       // ) 적을 잡았을 경우 Gold 지급.
    #endregion ▲▲▲▲▲ Variable ▲▲▲▲▲





    // [2] Property.
    #region ▼▼▼▼▼ Property ▼▼▼▼▼
    // [◆] - ▶▶▶ enemyMove의 IsArrive를 이용하여적이 목표지점에 도착하였는지 알려줌.
    public bool IsArrive => enemyMove.IsArrive;
    #endregion ▲▲▲▲▲ Property ▲▲▲▲▲





    // [3] Unity Event Method.
    #region ▼▼▼▼▼ Unity Event Method ▼▼▼▼▼
    // [◆] - ▶▶▶ Start.
    private void Start()
    {
        // [◇] - [◆] - ) EnemyMove를 참조함.
        enemyMove = this.GetComponent<EnemyMove>();

        // [◇] - [◆] - ) 적의 현재 체력을 체력초기값으로 설정함.
        health = startHealth;
    }
    #endregion ▲▲▲▲▲ Unity Event Method ▲▲▲▲▲





    // [4] Custom Method.
    #region ▼▼▼▼▼ Custom Method ▼▼▼▼▼
    // [◆] - ▶▶▶ TakeDamage.
    public void TakeDamage(float damage)
    {
        // [◇] - [◆] - ) 목적지에 도착한 적은 데미지를 받지않음.
        if (enemyMove.IsArrive)
            return;
        // [◇] - [◆] - ) 데미지 받은 만큼 health가 감소함.
        health -= damage;
        // [◇] - [◆] - ) 체력바 UI(백분율) = 현재체력(health) / 체력초기값(startHealth).
        healthBarImage.fillAmount = health / startHealth;
        // [◇] - [◆] - ) 적의 사망 확인 및 체력이 0이하가 되면 죽음으로 처리.
        if (health <= 0f && isDeath == false)
        {
            Die();
        }
    }


    // [◆] - ▶▶▶ Die.
    private void Die()
    {
        // [◇] - [◆] - ) 사망.
        isDeath = true;
        // [◇] - [◆] - ) 적을 잡았을 경우 보상을 지급.
        // ) PlayerStats.AddMoney(rewardGold);
        // [◇] - [◆] - ) 적이 죽었을 때, 이팩트 실행.
        GameObject effectGo = Instantiate(deathEffectPrefab, this.transform.position, Quaternion.identity);
        Destroy(effectGo, 2f);
        // [◇] - [◆] - ) 현재 생존한 적의 숫자 카운팅.
        WaveManager.enemyAlive--;
        Debug.Log($"enemyAlive: {WaveManager.enemyAlive}");
        // [◇] - [◆] - ) 죽었을 경우 오브젝트 제거.
        Destroy(this.gameObject, 0f);
    }


    // [◆] - ▶▶▶ Slow.
    public void Slow(float rate)
    {
        // [◇] - [◆] - ) 감속률만큼 적의 속도를 감속.
        enemyMove.moveSpeed = enemyMove.StartMoveSpeed * (1 - rate);
    }
    #endregion ▲▲▲▲▲ Custom Method ▲▲▲▲▲
}
