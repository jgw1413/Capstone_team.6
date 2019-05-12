using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_M : MonoBehaviour
{
    int basic_layer = 1 << 11;
    int math_layer = 1 << 12;
    int menu_layer = 1 << 13;

    [HideInInspector]
    public GameObject holder;
    public GameObject pointer;
    public float thickness = 0.002f;
    public Color color;

    private GameObject target;
    private GameManager_M gameManager_M;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller device;
    private SteamVR_Controller.Device _controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }



    void Start()
    {
        gameManager_M = GameObject.Find("GameManager").GetComponent<GameManager_M>();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        RayRendering();
    }

    void Update()
    {
        if (_controller == null)
        {
            return;
        }

        if (_controller.GetHairTriggerDown())
        {
            TriggerDown();

            target = GetClickedObject();
            if (target.tag == "enemy")
            {   // enemy를 눌렀을 때
                EnemyInfo_M targetInfo = target.GetComponent<EnemyInfo_M>(); // EnemyInfo를 가져옴
                targetInfo.getDamage();  // EnemyInfo의 getDamage() 메소드 호출
                if (targetInfo.isRightResult())
                {   // 클릭한 enemy가 false면 true 반환, true면 false 반환
                    gameManager_M.NextLevel();    // true를 얻어옴(정답) = 다음 레벨
                }
                else
                {
                    gameManager_M.GameOver();     // false를 얻어옴(틀림) = 게임 오버
                }
            }
            //오류
            else
            {   // 빈 공간을 눌렀을 때
                Debug.Log("적 없음");
            }
        }

        //if (_controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        //{
        //    Debug.Log("asdf");
        //}
        //if (_controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        //{
        //    Debug.Log("menu");
        //}


    }

    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;

        Ray ray = new Ray(transform.position, transform.forward);
        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
        {   //마우스 근처에 오브젝트가 있는지 확인
            target = hit.collider.gameObject;
        }
        return target;
    }

    public void TriggerDown()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, basic_layer))
        {
            SceneManager.LoadScene("Basic");
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, math_layer))
        {
            SceneManager.LoadScene("Math");
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, menu_layer))
        {
            SceneManager.LoadScene("Main");
        }
    }

    private void RayRendering()
    {
        holder = new GameObject();
        holder.transform.parent = this.transform;
        holder.transform.localPosition = Vector3.zero;
        holder.transform.localRotation = Quaternion.identity;
        pointer.transform.parent = holder.transform;
        pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
        pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
        pointer.transform.localRotation = Quaternion.identity;

        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", color);
        pointer.GetComponent<MeshRenderer>().material = newMaterial;
    }
}
