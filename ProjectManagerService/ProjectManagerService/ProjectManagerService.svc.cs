using ProjectManagerService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ProjectManagerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ProjectManagerService : IProjectManagerService
    {

        #region Task Details
        public List<TaskDetails> GetTaskDetails()
        {
            List<TaskDetails> objTaskDetails = new List<TaskDetails>();

            using (var context = new FSD_SBAEntities())
            {
                var taskDetails = context.GetTaskDetails().ToList();

                foreach (var task in taskDetails)
                {
                    objTaskDetails.Add(new TaskDetails
                    {
                        TaskId = task.TaskID,
                        TaskName = task.TaskName,
                        ParentTaskName = task.ParentTaskName,
                        Priority = task.Priority,
                        StartDate = task.StartDate,
                        EndDate = task.EndDate,
                        IsEnded = task.Status
                    });
                }

            }
            return objTaskDetails;
        }

        public TaskDetails GetTaskDetailsByProjectName(string projectName)
        {
            TaskDetails objTaskDetails = new TaskDetails();
            using (var context = new FSD_SBAEntities())
            {
                var taskDetails = context.GetTaskDetailsByProjectName(projectName);
                foreach (var task in taskDetails)
                {
                    objTaskDetails.TaskId = task.TaskID;
                    objTaskDetails.TaskName = task.TaskName;
                    objTaskDetails.ParentTaskName = task.ParentTaskName;
                    objTaskDetails.Priority = task.Priority;
                    objTaskDetails.StartDate = task.StartDate;
                    objTaskDetails.EndDate = task.EndDate;
                    objTaskDetails.IsEnded = task.Status;
                }
            }
            return objTaskDetails;
        }

        public void AddTaskDetails(TaskDetails taskDetail)
        {
            using (var context = new FSD_SBAEntities())
            {
                int i = context.AddTaskDetails(taskDetail.ProjectName, taskDetail.TaskName, taskDetail.ParentTaskName, taskDetail.StartDate, taskDetail.EndDate, taskDetail.Priority, taskDetail.UserID, taskDetail.IsEnded);
                context.SaveChangesAsync();
            }
        }

        public void AddParentTaskDetails(string ParentTaskName)
        {
            using (var context = new FSD_SBAEntities())
            {
                int i = context.AddParentTaskDetails(ParentTaskName);
                context.SaveChangesAsync();
            }
        }

        public string EndTaskDetails(string TaskID)
        {
            using (var context = new FSD_SBAEntities())
            {
                int i = context.EndTask(Int32.Parse(TaskID));
                context.SaveChangesAsync();
            }
            return TaskID;
        }

        public void EditTaskDetails(TaskDetails taskDetail)
        {
            using (var context = new FSD_SBAEntities())
            {
                int i = context.UpdateTaskDetails(taskDetail.ProjectName, taskDetail.TaskId, taskDetail.TaskName, taskDetail.ParentTaskName, taskDetail.StartDate, taskDetail.EndDate, taskDetail.Priority, taskDetail.UserID, taskDetail.IsEnded);
                context.SaveChangesAsync();
            }
        }

        #endregion

        #region User Details
        public List<UserDetails> GetUserDetails()
        {
            List<UserDetails> objUserDetails = new List<UserDetails>();
            using (var context = new FSD_SBAEntities())
            {
                var userDetails = context.GetUserDetails().ToList();

                foreach (var user in userDetails)
                {
                    objUserDetails.Add(new UserDetails
                    {
                        UserID = user.USER_ID,
                        EmployeeID = user.Employee_ID,
                        FirstName = user.First_Name,
                        LastName = user.Last_Name
                    });
                }
            }

            return objUserDetails;
        }

        public UserDetails GetUserDetailsByUserName(string UserName)
        {
            UserDetails objUserDetails = new UserDetails();
            using (var context = new FSD_SBAEntities())
            {
                var userDetails = context.GetUserDetailsByUserName(UserName);

                foreach (var user in userDetails)
                {
                    objUserDetails.UserID = user.USER_ID;
                    objUserDetails.EmployeeID = user.Employee_ID;
                    objUserDetails.FirstName = user.First_Name;
                    objUserDetails.LastName = user.Last_Name;
                }
            }
            return objUserDetails;
        }

        public void AddUserDetails(UserDetails userDetail)
        {
            using (var context = new FSD_SBAEntities())
            {
                int i = context.AddUserDetails(userDetail.FirstName, userDetail.LastName, userDetail.EmployeeID);
                context.SaveChangesAsync();
            }
        }

        public void EditUserDetails(UserDetails userDetail)
        {
            using (var context = new FSD_SBAEntities())
            {
                int i = context.UpdateUserDetails(userDetail.UserID, userDetail.FirstName, userDetail.LastName, userDetail.EmployeeID);
                context.SaveChangesAsync();
            }
        }

        public void DeleteUser(int UserId)
        {
            using (var context = new FSD_SBAEntities())
            {
                int i = context.DeleteUser(UserId);
                context.SaveChangesAsync();
            }
        }

        #endregion

        #region Project Details
        public List<ProjectDetails> GetProjectDetails()
        {
            List<ProjectDetails> objProjectDetails = new List<ProjectDetails>();
            using (var context = new FSD_SBAEntities())
            {
                var projectDetail = context.GetProjectDetails();
                foreach (var project in projectDetail)
                {
                    objProjectDetails.Add(new ProjectDetails
                    {
                        ProjectId = project.Project_ID,
                        ProjectName = project.Project_Name,
                        StartDate = project.Start_Date,
                        EndDate = project.End_Date,
                        Priority = project.Priority,
                        NumberofTasks = project.CountOfTasks,
                        Completed = project.Status,
                        Manager = project.Manager
                    });
                }
            }

            return objProjectDetails;
        }

        public ProjectDetails GetProjectDetailsByProjectName(string projectName)
        {
            ProjectDetails objProjectDetails = new ProjectDetails();
            using (var context = new FSD_SBAEntities())
            {
                var projectDetail = context.GetProjectDetailsByProjectName(projectName);
                foreach (var project in projectDetail)
                {
                    objProjectDetails.ProjectId = project.Project_ID;
                    objProjectDetails.ProjectName = project.Project_Name;
                    objProjectDetails.StartDate = project.Start_Date;
                    objProjectDetails.EndDate = project.End_Date;
                    objProjectDetails.Priority = project.Priority;
                    objProjectDetails.Manager = project.Manager;
                    objProjectDetails.NumberofTasks = project.CountOfTasks;
                    objProjectDetails.Completed = project.Status;
                }
            }
            return objProjectDetails;
        }

        public void AddProjectDetails(ProjectDetails projectDetail)
        {
            using (var context = new FSD_SBAEntities())
            {
                int i = context.AddProjectDetails(projectDetail.ProjectName, projectDetail.StartDate, projectDetail.EndDate, projectDetail.Priority, projectDetail.UserID);
                context.SaveChangesAsync();
            }
        }

        public void EditProjectDetail(ProjectDetails projectDetail)
        {
            using (var context = new FSD_SBAEntities())
            {
                int i = context.UpdateProjectDetails(projectDetail.ProjectId, projectDetail.ProjectName, projectDetail.StartDate, projectDetail.EndDate, projectDetail.Priority, projectDetail.UserID);
                context.SaveChangesAsync();
            }
        }

        public void DeleteProject(int ProjectID)
        {
            using (var context = new FSD_SBAEntities())
            {
                int i = context.DeleteProject(ProjectID);
                context.SaveChangesAsync();
            }
        }

        #endregion
    }
}
