use master


CREATE DATABASE FSD_SBA
GO

USe FSD_SBA
GO

CREATE TABLE dbo.Parent_Task_Table
(
	Parent_ID int identity(1,1),
	Parent_Task_Name varchar(500)
)
GO

Create table dbo.Task_Table
(
	Task_ID int identity(1,1),
	Task_Name varchar(500),
	Parent_ID int,
	Project_ID int,
	[Start_Date] datetime,
	End_Date Datetime,
	[Priority] int,
	[Status] bit
)
GO

Create table dbo.Project_Table
(
	Project_ID int identity(1,1),
	Project_Name varchar(500),
	[Start_Date] datetime,
	[End_Date] Datetime,
	[Priority] int,	
)
GO

Create table dbo.Users_Table
(
	[USER_ID] int identity(1,1),
	First_Name varchar(500),
	Last_Name varchar(500),
	Employee_ID int,
	Project_ID int,
	[Task_ID] int
)
GO


---------------stored proc

CREATE Procedure dbo.AddUserDetails
@firstName varchar(500),
@lastName Varchar(500),
@employeeID int
AS
BEGIN
	
	INsert into dbo.Users_Table (First_Name,Last_Name,Employee_ID) 
	Values (@firstName,@lastName,@employeeID)

END
GO


CREATE Procedure dbo.UpdateUserDetails
@userid Int,
@firstName varchar(500),
@lastName Varchar(500),
@employeeID int
AS
BEGIN
	Update dbo.Users_Table 
			set First_Name= Isnull(@firstName,First_Name), 
				Last_Name= Isnull(@lastName,Last_Name), 
				Employee_ID=Isnull(@employeeID ,Employee_ID)
		where [USER_ID]=@userid
END
GO



CREATE Procedure dbo.GetUserDetails
AS
BEGIN
	Select * from dbo.Users_Table
END
GO

CREATE Procedure dbo.GetUserDetailsByUserName
@UserNameOrEmployeeID varchar(500)
AS
BEGIN
	Select * from dbo.Users_Table
		 where First_Name like '%'+@UserNameOrEmployeeID+'%'
				 or  Last_Name like '%'+@UserNameOrEmployeeID+'%' 
				 or  Employee_ID like '%'+@UserNameOrEmployeeID+'%'
END
GO

Create Procedure dbo.AddProjectDetails
@projectName varchar(500),
@startdate datetime =null,
@enddate datetime =null,
@priority int,
@UserID int
AS 
BEGIN

	Insert into dbo.Project_Table values(@projectName,@startdate,@enddate,@priority)

	Declare @projectID int =(select Project_ID from dbo.Project_Table where Project_Name=@projectName)

	If EXISTS (select count(*) from dbo.Users_Table where [USER_ID]=@UserID)
		BEGIN
			Update dbo.Users_Table set Project_ID=@projectID where [USER_ID]= @UserID
		END
END
GO


CREATE Procedure dbo.UpdateProjectDetails
@projectID int,
@projectName varchar(500),
@startdate datetime =null,
@enddate datetime =null,
@priority int,
@UserID int
AS 
BEGIN

			Update T 
				set t.Project_Name=isnull(@projectName,Project_Name),
				t.Start_Date=isnull(@startdate,Start_Date),
				t.End_Date=isnull(@Enddate,End_Date),
				t.Priority=isnull(@priority,Priority)
			from dbo.Project_Table T where Project_ID=@projectID

			Update dbo.Users_Table set Project_ID=@projectID where [USER_ID]= @UserID
		END
GO



CREATE procedure dbo.GetProjectDetails
AS
BEGIN
	
	Declare @projectTaskDetails as Table
	(
		CountOfTasks int,
		ProjectID int,
		Status bit
	)
	INsert into @projectTaskDetails
	Select Count(Task_ID),T1.Project_ID, T2.Status from dbo.Project_Table T1 join dbo.Task_Table T2 on T1.Project_ID= t2.Project_ID
	group by T1.Project_ID, Status

	select T1.Project_ID,T1.Project_Name,T1.Start_Date,T1.End_Date,T1.Priority,T2.CountOfTasks, T2.Status,(T3.First_Name +' '+t3.Last_Name) as Manager
	 from dbo.Project_Table T1 join @projectTaskDetails T2 on T1.Project_ID= T2.ProjectID
	 Left join dbo.Users_Table t3 on T1.Project_ID= t3.Project_ID
END
GO

CREATE procedure dbo.GetProjectDetailsByProjectName
@projectName Varchar(500)
AS
BEGIN


	Declare @projectTaskDetails as Table
	(
		CountOfTasks int,
		ProjectID int,
		Status bit
	)
	INsert into @projectTaskDetails
	Select Count(Task_ID),T1.Project_ID, T2.Status from dbo.Project_Table T1 join dbo.Task_Table T2 on T1.Project_ID= t2.Project_ID
	group by T1.Project_ID, Status

	select T1.Project_ID,T1.Project_Name,T1.Start_Date,T1.End_Date,T1.Priority,T2.CountOfTasks, T2.Status,(T3.First_Name +' '+t3.Last_Name) as Manager
	 from dbo.Project_Table T1 join @projectTaskDetails T2 on T1.Project_ID= T2.ProjectID
	 Left join dbo.Users_Table t3 on T1.Project_ID= t3.Project_ID
	 where Project_Name=@projectName
END
GO


Create Procedure dbo.AddParentTaskDetails
@parentTaskName varchar(500)
AS
BEGIN
	Insert into dbo.Parent_Task_Table values(@parentTaskName)
