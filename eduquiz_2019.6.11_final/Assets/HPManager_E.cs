﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager_E : MonoBehaviour
{
    public GameObject[] heart = new GameObject[5];
    // 플레이어의 몫을 표시할 오브젝트(최대 5개)
    public int HP;          // 플레이어 HP 저장

    private void Start()
    {
        HP = 30;
        Debug.Log(HP + "플");

        Debug.Log(HP + "플하트체크");
        HeartCheck();
    }

    public void HeartCheck()
    {
        if (HP == 60)
        {   // 최대 체력 50
            HP = 50;
        }
        for (int i = 0; i < HP / 10; i++)
        {   // 0부터 현재 HP까지의 하트 온
            heart[i].SetActive(true);
        }
        for (int i = HP / 10; i < heart.Length; i++)
        {   // 현재 HP부터 마지막 배열까지의 하트 오프
            if (HP == 50)
            {   // 현재 체력이 50이면 하트를 오프할 필요가 없다.
                break;
            }
            heart[i].SetActive(false);
        }
        
    }
}
