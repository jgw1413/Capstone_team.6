using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy_M
{
    public string name;             // 이름 ( 프리팹 이름 )
    public bool result;             // 정답 유무
    //public Transform spawnPos;        // 생성 위치
    public string meshNum;
    //public Vector2 arrivePos;       // 생성 후 도착 위치
    //public float arriveMovingTime;  // 생성 후 도착 위치까지 이동 시간

    public Enemy_M(string name, bool result, string meshNum)
    {
        this.name = name;
        this.result = result;
        //this.spawnPos = spawnPos;
        this.meshNum = meshNum;
        //this.arrivePos = arrivePos;
        //this.arriveMovingTime = arriveMovingTime;
    }
}

[RequireComponent (typeof (NavMeshAgent))]
public class EnemyInfo_M : MonoBehaviour
{
    public HPManager_M hpManager;   // 

    public GameObject spawnEffect;  // 적이 생성되면 발생하는 이펙트
    public GameObject HitEffect;    // 적이 죽으면 발생하는 이펙트
    public GameObject ShotEffect;   // 적이 공격하면 발생하는 이펙트
    public GameObject Player;

    public float MoveSpeed;       // 적 이동 속도
    private float DistanceToPlayer;      // 적과 플레이어 사이의 거리

    private Animator ani;       // 적의 Animator를 가져오는 변수

    bool hasTarget;               // 플레이어 존재 여부
    
    private bool result;

    private void Awake()
    {
        hpManager = GameObject.Find("HPManager").GetComponent<HPManager_M>();
    }

    void Start()
    {
        hasTarget = true;
        // 타켓(플레이어) 존재
        Item_M.timerFlag = false;
        // timerFlag의 시작을 false로 줍니다.

        GameObject spawn = Instantiate(spawnEffect, transform.position, transform.rotation);
        // 적 스폰 이펙트 메소드 호출
        Destroy(spawn, 1.0f);
        // spawn 오브젝트 제거
        ani = GetComponent<Animator>();
        // 적의 Animator를 가져온다.
    }
    
    void Update()
    {
        if (hpManager.HP == 0)
        {   // 플레이어 HP가 0보다 작을 때
            hasTarget = false;
            // 플레이어를 false로 하면서 적의 움직임을 멈춘다.
        }

        if (hasTarget == true)
        {   // 플레이어 ture
            DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            // 적과 플레이어 사이의 거리를 구해서 DistanceToPlayer 변수에 저장
            // 플레이어 위치는 (0, 0, 0)이기 때문에 Vector3.zero를 사용

            if (DistanceToPlayer > 2.0f)
            {   // 적과 플레이어 거리가 4.0보다 클때
                if (Item_M.timerFlag)
                {   // timerFlag 가 true 일때
                    MoveSpeed = 0.0f;
                }
                else if (!Item_M.timerFlag)
                {   // timerFlag 가 false 일때
                    MoveSpeed = speedChange(WaveManager_M.instance.hardMode);//영우
                }
                Move();
                // 적 이동 함수 호출
            }
            else
            {
                StartCoroutine(Attack());
            }
        }
    }

    // 적 이동 메소드
    void Move()
    {
        ani.SetInteger("Walk", 1);
        transform.LookAt(Player.transform);
        // 적은 플레이어 방향을 바라봄.
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        // 초당 MoveSpeed의 거리를 이동
    }

    // 적 공격 메소드
    IEnumerator Attack() {
        //ani.SetBool("Shot", true);      
        // 적의 Animator의 Shot을 true로 하여 공격하게 함.
        yield return new WaitForSeconds(1.0f);
        // 1.0초간 대기
        WaveManager_M.instance.timeFlag = true;
        // timeFlag를 true로 주어 3초간 딜레이를 준다.
        Destroy(gameObject);
        // 자기자신 제거
        hpManager.HP -= 10;
        // 플레이어에게 10 데미지를 줌.
        hpManager.HeartCheck();
        // 플레이어의 HP 업데이트
        GameManager_M.instance.QuizText.text = "Fail";
        // Fail 표시
        //영우 05.25
        WaveManager_M.instance.timeFlag = true;
        // timeFlag를 true로 주어 3초간 딜레이를 준다.

        GameObject shot = Instantiate(ShotEffect, transform.position, transform.rotation);
        // shot GameObject 변수에 HitEffect를 저장하고 자신의 위치에 생성
        Destroy(shot, 1.0f);    
        // shot 오브젝트 제거

        if (hpManager.HP > 0)
        {   // 플레이어 HP가 0보다 크면 다음레벨
            if (WaveManager_M.instance.curWave < QuizManager_M.instance.dictionary.Count - 1)
            {   // 문제가 더 남아있을 때
                GameManager_M.instance.NextLevel();
                Debug.Log("다음문제 넘어감.");
            }
            else
            {   // 모든 문제를 풀었을 때
                GameManager_M.instance.GameClear();
            }
        }
        else if (hpManager.HP == 0)
        {   // 플레이어 HP가 0일 때 게임 종료
            GameManager_M.instance.GameOver();
        }
    }

