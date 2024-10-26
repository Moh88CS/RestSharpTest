Feature: Delete Boards Validation
	As a Trello user
	I want to have my board protected
	So that I want to call a single endpoint that will delete my board only for me

Scenario Outline: Check Delete Board With Invalid Id
	Given a request with authorization
	And the request has path params:
		| name | value      |
		| id   | <id_value> |
	When the 'Delete' request is sent to 'DeleteABoard' endpoint
	Then the response status code is <status_code>
	And the response body is equal to '<error_message>'
Examples:
	| id_value                 | status_code | error_message                         |
	| invalid                  | BadRequest  | invalid id                            |
	| 670bfaf62656fd3e8f5e7540 | NotFound    | The requested resource was not found. |

Scenario Outline: Check Delete Board With Invalid Auth
	Given a request without authorization
	And the request has path params:
		| name | value                    |
		| id   | 670bfaf62656fd3e8f5e7549 |
	And the request has query params:
		| name  | value   |
		| key   | <key>   |
		| token | <token> |
	When the 'Delete' request is sent to 'DeleteABoard' endpoint
	Then the response status code is Unauthorized
	And the response body is equal to '<error_message>'
Examples:
	| key              | token              | error_message                     |
	| current_user_key | empty_value        | unauthorized permission requested |
	| empty_value      | current_user_token | invalid app key                   |
	| empty_value      | empty_value        | unauthorized permission requested |
	| another_user_key | another_user_token | invalid token                     |