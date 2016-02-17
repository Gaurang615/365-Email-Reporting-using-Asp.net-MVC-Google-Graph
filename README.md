# 365-Email-Reporting-using-Asp.net-MVC-Google-Graph

This Application is design to show Email Statics based on 365 Mail. Such as email received by particular user, number of email send and Unction mails i.e.( Emails which is not replied)

 This can be useful for complain department to track the performance of each user in their complain handling department. Such as number of complaints received per day and how many complain solve by each user.
 
 Following the link of resources that you can use:
 
Integrate Office 365 APIs into .NET Visual Studio project
https://msdn.microsoft.com/office/office365/howto/authenticate-and-use-services

Active Directory Graph API REST
https://msdn.microsoft.com/en-AU/library/azure/hh974476.aspx

Build service and daemon apps in Office 365: To create self-signed x509 Public certificate 
https://msdn.microsoft.com/en-us/office/office365/howto/building-service-apps-in-office-365
# Screen Shots of Application
Signup Page: Note: This app can only be accessible by Admin, Global Admin or Customize Admin with appropriate rights.

![image](https://cloud.githubusercontent.com/assets/14831549/13098448/a7ab4df2-d57d-11e5-805f-f83067e64aa1.png)

Allow Application to access user emails
![image](https://cloud.githubusercontent.com/assets/14831549/13098462/d2ae018e-d57d-11e5-93b0-b68d825a9f55.png)

From this page admin can view any user performance by selecting respective username from the dropdown list and then click on show performance button.
![image](https://cloud.githubusercontent.com/assets/14831549/13098634/c6ff169c-d57e-11e5-85f2-0716c83c2561.png)

Following is the user statics page from where we can find user/Company’s performance, in this case the Bar chart on top left corner shows the user performance in last 14 days, i.e. Number of received mail, Number of send mails and number of unactioned emails. The pie chart on right top corner shows the user overall performance and the third Line chart shows the Overall company’s performance. The pie chart on bottom left corner shows the Response time means (Send mail time- Received mail time)
![image](https://cloud.githubusercontent.com/assets/14831549/13098356/05945b62-d57d-11e5-997c-a6d9eb430cc3.png)



