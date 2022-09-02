# TestAutomation

![Alt Text](https://github.com/christianshub/TestAutomation/blob/main/Snapshots/demo.gif)

## What is this?
This automation tool reads excel files and can be used to automate tasks quickly without the need to write any code. 

## Operation system, project, etc.

- IDE: Visual Studio 2019 (x86 project).
- OS: Windows 10, version 1909.

## Setup

1) Compile TestAutomation
2) Download the latest [chrome driver](https://chromedriver.chromium.org/downloads)
3) Create excel files containing your tests/tasks in the same directory from where `TestAutomation.exe` is located
    - 3.1) The first excel file should specify the webpage you want to visit.
    - 3.2) Note, the tool iterates over all excel files and sorts them by the lowest numerical value first. E.g.:
        - 00_Open.xlsx (read first)
        - 01_SignUp.xlsx (read secondly)
        - 02_SignInPage.xlsx (read third)
4) Run `TestAutomation.exe`

## How do I write a testcase/task?

Five fields needs to be filled for each step (row) in a test/task.

| XPATH      	| Action 	| Input 	| Step text                   	| WindowTitle                             	|
|------------	|--------	|-------	|-----------------------------	|-----------------------------------------	|
| /html/body 	| Click  	| -     	| Clicks the 'Sign up' button 	| GitHub: Where the world builds software 	|

Each column is described below followed by examples:

- `XPATH`: Find the element you want to interact with in your browser.
    - To retrieve the xpath, right click on an element in your browser -> choose inspect -> DevTools -> Right click -> Copy -> Copy full xpath
- `Action`: Consists of three comands; `Open`, `Click`, `Send`
    - Open: Opens a webpage
    - Click: Clicks on an element (e.g. a button) found by the xpath
    - Send: Sends some input (e.g. username field) found by the xpath 
- `Input`: Username, e-mails etc. should be inserted here
- `Step text`: Insert a descriptive text of your current step (used for logging)
- `WindowTitle`: Insert the window title of the webpage you visit (needed if navigation between different pages/pop-up windows)   

NB: Make sure the cells in the excel files are text formatted. Also, empty fields should be marked with `- `



- Open page example:
    - ![Alt Text](https://github.com/christianshub/TestAutomation/blob/main/Snapshots/snapshot1.png)

- Click example:
    - ![Alt Text](https://github.com/christianshub/TestAutomation/blob/main/Snapshots/snapshot2.png)

- Send and Click examples:
    - ![Alt Text](https://github.com/christianshub/TestAutomation/blob/main/Snapshots/snapshot3.png)

Your file structure where `TestAutomation.exe` is located should look as follows: 

![Alt Text](https://github.com/christianshub/TestAutomation/blob/main/Snapshots/snapshot4.png)


## Dependencies
- Chrome webdriver (https://chromedriver.chromium.org/downloads)
- Nuget package: DotNetSeleniumExtras.WaitHelpers v. 3.11.0
- Nuget package: Microsoft.Office.Interop.Excel v. 15.0.4795.1000
- Nuget package: Selenium.WebDriver v. 3.141.0

## To-do

- Sheet navigation possibility.
- Seperate methods into sub methods for better readability.
- Use logfiles to write an error report with reproduceable steps.
- Create FAQ
