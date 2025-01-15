using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000059 RID: 89
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	[Serializable]
	public abstract class Page
	{
		// Token: 0x06000676 RID: 1654 RVA: 0x00019156 File Offset: 0x00017356
		protected Page(PageSection pageHeader, PageSection pageFooter)
		{
			this.Header = pageHeader;
			this.Footer = pageFooter;
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001916C File Offset: 0x0001736C
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x00019174 File Offset: 0x00017374
		internal PageSection PageSectionHeader
		{
			get
			{
				return this.m_pageHeader;
			}
			set
			{
				this.m_pageHeader = value;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001917D File Offset: 0x0001737D
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x00019185 File Offset: 0x00017385
		internal PageSection PageSectionFooter
		{
			get
			{
				return this.m_pageFooter;
			}
			set
			{
				this.m_pageFooter = value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001918E File Offset: 0x0001738E
		internal PageSectionInstance HeaderInstance
		{
			get
			{
				return this.m_pageHeaderInstance;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00019196 File Offset: 0x00017396
		internal PageSectionInstance FooterInstance
		{
			get
			{
				return this.m_pageFooterInstance;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0001919E File Offset: 0x0001739E
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x000191A6 File Offset: 0x000173A6
		public PageSection Header
		{
			get
			{
				return this.m_pageHeader;
			}
			set
			{
				this.m_pageHeader = value;
				if (value != null)
				{
					this.m_pageHeaderInstance = (PageSectionInstance)value.ReportItemInstance;
				}
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x000191C3 File Offset: 0x000173C3
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x000191CB File Offset: 0x000173CB
		public PageSection Footer
		{
			get
			{
				return this.m_pageFooter;
			}
			set
			{
				this.m_pageFooter = value;
				if (value != null)
				{
					this.m_pageFooterInstance = (PageSectionInstance)value.ReportItemInstance;
				}
			}
		}

		// Token: 0x040001AB RID: 427
		private PageSectionInstance m_pageHeaderInstance;

		// Token: 0x040001AC RID: 428
		private PageSectionInstance m_pageFooterInstance;

		// Token: 0x040001AD RID: 429
		[NonSerialized]
		private PageSection m_pageHeader;

		// Token: 0x040001AE RID: 430
		[NonSerialized]
		private PageSection m_pageFooter;
	}
}
