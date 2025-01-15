using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005F8 RID: 1528
	[Serializable]
	public sealed class ProcessingAbortedException : RSException
	{
		// Token: 0x0600544B RID: 21579 RVA: 0x001621F8 File Offset: 0x001603F8
		private ProcessingAbortedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600544C RID: 21580 RVA: 0x00162202 File Offset: 0x00160402
		internal ProcessingAbortedException()
			: this(CancelationTrigger.None)
		{
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x0016220B File Offset: 0x0016040B
		internal ProcessingAbortedException(CancelationTrigger cancelationTrigger)
			: base(ErrorCode.rsProcessingAborted, RPRes.rsProcessingAbortedByUser, null, Global.Tracer, ProcessingAbortedException.CreateAdditionalTraceMessage(ProcessingAbortedException.Reason.UserCanceled, cancelationTrigger), Array.Empty<object>())
		{
			this.m_reason = ProcessingAbortedException.Reason.UserCanceled;
			this.m_cancelationTrigger = cancelationTrigger;
		}

		// Token: 0x0600544E RID: 21582 RVA: 0x0016223D File Offset: 0x0016043D
		internal ProcessingAbortedException(Exception innerException)
			: this(CancelationTrigger.None, innerException)
		{
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x00162247 File Offset: 0x00160447
		internal ProcessingAbortedException(CancelationTrigger cancelationTrigger, Exception innerException)
			: base(ErrorCode.rsProcessingAborted, RPRes.rsProcessingAbortedByError, innerException, Global.Tracer, ProcessingAbortedException.CreateAdditionalTraceMessage(ProcessingAbortedException.Reason.AbnormalTermination, cancelationTrigger), Array.Empty<object>())
		{
			this.m_reason = ProcessingAbortedException.Reason.AbnormalTermination;
			this.m_cancelationTrigger = cancelationTrigger;
		}

		// Token: 0x17001F05 RID: 7941
		// (get) Token: 0x06005450 RID: 21584 RVA: 0x00162279 File Offset: 0x00160479
		public ProcessingAbortedException.Reason ReasonForAbort
		{
			get
			{
				return this.m_reason;
			}
		}

		// Token: 0x17001F06 RID: 7942
		// (get) Token: 0x06005451 RID: 21585 RVA: 0x00162281 File Offset: 0x00160481
		internal CancelationTrigger Trigger
		{
			get
			{
				return this.m_cancelationTrigger;
			}
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x00162289 File Offset: 0x00160489
		private static string CreateAdditionalTraceMessage(ProcessingAbortedException.Reason reason, CancelationTrigger trigger)
		{
			return string.Format(CultureInfo.InvariantCulture, "[{0}:{1}]", reason.ToString(), trigger.ToString());
		}

		// Token: 0x04002CE2 RID: 11490
		private ProcessingAbortedException.Reason m_reason;

		// Token: 0x04002CE3 RID: 11491
		private readonly CancelationTrigger m_cancelationTrigger;

		// Token: 0x02000C12 RID: 3090
		public enum Reason
		{
			// Token: 0x04004836 RID: 18486
			UserCanceled,
			// Token: 0x04004837 RID: 18487
			AbnormalTermination
		}
	}
}
