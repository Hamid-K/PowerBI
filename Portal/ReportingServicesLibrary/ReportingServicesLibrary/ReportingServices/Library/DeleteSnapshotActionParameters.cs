using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001BE RID: 446
	internal sealed class DeleteSnapshotActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x000384B1 File Offset: 0x000366B1
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x000384B9 File Offset: 0x000366B9
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

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x000384C2 File Offset: 0x000366C2
		// (set) Token: 0x06000FCE RID: 4046 RVA: 0x000384CA File Offset: 0x000366CA
		public string SnapshotID
		{
			get
			{
				return this.m_snapshotID;
			}
			set
			{
				this.m_snapshotID = value;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x000384D3 File Offset: 0x000366D3
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.m_reportPath, this.m_snapshotID);
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x000384F0 File Offset: 0x000366F0
		internal override void Validate()
		{
			if (this.m_reportPath == null)
			{
				throw new MissingParameterException("Report");
			}
			if (this.m_snapshotID == null)
			{
				throw new MissingParameterException("HistoryID");
			}
		}

		// Token: 0x0400063F RID: 1599
		private string m_reportPath;

		// Token: 0x04000640 RID: 1600
		private string m_snapshotID;
	}
}
