using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager_B : MonoBehaviour
{
    public static QuizManager_B instance;

    public Dictionary<int, Item_B> dictionary;
    public Dictionary<int, Item_B> dictionary_temp;   // 문제를 섞기 위한 임시 변수

    void Awake()
    {
        instance = this;

        dictionary = new Dictionary<int, Item_B>();

        int num;

        // 문제 추가
        num = 0;
        dictionary.Add(num, new Item_B(num, 1,
            "한국의 수도는 ??", "대전", "서울", "부산", "광주"));
        num = 1;
        dictionary.Add(num, new Item_B(num, 2,
            "사과는 영어로 ??.", "orange", "apply", "apple", "banana"));
        num = 2;
        dictionary.Add(num, new Item_B(num, 0,
            "1  2  4  8 ( )", "16", "1", "2", "3"));
        num = 3;
        dictionary.Add(num, new Item_B(num, 0,
            "그리스의 수도는 ??.", "아테네", "파트라스", "케르키라", "요아니나"));
        num = 4;
        dictionary.Add(num, new Item_B(num, 2,
            "OS가 아닌 것은?", "Windows", "Linux", "Java", "Mac"));
        num = 5;
        dictionary.Add(num, new Item_B(num, 2,
            "일본어인 '부타(豚)'를 한국어로 번역하면 무슨 말 일까요?", "닭", "소", "돼지", "물고기"));
        num = 6;
        dictionary.Add(num, new Item_B(num, 3,
            "가장 뜨거운 과일은?", "멜론", "수박", "딸기", "천도복숭아"));

        RandomNumber();
    }


    void RandomNumber() {

        System.Random prng = new System.Random();

        dictionary_temp = new Dictionary<int, Item_B>();

        for (int i = 0; i < dictionary.Count - 1; i++) {
            int randomIndex = prng.Next(i, dictionary.Count);   // randomIndex에 랜덤 숫자가 지정
            dictionary_temp[randomIndex] = dictionary[i];
            dictionary[i] = dictionary[randomIndex];
            dictionary[randomIndex] = dictionary_temp[randomIndex];
        }
    }
}