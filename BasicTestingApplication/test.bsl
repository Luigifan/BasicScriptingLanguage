#BasicScriptFile
#metadata title "Test Script"
declare diagResult|"notrun"

diagResult = showQuestionDialog|"The current directory is: {CURRENTDIRECTORY}","Nice","Meme"

showQuestionDialog|"The result of the dialog is [diagResult]"

wait