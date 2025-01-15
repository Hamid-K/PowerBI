using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001A4 RID: 420
	internal sealed class SetReportLinkActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x00036DA0 File Offset: 0x00034FA0
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x00036DA8 File Offset: 0x00034FA8
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

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00036DB1 File Offset: 0x00034FB1
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x00036DB9 File Offset: 0x00034FB9
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

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00036DC2 File Offset: 0x00034FC2
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.ReportPath, this.LinkPath);
			}
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00036DDF File Offset: 0x00034FDF
		internal override void Validate()
		{
			if (this.ReportPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Report");
			}
			if (this.LinkPath == null)
			{
				throw new MissingParameterException("Link");
			}
		}

		// Token: 0x04000622 RID: 1570
		private string m_reportPath;

		// Token: 0x04000623 RID: 1571
		private string m_linkPath;
	}
}
