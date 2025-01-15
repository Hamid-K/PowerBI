using System;
using System.Diagnostics;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200063F RID: 1599
	internal class SubreportCallbackAdapter
	{
		// Token: 0x06005777 RID: 22391 RVA: 0x0016F8FF File Offset: 0x0016DAFF
		public SubreportCallbackAdapter(ReportProcessing.OnDemandSubReportCallback subreportCallback, ErrorContext errorContext)
		{
			this.m_subreportCallback = subreportCallback;
			this.m_errorContext = errorContext;
		}

		// Token: 0x06005778 RID: 22392 RVA: 0x0016F915 File Offset: 0x0016DB15
		public SubreportCallbackAdapter(ReportProcessing.OnDemandSubReportDataSourcesCallback dataSourcesCallback)
		{
			this.m_subreportDataSourcesCallback = dataSourcesCallback;
		}

		// Token: 0x06005779 RID: 22393 RVA: 0x0016F924 File Offset: 0x0016DB24
		public void SubReportCallback(ICatalogItemContext reportContext, string subreportPath, out ICatalogItemContext subreportContext, out string description, out ReportProcessing.GetReportChunk getCompiledDefinitionCallback, out ParameterInfoCollection parameters)
		{
			getCompiledDefinitionCallback = null;
			IChunkFactory chunkFactory = null;
			this.m_subreportCallback(reportContext, subreportPath, null, new ReportProcessing.NeedsUpgrade(this.NeedsUpgrade), null, out subreportContext, out description, out chunkFactory, out parameters);
			if (chunkFactory != null)
			{
				if (ReportProcessing.ContainsFlag(chunkFactory.ReportProcessingFlags, ReportProcessingFlags.OnDemandEngine))
				{
					subreportContext = null;
					description = null;
					getCompiledDefinitionCallback = null;
					parameters = null;
					string itemPathAsString = reportContext.ItemPathAsString;
					Global.Tracer.Trace(TraceLevel.Warning, "The subreport '{0}' could not be processed.  Parent report '{1}' failed to automatically republish, or it contains a Reporting Services 2005-style CustomReportItem, and is therefore incompatible with the subreport. To correct this error, please attempt to republish the parent report manually. If it contains a CustomReportItem, please upgrade the report to the latest version.", new object[] { subreportPath, itemPathAsString });
					if (this.m_errorContext != null)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsEngineMismatchParentReport, Severity.Warning, ObjectType.Subreport, subreportPath, null, new string[] { subreportPath, itemPathAsString });
					}
					throw new ReportProcessingException(ErrorCode.rsInvalidOperation, new object[] { RPResWrapper.rsEngineMismatchParentReport(ObjectType.Subreport.ToString(), subreportPath, null, subreportPath, reportContext.ItemPathAsString) });
				}
				ChunkFactoryAdapter chunkFactoryAdapter = new ChunkFactoryAdapter(chunkFactory);
				getCompiledDefinitionCallback = new ReportProcessing.GetReportChunk(chunkFactoryAdapter.GetReportChunk);
				return;
			}
			else
			{
				if (subreportContext == null)
				{
					throw new ReportProcessingException(RPResWrapper.rsMissingSubReport(subreportPath, subreportPath), ErrorCode.rsItemNotFound);
				}
				return;
			}
		}

		// Token: 0x0600577A RID: 22394 RVA: 0x0016FA30 File Offset: 0x0016DC30
		public void SubReportDataSourcesCallback(ICatalogItemContext reportContext, string subreportPath, out ICatalogItemContext subreportContext, out ReportProcessing.GetReportChunk getCompiledDefinitionCallback, out DataSourceInfoCollection dataSources)
		{
			getCompiledDefinitionCallback = null;
			IChunkFactory chunkFactory = null;
			DataSetInfoCollection dataSetInfoCollection;
			this.m_subreportDataSourcesCallback(reportContext, subreportPath, new ReportProcessing.NeedsUpgrade(this.NeedsUpgrade), out subreportContext, out chunkFactory, out dataSources, out dataSetInfoCollection);
			if (chunkFactory != null)
			{
				if (ReportProcessing.ContainsFlag(chunkFactory.ReportProcessingFlags, ReportProcessingFlags.OnDemandEngine))
				{
					subreportContext = null;
					getCompiledDefinitionCallback = null;
					dataSources = null;
					string itemPathAsString = reportContext.ItemPathAsString;
					Global.Tracer.Trace(TraceLevel.Warning, "The subreport '{0}' could not be processed.  Parent report '{1}' failed to automatically republish, or it contains a Reporting Services 2005-style CustomReportItem, and is therefore incompatible with the subreport. To correct this error, please attempt to republish the parent report manually. If it contains a CustomReportItem, please upgrade the report to the latest version.", new object[] { subreportPath, itemPathAsString });
					throw new ReportProcessingException(ErrorCode.rsInvalidOperation, new object[] { RPResWrapper.rsEngineMismatchParentReport(ObjectType.Subreport.ToString(), subreportPath, null, subreportPath, itemPathAsString) });
				}
				ChunkFactoryAdapter chunkFactoryAdapter = new ChunkFactoryAdapter(chunkFactory);
				getCompiledDefinitionCallback = new ReportProcessing.GetReportChunk(chunkFactoryAdapter.GetReportChunk);
				return;
			}
			else
			{
				if (subreportContext == null)
				{
					throw new ReportProcessingException(RPResWrapper.rsMissingSubReport(subreportPath, subreportPath), ErrorCode.rsItemNotFound);
				}
				return;
			}
		}

		// Token: 0x0600577B RID: 22395 RVA: 0x0016FB02 File Offset: 0x0016DD02
		public bool NeedsUpgrade(ReportProcessingFlags processingFlags)
		{
			return false;
		}

		// Token: 0x04002E46 RID: 11846
		private ReportProcessing.OnDemandSubReportCallback m_subreportCallback;

		// Token: 0x04002E47 RID: 11847
		private ReportProcessing.OnDemandSubReportDataSourcesCallback m_subreportDataSourcesCallback;

		// Token: 0x04002E48 RID: 11848
		private ErrorContext m_errorContext;
	}
}
