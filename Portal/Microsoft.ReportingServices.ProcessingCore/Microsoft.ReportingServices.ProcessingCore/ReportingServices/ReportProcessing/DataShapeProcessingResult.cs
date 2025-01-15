using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000604 RID: 1540
	internal sealed class DataShapeProcessingResult
	{
		// Token: 0x06005518 RID: 21784 RVA: 0x0016724C File Offset: 0x0016544C
		internal DataShapeProcessingResult(DataShapePublishingResult publishingResult, ProgressiveProcessingResult processingResult)
		{
			ProcessingMessageList warnings = publishingResult.Warnings;
			ProcessingMessageList warnings2 = processingResult.Warnings;
			int num = ((warnings == null) ? 0 : warnings.Count);
			num += ((warnings2 == null) ? 0 : warnings2.Count);
			this.m_warnings = new ProcessingMessageList(num);
			if (warnings != null)
			{
				this.m_warnings.AddRange(warnings);
			}
			if (warnings2 != null)
			{
				this.m_warnings.AddRange(warnings2);
			}
		}

		// Token: 0x17001F20 RID: 7968
		// (get) Token: 0x06005519 RID: 21785 RVA: 0x001672B3 File Offset: 0x001654B3
		internal ProcessingMessageList Warnings
		{
			get
			{
				return this.m_warnings;
			}
		}

		// Token: 0x04002D05 RID: 11525
		private readonly ProcessingMessageList m_warnings;
	}
}
