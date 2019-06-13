using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo_E : MonoBehaviour
{
    private int result;

    public void InitEnemyInfo(Enemy_E enemy)
    {
        result = enemy.result;
        transform.position = enemy.spawnPos;
        this.transform.Find("TextMesh").GetComponent<TextMesh>().text = enemy.meshNum;
        gameObject.SetActive(true);
    }
    public void getDamage()
    {
        gameObject.SetActive(false);
    }
    public int isRightResult()
    {
        return result;
    }
    private void OnDisable()
    {
        if (WaveManager_E.instance != null)
        {
            WaveManager_E.instance.DieEnemy();
        }
    }
}
