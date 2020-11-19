using JobApplicationAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace JobApplicationAPITests.Models
{
    [TestClass]
    public class JobApplicationTests
    {
        private JobApplication _jobApplication;

        [TestInitialize]
        public void Intialize()
        {
            _jobApplication = new JobApplication();
        }

        [TestMethod]
        public void QualificationsNotValid_JobApplicationMissingQuestion()
        {
            var jobApp = new JobApplication();
            List<QuestionContent> questions = new List<QuestionContent>();
            generateJobApplication(jobApp);
            generateQuestions(questions);
            questions.Add(new QuestionContent()
            {
                Id = "3",
                Question = "What's your biggest weakness?",
                AcceptedAnswers = new List<string>() { "care too much", "work too hard" }
            });

            jobApp.ValidateQualifications(questions);

            Assert.IsFalse(jobApp.IsQualified);
        }

        [TestMethod]
        public void QualificationsNotValid_IncorrectAnswer()
        {
            var jobApp = new JobApplication();
            List<QuestionContent> questions = new List<QuestionContent>();
            generateJobApplication(jobApp);
            generateQuestions(questions);

            jobApp.Questions.Find(q => q.Id == "1").Answer = "uhhhh, i don't know";
            jobApp.ValidateQualifications(questions);

            Assert.IsFalse(jobApp.IsQualified);
        }

        [TestMethod]
        public void QualificationsValid()
        {
            var jobApp = new JobApplication();
            List<QuestionContent> questions = new List<QuestionContent>();
            generateJobApplication(jobApp);
            generateQuestions(questions);

            jobApp.ValidateQualifications(questions);


            Assert.IsTrue(jobApp.IsQualified);
        }

        private void generateJobApplication(JobApplication jobApp)
        {
            jobApp.Name = "Test";
            jobApp.Questions = new List<QuestionResponse>()
            {
                new QuestionResponse()
                {
                    Id="1",
                    Answer="passionate"
                },
                new QuestionResponse()
                {
                    Id="2",
                    Answer="everything"
                }
            };
        }

        private void generateQuestions(List<QuestionContent> questions)
        {
            questions.Add(new QuestionContent()
            {
                Id = "1",
                Question = "What's your best quality?",
                AcceptedAnswers = new List<string>() { "passionate", "intelligent" }
            });
            questions.Add(new QuestionContent()
            {
                Id = "2",
                Question = "What are you scared of?",
                AcceptedAnswers = new List<string>() { "nothing", "everything" }
            });

        }
    }
}
