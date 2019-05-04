using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class EnemyInfo : MonoBehaviour
{
    NavMeshAgent pathfinder;    // 길 찾기 관리
    Transform target;   // 플레이어 위치

    private bool result;

    void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        // pathfinder의 레퍼런스를 얻음
        target = GameObject.FindGameObjectWithTag("Player").transform;
        // 플레이어 위치 가져옴
        StartCoroutine(UpdatePath());
        // UpdatePath() 코루틴 호출
    }
    
    void Update()
    {

    }

    IEnumerator UpdatePath()
    {   // Update문은 매 프레임마다 경로 계산을 하기 때문에, 타이머로 고정된 가격으로 하게 함.
        float refreshRate = 1;  // 경로를 업데이트할 때 사용할 갱신간격

        while (target != null)
        {   // 타켓이 존재하는 동안 타켓의 위치를 가져옴
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            pathfinder.SetDestination(targetPosition);  
            // pathfinder의 SetDestination 메소드를 호출하여 target.position을 넣어 목적지를 플레이어의 위치로 지정

            yield return new WaitForSeconds(refreshRate);
        }
        
    }

    public void InitEnemyInfo(Enemy enemy)
    {
        result = enemy.result;
        transform.position = enemy.spawnPos;
        this.transform.Find("TextMesh").GetComponent<TextMesh>().text = enemy.meshNum;
        gameObject.SetActive(true);
    }

    public void getDamage()  // ??
    {
        gameObject.SetActive(false);
    }

    public void EnemyDeathEffect()  // 적 죽음 이펙트
    {

    }

    public bool isRightResult()     // 답이 아닌지 맞는지 리턴
    {
        if (result)
        {
            return false;
        }
        else
            return true;
    }
    
    private void OnDisable()
    {
        if (WaveManager.instance != null)
        {
            WaveManager.instance.DieEnemy();
        }
    }
}
