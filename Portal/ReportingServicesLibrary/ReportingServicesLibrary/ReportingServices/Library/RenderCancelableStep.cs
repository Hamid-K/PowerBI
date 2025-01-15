using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002B1 RID: 689
	internal abstract class RenderCancelableStep : CancelableLibraryStep
	{
		// Token: 0x06001918 RID: 6424 RVA: 0x00064E24 File Offset: 0x00063024
		protected RenderCancelableStep(RSService rs, CatalogItemContext ctx, ClientRequest session, JobType type)
			: base(UrlFriendlyUIDGenerator.Create(), ctx.ItemPath, JobActionEnum.Render, type, rs.UserContext)
		{
			this.m_ctx = ctx;
			this.m_session = session;
			this.m_rs = rs;
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x00064E55 File Offset: 0x00063055
		public Warning[] Warnings
		{
			get
			{
				return this.m_warnings;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x00064E5D File Offset: 0x0006305D
		public ParameterInfoCollection EffectiveParameters
		{
			get
			{
				return this.m_effectiveParameters;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x00064E65 File Offset: 0x00063065
		public RSStream PrimaryStream
		{
			get
			{
				return this.m_primarystream;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x0600191C RID: 6428 RVA: 0x00064E6D File Offset: 0x0006306D
		public string[] SecondaryStreamNames
		{
			get
			{
				return this.m_secondaryStreamNames;
			}
		}

		// Token: 0x0400090A RID: 2314
		protected CatalogItemContext m_ctx;

		// Token: 0x0400090B RID: 2315
		protected ClientRequest m_session;

		// Token: 0x0400090C RID: 2316
		protected RSService m_rs;

		// Token: 0x0400090D RID: 2317
		protected Warning[] m_warnings;

		// Token: 0x0400090E RID: 2318
		protected ParameterInfoCollection m_effectiveParameters;

		// Token: 0x0400090F RID: 2319
		protected RSStream m_primarystream;

		// Token: 0x04000910 RID: 2320
		protected string[] m_secondaryStreamNames;
	}
}
