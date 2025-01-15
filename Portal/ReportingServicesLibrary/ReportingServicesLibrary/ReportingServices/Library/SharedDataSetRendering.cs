using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200004D RID: 77
	internal static class SharedDataSetRendering
	{
		// Token: 0x0600038A RID: 906 RVA: 0x0000FDC8 File Offset: 0x0000DFC8
		internal static byte[] GetSharedDataSetBytes(RSService rsService, Report baseReport, DataSet dataSet, string renderFormat, string deviceInfo = null)
		{
			byte[] rdlReportBytes = SharedDataSetJsonRendering.GetRdlReportBytes(baseReport);
			CreateEditSessionParameters createEditSessionParameters = SharedDataSetRendering.CreateReportEditSession(rsService, rdlReportBytes);
			ExternalItemPath externalItemPath = ExternalItemPath.ConstructFromEditSessionPath(createEditSessionParameters.EditSessionID);
			SnapshotNoOpClientRequest snapshotNoOpClientRequest = new SnapshotNoOpClientRequest(rsService.UserContext, externalItemPath);
			byte[] array;
			try
			{
				using (rsService.SetStreamFactory(new MemoryThenFileStreamFactory()))
				{
					array = SharedDataSetRendering.RenderDataSet(rsService, snapshotNoOpClientRequest, renderFormat, deviceInfo);
				}
			}
			finally
			{
				ReportServerThreadPool.QueueUserWorkItem(delegate(object o)
				{
					string text = (string)o;
					RSService rsservice = new RSService(false);
					rsservice.WillDisconnectStorage();
					try
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Info, "Deleting item with id {0} asynchronously...", new object[] { text });
						DeleteItemAction deleteItemAction = rsservice.DeleteItemAction;
						deleteItemAction.ActionParameters.ItemPath = text;
						deleteItemAction.Execute();
					}
					catch (Exception ex)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Error while deleting with id {0} asynchronously..., {1}:{2}", new object[]
						{
							text,
							ex.GetType().ToString(),
							ex.Message
						});
					}
					finally
					{
						rsservice.DisconnectStorage();
					}
				}, createEditSessionParameters.EditSessionID);
			}
			return array;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000FE70 File Offset: 0x0000E070
		internal static byte[] RenderDataSet(RSService rsService, ClientRequest session, string format, string deviceInfo)
		{
			RenderReportAction renderReportAction = RenderReportAction.CreateWithFormatDeviceInfo(session, rsService, format, deviceInfo, PageCountMode.Estimate);
			renderReportAction.JobType = JobType.SystemJobType;
			renderReportAction.Render();
			return renderReportAction.Result;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000FE94 File Offset: 0x0000E094
		internal static CreateEditSessionParameters CreateReportEditSession(RSService rsService, byte[] reportBytes)
		{
			CreateReportEditSessionAction createReportEditSessionAction = new CreateReportEditSessionAction(rsService);
			createReportEditSessionAction.ActionParameters.ReportDefinition = reportBytes;
			createReportEditSessionAction.ActionParameters.ItemName = Guid.NewGuid().ToString();
			createReportEditSessionAction.ActionParameters.ParentPath = "/";
			createReportEditSessionAction.Execute();
			return createReportEditSessionAction.ActionParameters;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000FEEC File Offset: 0x0000E0EC
		internal static string GetDataSourceExtensionForDataSet(RSService rsService, string path)
		{
			GetItemDataSourcesAction getItemDataSourcesAction = rsService.GetItemDataSourcesAction;
			getItemDataSourcesAction.ActionParameters.ItemPath = path;
			getItemDataSourcesAction.ActionParameters.InternalUsePermissionForExecution = true;
			getItemDataSourcesAction.Execute();
			string text = string.Empty;
			Microsoft.ReportingServices.Library.Soap2005.DataSource dataSource = getItemDataSourcesAction.ActionParameters.DataSources.FirstOrDefault<Microsoft.ReportingServices.Library.Soap2005.DataSource>();
			if (dataSource != null)
			{
				DataSourceReference dataSourceReference = dataSource.Item as DataSourceReference;
				if (dataSourceReference != null)
				{
					text = dataSourceReference.Reference;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				GetDataSourceContentsAction getDataSourceContentsAction = rsService.GetDataSourceContentsAction;
				getDataSourceContentsAction.ActionParameters.DataSourcePath = text;
				getDataSourceContentsAction.ActionParameters.InternalUsePermissionForExecution = true;
				getDataSourceContentsAction.Execute();
				return getDataSourceContentsAction.ActionParameters.DataSourceDefinition.Extension;
			}
			return string.Empty;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000FF8D File Offset: 0x0000E18D
		internal static byte[] LoadDataSetDefinition(RSService rsService, string dataSetPath)
		{
			GetDataSetDefinitionAction getDataSetDefinitionAction = new GetDataSetDefinitionAction(rsService);
			getDataSetDefinitionAction.ActionParameters.ItemPath = dataSetPath;
			getDataSetDefinitionAction.Execute();
			return getDataSetDefinitionAction.ActionParameters.DataSetDefinition;
		}
	}
}
