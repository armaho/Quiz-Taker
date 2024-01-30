using System;
using System.Collections.Generic;
using System.Linq;

namespace Question;

public class Exam
{
    #region Fields

    private List<(IQuestion question, AnswerState answerState, string givenAnswer)> _examResult = new();

    #endregion

    #region Properties

    public IReadOnlyList<(IQuestion question, AnswerState answerState, string givenAnswer)> ExamResult => _examResult;

    public decimal Grade => (decimal)_examResult.Count(er => er.answerState == AnswerState.Correct) / _examResult.Count;

    #endregion

    #region

    public void AddQuestion(IQuestion question)
    {
        _examResult.Add((question: question, answerState: AnswerState.NotAnswered, givenAnswer: string.Empty));
    }

    public bool AnswerQuestion(int questionIdx, string answer)
    {
        if (_examResult[questionIdx].answerState != AnswerState.NotAnswered)
        {
            throw new Exception("You already answered this question");
        }
        
        var question = _examResult[questionIdx].question;
        
        _examResult[questionIdx] =
            (question, answerState: question.ValidateAnswer(answer) ? AnswerState.Correct : AnswerState.Wrong,
                givenAnswer: answer);

        return _examResult[questionIdx].answerState == AnswerState.Correct;
    }

    #endregion
}