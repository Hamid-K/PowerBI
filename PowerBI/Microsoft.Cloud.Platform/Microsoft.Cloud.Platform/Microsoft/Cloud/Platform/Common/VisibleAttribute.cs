using System;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000534 RID: 1332
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class VisibleAttribute : Attribute
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060028CA RID: 10442 RVA: 0x00092622 File Offset: 0x00090822
		// (set) Token: 0x060028CB RID: 10443 RVA: 0x0009262A File Offset: 0x0009082A
		public bool Enable { get; set; }

		// Token: 0x060028CC RID: 10444 RVA: 0x00092633 File Offset: 0x00090833
		public VisibleAttribute()
		{
			this.Enable = true;
		}
	}
}
