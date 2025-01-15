using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005F7 RID: 1527
	[Serializable]
	public sealed class ReportPublishingException : ReportProcessingException
	{
		// Token: 0x06005445 RID: 21573 RVA: 0x0016217A File Offset: 0x0016037A
		public ReportPublishingException(ProcessingMessageList messages, ReportProcessingFlags processingFlags)
			: base(messages)
		{
			this.m_processingFlags = processingFlags;
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x0016218A File Offset: 0x0016038A
		public ReportPublishingException(ProcessingMessageList messages, Exception innerException, ReportProcessingFlags processingFlags)
			: base(messages, innerException)
		{
			this.m_processingFlags = processingFlags;
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x0016219B File Offset: 0x0016039B
		public ReportPublishingException(ErrorCode code, Exception innerException, params object[] arguments)
			: base(code, innerException, arguments)
		{
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x001621A6 File Offset: 0x001603A6
		private ReportPublishingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_processingFlags = (ReportProcessingFlags)info.GetValue("ReportProcessingFlags", typeof(ReportProcessingFlags));
		}

		// Token: 0x17001F04 RID: 7940
		// (get) Token: 0x06005449 RID: 21577 RVA: 0x001621D0 File Offset: 0x001603D0
		public ReportProcessingFlags ReportProcessingFlags
		{
			get
			{
				return this.m_processingFlags;
			}
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x001621D8 File Offset: 0x001603D8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ReportProcessingFlags", this.m_processingFlags);
		}

		// Token: 0x04002CE0 RID: 11488
		private ReportProcessingFlags m_processingFlags;

		// Token: 0x04002CE1 RID: 11489
		private const string ReportProcessingFlagsName = "ReportProcessingFlags";
	}
}
