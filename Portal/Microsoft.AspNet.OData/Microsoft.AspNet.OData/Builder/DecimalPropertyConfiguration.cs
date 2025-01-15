using System;
using System.Reflection;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x020000FC RID: 252
	public class DecimalPropertyConfiguration : PrecisionPropertyConfiguration
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x0002537A File Offset: 0x0002357A
		public DecimalPropertyConfiguration(PropertyInfo property, StructuralTypeConfiguration declaringType)
			: base(property, declaringType)
		{
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00025384 File Offset: 0x00023584
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x0002538C File Offset: 0x0002358C
		public int? Scale { get; set; }
	}
}
