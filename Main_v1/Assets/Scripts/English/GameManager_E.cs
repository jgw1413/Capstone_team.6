using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_E : MonoBehaviour
{
    public static GameManager_E instance;
    public WaveManager_E waveManager;
    public GameObject CubePrepab;
    public GameObject Panel;
    public GameObject Buttons;

    public GameObject ResultImage;
    private GameObject target;

    int i = 0;

    void Awake()
    {
        instance = this;
        waveManager.InitWave();
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
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 포인트 근처 좌표를 만든다. 
        
        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))   //마우스 근처에 오브젝트가 있는지 확인
        {
            target = hit.collider.gameObject;
        }
        return target;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = GetClickedObject();
            if (target.tag == "enemy")
            {
                EnemyInfo_E targetInfo = target.GetComponent<EnemyInfo_E>();
                targetInfo.getDamage();

                if (i != targetInfo.isRightResult())
                    GameOver();
                if (i == 6)
                {
                    NextLevel();
                    i=0;
                }
                i++;
            }
        }
    }

    public void GameClear()
    {
        ResultImage.SetActive(false);
        Image image = Panel.GetComponent<Image>();
        image.DOFade(0.8f, 3);
        StartCoroutine("ButtonsOn");
        Debug.Log("게임클리어");
    }

    public void GameOver()
    {
        ResultImage.SetActive(false);
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
        SceneManager.LoadScene("English");
    }


    private void OnDestroy()
    {
        instance = null;
    }
}
