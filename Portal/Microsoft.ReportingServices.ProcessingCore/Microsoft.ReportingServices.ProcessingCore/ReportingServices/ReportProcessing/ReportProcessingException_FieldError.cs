using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005E3 RID: 1507
	[Serializable]
	internal sealed class ReportProcessingException_FieldError : Exception
	{
		// Token: 0x06005408 RID: 21512 RVA: 0x00161951 File Offset: 0x0015FB51
		internal ReportProcessingException_FieldError(DataFieldStatus status, string message)
			: base((message == null) ? "" : message, null)
		{
			this.m_status = status;
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x0016196C File Offset: 0x0015FB6C
		private ReportProcessingException_FieldError(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17001EFE RID: 7934
		// (get) Token: 0x0600540A RID: 21514 RVA: 0x00161976 File Offset: 0x0015FB76
		internal DataFieldStatus Status
		{
			get
			{
				return this.m_status;
			}
		}

		// Token: 0x04002CD4 RID: 11476
		private DataFieldStatus m_status;
	}
}
