using System;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000474 RID: 1140
	public abstract class EdmTypeAttribute : Attribute
	{
		// Token: 0x0600379D RID: 14237 RVA: 0x000B6081 File Offset: 0x000B4281
		internal EdmTypeAttribute()
		{
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x0600379E RID: 14238 RVA: 0x000B6089 File Offset: 0x000B4289
		// (set) Token: 0x0600379F RID: 14239 RVA: 0x000B6091 File Offset: 0x000B4291
		public string Name { get; set; }

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x000B609A File Offset: 0x000B429A
		// (set) Token: 0x060037A1 RID: 14241 RVA: 0x000B60A2 File Offset: 0x000B42A2
		public string NamespaceName { get; set; }
	}
}
