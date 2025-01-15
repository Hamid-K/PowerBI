using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000102 RID: 258
	internal sealed class GetDataSetParametersAction : RSSoapAction<GetDataSetParametersActionParameters>
	{
		// Token: 0x06000A67 RID: 2663 RVA: 0x00027962 File Offset: 0x00025B62
		internal GetDataSetParametersAction(RSService service)
			: base("GetDataSetParametersAction", service)
		{
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00027970 File Offset: 0x00025B70
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "DataSet");
			base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true).ThrowIfWrongItemType(new ItemType[] { ItemType.DataSet });
			ItemParameterDefinition itemParameterDefinition = ItemParameterDefinition.Load(catalogItemContext, null, base.ActionParameters.ForRendering, base.Service, SecurityRequirements.GenerateForLoadCompiledDefinition(base.Service.SecMgr, base.Service.UserName));
			ParameterInfoCollection parameterInfoCollection;
			if (!base.ActionParameters.ForRendering)
			{
				parameterInfoCollection = ParameterInfoCollection.DecodeFromXml(itemParameterDefinition.StoredParametersXml);
				parameterInfoCollection.ValuesAreValid();
				base.ActionParameters.Parameters = parameterInfoCollection;
				return;
			}
			parameterInfoCollection = base.Service.GetDataSetParameters(itemParameterDefinition, base.ActionParameters.ParameterValidationValues, JobType.UserJobType);
			base.ActionParameters.Parameters = parameterInfoCollection;
		}
	}
}
