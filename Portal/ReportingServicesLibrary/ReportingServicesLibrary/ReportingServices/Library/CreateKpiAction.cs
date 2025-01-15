using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200006B RID: 107
	internal sealed class CreateKpiAction : CreateItemAction<CreateKpiActionParameters, KpiCatalogItem>
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x000128C0 File Offset: 0x00010AC0
		internal CreateKpiAction(RSService service)
			: base("CreateKpiAction", service)
		{
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000128D0 File Offset: 0x00010AD0
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.CreateKpi, base.ActionParameters.ItemName, "Kpi", base.ActionParameters.ParentPath, "Parent", null, null, base.ActionParameters.Overwrite, null, Property.ThisArrayToXml(base.ActionParameters.Properties));
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00012940 File Offset: 0x00010B40
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.ItemName = parameters.Item;
			base.ActionParameters.ParentPath = parameters.Parent;
			base.ActionParameters.Overwrite = parameters.BoolParam;
			base.ActionParameters.Properties = Property.XmlToThisArray(parameters.Properties);
			this.PerformActionNow();
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool IsUpdateSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001299C File Offset: 0x00010B9C
		protected override void UpdateExistingItem(KpiCatalogItem item)
		{
			item.SharedDataSets = base.ActionParameters.SharedDataSets;
			item.Save(true);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000129B6 File Offset: 0x00010BB6
		protected override void PrepareForNewItem(KpiCatalogItem item)
		{
			item.SharedDataSets = base.ActionParameters.SharedDataSets;
		}
	}
}
