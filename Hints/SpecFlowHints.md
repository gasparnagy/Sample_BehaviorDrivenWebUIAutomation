
# Using 1-row Table in a Given/Then step

        // define a class that represents the table structure
        class QuestionData
        {
            public string Title { get; set; }
            public string Body { get; set; }
        }

        [When(@"I ask a new question with")]
        public void WhenIAskANewQuestionWith(Table table)
        {
            // convert the table to an instance of the class with CreateInstance<T>();
            QuestionData question = table.CreateInstance<QuestionData>();
            //TODO: do something with the question
        }

# Using n-row Table in a Given/Then step

        // define a class that represents the table structure
        class QuestionData
        {
            public string Title { get; set; }
            public string Body { get; set; }
        }

        [When(@"I ask a new question with")]
        public void WhenIAskANewQuestionWith(Table table)
        {
            // convert the table to set of the class instances with CreateSet<T>();
            IEnumerable<QuestionData> questions = table.CreateSet<QuestionData>();
            foreach (var question in questions)
            {
                //TODO: do something with the question
            }
        }

