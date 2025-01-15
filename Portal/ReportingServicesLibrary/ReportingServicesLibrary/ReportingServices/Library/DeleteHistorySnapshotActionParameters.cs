using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001C0 RID: 448
	internal sealed class DeleteHistorySnapshotActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0003864D File Offset: 0x0003684D
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x00038655 File Offset: 0x00036855
		public string ReportPath
		{
			get
			{
				return this.m_reportPath;
			}
			set
			{
				this.m_reportPath = value;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0003865E File Offset: 0x0003685E
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x00038666 File Offset: 0x00036866
		public string HistoryId
		{
			get
			{
				return this.m_historyID;
			}
			set
			{
				this.m_historyID = value;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0003866F File Offset: 0x0003686F
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.m_reportPath, this.m_historyID);
			}
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0003868C File Offset: 0x0003688C
		internal override void Validate()
		{
			if (this.m_reportPath == null)
			{
				throw new MissingParameterException("Report");
			}
			if (this.m_historyID == null)
			{
				throw new MissingParameterException("HistoryID");
			}
		}

		// Token: 0x04000641 RID: 1601
		private string m_reportPath;

		// Token: 0x04000642 RID: 1602
		private string m_historyID;
	}
}
