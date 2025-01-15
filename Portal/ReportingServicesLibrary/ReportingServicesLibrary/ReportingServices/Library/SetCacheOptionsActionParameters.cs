using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001AA RID: 426
	internal sealed class SetCacheOptionsActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00037332 File Offset: 0x00035532
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x0003733A File Offset: 0x0003553A
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

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00037343 File Offset: 0x00035543
		// (set) Token: 0x06000F61 RID: 3937 RVA: 0x0003734B File Offset: 0x0003554B
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

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x00037354 File Offset: 0x00035554
		// (set) Token: 0x06000F63 RID: 3939 RVA: 0x0003735C File Offset: 0x0003555C
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

		// Token: 0x06000F64 RID: 3940 RVA: 0x00037368 File Offset: 0x00035568
		internal override void Validate()
		{
			if (this.m_reportPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Report");
			}
			if (!this.m_cacheReport)
			{
				if (this.m_expiration != null)
				{
					throw new InvalidParameterCombinationException();
				}
			}
			else
			{
				if (this.m_expiration == null)
				{
					throw new InvalidParameterCombinationException();
				}
				if (this.m_expiration is TimeExpiration && ((TimeExpiration)this.m_expiration).Minutes <= 0)
				{
					throw new InvalidParameterCombinationException();
				}
			}
		}

		// Token: 0x04000629 RID: 1577
		private string m_reportPath;

		// Token: 0x0400062A RID: 1578
		private bool m_cacheReport;

		// Token: 0x0400062B RID: 1579
		private ExpirationDefinition m_expiration;
	}
}
