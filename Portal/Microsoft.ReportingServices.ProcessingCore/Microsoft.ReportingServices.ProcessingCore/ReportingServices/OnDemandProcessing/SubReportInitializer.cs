using System;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200082B RID: 2091
	internal class SubReportInitializer
	{
		// Token: 0x0600756A RID: 30058 RVA: 0x001E7030 File Offset: 0x001E5230
		internal static void InitializeSubReportOdpContext(Microsoft.ReportingServices.ReportIntermediateFormat.Report report, OnDemandProcessingContext parentOdpContext)
		{
			foreach (Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport in report.SubReports)
			{
				if (!subReport.ExceededMaxLevel)
				{
					OnDemandProcessingContext onDemandProcessingContext = new OnDemandProcessingContext(parentOdpContext, subReport.ReportContext, subReport);
					subReport.OdpContext = onDemandProcessingContext;
					if (subReport.RetrievalStatus != Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.Status.DefinitionRetrieveFailed && subReport.Report.HasSubReports)
					{
						SubReportInitializer.InitializeSubReportOdpContext(subReport.Report, onDemandProcessingContext);
					}
				}
			}
		}

		// Token: 0x0600756B RID: 30059 RVA: 0x001E70BC File Offset: 0x001E52BC
		internal static bool InitializeSubReports(Microsoft.ReportingServices.ReportIntermediateFormat.Report report, Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance, OnDemandProcessingContext odpContext, bool inDataRegion, bool fromCreateSubReportInstance)
		{
			bool flag3;
			try
			{
				odpContext.IsTopLevelSubReportProcessing = true;
				bool flag = true;
				OnDemandProcessingContext onDemandProcessingContext = odpContext;
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport in report.SubReports)
				{
					if (subReport.ExceededMaxLevel)
					{
						return flag;
					}
					IReference<Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance> reference = null;
					try
					{
						bool flag2 = false;
						if (subReport.RetrievalStatus != Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.Status.DefinitionRetrieveFailed)
						{
							onDemandProcessingContext = SubReportInitializer.InitializeSubReport(odpContext, subReport, reportInstance, inDataRegion || subReport.InDataRegion, fromCreateSubReportInstance, out flag2);
							if (!inDataRegion && !subReport.InDataRegion && (!odpContext.SnapshotProcessing || odpContext.ReprocessSnapshot))
							{
								reference = subReport.CurrentSubReportInstance;
							}
						}
						if (flag2 && subReport.Report.HasSubReports)
						{
							flag &= SubReportInitializer.InitializeSubReports(subReport.Report, (subReport.CurrentSubReportInstance != null) ? subReport.CurrentSubReportInstance.Value().ReportInstance.Value() : null, onDemandProcessingContext, inDataRegion || subReport.InDataRegion, fromCreateSubReportInstance);
						}
						if (onDemandProcessingContext.ErrorContext.Messages != null && 0 < onDemandProcessingContext.ErrorContext.Messages.Count)
						{
							odpContext.TopLevelContext.ErrorContext.Register(ProcessingErrorCode.rsWarningExecutingSubreport, Severity.Warning, subReport.ObjectType, subReport.Name, null, onDemandProcessingContext.ErrorContext.Messages, Array.Empty<string>());
						}
						flag = flag && flag2;
					}
					catch (Exception ex)
					{
						flag = false;
						ReportProcessing.HandleSubReportProcessingError(onDemandProcessingContext.TopLevelContext.ErrorContext, subReport, InstancePathItem.GenerateInstancePathString(subReport.InstancePath), onDemandProcessingContext.ErrorContext, ex);
					}
					finally
					{
						if (reference != null)
						{
							reference.Value().InstanceComplete();
						}
					}
				}
				flag3 = flag;
			}
			finally
			{
				odpContext.IsTopLevelSubReportProcessing = false;
			}
			return flag3;
		}

		// Token: 0x0600756C RID: 30060 RVA: 0x001E72BC File Offset: 0x001E54BC
		internal static bool InitializeSubReport(Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport)
		{
			bool flag = false;
			OnDemandProcessingContext onDemandProcessingContext = null;
			try
			{
				onDemandProcessingContext = subReport.OdpContext;
				flag = new Merge(subReport.Report, onDemandProcessingContext).InitAndSetupSubReport(subReport);
				if (onDemandProcessingContext.ErrorContext.Messages != null && 0 < onDemandProcessingContext.ErrorContext.Messages.Count)
				{
					onDemandProcessingContext.TopLevelContext.ErrorContext.Register(ProcessingErrorCode.rsWarningExecutingSubreport, Severity.Warning, subReport.ObjectType, subReport.Name, null, onDemandProcessingContext.ErrorContext.Messages, Array.Empty<string>());
				}
			}
			catch (Exception ex)
			{
				ReportProcessing.HandleSubReportProcessingError(onDemandProcessingContext.TopLevelContext.ErrorContext, subReport, InstancePathItem.GenerateInstancePathString(subReport.InstancePath), onDemandProcessingContext.ErrorContext, ex);
			}
			return flag;
		}

		// Token: 0x0600756D RID: 30061 RVA: 0x001E7374 File Offset: 0x001E5574
		private static OnDemandProcessingContext InitializeSubReport(OnDemandProcessingContext parentOdpContext, Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport, Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance, bool inDataRegion, bool fromCreateSubReportInstance, out bool prefetchSuccess)
		{
			Global.Tracer.Assert(subReport.OdpContext != null, "(null != subReport.OdpContext)");
			prefetchSuccess = true;
			if (!inDataRegion)
			{
				IReference<Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance> reference;
				if (!subReport.OdpContext.SnapshotProcessing || subReport.OdpContext.ReprocessSnapshot)
				{
					reference = Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance.CreateInstance(reportInstance, subReport, parentOdpContext.OdpMetadata);
				}
				else
				{
					reference = reportInstance.SubreportInstances[subReport.IndexInCollection];
				}
				subReport.CurrentSubReportInstance = reference;
				if (!fromCreateSubReportInstance)
				{
					ReportSection containingSection = subReport.GetContainingSection(parentOdpContext);
					parentOdpContext.SetupContext(containingSection, null);
				}
				Merge merge = new Merge(subReport.Report, subReport.OdpContext);
				prefetchSuccess = merge.InitAndSetupSubReport(subReport);
			}
			return subReport.OdpContext;
		}
	}
}
