Feature: ReadBooking

@mytag
Scenario: Read booking information
	When send Read booking request
	Then info is successfully read