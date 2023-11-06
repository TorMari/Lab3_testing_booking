Feature: CreateBooking

@mytag
Scenario: Add new booking
	Given Input firstname "Jim"
	And input lastname "Brown"
	And set a total price at "111"
	And set depositpaid as "true"
	And select the date of checkin in "2018-01-01" and checkout in "2019-01-01"
	And set in the additional needs "Breakfast"
	When send Create booking request
	Then response is success