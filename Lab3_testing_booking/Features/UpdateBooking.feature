Feature: UpdateBooking

@mytag
Scenario: Update booking information
	When send Update booking request
	Then info is update