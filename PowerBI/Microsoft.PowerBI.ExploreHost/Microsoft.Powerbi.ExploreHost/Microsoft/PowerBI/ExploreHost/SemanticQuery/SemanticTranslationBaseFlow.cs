using System;
using Microsoft.DataShaping.Engine;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ExploreHost.Errors;
using Microsoft.PowerBI.ExploreHost.Utils;
using Microsoft.PowerBI.Query.Contracts;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.PowerBI.Telemetry;
using Microsoft.ReportingServices.Common;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x02000046 RID: 70
	internal abstract class SemanticTranslationBaseFlow : ExploreClientHandlerBaseFlow
	{
		// Token: 0x0600023D RID: 573 RVA: 0x000071D0 File Offset: 0x000053D0
		internal SemanticTranslationBaseFlow(ExploreClientHandlerContext context, string databaseID)
			: base(context, databaseID)
		{
		}

		// Token: 0x0600023E RID: 574
		protected abstract void Translate(EngineDataModel engineDataModel);

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000071DA File Offset: 0x000053DA
		private ServiceErrorExtractor ServiceErrorExtractor
		{
			get
			{
				if (this.serviceErrorExtractor == null)
				{
					this.serviceErrorExtractor = ExploreHostServiceErrorExtractorFactory.Instance.Create();
				}
				return this.serviceErrorExtractor;
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000071FC File Offset: 0x000053FC
		protected override void InternalRun()
		{
			string text = "2.0";
			EngineDataModel engineDataModel;
			ServiceError serviceError;
			IDataSourceInfo dataSourceInfo;
			if (!ExploreHostUtils.TryGetEngineDataModel(this.Context, 0L, this.ServiceErrorExtractor, base.DatabaseID, text, null, out engineDataModel, out serviceError, out dataSourceInfo))
			{
				throw new PowerBIExploreException(serviceError.StatusCode.ToString(), "Failed to get the DataShapeEngine data model", ErrorSource.PowerBI, serviceError.StatusCode);
			}
			this.SafeTranslate(engineDataModel);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000726C File Offset: 0x0000546C
		private void SafeTranslate(EngineDataModel engineDataModel)
		{
			try
			{
				this.Translate(engineDataModel);
			}
			catch (DataShapeEngineException ex)
			{
				TelemetryService.Instance.Log(new PBIWinDataShapingException("SQDaxTranslation", ex.ToTraceString(), ex.Message));
				throw new PowerBIExploreException(ServiceErrorStatusCode.TranslateSemanticQueryError.ToString(), "Failed to translate the semantic query due to DataShapeEngineException", ex, ErrorSource.PowerBI, null, ServiceErrorStatusCode.TranslateSemanticQueryError);
			}
			catch (Exception ex2)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex2))
				{
					throw;
				}
				ExploreHostUtils.TraceSemanticQueryTranslationException(ex2);
				throw new PowerBIExploreException(ServiceErrorStatusCode.TranslateSemanticQueryError.ToString(), "Failed to translate the semantic query due to general exception", ex2, ErrorSource.PowerBI, null, ServiceErrorStatusCode.TranslateSemanticQueryError);
			}
		}

		// Token: 0x040000DB RID: 219
		private const string SQDaxTranslationComponentName = "SQDaxTranslation";

		// Token: 0x040000DC RID: 220
		private ServiceErrorExtractor serviceErrorExtractor;
	}
}
