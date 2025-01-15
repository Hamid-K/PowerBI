using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200019F RID: 415
	internal sealed class GetCacheOptionsActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00036B4B File Offset: 0x00034D4B
		// (set) Token: 0x06000F22 RID: 3874 RVA: 0x00036B53 File Offset: 0x00034D53
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

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x00036B5C File Offset: 0x00034D5C
		// (set) Token: 0x06000F24 RID: 3876 RVA: 0x00036B64 File Offset: 0x00034D64
		public bool CacheReport
		{
			get
			{
				return this.m_cacheReport;
			}
			set
			{
				this.m_cacheReport = value;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00036B6D File Offset: 0x00034D6D
		// (set) Token: 0x06000F26 RID: 3878 RVA: 0x00036B75 File Offset: 0x00034D75
		public ExpirationDefinition Expiration
		{
			get
			{
				return this.m_expiration;
			}
			set
			{
				this.m_expiration = value;
			}
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00036B7E File Offset: 0x00034D7E
		internal override void Validate()
		{
			if (this.m_reportPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Report");
			}
		}

		// Token: 0x0400061D RID: 1565
		private string m_reportPath;

		// Token: 0x0400061E RID: 1566
		private bool m_cacheReport;

		// Token: 0x0400061F RID: 1567
		private ExpirationDefinition m_expiration;
	}
}
