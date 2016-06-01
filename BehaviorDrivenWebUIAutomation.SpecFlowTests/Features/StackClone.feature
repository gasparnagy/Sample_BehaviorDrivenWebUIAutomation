Feature: StackClone

Scenario: The application can be loaded
	When I navigate to the home page
	Then the title should be 'Stackoverflow | Stamplay app'

Scenario: New questions should be added to the question list
	When I ask a new question with
		| Title        | Body                |
		| New question | This is what i need |
	Then the question should appear in the question list with todays date as
		| Title        | Views | Votes |
		| New question | 0     | 0     |

Scenario: The details of the question can be accessed from the question list
	Given there is a question added with
		| Title        | Body                |
		| New question | This is what i need |
	When I navigate to the question details from the main page
	Then the question details should be visible
		| Title        | Body                | Votes |
		| New question | This is what i need | 0     |
