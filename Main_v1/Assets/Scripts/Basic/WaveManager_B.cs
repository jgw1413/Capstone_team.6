using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager_B : MonoBehaviour
{
    public static WaveManager_B instance;

    public GameObject CubePrepab;

    private Dictionary<int, List<Enemy_B>> waveEnemyDic = new Dictionary<int, List<Enemy_B>>();

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
        QuizManagerText.text = QuizManager_B.instance.dictionary[cur].quiz;
    }

    public void InitWave()  // 웨이브 생성 메소드
    {
        Enemy_B enemy;
        Dictionary<int, Item_B> quizDictionary = QuizManager_B.instance.dictionary;
        List<Enemy_B> enemyList= new List<Enemy_B>();

        for (int i = 0; i < quizDictionary.Count; i++) {

            enemyList = new List<Enemy_B>();

            enemy = new Enemy_B("Cube1", (0 != quizDictionary[i].resultNum), new Vector3(-15, 0.5f, 20.5f),
                quizDictionary[i].ans1);
            enemyList.Add(enemy);   // 적 생성

            enemy = new Enemy_B("Cube2", (1 != quizDictionary[i].resultNum), new Vector3(0.5f, 0.5f, 23.5f),
                quizDictionary[i].ans2);
            enemyList.Add(enemy);

            enemy = new Enemy_B("Cube3", (2 != quizDictionary[i].resultNum), new Vector3(8, 0.5f, 21),
                quizDictionary[i].ans3);
            enemyList.Add(enemy);

            enemy = new Enemy_B("Cube4", (3 != quizDictionary[i].resultNum), new Vector3(15, 0.5f, 24),
                quizDictionary[i].ans4);
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
            List<Enemy_B> enemyList = waveEnemyDic[curWave];
            curWaveEnemyCount = enemyList.Count;  // curWaveEnemyCount에 enemyList 개수만큼

            for (int i = 0; i < enemyList.Count; i++)
            {
                // enemyList 개수만큼 적 생성 
                GameObject enemyObj = Instantiate(CubePrepab, this.transform);
                enemyObj.name = enemyList[i].name;
                EnemyInfo_B enemyInfo = enemyObj.GetComponent<EnemyInfo_B>();
                enemyInfo.InitEnemyInfo(enemyList[i]);
            }

            UIoutput(curWave);   // UI 출력 메소드
        }
        else
        {   // 모든 문제를 다 풀어서 더이상 문제가 남아있지 않을 때
            Debug.Log("??");
            GameManager_B.instance.GameClear();  //  GameClear() 메소드 호출
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