END
GO



CREATE PROCEDURE dbo.AddTaskDetails
@projectName varchar(500),
@taskname varchar(500),
@parenttaskname varchar(500),
@startdate datetime,
@Enddate datetime,
@priority int,
@userid int,
@isended bit=0
AS
BEGIN

	Declare @parentTaskID int=(select Parent_ID from dbo.Parent_Task_Table where Parent_Task_Name=@parenttaskname)
	Declare @projectid int=(select Project_ID from dbo.Project_Table where Project_Name=@projectName)

	Insert into dbo.Task_Table 
		values (@taskname,@parentTaskID,@projectid,@startdate,@Enddate,@priority,@isended)

	Declare @taskId int =(select Task_ID from dbo.Task_Table where Task_Name=@taskname)

	If EXISTS (select count(*) from dbo.Users_Table where USER_ID=@UserID)
		BEGIN
			Update dbo.Users_Table set Task_ID=@taskId, Project_ID=@projectid where USER_ID= @UserID
		END
	
END
GO


Create PROCEDURE dbo.GetTaskDetails
AS
BEGIN
	Select 
		T1.Task_ID as TaskID,
		T1.Task_Name as TaskName,
		T2.Parent_Task_Name as ParentTaskName,
		T1.Start_Date as StartDate,
		T1.End_Date as EndDate,
		T1.Priority,
		T1.Status
	 from dbo.Task_Table T1 left join dbo.Parent_Task_Table T2 on T1.Parent_ID= t2.Parent_ID
END
GO


Create PROCEDURE dbo.GetTaskDetailsByProjectName
@projectName Varchar(500)
AS
BEGIN
	Select 
		T1.Task_ID as TaskID,
		T1.Task_Name as TaskName,
		T3.Parent_Task_Name as ParentTaskName,
		T1.Start_Date as StartDate,
		T1.End_Date as EndDate,
		T1.Priority,
		T1.Status
	 from dbo.Task_Table T1 
		join dbo.Project_Table T2 on   T1.Project_ID= t2.Project_ID and T2.Project_Name=@projectName
		left join dbo.Parent_Task_Table T3 on T1.Parent_ID= t3.Parent_ID
END
GO



ALTER PROCEDURE dbo.UpdateTaskDetails
@projectName varchar(500),
@taskid Int,
@taskname varchar(500),
@parenttaskname varchar(500),
@startdate datetime,
@Enddate datetime,
@priority int,
@userid int,
@isended bit=0

AS
BEGIN

	Declare @parentTaskID int=(select Parent_ID from dbo.Parent_Task_Table where Parent_Task_Name=@parenttaskname)
	Declare @projectid int=(select Project_ID from dbo.Project_Table where Project_Name=@projectName)

			Update T 
				set t.Task_Name=Isnull(@taskname,Task_Name),
				t.Parent_ID= Isnull(@parentTaskID,Parent_ID),
				t.Start_Date=Isnull(@startdate,Start_Date),
				t.End_Date= Isnull(@Enddate,End_Date),
				t.Priority= Isnull(@priority,Priority),
				t.Status= Isnull(@isended,Status),
				t.Project_ID= Isnull(@projectid,Project_ID) from dbo.Task_Table T where Task_ID=@taskid

			Update dbo.Users_Table set Task_ID=@taskid, Project_ID=@projectid where USER_ID= @userid
	
END
GO

CREATE Procedure dbo.EndTask
@taskid int
AS
BEGIN
	Update T set t.Status=1 from dbo.Task_Table T where Task_ID=@taskid

END
GO


Create Procedure dbo.DeleteUser
@userID int
AS
BEGIN
	delete from dbo.Users_Table where [USER_ID]=@userID
END
GO

Create Procedure dbo.DeleteProject
@projectID int
AS
BEGIN
	
	Delete from dbo.Project_Table where Project_ID=@projectID

	If EXISTS (select count(*) from dbo.Users_Table where Project_ID=@projectID)
	BEGIN
		Update dbo.Users_Table set Project_ID=NULL where Project_ID= @projectID
	END
	
	If EXISTS (select count(*) from dbo.Task_Table where Project_ID=@projectID)
	BEGIN
		Update dbo.Task_Table set Project_ID=NULL where Project_ID= @projectID
	END

END
GO



---- Sample TEsting



exec dbo.AddUserDetails 'Harish','Chandru','577116'

exec dbo.GetUserDetails

exec dbo.GetUserDetailsByUserName 'Chand'

exec dbo.UpdateUserDetails 3, 'Harish','Chandrasekaran','577116'


Declare @getdate datetime=getdate()
exec dbo.AddProjectDetails  'Project1',@getdate,@getdate,1,1

Declare @getdate1 datetime=getdate()
exec dbo.UpdateProjectDetails  1,'Project1',@getdate1,@getdate1,2,1

exec dbo.AddParentTaskDetails 'ParentTask1'

Declare @getdate2 datetime=getdate()
exec dbo.AddTaskDetails  'Project1','Task1','ParentTask1',@getdate2,@getdate2,1,1,0

exec dbo.GetTaskDetailsByProjectName 'Project1'
exec dbo.GetTaskDetails
exec dbo.GetUserDetails
exec dbo.GetProjectDetails
exec dbo.GetProjectDetailsByProjectName 'Project1'

select * from dbo.Project_Table
select * from dbo.Users_Table
select * from dbo.Parent_Task_Table
select * from dbo.Task_Table
