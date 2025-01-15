using System;
using System.Linq;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataShapeDefinition;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing.Execution;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005FA RID: 1530
	internal sealed class DataShapeProcessing
	{
		// Token: 0x06005455 RID: 21589 RVA: 0x001622EB File Offset: 0x001604EB
		internal static DataShapeProcessingResult RenderDataShape(DataShapePublishingContext publishingContext, DataShapeProcessingContext processingContext)
		{
			return DataShapeProcessing.RenderDataShape(publishingContext, processingContext, null);
		}

		// Token: 0x06005456 RID: 21590 RVA: 0x001622F8 File Offset: 0x001604F8
		internal static DataShapeProcessingResult RenderDataShape(DataShapePublishingContext publishingContext, DataShapeProcessingContext processingContext, IDataShapeResultRenderer renderer)
		{
			Global.Tracer.Assert(publishingContext != null, "publishingContext != null");
			Global.Tracer.Assert(processingContext != null, "processingContext != null");
			string id = publishingContext.DataShapeDefinition.DataShapes.First<Microsoft.ReportingServices.DataShapeDefinition.DataShape>().ID;
			DataShapePublishingResult dataShapePublishingResult;
			using (MonitoredScope.NewFormat("DataShapeProcessing.PublishDataShape[DataShape ID={0}]", id))
			{
				dataShapePublishingResult = DataShapeProcessing.PublishDataShape(publishingContext);
			}
			ProgressiveProcessingResult progressiveProcessingResult;
			using (MonitoredScope.NewFormat("DataShapeProcessing.ProcessDataShape[DataShape ID={0}]", id))
			{
				progressiveProcessingResult = DataShapeProcessing.ProcessDataShape(processingContext, publishingContext.CatalogContext, dataShapePublishingResult.Report, dataShapePublishingResult.DataSourceInfos, renderer);
			}
			return new DataShapeProcessingResult(dataShapePublishingResult, progressiveProcessingResult);
		}

		// Token: 0x06005457 RID: 21591 RVA: 0x001623B8 File Offset: 0x001605B8
		internal static DataShapePublishingResult PublishDataShape(DataShapePublishingContext context)
		{
			PublishingErrorContext publishingErrorContext = new PublishingErrorContext();
			DataShapePublishingResult dataShapePublishingResult;
			try
			{
				DataSourceInfoCollection dataSourceInfoCollection;
				dataShapePublishingResult = new DataShapePublishingResult(new ReportPublishing(context, publishingErrorContext).CreateDataShapeIntermediateFormat(out dataSourceInfoCollection), dataSourceInfoCollection, publishingErrorContext.Messages);
			}
			catch (RSException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				throw new ReportProcessingException(ex, publishingErrorContext.Messages);
			}
			return dataShapePublishingResult;
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x00162420 File Offset: 0x00160620
		internal static ProgressiveProcessingResult ProcessDataShape(DataShapeProcessingContext processingContext, ICatalogItemContext reportContext, Microsoft.ReportingServices.ReportIntermediateFormat.Report report, DataSourceInfoCollection dataSourceInfos, IDataShapeResultRenderer renderer)
		{
			return new RenderDataShape(processingContext, reportContext, report, dataSourceInfos, renderer).Execute();
		}
	}
}
