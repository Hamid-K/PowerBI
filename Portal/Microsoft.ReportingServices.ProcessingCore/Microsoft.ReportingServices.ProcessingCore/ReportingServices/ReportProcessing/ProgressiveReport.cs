using System;
using System.IO;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing.Execution;
using Microsoft.ReportingServices.ReportProcessing.Utils;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200062A RID: 1578
	internal sealed class ProgressiveReport
	{
		// Token: 0x060056D8 RID: 22232 RVA: 0x0016E899 File Offset: 0x0016CA99
		private ProgressiveReport()
		{
		}

		// Token: 0x060056D9 RID: 22233 RVA: 0x0016E8A1 File Offset: 0x0016CAA1
		public static ProgressivePublishingResult Create(ProgressivePublishingContext context)
		{
			return new ProgressiveReport().UpdateCompiledDefinition(context);
		}

		// Token: 0x060056DA RID: 22234 RVA: 0x0016E8AE File Offset: 0x0016CAAE
		public ProgressivePublishingResult MergeDefinition(ProgressivePublishingContext context)
		{
			return this.UpdateCompiledDefinition(context);
		}

		// Token: 0x060056DB RID: 22235 RVA: 0x0016E8B8 File Offset: 0x0016CAB8
		private ProgressivePublishingResult UpdateCompiledDefinition(ProgressivePublishingContext progressivePublishingContext)
		{
			if (progressivePublishingContext.DataProtection == null)
			{
				throw new ArgumentNullException("dataProtection");
			}
			PublishingErrorContext publishingErrorContext = new PublishingErrorContext();
			DataSourceInfoCollection dataSourceInfoCollection = null;
			ProgressivePublishingResult progressivePublishingResult;
			try
			{
				this.m_reportContext = progressivePublishingContext.CatalogContext;
				Microsoft.ReportingServices.ReportIntermediateFormat.Report report = new ReportPublishing(progressivePublishingContext, publishingErrorContext, new NoOpUpgradeStrategy()).CreateProgressiveIntermediateFormat(progressivePublishingContext.Definition, out this.m_description, out this.m_parameters, out dataSourceInfoCollection);
				this.m_compiledDefinition = new MemoryStream();
				ChunkManager.SerializeReport(report, this.m_compiledDefinition, progressivePublishingContext.Configuration);
				SerializableValues serializableValues = new SerializableValues();
				serializableValues.AddProcessingMessages("publishingWarnings", publishingErrorContext.Messages);
				progressivePublishingResult = new ProgressivePublishingResult(this, serializableValues, dataSourceInfoCollection, publishingErrorContext.Messages);
			}
			catch (RSException)
			{
				throw;
			}
			catch (Exception ex)
			{
				ProcessingMessageList processingMessageList;
				if (publishingErrorContext == null)
				{
					processingMessageList = new ProcessingMessageList();
				}
				else
				{
					processingMessageList = publishingErrorContext.Messages;
				}
				throw new ReportProcessingException(ex, processingMessageList);
			}
			return progressivePublishingResult;
		}

		// Token: 0x060056DC RID: 22236 RVA: 0x0016E994 File Offset: 0x0016CB94
		public void MergeInteractivityState(Stream interactivityState)
		{
			throw new NotYetSupportedException();
		}

		// Token: 0x060056DD RID: 22237 RVA: 0x0016E99B File Offset: 0x0016CB9B
		public ProgressiveProcessingResult Render(Stream dataSegmentQuery, ProgressiveProcessingContext context)
		{
			return this.Render(dataSegmentQuery, context, null);
		}

		// Token: 0x060056DE RID: 22238 RVA: 0x0016E9A8 File Offset: 0x0016CBA8
		public ProgressiveProcessingResult Render(Stream dataSegmentQuery, ProgressiveProcessingContext context, IDataSegmentRenderer renderer)
		{
			GlobalIDOwnerCollection globalIDOwnerCollection;
			Microsoft.ReportingServices.ReportIntermediateFormat.Report report = this.DeserializeReport(out globalIDOwnerCollection);
			return new RenderProgressiveDataSegment(context, this.m_reportContext, this.m_parameters, report, globalIDOwnerCollection, this.m_description, dataSegmentQuery, renderer).Execute();
		}

		// Token: 0x060056DF RID: 22239 RVA: 0x0016E9E0 File Offset: 0x0016CBE0
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Report DeserializeReport(out GlobalIDOwnerCollection globalIDOwners)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Report report;
			try
			{
				this.m_compiledDefinition.Seek(0L, SeekOrigin.Begin);
				Stream stream = new ReadOnlyStream(this.m_compiledDefinition, false);
				globalIDOwners = new GlobalIDOwnerCollection();
				report = ChunkManager.DeserializeReport(false, globalIDOwners, null, null, stream);
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
				throw new ReportProcessingException(ex, null);
			}
			return report;
		}

		// Token: 0x17001FA7 RID: 8103
		// (get) Token: 0x060056E0 RID: 22240 RVA: 0x0016EA50 File Offset: 0x0016CC50
		public ParameterInfoCollection Parameters
		{
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x17001FA8 RID: 8104
		// (get) Token: 0x060056E1 RID: 22241 RVA: 0x0016EA58 File Offset: 0x0016CC58
		public ICatalogItemContext ReportContext
		{
			get
			{
				return this.m_reportContext;
			}
		}

		// Token: 0x17001FA9 RID: 8105
		// (get) Token: 0x060056E2 RID: 22242 RVA: 0x0016EA60 File Offset: 0x0016CC60
		public string Description
		{
			get
			{
				return this.m_description;
			}
		}

		// Token: 0x04002DD9 RID: 11737
		private Stream m_compiledDefinition;

		// Token: 0x04002DDA RID: 11738
		private ICatalogItemContext m_reportContext;

		// Token: 0x04002DDB RID: 11739
		private ParameterInfoCollection m_parameters;

		// Token: 0x04002DDC RID: 11740
		private string m_description;
	}
}
