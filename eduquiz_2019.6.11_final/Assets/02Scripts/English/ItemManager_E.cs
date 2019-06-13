﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager_E : MonoBehaviour
{
    public static ItemManager_E instance;

    public Transform[] ItemSpawnPoint = new Transform[4];   
    // 아이템 생성 위치를 저장하는 배열

    public GameObject HPItem;       // HPItem 오브젝트를 저장하는 변수
    public GameObject TimerItem;    // TimerItem 오브젝트를 저장하는 변수

    private float HPSpawnTime;        // HP 아이템 생성 시간을 저장하는 변수
    private float TimerSpawnTime;        // Timer 아이템 생성 시간을 저장하는 변수

    public static int itemCount;      // 동시 최대 생성가능한 아이템 개수를 저장하는 변수
                                      // 아이템이 사라졌을 때 변경해주기 위해 public 선언

    public int[] itemSpawnChk = new int[4];
    // 랜덤숫자 반복 방지를 위한 체크배열 변수(스폰지역 수만큼 배열크기 지정)


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        itemCount = 3;
        // 아이템 최대 생성 개수
        HPSpawnTime = 6.0f;
        // 처음 HPSpawnTime 60초로 지정
        TimerSpawnTime = 6.0f;
        // 처음 TimerSpawnTime 60초로 지정
        StartCoroutine("HPSpawn");
        // HP 아이템을 생성하는 코루틴 함수를 사용
        StartCoroutine("TimerSpawn");
        // Timer 아이템을 생성하는 코루틴 함수를 사용
    }

    // HP 아이템 생성하는 함수
    IEnumerator HPSpawn()
    {
        while (true)
        {   // while문으로 코루틴을 지속
            yield return new WaitForSeconds(HPSpawnTime);
            // HPSpawnTime만큼 대기
            EventItemSpawnPoint(HPItem);
            // HP 아이템 생성
            HPSpawnTime = Random.Range(6, 10);
            // HP 아이템 생성 간격 다시 지정
        }
        yield return null;
    }

    // TImer 아이템 생성하는 함수
    IEnumerator TimerSpawn()
    {
        while (true)
        {   // while문으로 코루틴을 지속
            yield return new WaitForSeconds(TimerSpawnTime);
            // TimerSpawnTime만큼 대기
            EventItemSpawnPoint(TimerItem);
            // Timer 아이템 생성
            TimerSpawnTime = Random.Range(6, 10);
            // Timer 아이템 생성 간격 다시 지정
        }
        yield return null;
    }

    // 아이템의 생성위치를 결정해서 생성해주는 메소드
    // 아이템을 처치했을 때 그 지점은 초기화 해주고 생성 가능한 아이템 개수 +1 해주면 된다.
    void EventItemSpawnPoint(GameObject gameObject)
    {
        while (true)
        {
            int itemPoint = Random.Range(0, itemSpawnChk.Length);
            // itemSpawnChk 배열의 크기만큼 랜덤시드

            if (itemCount == 0)
            {   // 생성가능한 아이템 개수가 0일때
                break;
            }

            else if (itemCount != 0)
            {   // 생성가능한 아이템 개수가 0이 아닐때
                if (itemSpawnChk[itemPoint] == 0)
                {   // 아이템이 생성안된 곳일 때
                    itemSpawnChk[itemPoint] = 1;
                    // 생성할 곳 체크


                    Instantiate(gameObject, ItemSpawnPoint[itemPoint].position, ItemSpawnPoint[itemPoint].rotation);
                    // 아이템 생성

                    itemCount--;
                    // 생성가능한 아이템 개수 -1
                    break;
                }
            }
        } // while

        // 아이템을 생성한 후,
        // itemSpawnChk[]을 맨 앞부터 검사하여 생성가능(0)한 위치가 존재하면 break,
        // 맨 마지막이 생성불가능(1)이면 모든 배열 초기화.
        for (int i = 0; i < itemSpawnChk.Length; i++)
        {
            if (itemSpawnChk[i] == 0)
            {
                break;
            }
            else if (itemSpawnChk[itemSpawnChk.Length - 1] == 1)
            {
                for (int j = 0; j < itemSpawnChk.Length; j++)
                {
                    itemSpawnChk[j] = 0;
                    Debug.Log("아이템 생성 배열 초기화.");
                }
            }
        } // for
    }
}