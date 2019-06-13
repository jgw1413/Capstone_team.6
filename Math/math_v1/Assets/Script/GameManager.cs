using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public WaveManager waveManager;
    public GameObject CubePrepab;
    public GameObject Panel;
    public GameObject RetryButton;

    private GameObject target;
    
    void Awake()
    {
        instance = this;
        waveManager.InitWave();
    }

    IEnumerator RetryButtonOn()
    {
        yield return new WaitForSeconds(1f);
        RetryButton.SetActive(true);
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
                EnemyInfo targetInfo = target.GetComponent<EnemyInfo>();
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
        StartCoroutine("RetryButtonOn");
        Debug.Log("게임클리어");
    }

    public void GameOver()
    {
        Image image = Panel.GetComponent<Image>();
        image.DOFade(0.8f, 3);
        StartCoroutine("RetryButtonOn");
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
        SceneManager.LoadScene("SampleScene");
    }


    private void OnDestroy()
    {
        instance = null;
    }
}
