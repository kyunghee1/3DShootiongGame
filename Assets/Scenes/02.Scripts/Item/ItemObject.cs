using System.Collections;
using UnityEngine;



public enum ItemState
{
    Idle,  // ��� ����     (�÷��̾���� �Ÿ��� üũ�Ѵ�.)
           // �� (if ����� ����� ����..)
    Trace,// ������� ����  (N�ʿ� ���ļ� Slerp�� �÷��̾�� ����´�.)
}
public class ItemObject : MonoBehaviour
{
  

    private Vector3 _startPosition;
    private const  float TRACE_DURATION = 0.3f;
    private float _progress = 0;

    private Transform _player;
    public float EatDistance = 5f;

    private ItemState _itemState = ItemState.Idle;
    public ItemType ItemType;

    // Todo 1. ������ �������� 3��(Health, Stamina, Bullet) �����. (�����̳� ���� �ٸ����ؼ� �����ǰ�)
    // Todo 2. �÷��̾�� ���� �Ÿ��� �Ǹ� �������� �Ծ����� �������.
   

    // �ǽ� ���� 31. ���Ͱ� ������ �������� ���(Health: 20%, Stamina: 20%, Bullet: 10%)
    // �ǽ� ���� 32. ���� �Ÿ��� �Ǹ� �������� ������ ����� ������� �ϱ�

    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _startPosition = transform.position;
    }
    public void Init()
    {
        _startPosition = transform.position;
        _progress = 0f;
        _traceCoroutine = null;
        _itemState = ItemState.Idle;
    }


    void Update()
    {
        switch (_itemState)
        {
            case ItemState.Idle:
                Idle();
                break;

            case ItemState.Trace:
                Trace();
                break;


        }
    }
    private void Idle()
    {
        // ��� ������ �ൿ: �÷��̾���� �Ÿ��� üũ�Ѵ�.
        float distance = Vector3.Distance(_player.position, transform.position);
        // ���� ����: ����� ����� ����..
        if (distance <= EatDistance)
        {
            _itemState = ItemState.Trace;
        }
    }
    private Coroutine _traceCoroutine;
    private void Trace()
    {

        if (_traceCoroutine != null)
        {
            _traceCoroutine = StartCoroutine(Trace_Coroutine());
        }
    }
    private IEnumerator Trace_Coroutine()
    {
        // ���� ����: ����� ����� ����..
       while (_progress <0.6)
        {
            _progress += Time.deltaTime / TRACE_DURATION;
            transform.position = Vector3.Slerp(_startPosition, _player.position, _progress);

            yield return null;
        }
        ItemManager.Instance.AddItem(ItemType);
        gameObject.SetActive(false);


        //�ǽ� ���� 37. 36�� ������ ������� ���¸� Update �� �ƴ� �ڷ�ƾ ������� ����
    }
    /*   _progress += Time.deltaTime / TRACE_DURATION;
       transform.position = Vector3.Slerp(_startPosition, _target.position, _progress);

       if (_progress >= 0.6)
       {
           // 1. ������ �Ŵ���(�κ��丮)�� �߰��ϰ�,
           ItemManager.Instance.AddItem(ItemType);

           // 2. �������.
           gameObject.SetActive(false);
       }
   }*/

}
