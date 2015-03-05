#BasicScriptFile

#metadata title "Hello World Script"

#declaring variables
declare diagResult|"notrun"
declare howAreYou|"How are you?"

#getting the result of the dialog
diagResult = showQuestionDialog|"Hello world! [howAreYou]","Great","Not good"

if diagResult|"Great"
	showQuestionDialog|"That's great!!"
	echo|"great"
endif

if diagResult|"Not good"
	showQuestionDialog|"I'm sorry"
endif