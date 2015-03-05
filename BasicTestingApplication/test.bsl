#BasicScriptFile

#metadata title "Hello World Script"

#declaring variables
declare diagResult|"notrun"
declare howAreYou|"How are you?"

#getting the result of the dialog
diagResult = showQuestionDialog|"Hello world! [howAreYou]","Great","Not good"

showQuestionDialog|"You are feeling [diagResult]"