using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000EC RID: 236
	internal sealed class SetDataSourceContentsAction : UpdateItemAction<SetDataSourceContentsActionParameters, DataSourceCatalogItem>
	{
		// Token: 0x060009F4 RID: 2548 RVA: 0x0002663A File Offset: 0x0002483A
		internal SetDataSourceContentsAction(RSService service)
			: base("SetDataSourceContentsAction", service)
		{
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00026648 File Offset: 0x00024848
		protected override void AddActionToBatch()
		{
			string text = DataSourceDefinition.ThisToXml(base.ActionParameters.DataSourceDefinition);
			base.Service.Storage.ConnectionManager.VerifyConnection(true);
			byte[] array = CatalogEncryption.Instance.Encrypt(text, null);
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetDataSourceContents, base.ActionParameters.ItemPath, "DataSource", null, null, null, null, false, array, null);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x000266C4 File Offset: 0x000248C4
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			DataSourceDefinition dataSourceDefinition = DataSourceDefinition.XmlToThis(CatalogEncryption.Instance.DecryptToString(parameters.Content, null));
			base.ActionParameters.ItemPath = parameters.Item;
			base.ActionParameters.DataSourceDefinition = dataSourceDefinition;
			this.PerformActionNow();
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002670C File Offset: 0x0002490C
		internal override void Update(DataSourceCatalogItem dsItem)
		{
			dsItem.ThrowIfNoAccess(DatasourceOperation.UpdateContent);
			if (base.ActionParameters.DataSourceDefinition != null)
			{
				dsItem.DataSourceInfo = DataSourceDefinition.ThisToDataSourceInfo(dsItem.ItemContext.ItemName, null, base.ActionParameters.DataSourceDefinition);
				dsItem.Content = dsItem.DataSourceInfo.GetXmlBytes(DataProtection.Instance);
			}
			dsItem.Save(false);
		}
	}
}
