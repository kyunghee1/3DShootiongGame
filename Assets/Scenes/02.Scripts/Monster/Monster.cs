using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MonsterState // ������ ����
{
    Idle,           // ���
    Trace,          // ����
    Attack,         // ����
    Comeback,       // ����
    Damaged,        // ���� ����
    Die             // ���
}

public class Monster : MonoBehaviour, IHitable
{
    [Range(0, 100)]
    public int Health;
    public int MaxHealth = 100;
    public Slider HealthSliderUI;
    /***********************************************************************/

    private CharacterController _characterController;

    private Transform _target;         // �÷��̾�
    public float FindDistance = 5f;  // ���� �Ÿ�
    public float AttackDistance = 2f;  // ���� ���� 
    public float MoveSpeed = 4f;  // �̵� ����
    public Vector3 StartPosition;     // ���� ��ġ
    public float MoveDistance = 40f; // ������ �� �ִ� �Ÿ�
    public const float TOLERANCE = 0.1f;
    public int Damage = 10;
    public const float AttackDelay = 1f;
    private float _attackTimer = 0f;

    private Vector3 _knockbackStartPosition;
    private Vector3 _knockbackEndPosition;
    private const float KNOCKBACK_DURATION = 0.1f;
    private float _knockbackProgress = 0f;
    public float KnockbackPower = 1.2f;

    private MonsterState _currentState = MonsterState.Idle;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        StartPosition = transform.position;

        Init();
    }
    public void Init()
    {
        Health = MaxHealth;
    }

    private void Update()
    {
        HealthSliderUI.value = (float)Health / (float)MaxHealth;  // 0 ~ 1

        // ���� ����: ���¿� ���� �ൿ�� �ٸ��� �ϴ� ���� 
        // 1. ���Ͱ� ���� �� �ִ� �ൿ�� ���� ���¸� ������.
        // 2. ���µ��� ���ǿ� ���� �ڿ������� ��ȯ(Transition)�ǰ� �����Ѵ�.

        switch (_currentState)
        {
            case MonsterState.Idle:
                Idle();
                break;

            case MonsterState.Trace:
                Trace();
                break;

            case MonsterState.Comeback:
                Comeback();
                break;

            case MonsterState.Attack:
                Attack();
                break;

            case MonsterState.Damaged:
                Damaged();
                break;
        }
    }

    private void Idle()
    {
        // todo: ������ Idle �ִϸ��̼� ���
        if (Vector3.Distance(_target.position, transform.position) <= FindDistance)
        {
            Debug.Log("���� ��ȯ: Idle -> Trace");
            _currentState = MonsterState.Trace;
        }
    }

    private void Trace()
    {
        // Trace �����϶��� �ൿ �ڵ带 �ۼ�

        // �÷��̾�� �ٰ�����.
        // 1. ������ ���Ѵ�. (target - me)
        Vector3 dir = _target.transform.position - this.transform.position;
        dir.y = 0;
        dir.Normalize();
        // 2. �̵��Ѵ�.
        _characterController.Move(dir * MoveSpeed * Time.deltaTime);
        // 3. �Ĵٺ���.
        transform.forward = dir; //(_target);

        if (Vector3.Distance(transform.position, StartPosition) >= MoveDistance)
        {
            Debug.Log("���� ��ȯ: Trace -> Comeback");
            _currentState = MonsterState.Comeback;
        }

        if (Vector3.Distance(_target.position, transform.position) <= AttackDistance)
        {
            Debug.Log("���� ��ȯ: Trace -> Attack");
            _currentState = MonsterState.Attack;
        }
    }

    private void Comeback()
    {
        // �ǽ� ���� 34. ���� ������ �ൿ �����ϱ�:
        // ���� ���� �Ĵٺ��鼭 ������������ �̵��ϱ� (�̵� �Ϸ��ϸ� �ٽ� Idle ���·� ��ȯ)
        // 1. ������ ���Ѵ�. (target - me)
        Vector3 dir = StartPosition - this.transform.position;
        dir.y = 0;
        dir.Normalize();
        // 2. �̵��Ѵ�.
        _characterController.Move(dir * MoveSpeed * Time.deltaTime);
        // 3. �Ĵٺ���.
        transform.forward = dir; //(_target);

        if (Vector3.Distance(StartPosition, transform.position) <= TOLERANCE)
        {
            Debug.Log("���� ��ȯ: Comeback -> idle");
            _currentState = MonsterState.Idle;
        }

    }

    private void Attack()
    {
        // ���� ���: �÷��̾�� �Ÿ��� ���� �������� �־����� �ٽ� Trace
        if (Vector3.Distance(_target.position, transform.position) > AttackDistance)
        {
            _attackTimer = 0f;
            Debug.Log("���� ��ȯ: Attack -> Trace");
            _currentState = MonsterState.Trace;
            return;
        }

        // �ǽ� ���� 35. Attack ������ �� N�ʿ� �� �� ������ ������ �ֱ�
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= AttackDelay)
        {
            IHitable playerHitable = _target.GetComponent<IHitable>();
            if (playerHitable != null)
            {
                Debug.Log("���ȴ�!");
                playerHitable.Hit(Damage);
                _attackTimer = 0f;
            }
        }

    }

    private void Damaged()
    {
        // 1. Damage �ִϸ��̼� ����(0.5��)
        // todo: �ִϸ��̼� ����

        // 2. �˹� ����
        // 2-1. �˹� ����/���� ��ġ�� ���Ѵ�.
        if (_knockbackProgress == 0)
        {
            _knockbackStartPosition = transform.position;

            Vector3 dir = transform.position - _target.position;
            dir.y = 0;
            dir.Normalize();

            _knockbackEndPosition = transform.position + dir * KnockbackPower;
        }

        _knockbackProgress += Time.deltaTime / KNOCKBACK_DURATION;

        // 2-2. Lerp�� �̿��� �˹��ϱ�
        transform.position = Vector3.Lerp(_knockbackStartPosition, _knockbackEndPosition, _knockbackProgress);

        if (_knockbackProgress > 1)
        {
            _knockbackProgress = 0f;

            Debug.Log("���� ��ȯ: Damaged -> Trace");
            _currentState = MonsterState.Trace;
        }
    }

    public void Hit(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("���� ��ȯ: Any -> Damaged");
            _currentState = MonsterState.Damaged;
        }
    }

    private void Die()
    {

        // ������ ������ ����
        ItemObjectFactory.Instance.MakePercent(transform.position);

        Destroy(gameObject);
    }









}