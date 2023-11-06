Feature: DeleteBooking

@mytag
Scenario: Delete booking information
	When send Delete booking request
	Then info is deleted