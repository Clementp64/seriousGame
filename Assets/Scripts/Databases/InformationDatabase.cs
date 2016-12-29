using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InformationDatabase : ScriptableObject {

    public List<Information> info = new List<Information>();

    public void Initialize() {
        foreach(Information myInfo in info){
            myInfo.seen = false;
            myInfo.asked = false;
        }
    }

    public int GetNbStatements() {
        return info.Count;
    }
    public string GetStatement(int i) {
        return info[i].statement;
    }
    public string GetRandomStatement() {
        int i = Random.Range(0, info.Count);
        info[i].seen = true;
        return info[i].statement;
    }
    public string[] GetRandomQuestion() {
        int i = 0;
        bool found = false;
        while (i < info.Count && !found) {
            if (info[i].seen && !info[i].asked)
                found = true;
            else
                i++;
        }
        if (!found) {
            Debug.Log("NOT ENOUGH SIGNS FOR QUESTION");
            i = 0;
        }
        info[i].asked = true;
        string[] result = new string[5];
        result[0] = info[i].question;
        result[1] = info[i].answer1;
        result[2] = info[i].answer2;
        result[3] = info[i].answer3;
        result[4] = info[i].correctAnswer.ToString();
        return result;
    }

}

[System.Serializable]
public class Information {
    public string statement;
    public string question;
    public string answer1;
    public string answer2;
    public string answer3;
    [Range(0, 2)]
    public int correctAnswer;
    public bool seen = false;
    public bool asked = false;
}
