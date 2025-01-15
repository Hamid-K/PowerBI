using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B24 RID: 2852
	internal abstract class GoogleAnalyticsProperty
	{
		// Token: 0x06004EF6 RID: 20214 RVA: 0x00105BA9 File Offset: 0x00103DA9
		protected GoogleAnalyticsProperty(IGoogleAnalyticsService service, string accountId, string id, string name)
		{
			this.service = service;
			this.accountId = accountId;
			this.id = id;
			this.name = name;
		}

		// Token: 0x170018BD RID: 6333
		// (get) Token: 0x06004EF7 RID: 20215 RVA: 0x00105BCE File Offset: 0x00103DCE
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170018BE RID: 6334
		// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x00105BD6 File Offset: 0x00103DD6
		public string ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170018BF RID: 6335
		// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x00105BDE File Offset: 0x00103DDE
		public string AccountID
		{
			get
			{
				return this.accountId;
			}
		}

		// Token: 0x170018C0 RID: 6336
		// (get) Token: 0x06004EFA RID: 20218 RVA: 0x00105BE6 File Offset: 0x00103DE6
		public IList<GoogleAnalyticsCube> Views
		{
			get
			{
				if (this.views == null)
				{
					this.views = this.CreateViews();
				}
				return this.views;
			}
		}

		// Token: 0x06004EFB RID: 20219
		public abstract IGoogleAnalyticsQueryCompiler CreateCompiler(GoogleAnalyticsCube cube);

		// Token: 0x06004EFC RID: 20220
		protected abstract IList<GoogleAnalyticsCube> CreateViews();

		// Token: 0x04002A80 RID: 10880
		protected readonly IGoogleAnalyticsService service;

		// Token: 0x04002A81 RID: 10881
		protected readonly string accountId;

		// Token: 0x04002A82 RID: 10882
		protected readonly string id;

		// Token: 0x04002A83 RID: 10883
		protected readonly string name;

		// Token: 0x04002A84 RID: 10884
		private IList<GoogleAnalyticsCube> views;
	}
}
