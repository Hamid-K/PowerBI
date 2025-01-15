using System;
using System.Globalization;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000205 RID: 517
	internal sealed class ListSubscriptionActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x00041AB4 File Offset: 0x0003FCB4
		// (set) Token: 0x0600125B RID: 4699 RVA: 0x00041ABC File Offset: 0x0003FCBC
		public string Path
		{
			get
			{
				return this.m_path;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_path = value;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x00041AD5 File Offset: 0x0003FCD5
		// (set) Token: 0x0600125D RID: 4701 RVA: 0x00041ADD File Offset: 0x0003FCDD
		public SubscriptionType SubscriptionType
		{
			get
			{
				return this.m_subscriptionType;
			}
			set
			{
				this.m_subscriptionType = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x00041AE6 File Offset: 0x0003FCE6
		// (set) Token: 0x0600125F RID: 4703 RVA: 0x00041AEE File Offset: 0x0003FCEE
		public bool PathIsSiteOrFolder
		{
			get
			{
				return this.m_pathIsSite;
			}
			set
			{
				this.m_pathIsSite = value;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x00041AF7 File Offset: 0x0003FCF7
		// (set) Token: 0x06001261 RID: 4705 RVA: 0x00041AFF File Offset: 0x0003FCFF
		public bool IncludeExtensionSettings
		{
			get
			{
				return this.m_includeExtensionSettings;
			}
			set
			{
				this.m_includeExtensionSettings = value;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x00041B08 File Offset: 0x0003FD08
		// (set) Token: 0x06001263 RID: 4707 RVA: 0x00041B10 File Offset: 0x0003FD10
		public string Owner
		{
			get
			{
				return this.m_owner;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
				}
				this.m_owner = value;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x00041B29 File Offset: 0x0003FD29
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", (this.Path != null) ? this.Path : "null", (this.Owner != null) ? this.Owner : "null");
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x00041B64 File Offset: 0x0003FD64
		// (set) Token: 0x06001266 RID: 4710 RVA: 0x00041B6C File Offset: 0x0003FD6C
		public SubscriptionImpl[] Children
		{
			get
			{
				return this.m_children;
			}
			set
			{
				this.m_children = value;
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal override void Validate()
		{
		}

		// Token: 0x04000698 RID: 1688
		private string m_path;

		// Token: 0x04000699 RID: 1689
		private SubscriptionType m_subscriptionType;

		// Token: 0x0400069A RID: 1690
		private bool m_pathIsSite;

		// Token: 0x0400069B RID: 1691
		private bool m_includeExtensionSettings = true;

		// Token: 0x0400069C RID: 1692
		private string m_owner;

		// Token: 0x0400069D RID: 1693
		private SubscriptionImpl[] m_children = new SubscriptionImpl[0];
	}
}
