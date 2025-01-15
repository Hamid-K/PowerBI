using System;
using System.Collections;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200021D RID: 541
	internal class ScheduleCoordinator : Storage
	{
		// Token: 0x06001332 RID: 4914 RVA: 0x00044884 File Offset: 0x00042A84
		public ScheduleCoordinator(UserContext userContext)
		{
			this.m_userContext = userContext;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x000448A9 File Offset: 0x00042AA9
		public ScheduleCoordinator(RSService service)
		{
			this.m_service = service;
			this.m_userContext = service.UserContext;
			this.m_securityMgr = service.SecMgr;
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x000448F4 File Offset: 0x00042AF4
		// (set) Token: 0x06001334 RID: 4916 RVA: 0x000448E6 File Offset: 0x00042AE6
		public override ConnectionManager ConnectionManager
		{
			get
			{
				return this.m_dbIface.ConnectionManager;
			}
			set
			{
				this.m_dbIface.ConnectionManager = value;
			}
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00044904 File Offset: 0x00042B04
		public string CreateTask(Guid taskId, string name, ScheduleDefinition taskSchedule, ExternalItemPath externalPath)
		{
			if (taskSchedule == null)
			{
				throw new MissingParameterException("ScheduleDefinition");
			}
			CatalogItemPath catalogItemPath = this.m_service.ExternalToCatalogItemPath(externalPath);
			this.CheckExistanceAndAccess(catalogItemPath, CatalogOperation.CreateSchedules);
			Microsoft.ReportingServices.Diagnostics.Task task = new Microsoft.ReportingServices.Diagnostics.Task(taskId);
			task.Name = name;
			task.Path = catalogItemPath;
			task.EventType = InternalEvents.SharedSchedule.ToString();
			task.EventData = task.ID.ToString();
			task.Type = ScheduleType.Shared;
			taskSchedule.PopulateTaskWithThis(task);
			task.Creator = this.m_userContext;
			this.m_dbIface.CreateTask(task);
			if (this.m_tracer.TraceInfo)
			{
				this.m_tracer.Trace("Schedule Created at {0} by {1}", new object[]
				{
					DateTime.Now,
					Environment.UserName
				});
			}
			return task.ID.ToString();
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x000449EC File Offset: 0x00042BEC
		private void ThrowScopedScheduleDataInvalidError(ReportCatalogException re, string scheduleData)
		{
			Guid guid = Guid.Empty;
			try
			{
				guid = new Guid(scheduleData);
			}
			catch (FormatException)
			{
			}
			if (guid != Guid.Empty)
			{
				throw new InvalidParameterCombinationException();
			}
			throw new InvalidParameterException("scheduleData", re);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00044A3C File Offset: 0x00042C3C
		public void GetSnapshotSchedule(Guid reportID, out ScheduleDefinitionOrReference schedule)
		{
			Microsoft.ReportingServices.Diagnostics.Task snapShotSchedule = this.m_dbIface.GetSnapShotSchedule(reportID);
			schedule = new NoSchedule();
			if (snapShotSchedule != null)
			{
				if (snapShotSchedule.Type == ScheduleType.Shared)
				{
					schedule = ScheduleReference.TaskToThis(snapShotSchedule);
					return;
				}
				schedule = ScheduleDefinition.TaskToThis(snapShotSchedule);
			}
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00044A7C File Offset: 0x00042C7C
		public void DeleteTask(Guid taskId)
		{
			Microsoft.ReportingServices.Diagnostics.Task task = this.m_dbIface.GetTask(taskId);
			this.CheckExistanceAndAccess(task.Path, CatalogOperation.DeleteSchedules);
			if (task.Type != ScheduleType.Shared)
			{
				throw new ScheduleNotFoundException(taskId.ToString());
			}
			ArrayList arrayList = this.m_dbIface.ListScheduledReports(taskId, true);
			SubscriptionDB subscriptionDB = new SubscriptionDB();
			subscriptionDB.ConnectionManager = this.ConnectionManager;
			string text = task.ToXml(true);
			ScheduleDefinition scheduleDefinition = ScheduleDefinition.TaskToThis(task);
			foreach (object obj in arrayList)
			{
				ItemScheduleAction itemScheduleAction = (ItemScheduleAction)obj;
				ReportScheduleActions action = itemScheduleAction.Action;
				if (action - ReportScheduleActions.UpdateReportExecutionSnapshot > 2)
				{
					if (action - ReportScheduleActions.TimedSubscription <= 1)
					{
						SubscriptionImpl subscription = subscriptionDB.GetSubscription(itemScheduleAction.SubscriptionID, this.m_service, itemScheduleAction.Action == ReportScheduleActions.RefreshCache);
						subscription.m_matchData = text;
						subscriptionDB.UpdateSubscription(subscription);
						this.m_dbIface.DeleteTimeBasedSubscriptionSchedule(itemScheduleAction.SubscriptionID);
						Microsoft.ReportingServices.Diagnostics.Task task2 = new Microsoft.ReportingServices.Diagnostics.Task(Guid.Empty);
						task2.FromXml(text);
						task2.EventType = subscription.EventType;
						task2.EventData = itemScheduleAction.SubscriptionID.ToString();
						task2.Creator = subscription.Owner;
						task2.Path = this.m_service.ExternalToCatalogItemPath(this.m_service.ServiceHelper.GetSiteUrl(subscription.ItemPath, this.m_userContext));
						this.m_dbIface.CreateTimeBasedSubscriptionSchedule(itemScheduleAction.ItemPath, itemScheduleAction.SubscriptionID, itemScheduleAction.Action, task2);
					}
				}
				else
				{
					this.SetReportSchedule(itemScheduleAction.ItemID, scheduleDefinition, itemScheduleAction.Action);
				}
			}
			this.m_dbIface.DeleteTask(taskId);
			if (this.m_tracer.TraceInfo)
			{
				this.m_tracer.Trace("Schedule {0} deleted by {1} at {2}.", new object[]
				{
					taskId,
					Environment.UserName,
					DateTime.Now
				});
			}
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00044CA8 File Offset: 0x00042EA8
		public Guid SetReportSchedule(Guid reportID, ScheduleDefinitionOrReference scheduleData, ReportScheduleActions reportAction)
		{
			Guid guid = Guid.Empty;
			ReportScheduleActions reportScheduleActions = ReportScheduleActions.None;
			Microsoft.ReportingServices.Diagnostics.Task task;
			if (reportAction == ReportScheduleActions.InvalidateCache || reportAction == ReportScheduleActions.UpdateReportExecutionSnapshot)
			{
				task = this.m_dbIface.GetCacheSchedule(reportID, out reportScheduleActions);
			}
			else
			{
				task = this.m_dbIface.GetSnapShotSchedule(reportID);
				reportScheduleActions = ReportScheduleActions.CreateReportHistorySnapshot;
			}
			if (task != null)
			{
				this.m_dbIface.RemoveReportFromSchedule(task.ID, reportID, Guid.Empty, reportScheduleActions);
			}
			if (scheduleData != null && !(scheduleData is NoSchedule))
			{
				if (scheduleData is ScheduleDefinition)
				{
					ScheduleDefinition scheduleDefinition = (ScheduleDefinition)scheduleData;
					task = new Microsoft.ReportingServices.Diagnostics.Task(Guid.Empty);
					guid = task.ID;
					scheduleDefinition.PopulateTaskWithThis(task);
					switch (reportAction)
					{
					case ReportScheduleActions.UpdateReportExecutionSnapshot:
						task.EventType = InternalEvents.ReportExecutionUpdateSchedule.ToString();
						break;
					case ReportScheduleActions.CreateReportHistorySnapshot:
						task.EventType = InternalEvents.ReportHistorySchedule.ToString();
						break;
					case ReportScheduleActions.InvalidateCache:
						task.EventType = InternalEvents.CacheInvalidateSchedule.ToString();
						break;
					default:
						throw new InternalCatalogException("Invalid reportAction in SetReportSchedule");
					}
					task.EventData = reportID.ToString();
					task.Creator = this.m_userContext;
					this.m_dbIface.CreateTask(task);
					ItemScheduleAction itemScheduleAction = new ItemScheduleAction();
					itemScheduleAction.ItemID = reportID;
					itemScheduleAction.ScheduleID = task.ID;
					itemScheduleAction.Action = reportAction;
					this.m_dbIface.AddReportToSchedule(itemScheduleAction);
				}
				else
				{
					guid = Globals.ParseGuidElement(((ScheduleReference)scheduleData).ScheduleID, "ScheduleID");
					task = this.m_dbIface.GetTask(guid);
					if (task.Type != ScheduleType.Shared)
					{
						throw new ScheduleNotFoundException(guid.ToString());
					}
					ItemScheduleAction itemScheduleAction2 = new ItemScheduleAction();
					itemScheduleAction2.ItemID = reportID;
					itemScheduleAction2.ScheduleID = task.ID;
					if (reportAction - ReportScheduleActions.UpdateReportExecutionSnapshot > 2)
					{
						throw new InternalCatalogException("Invalid reportAction in SetReportSchedule");
					}
					itemScheduleAction2.Action = reportAction;
					this.m_dbIface.AddReportToSchedule(itemScheduleAction2);
				}
			}
			return guid;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00044E88 File Offset: 0x00043088
		public void SetTaskProperties(Guid taskId, string name, ScheduleDefinition taskSchedule)
		{
			Microsoft.ReportingServices.Diagnostics.Task task = this.m_dbIface.GetTask(taskId);
			this.CheckExistanceAndAccess(task.Path, CatalogOperation.UpdateSchedules);
			if (task.Type != ScheduleType.Shared)
			{
				throw new ScheduleNotFoundException(taskId.ToString());
			}
			Microsoft.ReportingServices.Diagnostics.Task task2 = this.UpdateTask(taskId, task, taskSchedule);
			task2.Name = name;
			this.m_dbIface.UpdateTaskProperties(task2, true);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00044EEC File Offset: 0x000430EC
		private Microsoft.ReportingServices.Diagnostics.Task UpdateTask(Guid newTaskID, Microsoft.ReportingServices.Diagnostics.Task oldTask, ScheduleDefinition newTaskSchedule)
		{
			Microsoft.ReportingServices.Diagnostics.Task task = new Microsoft.ReportingServices.Diagnostics.Task(newTaskID);
			newTaskSchedule.PopulateTaskWithThis(task);
			task.EventData = oldTask.EventData;
			task.EventType = oldTask.EventType;
			task.LastRunStatus = oldTask.LastRunStatus;
			task.LastRunTime = oldTask.LastRunTime;
			task.DatabaseState = oldTask.DatabaseState;
			task.Name = oldTask.Name;
			return task;
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00044F50 File Offset: 0x00043150
		public Microsoft.ReportingServices.Diagnostics.Task GetTaskProperties(Guid taskId)
		{
			Microsoft.ReportingServices.Diagnostics.Task task = this.m_dbIface.GetTask(taskId);
			this.CheckExistanceAndAccess(task.Path, CatalogOperation.ReadSchedules);
			if (task.Type != ScheduleType.Shared)
			{
				throw new ScheduleNotFoundException(taskId.ToString());
			}
			return task;
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00044F94 File Offset: 0x00043194
		public Schedule[] ListTasksAsArray(ExternalItemPath externalPath)
		{
			CatalogItemPath catalogItemPath = this.m_service.ExternalToCatalogItemPath(externalPath);
			this.CheckExistanceAndAccess(catalogItemPath, CatalogOperation.ReadSchedules);
			return Schedule.TaskArrayToThisArray(this.m_dbIface.ListTasks(catalogItemPath));
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00044FC8 File Offset: 0x000431C8
		public CatalogItemList ListScheduledReports(Guid taskId)
		{
			Microsoft.ReportingServices.Diagnostics.Task task = this.m_dbIface.GetTask(taskId);
			this.CheckExistanceAndAccess(task.Path, CatalogOperation.ReadSchedules);
			ArrayList arrayList = this.m_dbIface.ListScheduledReports(taskId, false);
			CatalogItemList catalogItemList = new CatalogItemList();
			foreach (object obj in arrayList)
			{
				ItemScheduleAction itemScheduleAction = (ItemScheduleAction)obj;
				if (itemScheduleAction.ScheduleType != ScheduleType.Shared)
				{
					throw new ScheduleNotFoundException(taskId.ToString());
				}
				ExternalItemPath externalItemPath = this.m_service.CatalogToExternal(itemScheduleAction.ItemPath, itemScheduleAction.ItemZone);
				try
				{
					if (!this.m_securityMgr.CheckAccess(ItemType.Report, itemScheduleAction.ReportSecurityDescriptor, ReportOperation.ReadProperties, externalItemPath))
					{
						continue;
					}
				}
				catch (AccessDeniedException)
				{
					continue;
				}
				catalogItemList.Add(new CatalogItemDescriptor
				{
					ID = itemScheduleAction.ItemID.ToString(),
					Name = itemScheduleAction.ItemName,
					Path = externalItemPath,
					Type = itemScheduleAction.ItemType,
					Description = itemScheduleAction.Description,
					ModifiedBy = itemScheduleAction.ModifiedUser,
					ModifiedDate = itemScheduleAction.ModifiedDate,
					Size = itemScheduleAction.ReportSize,
					ExecutionDate = itemScheduleAction.ExecutionTime
				});
			}
			return catalogItemList;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00045154 File Offset: 0x00043354
		public void PauseTask(Guid taskId)
		{
			Microsoft.ReportingServices.Diagnostics.Task task = this.m_dbIface.GetTask(taskId);
			this.CheckExistanceAndAccess(task.Path, CatalogOperation.UpdateSchedules);
			if (task.Type != ScheduleType.Shared)
			{
				throw new ScheduleNotFoundException(taskId.ToString());
			}
			bool flag = false;
			TaskState scheduleState = task.ScheduleState;
			if (scheduleState != TaskState.Ready)
			{
				if (scheduleState - TaskState.Paused <= 1)
				{
					return;
				}
			}
			else
			{
				task.ScheduleState = TaskState.Paused;
				flag = true;
			}
			if (flag)
			{
				this.m_dbIface.PauseSchedule(task);
			}
			if (this.m_tracer.TraceInfo)
			{
				this.m_tracer.Trace("Schedule {0} Paused by {1} at {2}.", new object[]
				{
					taskId,
					Environment.UserName,
					DateTime.Now
				});
			}
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00045204 File Offset: 0x00043404
		public void ResumeTask(Guid taskId)
		{
			Microsoft.ReportingServices.Diagnostics.Task task = this.m_dbIface.GetTask(taskId);
			this.CheckExistanceAndAccess(task.Path, CatalogOperation.UpdateSchedules);
			if (task.Type != ScheduleType.Shared)
			{
				throw new ScheduleNotFoundException(taskId.ToString());
			}
			bool flag = false;
			switch (task.ScheduleState)
			{
			case TaskState.Ready:
			case TaskState.Expired:
				return;
			case TaskState.Paused:
				task.ScheduleState = TaskState.Ready;
				flag = true;
				break;
			}
			if (flag)
			{
				this.m_dbIface.ResumeSchedule(task);
			}
			if (this.m_tracer.TraceInfo)
			{
				this.m_tracer.Trace("Schedule {0} resumed by {1} at {2}.", new object[]
				{
					taskId,
					Environment.UserName,
					DateTime.Now
				});
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000452C0 File Offset: 0x000434C0
		private void CheckExistanceAndAccess(CatalogItemPath path, CatalogOperation operation)
		{
			ExternalItemPath externalItemPath = this.m_service.CatalogToExternal(path);
			this.m_service.ServiceHelper.CheckItemType(ItemType.Site, externalItemPath);
			if (!this.m_securityMgr.CheckAccess(this.m_securityMgr.SystemSecDesc, operation, externalItemPath))
			{
				throw new AccessDeniedException(this.m_service.UserName, ErrorCode.rsAccessDenied);
			}
		}

		// Token: 0x040006E0 RID: 1760
		protected SchedulingDBInterface m_dbIface = new SchedulingDBInterface();

		// Token: 0x040006E1 RID: 1761
		protected RSTrace m_tracer = RSTrace.ScheduleTracer;

		// Token: 0x040006E2 RID: 1762
		private RSService m_service;

		// Token: 0x040006E3 RID: 1763
		private UserContext m_userContext;

		// Token: 0x040006E4 RID: 1764
		private Security m_securityMgr;
	}
}
