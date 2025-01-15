using System;
using System.Data;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200003D RID: 61
	internal static class ExecutionLog
	{
		// Token: 0x060001CC RID: 460 RVA: 0x0000E9AA File Offset: 0x0000CBAA
		internal static void Init()
		{
			CancelablePhaseBase.JobEnd += ExecutionLog.OnJobEnd;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		private static void OnJobEnd(CancelablePhaseBase sender, RunningJobContext context)
		{
			bool flag = false;
			if (sender != null)
			{
				Type[] knownTypes = ExecutionLog.m_knownTypes;
				for (int i = 0; i < knownTypes.Length; i++)
				{
					if (knownTypes[i].IsInstanceOfType(sender))
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			if (sender is RenderForExistingSession)
			{
				Extension extension = Globals.Configuration.Extensions.Renderer[context.ExecutionInfo.Format];
				if (sender is RenderForStream || (extension != null && !extension.LogAllExecutionRequests))
				{
					return;
				}
			}
			bool flag2 = false;
			if (sender is ProcessReportParametersCancelableStep)
			{
				flag2 = true;
			}
			ConnectionManager connectionManager = new ConnectionManager(ConnectionTransactionType.Explicit, IsolationLevel.RepeatableRead);
			connectionManager.WillDisconnectStorage();
			try
			{
				DBInterface dbinterface = new DBInterface(context.UserContext);
				dbinterface.ConnectionManager = connectionManager;
				if (ExecutionLog.IsLoggingEnabled(dbinterface))
				{
					string text = ExecutionLog.ConstructMachineAndInstanceName(context.Machine);
					ExternalItemPath path = context.Path;
					DBInterface.RequestType requestType = ExecutionLog.GetRequestType(context);
					foreach (ReportExecutionInfo reportExecutionInfo in context.ExecutionInfos)
					{
						if (!flag2 || reportExecutionInfo != context.ExecutionInfo)
						{
							ExecutionLog.LogExecutionInfo(dbinterface, text, path, context.StartDate, reportExecutionInfo, requestType);
						}
					}
				}
				dbinterface.Commit();
			}
			finally
			{
				connectionManager.DisconnectStorage();
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000EB18 File Offset: 0x0000CD18
		private static DBInterface.RequestType GetRequestType(RunningJobContext jobContext)
		{
			DBInterface.RequestType requestType;
			if (jobContext.Action == JobActionEnum.RefreshCache)
			{
				requestType = DBInterface.RequestType.RefreshCache;
			}
			else
			{
				requestType = (Globals.IsServiceProcess ? DBInterface.RequestType.Subscription : DBInterface.RequestType.Interactive);
			}
			return requestType;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000EB40 File Offset: 0x0000CD40
		private static void LogExecutionInfo(DBInterface storage, string machineAndInstance, ExternalItemPath contextPath, DateTime startDate, ReportExecutionInfo executionInfo, DBInterface.RequestType reqType)
		{
			ExternalItemPath externalItemPath = executionInfo.ItemPath ?? contextPath;
			CatalogItemPath catalogItemPath = CatalogItemPath.Empty;
			if (!externalItemPath.IsEditSession)
			{
				catalogItemPath = new RSService(false).ExternalToCatalogItemPath(externalItemPath);
			}
			storage.AddExecutionLogEntry(machineAndInstance, catalogItemPath, reqType, executionInfo.Format, executionInfo.Parameters, startDate, DateTime.Now, (int)executionInfo.DataRetrievalTime.TotalMilliseconds, (int)executionInfo.ProcessingTime.TotalMilliseconds, (int)executionInfo.RenderingTime.TotalMilliseconds, executionInfo.Source, executionInfo.Status.ToString(), executionInfo.ByteCount, executionInfo.RowCount, executionInfo.ExecutionId, (byte)executionInfo.EventType, executionInfo.GetAdditionalInfoXml());
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000EC03 File Offset: 0x0000CE03
		private static bool IsLoggingEnabled(DBInterface storage)
		{
			return new BooleanParameter(CachedSystemProperties.Instance, Global.m_Tracer, "EnableExecutionLogging", CachedSystemProperties.Get("EnableExecutionLogging"), true, string.Empty).Value;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000EC30 File Offset: 0x0000CE30
		private static string ConstructMachineAndInstanceName(string machineName)
		{
			string instanceName = Globals.Configuration.InstanceName;
			string text;
			if (!string.IsNullOrEmpty(instanceName))
			{
				text = machineName + "\\" + instanceName;
			}
			else
			{
				text = machineName;
			}
			return text;
		}

		// Token: 0x04000135 RID: 309
		private static readonly Type[] m_knownTypes = new Type[]
		{
			typeof(RenderForNewSession),
			typeof(RenderForExistingSession),
			typeof(CreateHistorySnapshotCancelableStep),
			typeof(UpdateExecutionSnapshotCancelableStep),
			typeof(CancelableReportProcessingEvent),
			typeof(CreateReportCacheEntry),
			typeof(CreateDataSetCacheEntry),
			typeof(ProcessReportParametersCancelableStep),
			typeof(RenderEditCancelableStep)
		};
	}
}
