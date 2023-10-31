using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RNGEventQuestion
{
    public string name;
    public string description;
    public string[] answers;
    public int correctAnswer;
    public int reward;
    public string explaination;
    public RNGEventQuestion(string Name, string Description, string[] Answers, int Correct, int Reward, string Explain)
    {
        name = Name;
        description = Description;
        answers = Answers;
        correctAnswer = Correct;
        reward = Reward;
        explaination = Explain;
    }
}
