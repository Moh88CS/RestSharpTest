Feature: Get Boards
	As a Trello API user
	I want to access all my boards
	So that I want to call a single endpoint that will return all my boards
	
Background: a request with auth with fields query param
	Given a request with authorization
	And the request has query params:
		| name   | value   |
		| fields | id,name |

Scenario: Check Get Boards
	And the request has path params:
		| name   | value          |
		| member | mohamedabdulal |
	When the 'Get' request is sent to 'GetAllBoards' endpoint
	Then the response status code is OK
	And the response matches 'get_boards.json' schema

Scenario: Check Get Board
	And the request has path params:
		| name | value                    |
		| id   | 670bfaf62656fd3e8f5e7549 |
	When the 'Get' request is sent to 'GetABoard' endpoint
	Then the response status code is OK
	And the response matches 'get_board.json' schema
	And body value has the following values by paths:
		| path | expected_value  |
		| name | My Trello board |
	