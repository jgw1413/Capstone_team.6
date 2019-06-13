using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    private bool result;

    public void InitEnemyInfo(Enemy enemy)
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
        if (WaveManager.instance != null)
        {
            WaveManager.instance.DieEnemy();
        }
    }
}
