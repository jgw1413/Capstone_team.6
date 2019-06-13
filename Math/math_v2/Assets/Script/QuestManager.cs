using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public Dictionary<WaveManager.Difficult, List<Quest>> GetQuest()
    {
        Dictionary<WaveManager.Difficult, List<Quest>> questDic = new Dictionary<WaveManager.Difficult, List<Quest>>();

        List<Quest> Elist = MakeEasyQuestList();
        questDic[WaveManager.Difficult.Easy]=Elist;

        List<Quest> Nlist = MakeNomalQuestList();
        questDic[WaveManager.Difficult.Normal] = Nlist;

        return questDic;
    }

    public List<Quest> MakeEasyQuestList()
    {
        List<Quest> list = new List<Quest>();
        Quest quest = MakeOneQuest("1+45=5", 3);
        list.Add(quest);
        return list;
    }
    public List<Quest> MakeNomalQuestList()
    {
        List<Quest> list = new List<Quest>();
        Quest quest = MakeOneQuest("15+29=34", 3);
        list.Add(quest);
        return list;
    }

    public Quest MakeOneQuest(string ME, int answerNum)
    {
        List<string> tmpList = new List<string>();
        char[] tmpCh = ME.ToCharArray();
        for (int i = 0; i < tmpCh.Length; i++)
        {
            tmpList.Add(tmpCh.ToString());
        }
        Quest quest = new Quest();
        quest.stringList = tmpList;
        quest.answer = answerNum;
        return quest;
    }
    void Awake()
    {
        instance = this;
    }

}//난이도, 문제들 항목 > 문제 각각의 숫자, 정답 번호

public struct Quest
{
    public List<string> stringList; // 1, +, 4, =, 5
    public int answer;
}
