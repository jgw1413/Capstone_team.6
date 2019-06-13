using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BulletCtrl_M : MonoBehaviour
{
    
    private float BulletSpeed = 150.0f;  // 총알이 날아가는 속도



    void Start()
    {
        Destroy(gameObject, 3.0f);  // 총알 3초 후 제거
    }


    void Update()
    {
        transform.position += transform.forward * BulletSpeed * Time.deltaTime;
    }
}
