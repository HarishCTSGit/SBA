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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IProjectManagerService
    {
        [OperationContract]
        List<TaskDetails> GetTaskDetails();

        [OperationContract]
        TaskDetails GetTaskDetailsByProjectName(string projectName);

        [OperationContract]
        void AddTaskDetails(TaskDetails taskDetail);

        [OperationContract]
        void AddParentTaskDetails(string ParentTaskName);

        [OperationContract]
        string EndTaskDetails(string TaskID);

        [OperationContract]
        void EditTaskDetails(TaskDetails taskDetail);


        [OperationContract]
        List<UserDetails> GetUserDetails();

        [OperationContract]
        UserDetails GetUserDetailsByUserName(string Name);

        [OperationContract]
        void AddUserDetails(UserDetails userDetail);

        [OperationContract]
        void EditUserDetails(UserDetails userDetail);

        [OperationContract]
        void DeleteUser(int UserId);

        [OperationContract]
        List<ProjectDetails> GetProjectDetails();

        [OperationContract]
        ProjectDetails GetProjectDetailsByProjectName(string projectName);

        [OperationContract]
        void AddProjectDetails(ProjectDetails projectDetail);

        [OperationContract]
        void EditProjectDetail(ProjectDetails projectDetail);

        [OperationContract]
        void DeleteProject(int ProjectID);
    }

}
