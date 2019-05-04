using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public GameObject CubePrepab;

    private Dictionary<int, List<Enemy>> waveEnemyDic = new Dictionary<int, List<Enemy>>();

    private int curWave = -1;    // 현재 스테이지 변수

    private int curWaveEnemyCount;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartWave();    // 게임 스타트
    }

    public Text stageText;          // 스테이지 ui
    public Text QuizManagerText;    // 지문 ui

    void UIoutput(int cur)          // UI 출력 메소드
    {
        // 현재 스테이지와 같은 딕셔너리의 문제를 ui로 출력
        stageText.text = "Stage " + (cur + 1);  // 스테이지는 1부터 시작이기 때문에 +1 해준다.
        QuizManagerText.text = QuizManager.instance.dictionary[cur].quiz;
    }

    public void InitWave()  // 웨이브 생성 메소드
    {
        Enemy enemy;

        List<Enemy> enemyList= new List<Enemy>();

        for (int i = 0; i < QuizManager.instance.dictionary.Count; i++) {

            enemyList = new List<Enemy>();

            enemy = new Enemy("Cube1", true, new Vector3(-15, 0.5f, 20.5f), 
                QuizManager.instance.dictionary[i].ans1);
            enemyList.Add(enemy);   // 적 생성

            enemy = new Enemy("Cube2", true, new Vector3(0.5f, 0.5f, 23.5f), 
                QuizManager.instance.dictionary[i].ans2);
            enemyList.Add(enemy);

            enemy = new Enemy("Cube3", true, new Vector3(8, 0.5f, 21), 
                QuizManager.instance.dictionary[i].ans3);
            enemyList.Add(enemy);

            enemy = new Enemy("Cube4", false, new Vector3(15, 0.5f, 24), 
                QuizManager.instance.dictionary[i].ans4);
            enemyList.Add(enemy);

            waveEnemyDic.Add(i, enemyList);     // 웨이브 저장
        }
    }

    public void DieEnemy()
    {
        curWaveEnemyCount--;
        if (curWaveEnemyCount == 0)
        {
            StartWave();
        }
    }

    void StartWave()
    {
        curWave++;
        Debug.Log(curWave + "단계");
        
        if (waveEnemyDic.ContainsKey(curWave))
        {   // waveEnemyDic에 현재 스테이지인 curWave단계의 키가 존재 할때
            List<Enemy> enemyList = waveEnemyDic[curWave];
            curWaveEnemyCount = enemyList.Count;  // curWaveEnemyCount에 enemyList 개수만큼

            for (int i = 0; i < enemyList.Count; i++)
            {
                // enemyList 개수만큼 적 생성 
                GameObject enemyObj = Instantiate(CubePrepab, this.transform);
                enemyObj.name = enemyList[i].name;
                EnemyInfo enemyInfo = enemyObj.GetComponent<EnemyInfo>();
                enemyInfo.InitEnemyInfo(enemyList[i]);
            }

            UIoutput(curWave);   // UI 출력 메소드
        }
        else
        {   // 모든 문제를 다 풀어서 더이상 문제가 남아있지 않을 때
            Debug.Log("??");
            GameManager.instance.GameClear();  //  GameClear() 메소드 호출
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
