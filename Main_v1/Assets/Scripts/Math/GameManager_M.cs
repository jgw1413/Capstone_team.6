using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_M : MonoBehaviour
{
    public static GameManager_M instance;
    public WaveManager_M waveManager;
    public GameObject Panel;
    public GameObject Buttons;
    private GameObject target;
    public GameObject EnemyPrefab;

    void Awake()
    {
        instance = this;
        InitObjectPool();
        waveManager.InitWave();
    }

    void InitObjectPool()
    {
        ObjectPoolContainer.Instance.CreateObjectPool("Enemy_M", EnemyPrefab, 15);
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
                EnemyInfo_M targetInfo = target.GetComponent<EnemyInfo_M>();
                targetInfo.getDamage();
                if (targetInfo.isRightResult())
                {
                    NextLevel();
                }
                else
                {
                    GameOver();
                }
            }
            else
            {
            }
        }

    }

    public void GameClear()
    {
        Image image = Panel.GetComponent<Image>();
        image.DOFade(0.8f, 3);
        StartCoroutine("ButtonsOn");
        Debug.Log("게임클리어");
    }

    public void GameOver()
    {
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
        SceneManager.LoadScene("Math");
    }


    private void OnDestroy()
    {
        instance = null;
    }
}
