Feature: Create Boards Validation
	As a Trello user
	I want to have my board protected
	So that I want to call a single endpoint that will create my board only for me

Scenario: Check Create Board With Invalid Name
	Given a request with authorization
	And the request has body params:
		| name | value |
		| name |       |
	When the 'Post' request is sent to 'CreateABoard' endpoint
	Then the response status code is BadRequest
	And body value has the following values by paths:
		| path    | expected_value         |
		| message | invalid value for name |

Scenario Outline: Check Create Board With Invalid Auth
	Given a request without authorization
	And the request has body params:
		| name | value     |
		| name | New Board |
	And the request has query params:
		| name  | value   |
		| key   | <key>   |
		| token | <token> |
	When the 'Post' request is sent to 'CreateABoard' endpoint
	Then the response status code is Unauthorized
	And the response body is equal to '<error_message>'
Examples:
	| key              | token              | error_message                     |
	| current_user_key | empty_value        | unauthorized permission requested |
	| empty_value      | current_user_token | invalid app key                   |
	| empty_value      | empty_value        | unauthorized permission requested |