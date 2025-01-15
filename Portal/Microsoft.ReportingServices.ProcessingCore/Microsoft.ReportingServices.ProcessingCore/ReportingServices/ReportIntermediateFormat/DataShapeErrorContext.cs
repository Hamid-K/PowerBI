using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003C4 RID: 964
	internal sealed class DataShapeErrorContext
	{
		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x06002716 RID: 10006 RVA: 0x000BA3E4 File Offset: 0x000B85E4
		public bool HasError
		{
			get
			{
				return this.m_hasError;
			}
		}

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x000BA3EC File Offset: 0x000B85EC
		public List<DataShapeErrorMessage> Messages
		{
			get
			{
				return this.m_messages;
			}
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x000BA3F4 File Offset: 0x000B85F4
		public void Add(DataShapeErrorMessage message)
		{
			this.m_messages.Add(message);
			if (message.Severity == Severity.Error)
			{
				this.m_hasError = true;
			}
		}

		// Token: 0x04001671 RID: 5745
		private bool m_hasError;

		// Token: 0x04001672 RID: 5746
		private readonly List<DataShapeErrorMessage> m_messages = new List<DataShapeErrorMessage>();
	}
}
