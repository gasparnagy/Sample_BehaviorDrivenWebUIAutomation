# SpecOverflow

	Scenario: New questions should be added to the question list
	    When I ask a new question with
	      | Title| Body|
	      | New question | This is what i need |
	    Then the question should appear at the end of the question list as
	      | Title| Views | Votes |
	      | New question | 0 | 0 |
	    
	      Scenario: The title and the body are required fields for the question
	    When I ask a new question with
	      | Title | Body |
	      |   |  |
	    Then the following validation errors should be displayed
	      | Message  |
	      | title is missing |
	      | body is missing  |
	    
	Scenario: Display questions on home page ordered by views
	    # the following 3 questions are already in the database, so this step can be implemented with an empty step definition for now
	    Given the following questions are registered
	      | Title  | Views | Votes |
	      | Who am I   | 13| 4 |
	      | Does this question make sense? | 8 | 3 |
	      | Yet another question   | 42| 2 |
	    When I go to the home page
	    Then the following questions should be displayed in this order
	      | Title  | Views | Votes |
	      | Yet another question   | 42| 2 |
	      | Who am I   | 13| 4 |
	      | Does this question make sense? | 8 | 3 |
    
# Orbit

	Scenario: Admin user gets to the project overview screen after successful login
		Given I am on the login page
		#When I login as valid admin user
		When I am logged in as user 'admin' with password 'admin'
		Then the project overview page should be displayed

	Scenario: Should not be able to login with invalid password
		Given I am on the login page
		When I am logged in as user 'admin' with password 'wrong'
		Then there should be a login error displayed: 'Invalid user name or wrong password'

	Scenario: Project venues should be listed
		# the following project is already in the database, so this step can be implemented with an empty step definition for now
		Given there is a project 'Test WM 2012' with Venues
			| Venue      |
			| Kitzbühel  |
			| Schladming |
		Given I am logged in as admin
		And I work on project 'Test WM 2012'
		When I try to manage project venues
		Then the following venues are listed
			| Venue      |
			| Kitzbühel  |
			| Schladming |

	Scenario: There should be a warning if no orders placed for a project
		# the following project is already in the database, so this step can be implemented with an empty step definition for now
		Given there is a project 'Test WM 2012' with Venues
			| Venue      |
			| Kitzbühel  |
			| Schladming |
		Given I am logged in as admin
		And I work on project 'Test WM 2012'
		When I try to manage project orders
		Then there should be a warning displayed in the table: 'No records to display.'

	Scenario: Should be able to add order lines to an order
		Given I am logged in as admin
		And I work on project 'Test WM 2012'
		And have started placing a new order for 'GenericDropDown_A'
		When I add a new order line
		Then there should be 2 order lines added
		And it should be possible to fill out the added line

# StackClone

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
