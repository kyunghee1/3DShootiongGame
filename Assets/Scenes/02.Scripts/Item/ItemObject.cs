using System.Collections;
using UnityEngine;



public enum ItemState
{
    Idle,  // 대기 상태     (플레이어와의 거리를 체크한다.)
           // ▼ (if 충분히 가까워 지면..)
    Trace,// 날라오는 상태  (N초에 걸쳐서 Slerp로 플레이어에게 날라온다.)
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

    // Todo 1. 아이템 프리팹을 3개(Health, Stamina, Bullet) 만든다. (도형이나 색깔 다르게해서 구별되게)
    // Todo 2. 플레이어와 일정 거리가 되면 아이템이 먹어지고 사라진다.
   

    // 실습 과제 31. 몬스터가 죽으면 아이템이 드랍(Health: 20%, Stamina: 20%, Bullet: 10%)
    // 실습 과제 32. 일정 거리가 되면 아이템이 베지어 곡선으로 날라오게 하기

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
        // 대기 상태의 행동: 플레이어와의 거리를 체크한다.
        float distance = Vector3.Distance(_player.position, transform.position);
        // 전이 조건: 충분히 가까워 지면..
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
        // 전이 조건: 충분히 가까워 지면..
       while (_progress <0.6)
        {
            _progress += Time.deltaTime / TRACE_DURATION;
            transform.position = Vector3.Slerp(_startPosition, _player.position, _progress);

            yield return null;
        }
        ItemManager.Instance.AddItem(ItemType);
        gameObject.SetActive(false);


        //실습 과제 37. 36번 과제의 날라오는 상태를 Update 가 아닌 코루틴 방식으로 변경
    }
    /*   _progress += Time.deltaTime / TRACE_DURATION;
       transform.position = Vector3.Slerp(_startPosition, _target.position, _progress);

       if (_progress >= 0.6)
       {
           // 1. 아이템 매니저(인벤토리)에 추가하고,
           ItemManager.Instance.AddItem(ItemType);

           // 2. 사라진다.
           gameObject.SetActive(false);
       }
   }*/

}
