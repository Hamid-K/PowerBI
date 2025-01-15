using System;
using Microsoft.ReportingServices.OnDemandProcessing;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200062C RID: 1580
	internal sealed class ProgressiveProcessingResult
	{
		// Token: 0x060056E8 RID: 22248 RVA: 0x0016EAAD File Offset: 0x0016CCAD
		internal ProgressiveProcessingResult(ISerializableValues resultValues, ExecutionLogContext executionLogContext, ProcessingMessageList warnings)
		{
			this.m_resultValues = resultValues;
			this.m_executionLogContext = executionLogContext;
			this.m_warnings = warnings;
		}

		// Token: 0x17001FAE RID: 8110
		// (get) Token: 0x060056E9 RID: 22249 RVA: 0x0016EACA File Offset: 0x0016CCCA
		public ISerializableValues ResultValues
		{
			get
			{
				return this.m_resultValues;
			}
		}

		// Token: 0x17001FAF RID: 8111
		// (get) Token: 0x060056EA RID: 22250 RVA: 0x0016EAD2 File Offset: 0x0016CCD2
		public ExecutionLogContext RequestMetrics
		{
			get
			{
				return this.m_executionLogContext;
			}
		}

		// Token: 0x17001FB0 RID: 8112
		// (get) Token: 0x060056EB RID: 22251 RVA: 0x0016EADA File Offset: 0x0016CCDA
		internal ProcessingMessageList Warnings
		{
			get
			{
				return this.m_warnings;
			}
		}

		// Token: 0x04002DE1 RID: 11745
		private readonly ISerializableValues m_resultValues;

		// Token: 0x04002DE2 RID: 11746
		private readonly ExecutionLogContext m_executionLogContext;

		// Token: 0x04002DE3 RID: 11747
		private readonly ProcessingMessageList m_warnings;
	}
}
