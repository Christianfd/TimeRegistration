﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimeReg
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class WebToolEntities1 : DbContext
    {
        public WebToolEntities1()
            : base("name=WebToolEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<CustomerRef> CustomerRef { get; set; }
        public virtual DbSet<OrderNumber> OrderNumber { get; set; }
        public virtual DbSet<PlatformOrProduct> PlatformOrProduct { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<Requester> Requester { get; set; }
        public virtual DbSet<RequestOrg> RequestOrg { get; set; }
        public virtual DbSet<TaskType> TaskType { get; set; }
        public virtual DbSet<TimeRegistration> TimeRegistration { get; set; }
        public virtual DbSet<TimeType> TimeType { get; set; }
        public virtual DbSet<Turbine> Turbine { get; set; }
        public virtual DbSet<UserAssignment> UserAssignment { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<VI_Comments> VI_Comments { get; set; }
        public virtual DbSet<VI_CustomerRef> VI_CustomerRef { get; set; }
        public virtual DbSet<VI_OrderNumber> VI_OrderNumber { get; set; }
        public virtual DbSet<VI_ProjectAndOrderTools> VI_ProjectAndOrderTools { get; set; }
        public virtual DbSet<VI_Projects> VI_Projects { get; set; }
        public virtual DbSet<VI_Requester> VI_Requester { get; set; }
        public virtual DbSet<VI_RequestOrg> VI_RequestOrg { get; set; }
        public virtual DbSet<VI_TaskType> VI_TaskType { get; set; }
        public virtual DbSet<VI_TimeRegistration> VI_TimeRegistration { get; set; }
        public virtual DbSet<VI_TimeType> VI_TimeType { get; set; }
        public virtual DbSet<VI_UserAssignment> VI_UserAssignment { get; set; }
        public virtual DbSet<VI_Users> VI_Users { get; set; }
        public virtual DbSet<VI_UserTimePerProject> VI_UserTimePerProject { get; set; }
        public virtual DbSet<VI_Country> VI_Country { get; set; }
        public virtual DbSet<VI_PlatformOrProduct> VI_PlatformOrProduct { get; set; }
        public virtual DbSet<VI_Turbine> VI_Turbine { get; set; }
        public virtual DbSet<VI_ProjectAndOrderToolsUnion> VI_ProjectAndOrderToolsUnion { get; set; }
    
        public virtual int SP_AddComment(Nullable<int> weekNumber, Nullable<int> year, string text, Nullable<int> projectKey, Nullable<int> userKey)
        {
            var weekNumberParameter = weekNumber.HasValue ?
                new ObjectParameter("weekNumber", weekNumber) :
                new ObjectParameter("weekNumber", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("year", year) :
                new ObjectParameter("year", typeof(int));
    
            var textParameter = text != null ?
                new ObjectParameter("text", text) :
                new ObjectParameter("text", typeof(string));
    
            var projectKeyParameter = projectKey.HasValue ?
                new ObjectParameter("projectKey", projectKey) :
                new ObjectParameter("projectKey", typeof(int));
    
            var userKeyParameter = userKey.HasValue ?
                new ObjectParameter("userKey", userKey) :
                new ObjectParameter("userKey", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AddComment", weekNumberParameter, yearParameter, textParameter, projectKeyParameter, userKeyParameter);
        }
    
        public virtual int SP_UpdateTaskType(Nullable<int> updateId, string taskTypeName)
        {
            var updateIdParameter = updateId.HasValue ?
                new ObjectParameter("UpdateId", updateId) :
                new ObjectParameter("UpdateId", typeof(int));
    
            var taskTypeNameParameter = taskTypeName != null ?
                new ObjectParameter("taskTypeName", taskTypeName) :
                new ObjectParameter("taskTypeName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateTaskType", updateIdParameter, taskTypeNameParameter);
        }
    
        public virtual int SP_RemoveTaskType(Nullable<int> removeId)
        {
            var removeIdParameter = removeId.HasValue ?
                new ObjectParameter("RemoveId", removeId) :
                new ObjectParameter("RemoveId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_RemoveTaskType", removeIdParameter);
        }
    
        public virtual int SP_UpdateComments(Nullable<int> updateId, Nullable<int> weekNr, Nullable<int> year, string text, Nullable<int> projectId, Nullable<int> userId)
        {
            var updateIdParameter = updateId.HasValue ?
                new ObjectParameter("UpdateId", updateId) :
                new ObjectParameter("UpdateId", typeof(int));
    
            var weekNrParameter = weekNr.HasValue ?
                new ObjectParameter("WeekNr", weekNr) :
                new ObjectParameter("WeekNr", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(int));
    
            var textParameter = text != null ?
                new ObjectParameter("Text", text) :
                new ObjectParameter("Text", typeof(string));
    
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("ProjectId", projectId) :
                new ObjectParameter("ProjectId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateComments", updateIdParameter, weekNrParameter, yearParameter, textParameter, projectIdParameter, userIdParameter);
        }
    
        public virtual int SP_RemoveComment(Nullable<int> removeId)
        {
            var removeIdParameter = removeId.HasValue ?
                new ObjectParameter("RemoveId", removeId) :
                new ObjectParameter("RemoveId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_RemoveComment", removeIdParameter);
        }
    
        public virtual int SP_AddProject(string projectName, Nullable<int> orderNumber, Nullable<int> timeEstimation, Nullable<int> fK_ProjectLeader, string scope, Nullable<int> timeType, string siteOrVersion, Nullable<int> fK_Country, Nullable<int> fK_PlatformOrProduct, Nullable<int> fK_Turbine, string projectComment)
        {
            var projectNameParameter = projectName != null ?
                new ObjectParameter("ProjectName", projectName) :
                new ObjectParameter("ProjectName", typeof(string));
    
            var orderNumberParameter = orderNumber.HasValue ?
                new ObjectParameter("OrderNumber", orderNumber) :
                new ObjectParameter("OrderNumber", typeof(int));
    
            var timeEstimationParameter = timeEstimation.HasValue ?
                new ObjectParameter("TimeEstimation", timeEstimation) :
                new ObjectParameter("TimeEstimation", typeof(int));
    
            var fK_ProjectLeaderParameter = fK_ProjectLeader.HasValue ?
                new ObjectParameter("FK_ProjectLeader", fK_ProjectLeader) :
                new ObjectParameter("FK_ProjectLeader", typeof(int));
    
            var scopeParameter = scope != null ?
                new ObjectParameter("Scope", scope) :
                new ObjectParameter("Scope", typeof(string));
    
            var timeTypeParameter = timeType.HasValue ?
                new ObjectParameter("timeType", timeType) :
                new ObjectParameter("timeType", typeof(int));
    
            var siteOrVersionParameter = siteOrVersion != null ?
                new ObjectParameter("SiteOrVersion", siteOrVersion) :
                new ObjectParameter("SiteOrVersion", typeof(string));
    
            var fK_CountryParameter = fK_Country.HasValue ?
                new ObjectParameter("FK_Country", fK_Country) :
                new ObjectParameter("FK_Country", typeof(int));
    
            var fK_PlatformOrProductParameter = fK_PlatformOrProduct.HasValue ?
                new ObjectParameter("FK_PlatformOrProduct", fK_PlatformOrProduct) :
                new ObjectParameter("FK_PlatformOrProduct", typeof(int));
    
            var fK_TurbineParameter = fK_Turbine.HasValue ?
                new ObjectParameter("FK_Turbine", fK_Turbine) :
                new ObjectParameter("FK_Turbine", typeof(int));
    
            var projectCommentParameter = projectComment != null ?
                new ObjectParameter("ProjectComment", projectComment) :
                new ObjectParameter("ProjectComment", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AddProject", projectNameParameter, orderNumberParameter, timeEstimationParameter, fK_ProjectLeaderParameter, scopeParameter, timeTypeParameter, siteOrVersionParameter, fK_CountryParameter, fK_PlatformOrProductParameter, fK_TurbineParameter, projectCommentParameter);
        }
    
        public virtual int SP_RemoveProject(Nullable<int> removeId)
        {
            var removeIdParameter = removeId.HasValue ?
                new ObjectParameter("RemoveId", removeId) :
                new ObjectParameter("RemoveId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_RemoveProject", removeIdParameter);
        }
    
        public virtual int SP_UpdateProject(Nullable<int> updateId, string projectName, Nullable<int> orderNumber, Nullable<int> timeEstimation, Nullable<int> fK_ProjectLeader, string scope, Nullable<int> timeType, string siteOrVersion, Nullable<int> fK_Country, Nullable<int> fK_PlatformOrProduct, Nullable<int> fK_Turbine, string projectComment)
        {
            var updateIdParameter = updateId.HasValue ?
                new ObjectParameter("UpdateId", updateId) :
                new ObjectParameter("UpdateId", typeof(int));
    
            var projectNameParameter = projectName != null ?
                new ObjectParameter("ProjectName", projectName) :
                new ObjectParameter("ProjectName", typeof(string));
    
            var orderNumberParameter = orderNumber.HasValue ?
                new ObjectParameter("OrderNumber", orderNumber) :
                new ObjectParameter("OrderNumber", typeof(int));
    
            var timeEstimationParameter = timeEstimation.HasValue ?
                new ObjectParameter("TimeEstimation", timeEstimation) :
                new ObjectParameter("TimeEstimation", typeof(int));
    
            var fK_ProjectLeaderParameter = fK_ProjectLeader.HasValue ?
                new ObjectParameter("FK_ProjectLeader", fK_ProjectLeader) :
                new ObjectParameter("FK_ProjectLeader", typeof(int));
    
            var scopeParameter = scope != null ?
                new ObjectParameter("Scope", scope) :
                new ObjectParameter("Scope", typeof(string));
    
            var timeTypeParameter = timeType.HasValue ?
                new ObjectParameter("timeType", timeType) :
                new ObjectParameter("timeType", typeof(int));
    
            var siteOrVersionParameter = siteOrVersion != null ?
                new ObjectParameter("SiteOrVersion", siteOrVersion) :
                new ObjectParameter("SiteOrVersion", typeof(string));
    
            var fK_CountryParameter = fK_Country.HasValue ?
                new ObjectParameter("FK_Country", fK_Country) :
                new ObjectParameter("FK_Country", typeof(int));
    
            var fK_PlatformOrProductParameter = fK_PlatformOrProduct.HasValue ?
                new ObjectParameter("FK_PlatformOrProduct", fK_PlatformOrProduct) :
                new ObjectParameter("FK_PlatformOrProduct", typeof(int));
    
            var fK_TurbineParameter = fK_Turbine.HasValue ?
                new ObjectParameter("FK_Turbine", fK_Turbine) :
                new ObjectParameter("FK_Turbine", typeof(int));
    
            var projectCommentParameter = projectComment != null ?
                new ObjectParameter("ProjectComment", projectComment) :
                new ObjectParameter("ProjectComment", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateProject", updateIdParameter, projectNameParameter, orderNumberParameter, timeEstimationParameter, fK_ProjectLeaderParameter, scopeParameter, timeTypeParameter, siteOrVersionParameter, fK_CountryParameter, fK_PlatformOrProductParameter, fK_TurbineParameter, projectCommentParameter);
        }
    
        public virtual int SP_AddTimeRegistration(Nullable<int> userId, Nullable<int> projectId, Nullable<int> orderId, Nullable<int> taskId, Nullable<int> timeRegistered, Nullable<System.DateTime> date, Nullable<System.DateTime> dateEntry, string comment)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("ProjectId", projectId) :
                new ObjectParameter("ProjectId", typeof(int));
    
            var orderIdParameter = orderId.HasValue ?
                new ObjectParameter("OrderId", orderId) :
                new ObjectParameter("OrderId", typeof(int));
    
            var taskIdParameter = taskId.HasValue ?
                new ObjectParameter("TaskId", taskId) :
                new ObjectParameter("TaskId", typeof(int));
    
            var timeRegisteredParameter = timeRegistered.HasValue ?
                new ObjectParameter("TimeRegistered", timeRegistered) :
                new ObjectParameter("TimeRegistered", typeof(int));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("Date", date) :
                new ObjectParameter("Date", typeof(System.DateTime));
    
            var dateEntryParameter = dateEntry.HasValue ?
                new ObjectParameter("DateEntry", dateEntry) :
                new ObjectParameter("DateEntry", typeof(System.DateTime));
    
            var commentParameter = comment != null ?
                new ObjectParameter("Comment", comment) :
                new ObjectParameter("Comment", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AddTimeRegistration", userIdParameter, projectIdParameter, orderIdParameter, taskIdParameter, timeRegisteredParameter, dateParameter, dateEntryParameter, commentParameter);
        }
    
        public virtual int SP_RemoveTimeRegistration(Nullable<int> removeId)
        {
            var removeIdParameter = removeId.HasValue ?
                new ObjectParameter("RemoveId", removeId) :
                new ObjectParameter("RemoveId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_RemoveTimeRegistration", removeIdParameter);
        }
    
        public virtual int SP_UpdateTimeRegistration(Nullable<int> updateId, Nullable<int> userId, Nullable<int> projectId, Nullable<int> orderId, Nullable<int> taskId, Nullable<int> timeRegistered, Nullable<System.DateTime> date, Nullable<System.DateTime> dateEntry, string comment)
        {
            var updateIdParameter = updateId.HasValue ?
                new ObjectParameter("UpdateId", updateId) :
                new ObjectParameter("UpdateId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("ProjectId", projectId) :
                new ObjectParameter("ProjectId", typeof(int));
    
            var orderIdParameter = orderId.HasValue ?
                new ObjectParameter("OrderId", orderId) :
                new ObjectParameter("OrderId", typeof(int));
    
            var taskIdParameter = taskId.HasValue ?
                new ObjectParameter("TaskId", taskId) :
                new ObjectParameter("TaskId", typeof(int));
    
            var timeRegisteredParameter = timeRegistered.HasValue ?
                new ObjectParameter("TimeRegistered", timeRegistered) :
                new ObjectParameter("TimeRegistered", typeof(int));
    
            var dateParameter = date.HasValue ?
                new ObjectParameter("Date", date) :
                new ObjectParameter("Date", typeof(System.DateTime));
    
            var dateEntryParameter = dateEntry.HasValue ?
                new ObjectParameter("DateEntry", dateEntry) :
                new ObjectParameter("DateEntry", typeof(System.DateTime));
    
            var commentParameter = comment != null ?
                new ObjectParameter("Comment", comment) :
                new ObjectParameter("Comment", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateTimeRegistration", updateIdParameter, userIdParameter, projectIdParameter, orderIdParameter, taskIdParameter, timeRegisteredParameter, dateParameter, dateEntryParameter, commentParameter);
        }
    
        public virtual int SP_RemoveUser(Nullable<int> removeId)
        {
            var removeIdParameter = removeId.HasValue ?
                new ObjectParameter("RemoveId", removeId) :
                new ObjectParameter("RemoveId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_RemoveUser", removeIdParameter);
        }
    
        public virtual int SP_UpdateUser(Nullable<int> updateId, string userName, string userAuthentity)
        {
            var updateIdParameter = updateId.HasValue ?
                new ObjectParameter("UpdateId", updateId) :
                new ObjectParameter("UpdateId", typeof(int));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var userAuthentityParameter = userAuthentity != null ?
                new ObjectParameter("UserAuthentity", userAuthentity) :
                new ObjectParameter("UserAuthentity", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateUser", updateIdParameter, userNameParameter, userAuthentityParameter);
        }
    
        public virtual int SP_AddUserAssignment(Nullable<int> userId, Nullable<int> projectId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("ProjectId", projectId) :
                new ObjectParameter("ProjectId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AddUserAssignment", userIdParameter, projectIdParameter);
        }
    
        public virtual int SP_RemoveUserAssignment(Nullable<int> removeId)
        {
            var removeIdParameter = removeId.HasValue ?
                new ObjectParameter("RemoveId", removeId) :
                new ObjectParameter("RemoveId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_RemoveUserAssignment", removeIdParameter);
        }
    
        public virtual int SP_UpdateUserAssignment(Nullable<int> updateId, Nullable<int> userId, Nullable<int> projectId)
        {
            var updateIdParameter = updateId.HasValue ?
                new ObjectParameter("UpdateId", updateId) :
                new ObjectParameter("UpdateId", typeof(int));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("ProjectId", projectId) :
                new ObjectParameter("ProjectId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateUserAssignment", updateIdParameter, userIdParameter, projectIdParameter);
        }
    
        public virtual int SP_AddOrderNumber(string numberName, Nullable<int> fK_RequestOrg, Nullable<int> fK_Requester, Nullable<int> fK_CustomerRef, string title)
        {
            var numberNameParameter = numberName != null ?
                new ObjectParameter("NumberName", numberName) :
                new ObjectParameter("NumberName", typeof(string));
    
            var fK_RequestOrgParameter = fK_RequestOrg.HasValue ?
                new ObjectParameter("FK_RequestOrg", fK_RequestOrg) :
                new ObjectParameter("FK_RequestOrg", typeof(int));
    
            var fK_RequesterParameter = fK_Requester.HasValue ?
                new ObjectParameter("FK_Requester", fK_Requester) :
                new ObjectParameter("FK_Requester", typeof(int));
    
            var fK_CustomerRefParameter = fK_CustomerRef.HasValue ?
                new ObjectParameter("FK_CustomerRef", fK_CustomerRef) :
                new ObjectParameter("FK_CustomerRef", typeof(int));
    
            var titleParameter = title != null ?
                new ObjectParameter("title", title) :
                new ObjectParameter("title", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AddOrderNumber", numberNameParameter, fK_RequestOrgParameter, fK_RequesterParameter, fK_CustomerRefParameter, titleParameter);
        }
    
        public virtual int SP_RemoveOrderNumber(Nullable<int> removeId)
        {
            var removeIdParameter = removeId.HasValue ?
                new ObjectParameter("RemoveId", removeId) :
                new ObjectParameter("RemoveId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_RemoveOrderNumber", removeIdParameter);
        }
    
        public virtual int SP_UpdateOrderNumber(Nullable<int> updateId, string orderNumberName, Nullable<int> fK_RequestOrg, Nullable<int> fK_Requester, Nullable<int> fK_CustomerRef, string title)
        {
            var updateIdParameter = updateId.HasValue ?
                new ObjectParameter("UpdateId", updateId) :
                new ObjectParameter("UpdateId", typeof(int));
    
            var orderNumberNameParameter = orderNumberName != null ?
                new ObjectParameter("OrderNumberName", orderNumberName) :
                new ObjectParameter("OrderNumberName", typeof(string));
    
            var fK_RequestOrgParameter = fK_RequestOrg.HasValue ?
                new ObjectParameter("FK_RequestOrg", fK_RequestOrg) :
                new ObjectParameter("FK_RequestOrg", typeof(int));
    
            var fK_RequesterParameter = fK_Requester.HasValue ?
                new ObjectParameter("FK_Requester", fK_Requester) :
                new ObjectParameter("FK_Requester", typeof(int));
    
            var fK_CustomerRefParameter = fK_CustomerRef.HasValue ?
                new ObjectParameter("FK_CustomerRef", fK_CustomerRef) :
                new ObjectParameter("FK_CustomerRef", typeof(int));
    
            var titleParameter = title != null ?
                new ObjectParameter("title", title) :
                new ObjectParameter("title", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateOrderNumber", updateIdParameter, orderNumberNameParameter, fK_RequestOrgParameter, fK_RequesterParameter, fK_CustomerRefParameter, titleParameter);
        }
    
        public virtual int SP_AddTimeType(string timeTypeName)
        {
            var timeTypeNameParameter = timeTypeName != null ?
                new ObjectParameter("timeTypeName", timeTypeName) :
                new ObjectParameter("timeTypeName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AddTimeType", timeTypeNameParameter);
        }
    
        public virtual int SP_RemoveTimeType(Nullable<int> removeId)
        {
            var removeIdParameter = removeId.HasValue ?
                new ObjectParameter("RemoveId", removeId) :
                new ObjectParameter("RemoveId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_RemoveTimeType", removeIdParameter);
        }
    
        public virtual int SP_UpdateTimeType(Nullable<int> updateId, string timeTypeName)
        {
            var updateIdParameter = updateId.HasValue ?
                new ObjectParameter("UpdateId", updateId) :
                new ObjectParameter("UpdateId", typeof(int));
    
            var timeTypeNameParameter = timeTypeName != null ?
                new ObjectParameter("timeTypeName", timeTypeName) :
                new ObjectParameter("timeTypeName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateTimeType", updateIdParameter, timeTypeNameParameter);
        }
    
        public virtual int SP_AddCustomerRef(string name)
        {
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AddCustomerRef", nameParameter);
        }
    
        public virtual int SP_AddPlatformOrProduct(string platformOrProject)
        {
            var platformOrProjectParameter = platformOrProject != null ?
                new ObjectParameter("platformOrProject", platformOrProject) :
                new ObjectParameter("platformOrProject", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AddPlatformOrProduct", platformOrProjectParameter);
        }
    
        public virtual int SP_AddRequester(string name)
        {
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AddRequester", nameParameter);
        }
    
        public virtual int SP_AddRequestOrg(string organization)
        {
            var organizationParameter = organization != null ?
                new ObjectParameter("Organization", organization) :
                new ObjectParameter("Organization", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AddRequestOrg", organizationParameter);
        }
    
        public virtual int SP_AddTurbine(string turbineName)
        {
            var turbineNameParameter = turbineName != null ?
                new ObjectParameter("turbineName", turbineName) :
                new ObjectParameter("turbineName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AddTurbine", turbineNameParameter);
        }
    
        public virtual int SP_RemoveCustomerRef(Nullable<int> removeId)
        {
            var removeIdParameter = removeId.HasValue ?
                new ObjectParameter("RemoveId", removeId) :
                new ObjectParameter("RemoveId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_RemoveCustomerRef", removeIdParameter);
        }
    
        public virtual int SP_RemoveRequester(Nullable<int> removeId)
        {
            var removeIdParameter = removeId.HasValue ?
                new ObjectParameter("RemoveId", removeId) :
                new ObjectParameter("RemoveId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_RemoveRequester", removeIdParameter);
        }
    
        public virtual int SP_RemoveRequestOrg(Nullable<int> removeId)
        {
            var removeIdParameter = removeId.HasValue ?
                new ObjectParameter("RemoveId", removeId) :
                new ObjectParameter("RemoveId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_RemoveRequestOrg", removeIdParameter);
        }
    
        public virtual int SP_RemoveTurbine(Nullable<int> removeId)
        {
            var removeIdParameter = removeId.HasValue ?
                new ObjectParameter("RemoveId", removeId) :
                new ObjectParameter("RemoveId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_RemoveTurbine", removeIdParameter);
        }
    
        public virtual int SP_UpdateCustomerRef(Nullable<int> updateId, string name)
        {
            var updateIdParameter = updateId.HasValue ?
                new ObjectParameter("UpdateId", updateId) :
                new ObjectParameter("UpdateId", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateCustomerRef", updateIdParameter, nameParameter);
        }
    
        public virtual int SP_UpdateRequester(Nullable<int> updateId, string name)
        {
            var updateIdParameter = updateId.HasValue ?
                new ObjectParameter("UpdateId", updateId) :
                new ObjectParameter("UpdateId", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateRequester", updateIdParameter, nameParameter);
        }
    
        public virtual int SP_UpdateRequestOrg(Nullable<int> updateId, string organization)
        {
            var updateIdParameter = updateId.HasValue ?
                new ObjectParameter("UpdateId", updateId) :
                new ObjectParameter("UpdateId", typeof(int));
    
            var organizationParameter = organization != null ?
                new ObjectParameter("Organization", organization) :
                new ObjectParameter("Organization", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateRequestOrg", updateIdParameter, organizationParameter);
        }
    
        public virtual int SP_UpdateTurbine(Nullable<int> updateId, string turbineName)
        {
            var updateIdParameter = updateId.HasValue ?
                new ObjectParameter("UpdateId", updateId) :
                new ObjectParameter("UpdateId", typeof(int));
    
            var turbineNameParameter = turbineName != null ?
                new ObjectParameter("TurbineName", turbineName) :
                new ObjectParameter("TurbineName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateTurbine", updateIdParameter, turbineNameParameter);
        }
    
        public virtual int SP_RemovePlatformOrProduct(Nullable<int> removeId)
        {
            var removeIdParameter = removeId.HasValue ?
                new ObjectParameter("RemoveId", removeId) :
                new ObjectParameter("RemoveId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_RemovePlatformOrProduct", removeIdParameter);
        }
    
        public virtual int SP_UpdatePlatformOrProduct(Nullable<int> updateId, string productName)
        {
            var updateIdParameter = updateId.HasValue ?
                new ObjectParameter("UpdateId", updateId) :
                new ObjectParameter("UpdateId", typeof(int));
    
            var productNameParameter = productName != null ?
                new ObjectParameter("ProductName", productName) :
                new ObjectParameter("ProductName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdatePlatformOrProduct", updateIdParameter, productNameParameter);
        }
    
        public virtual int SP_AddNewUser(string name, string auth)
        {
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var authParameter = auth != null ?
                new ObjectParameter("Auth", auth) :
                new ObjectParameter("Auth", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AddNewUser", nameParameter, authParameter);
        }
    
        public virtual int SP_AddTaskType(string taskTypeName)
        {
            var taskTypeNameParameter = taskTypeName != null ?
                new ObjectParameter("taskTypeName", taskTypeName) :
                new ObjectParameter("taskTypeName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AddTaskType", taskTypeNameParameter);
        }
    
        public virtual int SP_AddUser(string name, string auth)
        {
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var authParameter = auth != null ?
                new ObjectParameter("Auth", auth) :
                new ObjectParameter("Auth", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AddUser", nameParameter, authParameter);
        }
    }
}
