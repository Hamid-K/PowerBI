using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000BD RID: 189
	internal sealed class GetReportServerConfigInfoActionParameters : RSSoapActionParameters
	{
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00021B27 File Offset: 0x0001FD27
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x00021B2F File Offset: 0x0001FD2F
		public bool ScaleOut
		{
			get
			{
				return this.m_scaleOut;
			}
			set
			{
				this.m_scaleOut = value;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00021B38 File Offset: 0x0001FD38
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x00021B40 File Offset: 0x0001FD40
		public ServerConfigInfo[] ServerConfigInfo
		{
			get
			{
				return this.m_serverConfigInfo;
			}
			set
			{
				this.m_serverConfigInfo = value;
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal override void Validate()
		{
		}

		// Token: 0x04000422 RID: 1058
		private bool m_scaleOut;

		// Token: 0x04000423 RID: 1059
		private ServerConfigInfo[] m_serverConfigInfo;
	}
}
