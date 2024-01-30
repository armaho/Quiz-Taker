namespace Question;

public interface IQuestion
{
    string Question { get; }
    string CorrectAnswerAsString { get; }
    
    bool ValidateAnswer(string answer);
}