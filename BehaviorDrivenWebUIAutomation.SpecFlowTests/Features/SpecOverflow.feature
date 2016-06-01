Feature: SpecOverflow

Scenario: The application can be loaded
	When I navigate to the home page
	Then the title should be 'Home Page - SpecOverflow'

Scenario: New questions should be added to the question list
	When I ask a new question with
		| Title        | Body                |
		| New question | This is what i need |
	Then the question should appear at the end of the question list as
		| Title        | Views | Votes |
		| New question | 0     | 0     |

Scenario: Display questions on home page ordered by views
  # the following 3 questions are already in the database, so this step can be implemented with an empty step definition for now
	Given the following questions are registered
		| Title                          | Views | Votes |
		| Who am I                       | 13    | 4     |
		| Does this question make sense? | 8     | 3     |
		| Yet another question           | 42    | 2     |
	When I go to the home page
	Then the following questions should be displayed in this order
		| Title                          | Views | Votes |
		| Yet another question           | 42    | 2     |
		| Who am I                       | 13    | 4     |
		| Does this question make sense? | 8     | 3     |
