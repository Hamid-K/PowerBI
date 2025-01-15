using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000260 RID: 608
	internal class EnableCommentsTask : UpgradeTask
	{
		// Token: 0x0600160E RID: 5646 RVA: 0x000580B6 File Offset: 0x000562B6
		public EnableCommentsTask(UpgradePollWorker worker)
			: base(worker)
		{
			this.CommentsTaskId = EnableCommentsTask.GetCatalogItemTaskIdByType(AuthzData.CatalogItemTaskEnum.Comment);
			this.ManageCommentsTaskId = EnableCommentsTask.GetCatalogItemTaskIdByType(AuthzData.CatalogItemTaskEnum.ManageComments);
			this.ViewReportsTaskId = EnableCommentsTask.GetCatalogItemTaskIdByType(AuthzData.CatalogItemTaskEnum.ViewReports);
			this.ManageReportsTaskId = EnableCommentsTask.GetCatalogItemTaskIdByType(AuthzData.CatalogItemTaskEnum.ManageReports);
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x000580F1 File Offset: 0x000562F1
		public override string Name
		{
			get
			{
				return "EnableComments";
			}
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x000580F8 File Offset: 0x000562F8
		public override void PerformUpgrade(string status)
		{
			if (status != null && Localization.CatalogCultureCompare(status, "True") == 0)
			{
				this.m_finished = true;
				return;
			}
			Security security = new Security(new UserContext(WebConfigUtil.WebServerAuthMode), false);
			security.ConnectionManager = this.ConnectionManager;
			Role[] array = security.CatalogGetRoleList(AuthzData.SecurityScope.CatalogItem, true);
			bool flag = true;
			RSTrace.CatalogTrace.Assert(this.ConnectionManager.ConnectionTransactionType == ConnectionTransactionType.AutoCommit);
			try
			{
				this.ConnectionManager.ConnectionTransactionType = ConnectionTransactionType.Explicit;
				this.ConnectionManager.BeginTransaction();
				foreach (Role role in array)
				{
					flag = this.EnableCommentsOnRole(role.Name, security);
					if (!flag)
					{
						break;
					}
				}
				if (flag)
				{
					this.ConnectionManager.CommitTransaction();
				}
				else
				{
					this.ConnectionManager.AbortTransaction();
				}
			}
			finally
			{
				this.ConnectionManager.ConnectionTransactionType = ConnectionTransactionType.AutoCommit;
			}
			this.m_finished = flag;
			if (this.m_finished)
			{
				base.SetUpgradeItemStatus("True");
			}
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x000581F4 File Offset: 0x000563F4
		private bool EnableCommentsOnRole(string roleName, Security security)
		{
			string text;
			Microsoft.ReportingServices.Library.Soap.Task[] array;
			security.CatalogGetRoleProperties(roleName, out text, out array);
			string[] taskIdsWithCommentsPermissions = this.GetTaskIdsWithCommentsPermissions(array);
			if (taskIdsWithCommentsPermissions.Length == array.Length)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Info, "No permissions updates were needed for role: {0}", new object[] { roleName });
				return true;
			}
			AuthzData.SecurityScope securityScope;
			string text2 = AuthzData.TaskList.TaskListToTaskMask(taskIdsWithCommentsPermissions, out securityScope);
			try
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Start of enabling of comments for role: {0}", new object[] { roleName });
				security.SetRolePropertiesAndInvalidatePolicies(roleName, text, text2, securityScope);
				RSTrace.CatalogTrace.Trace(TraceLevel.Info, "End of enabling of comments for role: {0}", new object[] { roleName });
			}
			catch (Exception ex)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Error on enabling comments for role: {0}", new object[] { roleName });
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, ex.ToString());
				return false;
			}
			return true;
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x000582CC File Offset: 0x000564CC
		private string[] GetTaskIdsWithCommentsPermissions(Microsoft.ReportingServices.Library.Soap.Task[] tasks)
		{
			List<string> list = tasks.Select((Microsoft.ReportingServices.Library.Soap.Task task) => task.TaskID).ToList<string>();
			if (list.Contains(this.ViewReportsTaskId) && !list.Contains(this.CommentsTaskId))
			{
				list.Add(this.CommentsTaskId);
			}
			if (list.Contains(this.ManageReportsTaskId) && !list.Contains(this.ManageCommentsTaskId))
			{
				list.Add(this.ManageCommentsTaskId);
			}
			return list.ToArray();
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0005835C File Offset: 0x0005655C
		private static string GetCatalogItemTaskIdByType(AuthzData.CatalogItemTaskEnum type)
		{
			string text = null;
			foreach (AuthzData.CatalogItemTask catalogItemTask in AuthzData.m_CatalogItemTasks)
			{
				if (catalogItemTask.TaskType == type)
				{
					return catalogItemTask.ID;
				}
			}
			return text;
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x00058394 File Offset: 0x00056594
		public override bool Finished
		{
			get
			{
				return this.m_finished;
			}
		}

		// Token: 0x0400080A RID: 2058
		private const string GoodStatus = "True";

		// Token: 0x0400080B RID: 2059
		private readonly string CommentsTaskId;

		// Token: 0x0400080C RID: 2060
		private readonly string ManageCommentsTaskId;

		// Token: 0x0400080D RID: 2061
		private readonly string ViewReportsTaskId;

		// Token: 0x0400080E RID: 2062
		private readonly string ManageReportsTaskId;

		// Token: 0x0400080F RID: 2063
		private bool m_finished;
	}
}
