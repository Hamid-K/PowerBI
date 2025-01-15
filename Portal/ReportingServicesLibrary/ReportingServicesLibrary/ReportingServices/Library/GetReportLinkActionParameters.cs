using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200019A RID: 410
	internal sealed class GetReportLinkActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00036796 File Offset: 0x00034996
		// (set) Token: 0x06000F00 RID: 3840 RVA: 0x0003679E File Offset: 0x0003499E
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

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x000367A7 File Offset: 0x000349A7
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x000367AF File Offset: 0x000349AF
		public string LinkPath
		{
			get
			{
				return this.m_linkPath;
			}
			set
			{
				this.m_linkPath = value;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x000367B8 File Offset: 0x000349B8
		internal override string InputTrace
		{
			get
			{
				return this.ReportPath;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x000367C0 File Offset: 0x000349C0
		internal override string OutputTrace
		{
			get
			{
				return this.LinkPath;
			}
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x000367C8 File Offset: 0x000349C8
		internal override void Validate()
		{
			if (this.ReportPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Report");
			}
		}

		// Token: 0x04000614 RID: 1556
		private string m_reportPath;

		// Token: 0x04000615 RID: 1557
		private string m_linkPath;
	}
}
