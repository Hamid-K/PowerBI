using System;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000201 RID: 513
	internal class ReportingServiceSPImpl : ReportingService2005Impl
	{
		// Token: 0x06001222 RID: 4642 RVA: 0x00040E17 File Offset: 0x0003F017
		internal ReportingServiceSPImpl(RSService service, GetExceptionForEndpoint getExceptionForEndpoint)
			: base(service, getExceptionForEndpoint)
		{
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00040E24 File Offset: 0x0003F024
		internal CatalogItemList ListParents(string Item)
		{
			CatalogItemList parents;
			try
			{
				ListParentsAction listParentsAction = base.Service.ListParentsAction;
				listParentsAction.ActionParameters.ItemPath = Item;
				listParentsAction.Execute();
				parents = listParentsAction.ActionParameters.Parents;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
			return parents;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00040E7C File Offset: 0x0003F07C
		internal void CreateFolder(string Folder, string Parent, out CatalogItem ItemInfo)
		{
			try
			{
				CreateFolderAction createFolderAction = base.Service.CreateFolderAction;
				createFolderAction.ActionParameters.ItemName = Folder;
				createFolderAction.ActionParameters.ParentPath = Parent;
				createFolderAction.Execute();
				ItemInfo = createFolderAction.ActionParameters.ItemInfo;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00040EE0 File Offset: 0x0003F0E0
		internal void CreateReport(string Report, string Parent, bool Overwrite, byte[] Definition, Property[] Properties, out CatalogItem ItemInfo, out Warning[] Warnings)
		{
			try
			{
				Global.CheckItemName(Report, ItemType.Report, "Report");
				CreateReportAction createReportAction = base.Service.CreateReportAction;
				createReportAction.ActionParameters.ItemName = Report;
				createReportAction.ActionParameters.ParentPath = Parent;
				createReportAction.ActionParameters.Overwrite = Overwrite;
				createReportAction.ActionParameters.ReportDefinition = Definition;
				createReportAction.ActionParameters.Properties = Properties;
				createReportAction.Execute();
				ItemInfo = createReportAction.ActionParameters.ItemInfo;
				Warnings = createReportAction.ActionParameters.Warnings;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00040F88 File Offset: 0x0003F188
		internal void CreateDataSource(string DataSource, string Parent, bool Overwrite, DataSourceDefinition Definition, Property[] Properties, out CatalogItem ItemInfo)
		{
			try
			{
				Global.CheckItemName(DataSource, ItemType.DataSource, "DataSource");
				this.VerifyRSDataSourceFileExtension(DataSource);
				CreateDataSourceAction createDataSourceAction = base.Service.CreateDataSourceAction;
				createDataSourceAction.ActionParameters.ItemName = DataSource;
				createDataSourceAction.ActionParameters.ParentPath = Parent;
				createDataSourceAction.ActionParameters.Overwrite = Overwrite;
				createDataSourceAction.ActionParameters.DataSourceDefinition = Definition;
				createDataSourceAction.ActionParameters.Properties = Properties;
				createDataSourceAction.Execute();
				ItemInfo = createDataSourceAction.ActionParameters.ItemInfo;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00041028 File Offset: 0x0003F228
		internal new void EnableDataSource(string DataSource)
		{
			try
			{
				Global.CheckItemPath(DataSource, ItemType.DataSource, "DataSource");
				this.VerifyRSDataSourceFileExtension(DataSource);
				base.EnableDataSource(DataSource);
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00041070 File Offset: 0x0003F270
		internal new void DisableDataSource(string DataSource)
		{
			try
			{
				Global.CheckItemPath(DataSource, ItemType.DataSource, "DataSource");
				this.VerifyRSDataSourceFileExtension(DataSource);
				base.DisableDataSource(DataSource);
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x000410B8 File Offset: 0x0003F2B8
		internal void CreateResource(string Resource, string Parent, bool Overwrite, byte[] Contents, string MimeType, Property[] Properties, out CatalogItem ItemInfo)
		{
			try
			{
				CreateResourceAction createResourceAction = base.Service.CreateResourceAction;
				createResourceAction.ActionParameters.ItemName = Resource;
				createResourceAction.ActionParameters.ParentPath = Parent;
				createResourceAction.ActionParameters.Overwrite = Overwrite;
				createResourceAction.ActionParameters.Content = Contents;
				createResourceAction.ActionParameters.MimeType = MimeType;
				createResourceAction.ActionParameters.Properties = Properties;
				createResourceAction.Execute();
				ItemInfo = createResourceAction.ActionParameters.ItemInfo;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00041150 File Offset: 0x0003F350
		internal void CreateModel(string Model, string Parent, byte[] Definition, Property[] Properties, out CatalogItem ItemInfo, out Warning[] Warnings)
		{
			try
			{
				Global.CheckItemName(Model, ItemType.Model, "Model");
				CreateModelAction createModelAction = base.Service.CreateModelAction;
				createModelAction.ActionParameters.ItemName = Model;
				createModelAction.ActionParameters.ParentPath = Parent;
				createModelAction.ActionParameters.ModelDefinition = Definition;
				createModelAction.ActionParameters.Properties = Properties;
				createModelAction.Execute();
				ItemInfo = createModelAction.ActionParameters.ItemInfo;
				Warnings = createModelAction.ActionParameters.Warnings;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x000411E8 File Offset: 0x0003F3E8
		internal void GenerateModel(string DataSource, string Model, string Parent, Property[] Properties, out CatalogItem ItemInfo, out Warning[] Warnings)
		{
			try
			{
				Global.CheckItemName(Model, ItemType.Model, "Model");
				GenerateModelAction generateModelAction = base.Service.GenerateModelAction;
				generateModelAction.ActionParameters.DataSourcePath = DataSource;
				generateModelAction.ActionParameters.ItemName = Model;
				generateModelAction.ActionParameters.ParentPath = Parent;
				generateModelAction.ActionParameters.Properties = Properties;
				generateModelAction.Execute();
				ItemInfo = generateModelAction.ActionParameters.ItemInfo;
				Warnings = generateModelAction.ActionParameters.Warnings;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00041280 File Offset: 0x0003F480
		internal new void SetDataSourceContents(string DataSource, DataSourceDefinition Definition)
		{
			try
			{
				Global.CheckItemPath(DataSource, ItemType.DataSource, CallingEndpoint.Is2010Endpoint ? "Itempath" : "DataSource");
				this.VerifyRSDataSourceFileExtension(DataSource);
				base.SetDataSourceContents(DataSource, Definition);
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x000412D8 File Offset: 0x0003F4D8
		internal new void GetReportParameters(string Report, string HistoryID, bool ForRendering, Microsoft.ReportingServices.Library.Soap.ParameterValue[] Values, DataSourceCredentials[] Credentials, out ParameterInfoCollection Parameters)
		{
			try
			{
				GetReportParametersAction getReportParametersAction = base.Service.GetReportParametersAction;
				getReportParametersAction.ActionParameters.ItemPath = Report;
				getReportParametersAction.ActionParameters.HistoryID = HistoryID;
				getReportParametersAction.ActionParameters.ForRendering = ForRendering;
				getReportParametersAction.ActionParameters.ParameterValidationValues = Microsoft.ReportingServices.Library.Soap.ParameterValue.ThisArrayToNameValueCollection(Values);
				getReportParametersAction.ActionParameters.DatasourceCredentials = DataSourceCredentials.ThisArrayToDatasourcesCredentials(Credentials);
				getReportParametersAction.ActionParameters.Use2006FallbackBehavior = true;
				getReportParametersAction.Execute();
				Parameters = getReportParametersAction.ActionParameters.Parameters;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0004137C File Offset: 0x0003F57C
		internal void GetRoleProperties(string Name, string Site, out Task[] Tasks, out string Description)
		{
			try
			{
				GetRolePropertiesAction getRolePropertiesAction = base.Service.GetRolePropertiesAction;
				getRolePropertiesAction.ActionParameters.RoleName = Name;
				getRolePropertiesAction.ActionParameters.SiteName = Site;
				getRolePropertiesAction.Execute();
				Tasks = getRolePropertiesAction.ActionParameters.Tasks;
				Description = getRolePropertiesAction.ActionParameters.Description;
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x000413F0 File Offset: 0x0003F5F0
		internal void FireEvent(string eventType, string eventData, string site)
		{
			try
			{
				FireEventAction fireEventAction = base.Service.FireEventAction;
				fireEventAction.ActionParameters.EventType = eventType;
				fireEventAction.ActionParameters.EventData = eventData;
				fireEventAction.ActionParameters.Site = site;
				fireEventAction.Execute();
			}
			catch (RSException ex)
			{
				throw base.GetExceptionForEndpoint(ex);
			}
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00041454 File Offset: 0x0003F654
		protected void VerifyRSDataSourceFileExtension(string dataSourcePath)
		{
			if (string.Equals(Path.GetExtension(dataSourcePath), ".ODC", StringComparison.OrdinalIgnoreCase))
			{
				throw new NotYetSupportedException();
			}
		}
	}
}