    // 적 움직임에 딜레이를 주는 메소드
    IEnumerator MoveDelay() { 
        //ani.SetBool("Hit", true);
        yield return new WaitForSeconds(1.0f);
        //ani.SetBool("Hit", false);
    }

    // 충돌 처리하는 메소드
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {   // 충돌한 오브젝트의 태그가 Bullet인 경우
            Destroy(coll.gameObject);   
            // 총알 제거  
            getDamage();
            // 적 제거
            WaveManager_M.instance.timeFlag = true;
            // timeFlag를 true로 주어 3초간 딜레이를 준다.
            GameObject hit = Instantiate(HitEffect, transform.position, transform.rotation);
            // hit GameObject 변수에 HitEffect를 충돌 위치에 생성한다.
            Destroy(hit, 1.0f);
            // hit 오브젝트를 제거한다.
            if (isRightResult())
            {   // 정답일 때
                Sound.instance.Correct();
                if (WaveManager_M.instance.curWave < QuizManager_M.instance.dictionary.Count - 1)
                {   // 문제가 더 남아있을 때
                    GameManager_M.instance.QuizText.text = "Success";
                    // Success 표시
                    //StartCoroutine("NextWaveDelay");
                    // 1초간 카운터하고 다음문제 넘어가기
                    GameManager_M.instance.NextLevel();
                }
                else
                {   // 모든 문제를 풀었을 때
                    GameManager_M.instance.GameClear();
                }
            }
            else
            {   // 오답일 때
                Debug.Log("현재 hp" + hpManager.HP);
                Sound.instance.InCorrect();
                GameManager_M.instance.QuizText.text = "Fail";
                // Fail 표시
                hpManager.HP -= 10;
                // 플레이어에게 10 데미지를 줌.
                hpManager.HeartCheck();
                
                if (hpManager.HP > 0)
                {   // 플레이어 HP가 0보다 클 때
                    if (WaveManager_M.instance.curWave < QuizManager_M.instance.dictionary.Count - 1)
                    {   // 문제가 더 남아있을 때
                        GameManager_M.instance.NextLevel();
                    }
                    else
                    {   // 모든 문제를 풀었을 때
                        GameManager_M.instance.GameClear();
                    }
                }
                else if (hpManager.HP == 0)
                {   // 플레이어 HP가 0일 때 게임 종료
                    GameManager_M.instance.GameOver();
                }
            }
        }
    }

    // 노말, 하드모드에 따라 적 이동속도를 지정해주는 메소드
    public float speedChange(bool hardMode = false)
    {   // 초기값 false
        if (hardMode)
            return 4f;
        else
            return 2.5f;
    }

    public void InitEnemyInfo(Enemy_M enemy)
    {
        result = enemy.result;
        //transform.position = enemy.spawnPos;
        this.transform.Find("TextMesh").GetComponent<TextMesh>().text = enemy.meshNum;
        gameObject.SetActive(true);
    }

    // 문제 맞추고 딜레이 주기 위한 코루틴
    IEnumerator NextWaveDelay()
    {
        yield return new WaitForSeconds(1f);
    }

    // 총알에 맞은 게임오브젝트 삭제하는 메소드
    public void getDamage()
    {
        gameObject.SetActive(false);
    }

    // 답이 아닌지 맞는지 리턴해주는 메소드
    public bool isRightResult()
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
        if (WaveManager_M.instance != null)
        {
            WaveManager_M.instance.DieEnemy();
        }
    }
}
