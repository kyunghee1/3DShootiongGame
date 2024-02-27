using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public enum MonsterState // ������ ����
{
    Any,            //� ���¶�
    Idle,           // ���
    Patrol,         // ����
    Trace,          // ����
    Attack,         // ����
    Comeback,       // ����
    Damaged,        // ���� ����
    Die  ,           // ���
    
}

public class Monster : MonoBehaviour, IHitable
{
    [Range(0, 100)]
    public int Health;
    public int MaxHealth = 100;
    public Slider HealthSliderUI;
    /***********************************************************************/

   // private CharacterController _characterController;
   
    private NavMeshAgent _navMeshAgent;

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

    private const float IDLE_DURATION = 3f;
    private float _idleTimer;
    public Transform PatrolTarget;

    private MonsterState _currentState = MonsterState.Idle;
    private Animator _animator;
   
    private void Start()
    {
        //_characterController = GetComponent<CharacterController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = MoveSpeed;


        _target = GameObject.FindGameObjectWithTag("Player").transform;

        StartPosition = transform.position;

        _animator = GetComponentInChildren<Animator>();

        Init();
    }
    public void Init()
    {
        _idleTimer = 0f;
        Health = MaxHealth;
    }

    private void Update()
    {
        HealthSliderUI.value = (float)Health / (float)MaxHealth;  // 0 ~ 1
        if (GameManager.Instance.State != GameState.Go)
        {
            return;
        }

        // ���� ����: ���¿� ���� �ൿ�� �ٸ��� �ϴ� ���� 
        // 1. ���Ͱ� ���� �� �ִ� �ൿ�� ���� ���¸� ������.
        // 2. ���µ��� ���ǿ� ���� �ڿ������� ��ȯ(Transition)�ǰ� �����Ѵ�.

        switch (_currentState)
        {
           

            case MonsterState.Idle:
                Idle();
                break;

            case MonsterState.Patrol:
                Patrol();
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

            case MonsterState.Die:
               Die();
                break;
           

        }
    }

    private void Idle()
    {
        _idleTimer += Time.deltaTime;
        // todo: ������ Idle �ִϸ��̼� ���
        if (PatrolTarget != null && _idleTimer >= IDLE_DURATION)
        {
            _idleTimer = 0f;
            Debug.Log("���� ��ȯ: Idle -> Patrol");
            _animator.SetTrigger("IdleToPatrol");
            _currentState = MonsterState.Patrol;
        }
        // todo: ������ Idle �ִϸ��̼� ���
        if (Vector3.Distance(_target.position, transform.position) <= FindDistance)
        {
            Debug.Log("���� ��ȯ: Idle -> Trace");
            _animator.SetTrigger("IdleToTrace");
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
        //_characterController.Move(dir * MoveSpeed * Time.deltaTime);
        //������̼��� �����ϴ� �ּ� �Ÿ��� ���� ���� �Ÿ��� ����
        _navMeshAgent.stoppingDistance = AttackDistance;
        _navMeshAgent.destination = _target.position;
        // 3. �Ĵٺ���.
       // transform.forward = dir; //(_target);

        if (Vector3.Distance(transform.position, StartPosition) >= MoveDistance)
        {
            Debug.Log("���� ��ȯ: Trace -> Comeback");
            _animator.SetTrigger("TraceToComeback");
            _currentState = MonsterState.Comeback;
        }

        if (Vector3.Distance(_target.position, transform.position) <= AttackDistance)
        {
            Debug.Log("���� ��ȯ: Trace -> Attack");
            _animator.SetTrigger("TraceToAttack");
            _currentState = MonsterState.Attack;
        }
    }
    private void Patrol()
    {
        _navMeshAgent.stoppingDistance = 0f;
        _navMeshAgent.SetDestination(PatrolTarget.position);

        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= TOLERANCE)
        {
            Debug.Log("���� ��ȯ: Patrol -> Comeback");
            _animator.SetTrigger("PatrolToComeback");
            _currentState = MonsterState.Comeback;
        }

        if (Vector3.Distance(_target.position, transform.position) <= FindDistance)
        {
            Debug.Log("���� ��ȯ: Patrol -> Trace");
            _animator.SetTrigger("PatrolToTrace");
            _currentState = MonsterState.Trace;
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
        //_characterController.Move(dir * MoveSpeed * Time.deltaTime);
        // 3. �Ĵٺ���.
        _navMeshAgent.stoppingDistance = TOLERANCE;
        _navMeshAgent.destination =StartPosition;
        //transform.forward = dir; //(_target);

        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance >= TOLERANCE)
        {
            Debug.Log("���� ��ȯ: Comeback -> idle");
            _animator.SetTrigger("ComebackToIdle");
            _currentState = MonsterState.Idle;
        }
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
            _animator.SetTrigger("AttackToTrace");
            _currentState = MonsterState.Trace;
            return;
        }

        // �ǽ� ���� 35. Attack ������ �� N�ʿ� �� �� ������ ������ �ֱ�
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= AttackDelay)
        { 
            _animator.SetTrigger("Attack");
          //  _animator.SetTrigger("Attack");
          
        }

    }
    public void PlayerAttack()
    {
        Debug.Log(_target.name);
        IHitable playerHitable = _target.GetComponent<IHitable>();

        if (playerHitable != null)
        {
            Debug.Log("���ȴ�!");
            playerHitable.Hit(Damage);
            _attackTimer = 0f;
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
            _animator.SetTrigger("DamagedToTrace");
            _currentState = MonsterState.Trace;
        }
    }

    public void Hit(int damage)
    {
      
        if(_currentState == MonsterState.Die)
        {
            return;
        }
        Health -= damage;
        if (Health <= 0)
        {
            Debug.Log("������ȯ: Any -> Die");
            
            _animator.SetTrigger($"Die { Random.Range(1, 3)}");
            _currentState = MonsterState.Die;
                
        }
        else
        {
            Debug.Log("���� ��ȯ: Any -> Damaged");
          
            _currentState = MonsterState.Damaged;
        }
    }
    IEnumerator Die_Coroutine()
    {

        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();

        HealthSliderUI.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);


        // ������ ������ ����
        ItemObjectFactory.Instance.MakePercent(transform.position);


    }
    private Coroutine _dieCoroutine;
    private void Die()
    {
       if(_dieCoroutine == null)
        {
            _dieCoroutine = StartCoroutine(Die_Coroutine());
        }
        // ������ ������ ����
      
       
    }









}