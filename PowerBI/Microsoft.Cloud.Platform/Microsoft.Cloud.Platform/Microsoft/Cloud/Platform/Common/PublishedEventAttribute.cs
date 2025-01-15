using System;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000531 RID: 1329
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class PublishedEventAttribute : Attribute
	{
		// Token: 0x060028BC RID: 10428 RVA: 0x0009258B File Offset: 0x0009078B
		public PublishedEventAttribute()
		{
			this.AlwaysEnabled = false;
			this.Enable = true;
			this.PublishTo = PublishEventTo.All;
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060028BD RID: 10429 RVA: 0x000925A8 File Offset: 0x000907A8
		// (set) Token: 0x060028BE RID: 10430 RVA: 0x000925B0 File Offset: 0x000907B0
		public PublishEventTo PublishTo { get; set; }

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060028BF RID: 10431 RVA: 0x000925B9 File Offset: 0x000907B9
		// (set) Token: 0x060028C0 RID: 10432 RVA: 0x000925C1 File Offset: 0x000907C1
		public bool Enable { get; set; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060028C1 RID: 10433 RVA: 0x000925CA File Offset: 0x000907CA
		// (set) Token: 0x060028C2 RID: 10434 RVA: 0x000925D2 File Offset: 0x000907D2
		public bool AlwaysEnabled { get; set; }
	}
}
