using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_B : MonoBehaviour
{
    public static GameManager_B instance;
    public WaveManager_B waveManager;
    public GameObject CubePrepab;
    public GameObject Panel;
    //public GameObject quizPanel;
    public GameObject Buttons;
    private GameObject target;
    public Controller controller;

    void Awake()
    {
        instance = this;

        waveManager.InitWave();

        controller = GetComponent<Controller>();
    }

   
    IEnumerator ButtonsOn()
    {
        yield return new WaitForSeconds(1f);
        Buttons.SetActive(true);
    }

    private GameObject GetClickedObject()
    {   
        RaycastHit hit;
        GameObject target = null;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        //마우스 포인트 근처 좌표를 만든다. 
        
        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
        {   //마우스 근처에 오브젝트가 있는지 확인
            target = hit.collider.gameObject;
        }
        return target;
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{   // 마우스 왼쪽을 눌렀을 때
        //    Debug.Log("트리거눌림");
        //    target = GetClickedObject();
        //    if (target.tag == "enemy")
        //    {   // enemy를 눌렀을 때
        //        EnemyInfo_B targetInfo = target.GetComponent<EnemyInfo_B>();  // EnemyInfo를 가져옴
        //        targetInfo.getDamage();  // EnemyInfo의 getDamage() 메소드 호출
        //        targetInfo.EnemyDeathEffect();  // 적 죽음 파티클 실행
        //        if (targetInfo.isRightResult())
        //        {   // 클릭한 enemy가 false면 true 반환, true면 false 반환
        //            NextLevel();    // true를 얻어옴(정답) = 다음 레벨
        //        }
        //        else
        //        {
        //            GameOver();     // false를 얻어옴(틀림) = 게임 오버
        //        }
        //    }
        //    else
        //    {   // 빈 공간을 눌렀을 때

        //    }
        //}
    }

    public void GameClear()
    {
        //quizPanel.SetActive(false);
        Image image = Panel.GetComponent<Image>();
        image.DOFade(0.8f, 3);
        StartCoroutine("ButtonsOn");
        Debug.Log("게임클리어");
    }

    public void GameOver()
    {
        //quizPanel.SetActive(false);
        Image image = Panel.GetComponent<Image>();
        image.DOFade(0.8f, 3);
        StartCoroutine("ButtonsOn");
        Debug.Log("게임오버");
    }

    public void NextLevel()
    {
        GameObject[] allCube = GameObject.FindGameObjectsWithTag("enemy");
        foreach(GameObject i in allCube)
        {
            Destroy(i.gameObject);
        }
        Debug.Log("레벨업");
    }

    public void onRetryButtonClicked()
    {
        SceneManager.LoadScene("Basic");
    }


    private void OnDestroy()
    {
        instance = null;
    }
}
