using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.Execution
{
	// Token: 0x020007CB RID: 1995
	internal class ProcessReportDefinitionOnly : ProcessReportOdpInitial
	{
		// Token: 0x060070B7 RID: 28855 RVA: 0x001D5998 File Offset: 0x001D3B98
		public ProcessReportDefinitionOnly(IConfiguration configuration, ProcessingContext pc, Report report, ErrorContext errorContext, ReportProcessing.StoreServerParameters storeServerParameters, GlobalIDOwnerCollection globalIDOwnerCollection, ExecutionLogContext executionLogContext, DateTime executionTime)
			: base(configuration, pc, report, errorContext, storeServerParameters, globalIDOwnerCollection, executionLogContext, executionTime)
		{
		}

		// Token: 0x060070B8 RID: 28856 RVA: 0x001D59B8 File Offset: 0x001D3BB8
		protected override OnDemandProcessingContext CreateOnDemandContext(OnDemandMetadata odpMetadata, ReportSnapshot reportSnapshot, UserProfileState initialUserDependency)
		{
			OnDemandProcessingContext onDemandProcessingContext = new OnDemandProcessingContext(base.PublicProcessingContext, base.ReportDefinition, odpMetadata, base.ErrorContext, reportSnapshot.ExecutionTime, base.StoreServerParameters, initialUserDependency, base.ExecutionLogContext, base.Configuration, this.GetAbortHelper());
			onDemandProcessingContext.ReportObjectModel.Initialize(base.PublicProcessingContext.Parameters);
			return onDemandProcessingContext;
		}

		// Token: 0x060070B9 RID: 28857 RVA: 0x001D5A12 File Offset: 0x001D3C12
		protected override void CompleteOdpContext(OnDemandProcessingContext odpContext)
		{
			base.CompleteOdpContext(odpContext);
		}

		// Token: 0x060070BA RID: 28858 RVA: 0x001D5A1B File Offset: 0x001D3C1B
		protected override ReportInstance CreateReportInstance(OnDemandProcessingContext odpContext, OnDemandMetadata odpMetadata, ReportSnapshot reportSnapshot, out Merge odpMerge)
		{
			odpMerge = null;
			return null;
		}

		// Token: 0x060070BB RID: 28859 RVA: 0x001D5A22 File Offset: 0x001D3C22
		protected override void ResetEnvironment(OnDemandProcessingContext odpContext, ReportInstance reportInstance)
		{
		}

		// Token: 0x060070BC RID: 28860 RVA: 0x001D5A24 File Offset: 0x001D3C24
		protected override void SetupReportLanguage(Merge odpMerge, ReportInstance reportInstance)
		{
		}

		// Token: 0x060070BD RID: 28861 RVA: 0x001D5A26 File Offset: 0x001D3C26
		protected override void UpdateUserProfileLocation(OnDemandProcessingContext odpContext)
		{
		}

		// Token: 0x060070BE RID: 28862 RVA: 0x001D5A28 File Offset: 0x001D3C28
		protected override void PreProcessSnapshot(OnDemandProcessingContext odpContext, Merge odpMerge, ReportInstance reportInstance, ReportSnapshot reportSnapshot)
		{
			if (base.ReportDefinition.HasSubReports)
			{
				ReportProcessing.FetchSubReports(base.ReportDefinition, odpContext.ChunkFactory, odpContext.ErrorContext, odpContext.OdpMetadata, odpContext.ReportContext, odpContext.SubReportCallback, 0, odpContext.SnapshotProcessing, odpContext.ProcessWithCachedData, base.GlobalIDOwnerCollection, base.PublicProcessingContext.QueryParameters);
				SubReportInitializer.InitializeSubReportOdpContext(base.ReportDefinition, odpContext);
			}
		}

		// Token: 0x1700266C RID: 9836
		// (get) Token: 0x060070BF RID: 28863 RVA: 0x001D5A95 File Offset: 0x001D3C95
		protected override OnDemandProcessingContext.Mode OnDemandProcessingMode
		{
			get
			{
				return OnDemandProcessingContext.Mode.DefinitionOnly;
			}
		}
	}
}
