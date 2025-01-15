using System;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200027A RID: 634
	internal sealed class ReportRenderingResult : IDisposable
	{
		// Token: 0x06001685 RID: 5765 RVA: 0x00059A88 File Offset: 0x00057C88
		internal ReportRenderingResult(OnDemandProcessingResult processingResult, ParameterInfoCollection effectiveParameters, ReportItem reportItem, DateTime executionDateTime, StreamManager streamManager)
		{
			if (processingResult != null)
			{
				this.Warnings = Warning.ProcessingMessagesToWarningArray(processingResult.Warnings);
			}
			else
			{
				this.Warnings = null;
			}
			this.EffectiveParameters = effectiveParameters;
			this.ReportItem = reportItem;
			this.Stream = streamManager.PrimaryStream;
			this.SecondaryStreamNames = streamManager.SecondaryStreamNames;
			this.SecondaryCacheableStreamNames = streamManager.SecondaryCacheableStreamNames;
			this.ExecutionDateTime = executionDateTime;
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00059AF8 File Offset: 0x00057CF8
		internal ReportRenderingResult(ReportRenderingResult other, RSStream stream)
		{
			if (other != null)
			{
				this.Warnings = other.Warnings;
				this.EffectiveParameters = other.EffectiveParameters;
				this.ReportItem = other.ReportItem;
				this.ExecutionDateTime = other.ExecutionDateTime;
				this.SecondaryStreamNames = other.SecondaryStreamNames;
				this.SecondaryCacheableStreamNames = other.SecondaryCacheableStreamNames;
			}
			this.Stream = stream;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00059B5D File Offset: 0x00057D5D
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00059B68 File Offset: 0x00057D68
		private void Dispose(bool disposing)
		{
			if (!this.m_disposed && disposing)
			{
				if (this.Stream != null)
				{
					this.Stream.Close();
				}
				IDisposable stream = this.Stream;
				if (stream != null)
				{
					stream.Dispose();
				}
			}
			this.m_disposed = true;
		}

		// Token: 0x04000836 RID: 2102
		internal RSStream Stream;

		// Token: 0x04000837 RID: 2103
		internal Warning[] Warnings;

		// Token: 0x04000838 RID: 2104
		internal ParameterInfoCollection EffectiveParameters;

		// Token: 0x04000839 RID: 2105
		internal ReportItem ReportItem;

		// Token: 0x0400083A RID: 2106
		internal DateTime ExecutionDateTime;

		// Token: 0x0400083B RID: 2107
		internal string[] SecondaryCacheableStreamNames;

		// Token: 0x0400083C RID: 2108
		internal string[] SecondaryStreamNames;

		// Token: 0x0400083D RID: 2109
		private bool m_disposed;
	}
}
