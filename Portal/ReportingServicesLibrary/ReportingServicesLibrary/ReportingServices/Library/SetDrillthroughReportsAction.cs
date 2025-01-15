using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Library.Soap2005;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000153 RID: 339
	internal sealed class SetDrillthroughReportsAction : RSSoapAction<SetDrillthroughReportsActionParameters>
	{
		// Token: 0x06000CFD RID: 3325 RVA: 0x0002FC8C File Offset: 0x0002DE8C
		public SetDrillthroughReportsAction(RSService service)
			: base("SetDrillthroughReportsAction", service)
		{
			Sku.ThrowIfFeatureNotEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.DynamicDrillthrough);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0002FCAC File Offset: 0x0002DEAC
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.SetDrillthroughReports, base.ActionParameters.ModelPath, "Model", null, null, base.ActionParameters.ModelItemID, "ModelItemID", false, null, ModelDrillthroughReport.ToXml(base.ActionParameters.Reports));
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0002FD10 File Offset: 0x0002DF10
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ModelPath, "Model");
			ModelCatalogItem modelCatalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, ItemType.Model, true) as ModelCatalogItem;
			Microsoft.ReportingServices.Modeling.ModelItem modelItem = modelCatalogItem.LoadUserModelAndGetEntity(base.ActionParameters.ModelItemID);
			modelCatalogItem.ThrowIfNoAccess(ModelOperation.UpdateContent);
			string text = Microsoft.ReportingServices.Modeling.ModelItem.IDToString(modelItem.ID);
			base.Service.Storage.DeleteDrillthroughReport(modelCatalogItem.CatalogItemID, text);
			foreach (ModelDrillthroughReport modelDrillthroughReport in base.ActionParameters.Reports)
			{
				CatalogItemContext catalogItemContext2 = new CatalogItemContext(base.Service, modelDrillthroughReport.Path, "Report");
				CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext2, true);
				catalogItem.ThrowIfWrongItemType(new ItemType[]
				{
					ItemType.Report,
					ItemType.LinkedReport
				});
				BaseReportCatalogItem baseReportCatalogItem = catalogItem as BaseReportCatalogItem;
				baseReportCatalogItem.ThrowIfNoAccess(ReportOperation.ReadProperties);
				if (baseReportCatalogItem is LinkedReportCatalogItem)
				{
					(baseReportCatalogItem as LinkedReportCatalogItem).EnsureLinkID();
				}
				this.ValidateDrillReport(baseReportCatalogItem);
				base.Service.Storage.SetDrillthroughReport(baseReportCatalogItem.ItemID, modelCatalogItem.CatalogItemID, text, (short)modelDrillthroughReport.Type);
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0002FE50 File Offset: 0x0002E050
		private void ValidateDrillReport(BaseReportCatalogItem report)
		{
			ParameterInfo parameterInfo = report.Parameters["DrillthroughContext"];
			if (parameterInfo == null)
			{
				throw new InvalidModelDrillthroughReportException(report.ItemContext.OriginalItemPath.Value);
			}
			if (parameterInfo.DataType != Microsoft.ReportingServices.ReportProcessing.DataType.String)
			{
				throw new InvalidModelDrillthroughReportException(report.ItemContext.OriginalItemPath.Value);
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0002FEA5 File Offset: 0x0002E0A5
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ModelPath = parameters.Item;
			base.ActionParameters.ModelItemID = parameters.Param;
			base.ActionParameters.Reports = ModelDrillthroughReport.FromXml(parameters.Properties);
			this.PerformActionNow();
		}
	}
}
