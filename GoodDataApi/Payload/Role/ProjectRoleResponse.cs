using System;

namespace GoodDataApi.Payload.Role
{
	public class ProjectRolePermissions
	{
		public bool CanAccessIntegration;
		public bool CanCreateProjectDashboard;
		public bool CanManageComment;
		public bool CanCreateDimensionMapping;
		public bool CanInitData;
		public bool CanManageIntegration;
		public bool CanManageFolder;
		public bool CanInviteUserToProject;
		public bool CanCreateDomain;
		public bool CanCreateTableDataLoad;
		public bool CanSeeOtherUserDetails;
		public bool CanCreateEtlInterface;
		public bool CanCreateRole;
		public bool CanCreateReportResult;
		public bool CanCreateHelp;
		public bool CanManageDomain;
		public bool CanManageAttributeLabel;
		public bool CanCreateColumn;
		public bool CanManageReport;
		public bool CanManageDataSet;
		public bool CanSetUserVariables;
		public bool CanCreateAttributeGroup;
		public bool CanValidateProject;
		public bool CanMaintainProject;
		public bool CanCreateEtlFile;
		public bool CanCreateScheduledMail;
		public bool CanManageEtlSession;
		public bool CanSuspendUserFromProject;
		public bool CanMaintainUserFilterRelation;
		public bool CanManageAttribute;
		public bool CanManageReportDefinition;
		public bool CanCreateReportResult2;
		public bool CanMaintainUserFilter;
		public bool CanCreateReport;
		public bool CanManageEtlFile;
		public bool CanCreateComment;
		public bool CanCreateDataSet;
		public bool CanCreateTable;
		public bool CanManageTableDataLoad;
		public bool CanManageDimensionMapping;
		public bool CanCreateMetric;
		public bool CanRefreshData;
		public bool CanManageProjectDashboard;
		public bool CanManageProject;
		public bool CanManagePrompt;
		public bool CanManageEtlInterface;
		public bool CanManageReportResult2;
		public bool CanAccessWorkbench;
		public bool CanCreateAttributeLabel;
		public bool CanManageColumn;
		public bool CanCreatePrompt;
		public bool CanManagePublicAccessCode;
		public bool CanListUsersInProject;
		public bool CanManageAttributeGroup;
		public bool CanManageMetric;
		public bool CanManageHelp;
		public bool CanManageTable;
		public bool CanSetProjectVariables;
		public bool CanCreateEtlSession;
		public bool CanCreateFolder;
		public bool CanManageFact;
		public bool CanListInvitationsInProject;
		public bool CanManageScheduledMail;
		public bool CanManageReportResult;
		public bool CanManageAnnotation;
		public bool CanSeePublicAccessCode;
		public bool CanCreateReportDefinition;
		public bool CanCreateFact;
		public bool CanCreateAttribute;
		public bool CanAssignUserWithRole;
		public bool CanCreateAnnotation;
	}

	public class ProjectRoleLinks
	{
		public string RoleUsers;
	}

	public class ProjectRoleMetadata
	{
		public DateTime Created;
		public string Identifier;
		public string Summary;
		public string Author;
		public DateTime Updated;
		public string Title;
		public string Contributor;
	}

	public class ProjectRole
	{
		public ProjectRolePermissions Permissions;
		public ProjectRoleLinks Links;
		public ProjectRoleMetadata Meta;
	}

	public class ProjectRoleResponse
	{
		public ProjectRole ProjectRole;
	}
}