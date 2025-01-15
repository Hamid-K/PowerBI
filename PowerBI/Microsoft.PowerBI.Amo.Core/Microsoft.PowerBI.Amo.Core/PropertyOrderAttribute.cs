using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyOrderAttribute : Attribute
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00003EF6 File Offset: 0x000020F6
		public PropertyOrderAttribute(int order)
		{
			this.order = order;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00003F05 File Offset: 0x00002105
		public int Order
		{
			get
			{
				return this.order;
			}
		}

		// Token: 0x04000050 RID: 80
		private int order;
	}
}
