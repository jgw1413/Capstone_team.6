using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager_M : MonoBehaviour
{
    public static WaveManager_M instance;

    private Dictionary<int, List<Enemy_M>> waveEnemyDic = new Dictionary<int, List<Enemy_M>>();
    private int curWave = -1;
    private int curWaveEnemyCount;

    private int easyQuestCount = 5;     // 해당 숫자만큼 문제를 맞춰야 다음 난이도로 넘어감
    private int normalQuestCount = 5;   // 해당 숫자만큼 문제를 맞춰야 다음 난이도로 넘어감
    private int curQuestCount = 0;      // 현재 문제 수

    public enum Difficult
    {
        Easy, Normal
    }

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartWave();

    }

    public void InitWave()
    {
        //Enemy_M enemy;
        //List<Enemy_M> enemyList = new List<Enemy_M>();
        //bool Result;
        //int xVector = -6;
        //Vector3 v;
        //string ch;
        //for (int i = 0; i < questDic[Difficult.Easy].Count;i++) {
        //    for (int j = 0; j < 6; j++)
        //    {
        //        if (questDic[Difficult.Easy][j].answer == j)
        //        {
        //            Result = false;
        //        }
        //        else
        //        {
        //            Result = true;
        //        }
        //        v = new Vector3(xVector, 0.5f, 9);
        //        ch = questDic[Difficult.Easy][i].stringList[j];
        //        enemy = new Enemy_M("Cube", Result, new Vector3(-6, 0.5f, 9), ch);
        //        enemyList.Add(enemy);
        //    }
        //}
        //waveEnemyDic.Add(0, enemyList);
        Enemy_M enemy;
        List<Enemy_M> enemyList = new List<Enemy_M>();
        enemy = new Enemy_M("Cube1", true, new Vector3(-6, 0.5f, 9), "1");
        enemyList.Add(enemy);
        enemy = new Enemy_M("Cube2", true, new Vector3(-4, 0.5f, 9), "+");
        enemyList.Add(enemy);
        enemy = new Enemy_M("Cube3", true, new Vector3(-2, 0.5f, 9), "4");
        enemyList.Add(enemy);
        enemy = new Enemy_M("Cube4", false, new Vector3(0, 0.5f, 9), "8");
        enemyList.Add(enemy);
        enemy = new Enemy_M("Cube5", true, new Vector3(2, 0.5f, 9), "=");
        enemyList.Add(enemy);
        enemy = new Enemy_M("Cube6", true, new Vector3(4, 0.5f, 9), "5");
        enemyList.Add(enemy);
        waveEnemyDic.Add(0, enemyList);

        enemyList = new List<Enemy_M>();
        enemy = new Enemy_M("Cube1", false, new Vector3(-4, 0.5f, 9), "5");
        enemyList.Add(enemy);
        enemy = new Enemy_M("Cube2", true, new Vector3(-2, 0.5f, 9), "3");
        enemyList.Add(enemy);
        enemy = new Enemy_M("Cube3", true, new Vector3(0, 0.5f, 9), "-");
        enemyList.Add(enemy);
        enemy = new Enemy_M("Cube4", true, new Vector3(2, 0.5f, 9), "2");
        enemyList.Add(enemy);
        enemy = new Enemy_M("Cube5", true, new Vector3(4, 0.5f, 9), "=");
        enemyList.Add(enemy);
        enemy = new Enemy_M("Cube6", true, new Vector3(6, 0.5f, 9), "1");
        enemyList.Add(enemy);
        waveEnemyDic.Add(1, enemyList);
    }
    /*
    public void Wave(int curwave)
    {
        Enemy enemy;
        List<Enemy> enemyList = new List<Enemy>();
        if (curwave == 0)
        {
            Debug.Log("1단계");
            enemy = new Enemy("Cube1", true, new Vector3(0, 0.5f, 15));
            enemyList.Add(enemy);
            enemy = new Enemy("Cube2", false, new Vector3(2, 0.5f, 15));
            enemyList.Add(enemy);
            enemy = new Enemy("Cube3", true, new Vector3(-2, 0.5f, 15));
            enemyList.Add(enemy);
            enemy = new Enemy("Cube4", true, new Vector3(4, 0.5f, 15));
            enemyList.Add(enemy);
            enemy = new Enemy("Cube5", true, new Vector3(-4, 0.5f, 15));
            enemyList.Add(enemy);
            enemy = new Enemy("Cube6", true, new Vector3(6, 0.5f, 15));
            enemyList.Add(enemy);
        }
        else if (curwave == 1)
        {
            Debug.Log("2단계");

            enemy = new Enemy("Cube1", true, new Vector3(0, 0.5f, 15));
            enemyList.Add(enemy);
            enemy = new Enemy("Cube2", true, new Vector3(2, 0.5f, 15));
            enemyList.Add(enemy);
            enemy = new Enemy("Cube3", true, new Vector3(-2, 0.5f, 15));
            enemyList.Add(enemy);
            enemy = new Enemy("Cube4", true, new Vector3(4, 0.5f, 15));
            enemyList.Add(enemy);
            enemy = new Enemy("Cube5", false, new Vector3(-4, 0.5f, 15));
            enemyList.Add(enemy);
            enemy = new Enemy("Cube6", true, new Vector3(6, 0.5f, 15));
            enemyList.Add(enemy);
        }
        waveEnemyDic.Add(curwave, enemyList);
    }
    */
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
        {
            List<Enemy_M> enemyList = waveEnemyDic[curWave];
            curWaveEnemyCount = enemyList.Count;
            Debug.Log(enemyList.Count);

            for (int i = 0; i < enemyList.Count; i++)
            {
                // 적 생성 코드
                GameObject enemyObj = ObjectPoolContainer.Instance.Pop("Enemy_M");
                enemyObj.name=enemyList[i].name;
                EnemyInfo_M enemyInfo = enemyObj.GetComponent<EnemyInfo_M>();
                enemyInfo.InitEnemyInfo(enemyList[i]);
            }
        }
        else
        {
            Debug.Log("??");
            GameManager_M.instance.GameClear();// Game Clear
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
