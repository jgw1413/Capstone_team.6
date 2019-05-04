using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int num;         // 문제 번호
    public string quiz;     // 문제 지문
    public string ans1;     // 답1
    public string ans2;
    public string ans3;
    public string ans4;

    public Item(int _num, string _quiz, string _ans1, string _ans2, string _ans3, string _ans4)
    {
        this.num = _num;
        this.quiz = _quiz;
        this.ans1 = _ans1;
        this.ans2 = _ans2;
        this.ans3 = _ans3;
        this.ans4 = _ans4;
    }
} // class