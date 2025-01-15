using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001B5 RID: 437
	internal class SnapshotLimitActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00037F06 File Offset: 0x00036106
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x00037F0E File Offset: 0x0003610E
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

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x00037F17 File Offset: 0x00036117
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x00037F1F File Offset: 0x0003611F
		public bool UseSystem
		{
			get
			{
				return this.m_useSystem;
			}
			set
			{
				this.m_useSystem = value;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x00037F28 File Offset: 0x00036128
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x00037F30 File Offset: 0x00036130
		public int ScopedLimit
		{
			get
			{
				return this.m_limit;
			}
			set
			{
				this.m_limit = value;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00037F39 File Offset: 0x00036139
		internal override string InputTrace
		{
			get
			{
				return this.ReportPath;
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00037F41 File Offset: 0x00036141
		internal override void Validate()
		{
			if (this.m_reportPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Report");
			}
		}

		// Token: 0x04000637 RID: 1591
		private string m_reportPath;

		// Token: 0x04000638 RID: 1592
		private bool m_useSystem;

		// Token: 0x04000639 RID: 1593
		private int m_limit;
	}
}
