Feature: Orbit

Scenario: The application can be loaded
	When I navigate to the home page
	Then the title should be 'Orbit'

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
	