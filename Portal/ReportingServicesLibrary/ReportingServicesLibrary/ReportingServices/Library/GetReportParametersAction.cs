using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200019D RID: 413
	internal sealed class GetReportParametersAction : RSSoapAction<GetReportParametersActionParameters>
	{
		// Token: 0x06000F1B RID: 3867 RVA: 0x00036913 File Offset: 0x00034B13
		internal GetReportParametersAction(RSService service)
			: base("GetReportParametersAction", service)
		{
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00036924 File Offset: 0x00034B24
		internal override void PerformActionNow()
		{
			CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.ItemPath, "Report");
			CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, true);
			catalogItemContext.RSRequestParameters.DatasourcesCred = base.ActionParameters.DatasourceCredentials;
			catalogItem.ThrowIfWrongItemType(new ItemType[]
			{
				ItemType.Report,
				ItemType.LinkedReport
			});
			bool forRendering = base.ActionParameters.ForRendering;
			ItemParameterDefinition itemParameterDefinition = ItemParameterDefinition.Load(catalogItemContext, base.ActionParameters.HistoryID, forRendering, base.Service, SecurityRequirements.GenerateForLoadCompiledDefinition(base.Service.SecMgr, base.Service.UserName));
			ParameterInfoCollection parameterInfoCollection = null;
			if (!forRendering || base.ActionParameters.Use2006FallbackBehavior)
			{
				parameterInfoCollection = ParameterInfoCollection.DecodeFromXml(itemParameterDefinition.StoredParametersXml);
				parameterInfoCollection.ValuesAreValid();
				if (!forRendering)
				{
					base.ActionParameters.Parameters = parameterInfoCollection;
					return;
				}
			}
			try
			{
				string storedParametersXml = itemParameterDefinition.StoredParametersXml;
				base.ActionParameters.Parameters = base.Service.GetReportParametersForRendering(catalogItemContext, itemParameterDefinition.ReportId, itemParameterDefinition.LinkId, itemParameterDefinition.SnapshotExecutionDate, new CatalogSessionParameterStorage(null, itemParameterDefinition), base.ActionParameters.ParameterValidationValues, null, null, JobType.UserJobType);
			}
			catch (RSException ex)
			{
				if (!base.ActionParameters.Use2006FallbackBehavior)
				{
					throw;
				}
				if (RSTrace.CatalogTrace.TraceVerbose)
				{
					RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Fail to connect data source to retrieve parameter values. Use the stored info in RS catalog. " + ex.Message);
				}
				base.ActionParameters.Parameters = parameterInfoCollection;
			}
		}
	}
}
