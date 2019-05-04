using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public static QuizManager instance;

    public Dictionary<int, Item> dictionary;
    public Dictionary<int, Item> dictionary_temp;   // 문제를 섞기 위한 임시 변수

    void Awake()
    {
        instance = this;

        dictionary = new Dictionary<int, Item>();

        int num;

        // 문제 추가
        num = 0;
        dictionary.Add(num, new Item(num,
            "한국의 수도는 ( )이다.", "대전", "부산", "광주", "서울"));
        num = 1;
        dictionary.Add(num, new Item(num,
            "사과는 영어로 ( )이다.", "orange", "apply", "sa", "apply"));
        num = 2;
        dictionary.Add(num, new Item(num,
            "1  2  4  8 ( )", "1", "2", "3", "16"));
        num = 3;
        dictionary.Add(num, new Item(num,
            "그리스의 수도는 ( )이다.", "몽", "뭉", "멍", "아테네"));
        num = 4;
        dictionary.Add(num, new Item(num,
            "겨울에 많이 사용하는 끈은?", "뜨끔", "야", "옹", "뜨끈뜨끈"));
        num = 5;
        dictionary.Add(num, new Item(num,
            "모자 여러개가 모여 있으면?", "루피", "상디", "조로", "밀짚모자"));
        num = 6;
        dictionary.Add(num, new Item(num,
            "가장 뜨거운 과일은?", "멜론", "수박", "딸기", "천도복숭아"));

        RandomNumber();
    }

    void RandomNumber() {

        System.Random prng = new System.Random();

        dictionary_temp = new Dictionary<int, Item>();

        for (int i = 0; i < dictionary.Count - 1; i++) {
            int randomIndex = prng.Next(i, dictionary.Count);   // randomIndex에 랜덤 숫자가 지정
            dictionary_temp[randomIndex] = dictionary[i];
            dictionary[i] = dictionary[randomIndex];
            dictionary[randomIndex] = dictionary_temp[randomIndex];
        }
    }
}