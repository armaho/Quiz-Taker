using System;
using System.IO;
using System.Linq;
using Question;

namespace ExamTakerConsole;

public class ExamTaker
{
    #region Fields

    private Exam _exam = new Exam();

    #endregion

    #region Constructors
    
    public ExamTaker(string questionFilePath)
    {
        if (!File.Exists(questionFilePath))
        {
            throw new FileNotFoundException(message: $"Cannot find {questionFilePath}.");
        }

        using var streamReader = File.OpenText(questionFilePath);

        while (streamReader.ReadLine() is { } line)
        {
            switch (line.Trim())
            {
                case "single_answer":
                    _exam.AddQuestion(ReadSingleAnswerQuestion(streamReader));
                    break;
                case "multiple_answer":
                    _exam.AddQuestion(ReadMultiAnswerQuestion(streamReader));
                    break;
                case "short_answer":
                    _exam.AddQuestion(ReadShortAnswerQuestion(streamReader));
                    break;
            }
        }
    }
    
    #endregion

    #region Methods

    public void TakeExam()
    {
        var command = Console.ReadLine().Split(" ", 3);

        while (command[0] == "submit_answer")
        {
            var questionIdx = Convert.ToInt32(command[1]) - 1;

            try
            {
                if (_exam.AnswerQuestion(questionIdx, command[2]))
                {
                    Console.WriteLine("correct answer");
                }
                else
                {
                    Console.WriteLine("wrong answer");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            command = Console.ReadLine().Split(" ", 3);
        }

        for (var i = 0; i < _exam.ExamResult.Count; i++)
        {
            var questionResult = _exam.ExamResult[i];
            
            switch (questionResult.answerState)
            {
                case AnswerState.NotAnswered:
                    Console.WriteLine($"{i + 1}: no_answer | correct answer: {questionResult.question.CorrectAnswerAsString}");
                    break;
                case AnswerState.Correct:
                    Console.WriteLine($"{i + 1}: correct");
                    break;
                case AnswerState.Wrong:
                    Console.WriteLine($"{i + 1}: wrong | correct answer: {questionResult.question.CorrectAnswerAsString}, your answer: {questionResult.givenAnswer}");
                    break;
            }
        }
        
        Console.WriteLine($"Score: {(_exam.Grade * 100):F1}");
    }
    
    private SingleAnswerQuestion ReadSingleAnswerQuestion(StreamReader streamReader)
    {
        var question = streamReader.ReadLine().Trim() + Environment.NewLine;
        var numberOfAnswers = Convert.ToInt32(streamReader.ReadLine().Trim());
        
        for (var i = 0; i < numberOfAnswers; i++)
        {
            question = question + streamReader.ReadLine().Trim();
            if (i != numberOfAnswers - 1)
            {
                question = question + Environment.NewLine;
            }
        }

        var correctAnswer = Convert.ToInt32(streamReader.ReadLine());

        return new SingleAnswerQuestion(question, correctAnswer);
    }

    private MultiAnswerQuestion ReadMultiAnswerQuestion(StreamReader streamReader)
    {
        var question = streamReader.ReadLine().Trim() + Environment.NewLine;
        var numberOfAnswers = Convert.ToInt32(streamReader.ReadLine().Trim());
        
        for (var i = 0; i < numberOfAnswers; i++)
        {
            question = question + streamReader.ReadLine().Trim();
            if (i != numberOfAnswers - 1)
            {
                question = question + Environment.NewLine;
            }
        }

        var correctAnswer = streamReader.ReadLine()
            .Trim()
            .Split(" ")
            .Select(ans => Convert.ToInt32(ans));

        return new MultiAnswerQuestion(question, correctAnswer);
    }

    private ShortAnswerQuestion ReadShortAnswerQuestion(StreamReader streamReader)
    {
        var question = streamReader.ReadLine();
        var correctAnswer = streamReader.ReadLine();

        return new ShortAnswerQuestion(question, correctAnswer);
    }
    
    #endregion
}