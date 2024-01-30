using System;

namespace Question;

public class SingleAnswerQuestion : IQuestion
{
    #region Fields

    private int _correctAnswer;

    #endregion
    
    #region Properties

    public string Question { get; init; }

    public string CorrectAnswerAsString => _correctAnswer.ToString();

    #endregion

    #region Constructors

    public SingleAnswerQuestion(string question, int correctAnswer)
    {
        Question = question;
        _correctAnswer = correctAnswer;
    }

    #endregion

    #region Methods

    public bool ValidateAnswer(string answer)
    {
        return Convert.ToInt32(answer.Trim()) == _correctAnswer;
    }

    #endregion
}