using System;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x0200053D RID: 1341
	[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class PerformanceCountersCategoryAttribute : Attribute
	{
		// Token: 0x060028D8 RID: 10456 RVA: 0x000926BB File Offset: 0x000908BB
		public PerformanceCountersCategoryAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060028D9 RID: 10457 RVA: 0x000926CA File Offset: 0x000908CA
		// (set) Token: 0x060028DA RID: 10458 RVA: 0x000926D2 File Offset: 0x000908D2
		public string Name { get; private set; }
	}
}
