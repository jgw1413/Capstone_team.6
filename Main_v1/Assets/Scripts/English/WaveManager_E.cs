using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager_E : MonoBehaviour
{
    public static WaveManager_E instance;

    public GameObject CubePrepab;

    private Dictionary<int, List<Enemy_E>> waveEnemyDic = new Dictionary<int, List<Enemy_E>>();
    private int curWave = -1;
    private int curWaveEnemyCount;

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
        Enemy_E enemy;
        List<Enemy_E> enemyList = new List<Enemy_E>();
        enemy = new Enemy_E("Cube1", 1, new Vector3(-6, 5f, 12), "O");
        enemyList.Add(enemy);
        enemy = new Enemy_E("Cube2", 2, new Vector3(-4, 2.3f, 15), "N");
        enemyList.Add(enemy);
        enemy = new Enemy_E("Cube3", 5, new Vector3(-2, 4f, 14), "Y");
        enemyList.Add(enemy);
        enemy = new Enemy_E("Cube4", 4, new Vector3(0, 1.5f, 10), "E");
        enemyList.Add(enemy);
        enemy = new Enemy_E("Cube5", 0, new Vector3(2, 1f, 8), "M");
        enemyList.Add(enemy);
        enemy = new Enemy_E("Cube6", 3, new Vector3(4, 3f, 9), "K");
        enemyList.Add(enemy);
        waveEnemyDic.Add(0, enemyList);

        //enemyList = new List<Enemy>();
        //enemy = new Enemy("Cube1", false, new Vector3(-4, 0.5f, 9), "5");
        //enemyList.Add(enemy);
        //enemy = new Enemy("Cube2", true, new Vector3(-2, 0.5f, 9), "3");
        //enemyList.Add(enemy);
        //enemy = new Enemy("Cube3", true, new Vector3(0, 0.5f, 9), "-");
        //enemyList.Add(enemy);
        //enemy = new Enemy("Cube4", true, new Vector3(2, 0.5f, 9), "2");
        //enemyList.Add(enemy);
        //enemy = new Enemy("Cube5", true, new Vector3(4, 0.5f, 9), "=");
        //enemyList.Add(enemy);
        //enemy = new Enemy("Cube6", true, new Vector3(6, 0.5f, 9), "1");
        //enemyList.Add(enemy);
        //waveEnemyDic.Add(1, enemyList);
        //웨이브 한번에 저장
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
            List<Enemy_E> enemyList = waveEnemyDic[curWave];
            curWaveEnemyCount = enemyList.Count;

            for (int i = 0; i < enemyList.Count; i++)
            {
                // 적 생성 코드
                GameObject enemyObj = Instantiate(CubePrepab, this.transform);
                enemyObj.name=enemyList[i].name;
                EnemyInfo_E enemyInfo = enemyObj.GetComponent<EnemyInfo_E>();
                enemyInfo.InitEnemyInfo(enemyList[i]);
            }
        }
        else
        {
            Debug.Log("??");
            GameManager_E.instance.GameClear();// Game Clear
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
