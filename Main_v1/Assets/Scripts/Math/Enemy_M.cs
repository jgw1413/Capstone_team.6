using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_M
{
    public string name;             // 이름 ( 프리팹 이름 )
    public bool result;              // 정답 유무
    public Vector3 spawnPos;        // 생성 위치
    public string meshNum;
    //public Vector2 arrivePos;       // 생성 후 도착 위치
    //public float arriveMovingTime;  // 생성 후 도착 위치까지 이동 시간

    public Enemy_M(string name, bool result, Vector3 spawnPos, string meshNum//, Vector3 arrivePos, float arriveMovingTime,
        )
    {
        this.name = name;
        this.result = result;              
        this.spawnPos = spawnPos;
        this.meshNum = meshNum;
    //this.arrivePos = arrivePos;
    //this.arriveMovingTime = arriveMovingTime;
    }
}
