using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    /*
     �� ���� (�켱���� 1)
    - �Ʊ� : ���� / ���Ÿ� / Ÿ��
    - ���� : �븻 / ����Ʈ / ����
    - �ɷ�ġ : ���ݷ�, ü��, ���ݼӵ�, ���ݹ���, �̵��ӵ�, ���(�Ʊ� ����), �г�Ƽ(���� ����) ....
     */

    // [0] ���� Ÿ�� ����.
    public enum UnitFaction { Ally, Enemy }
    public enum AllyUnitClass { Melee, Ranged, Tower }
    public enum EnemyUnitClass { Boss, Elite, Normal }

    public class UnitStat : MonoBehaviour
    {
        // [1] Variable.
        #region ������ Variable ������
        // [��] - ������ ü��.
        [SerializeField] private UnitFaction faction;            // ) �� ���ֺ� �Ҽ�.
        [SerializeField] private float maxHealth = 100f;       // ) ü�� �ʱⰪ.
        public Image healthBarImage;                         // ) HP ��.
                                                             // �� HP�� �Ʊ��� ������ ���� ������ ��.

        // [��] - ������ ����.
        [SerializeField] private float attackDamgae = 10f;         // ) ���ݷ�.
        [SerializeField] private float attackPerSecond = 1.0f;      // ) ���ݼӵ�.
        [SerializeField] private float attackRange = 10f;           // ) ���ݻ�Ÿ�.

        // [��] - ������ �̵��ӵ�.
        [SerializeField] private float moveSpeed = 10f;

        // [��] - ������ ����.
        private bool isDeath = false;       // ) ���� üũ.
        [SerializeField] public GameObject deathEffectPrefab;        // ) ���� ������.

        // [��] - ������ ���(�Ʊ�����).
        [SerializeField] private int cost;      // ) �Ʊ� ���ź��.

        // [��] - ������ �г�Ƽ(��������).

        // [��] - ������ ETC.
        [SerializeField] private int rewardGold = 100;       // ) ���� ����� �� ��� ��� ����.

        #endregion ������ Variable ������



        // 5. �ִϸ��̼� ����� ����.
        private Animator animator;


    }

    public class StatusEffect
    {
        public string effectName;
        public float duration;
        public float value;

        public StatusEffect(string name, float dur, float val)
        {
            effectName = name;
            duration = dur;
            value = val;
        }
    }
