using System;
using System.Collections.Generic;
using System.Linq;

namespace Question;

public class MultiAnswerQuestion : IQuestion
{
    #region Fields

    private int[] _correctAnswer;

    #endregion
    
    #region Properties
    
    public string Question { get; }

    public string CorrectAnswerAsString => string.Join(" ", _correctAnswer);
    
    #endregion

    #region Constructors

    public MultiAnswerQuestion(string question, IEnumerable<int> correctAnswer)
    {
        Question = question;
        _correctAnswer = correctAnswer.Order().ToArray();
    }

    #endregion

    #region Methods

    public bool ValidateAnswer(string answer)
    {
        var ansInCorrectFormat = answer.Trim()
            .Split(" ")
            .Select(ans => Convert.ToInt32(ans))
            .Order()
            .ToArray();

        if (ansInCorrectFormat.Length != _correctAnswer.Length)
        {
            return false;
        }

        for (var i = 0; i < _correctAnswer.Length; i++)
        {
            if (ansInCorrectFormat[i] != _correctAnswer[i])
            {
                return false;
            }
        }

        return true;
    }

    #endregion
}