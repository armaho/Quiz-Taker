namespace Question;

public class ShortAnswerQuestion : IQuestion
{
    #region Fields
    
    private string _correctAnswer;
    
    #endregion
    
    #region Properties

    public string Question { get; }

    public string CorrectAnswerAsString => _correctAnswer;
    
    #endregion

    #region Constructors

    public ShortAnswerQuestion(string question, string correctAnswer)
    {
        Question = question;
        _correctAnswer = correctAnswer;
    }

    #endregion

    #region Methods

    public bool ValidateAnswer(string answer)
    {
        return answer.Trim() == _correctAnswer;
    }

    #endregion
}