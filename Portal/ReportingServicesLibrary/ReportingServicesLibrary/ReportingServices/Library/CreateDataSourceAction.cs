using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000E4 RID: 228
	internal sealed class CreateDataSourceAction : CreateItemAction<CreateDataSourceActionParameters, DataSourceCatalogItem>
	{
		// Token: 0x060009C8 RID: 2504 RVA: 0x00025FCF File Offset: 0x000241CF
		internal CreateDataSourceAction(RSService service)
			: base("CreateDataSourceAction", service)
		{
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00025FE0 File Offset: 0x000241E0
		protected override void AddActionToBatch()
		{
			string text = DataSourceDefinition.ThisToXml(base.ActionParameters.DataSourceDefinition);
			base.Service.Storage.ConnectionManager.VerifyConnection(true);
			byte[] array = CatalogEncryption.Instance.Encrypt(text, null);
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.CreateDataSource, base.ActionParameters.ItemName, "DataSource", base.ActionParameters.ParentPath, "Parent", null, null, base.ActionParameters.Overwrite, array, Property.ThisArrayToXml(base.ActionParameters.Properties));
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00026084 File Offset: 0x00024284
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			string text = CatalogEncryption.Instance.DecryptToString(parameters.Content, null);
			base.ActionParameters.ItemName = parameters.Item;
			base.ActionParameters.ParentPath = parameters.Parent;
			base.ActionParameters.Overwrite = parameters.BoolParam;
			base.ActionParameters.DataSourceDefinition = DataSourceDefinition.XmlToThis(text);
			base.ActionParameters.Properties = Property.XmlToThisArray(parameters.Properties);
			this.PerformActionNow();
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool IsUpdateSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00026103 File Offset: 0x00024303
		protected override void UpdateExistingItem(DataSourceCatalogItem item)
		{
			SetDataSourceContentsAction setDataSourceContentsAction = base.Service.SetDataSourceContentsAction;
			setDataSourceContentsAction.ActionParameters.DataSourceDefinition = base.ActionParameters.DataSourceDefinition;
			setDataSourceContentsAction.Update(item);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0002612C File Offset: 0x0002432C
		protected override void PrepareForNewItem(DataSourceCatalogItem dsItem)
		{
			if (base.ActionParameters.DataSourceDefinition != null)
			{
				dsItem.DataSourceInfo = DataSourceDefinition.ThisToDataSourceInfo(dsItem.ItemContext.ItemName, null, base.ActionParameters.DataSourceDefinition);
			}
			if (dsItem.Content == null)
			{
				dsItem.Content = dsItem.DataSourceInfo.GetXmlBytes(DataProtection.Instance);
			}
		}
	}
}
