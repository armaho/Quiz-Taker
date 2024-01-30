using System.CommandLine;
using ExamTakerConsole;

var questionsFilePathArg = new Argument<string>(name: "Questions file path", description: "Path to the file containing questions to ask");

var rootCommand = new RootCommand { questionsFilePathArg };

rootCommand.SetHandler((questionsFilePath) =>
{
    var examTaker = new ExamTaker(questionsFilePath);
    examTaker.TakeExam();
}, questionsFilePathArg);

return rootCommand.Invoke(args);

