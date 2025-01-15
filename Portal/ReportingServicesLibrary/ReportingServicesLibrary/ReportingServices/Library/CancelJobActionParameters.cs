using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000133 RID: 307
	internal sealed class CancelJobActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0002E240 File Offset: 0x0002C440
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x0002E248 File Offset: 0x0002C448
		public string JobID
		{
			get
			{
				return this.m_jobID;
			}
			set
			{
				this.m_jobID = value;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0002E251 File Offset: 0x0002C451
		// (set) Token: 0x06000C45 RID: 3141 RVA: 0x0002E259 File Offset: 0x0002C459
		public bool Cancelled
		{
			get
			{
				return this.m_cancelled;
			}
			set
			{
				this.m_cancelled = value;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0002E262 File Offset: 0x0002C462
		internal override string InputTrace
		{
			get
			{
				return this.JobID;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0002E26A File Offset: 0x0002C46A
		internal override string OutputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", this.Cancelled);
			}
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002E286 File Offset: 0x0002C486
		internal override void Validate()
		{
			if (this.JobID == null)
			{
				throw new MissingParameterException("JobID");
			}
		}

		// Token: 0x04000503 RID: 1283
		private string m_jobID;

		// Token: 0x04000504 RID: 1284
		private bool m_cancelled;
	}
}
