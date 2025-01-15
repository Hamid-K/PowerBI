using System;
using System.Reflection;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x020000FE RID: 254
	public class PrecisionPropertyConfiguration : PrimitivePropertyConfiguration
	{
		// Token: 0x060008E7 RID: 2279 RVA: 0x00025395 File Offset: 0x00023595
		public PrecisionPropertyConfiguration(PropertyInfo property, StructuralTypeConfiguration declaringType)
			: base(property, declaringType)
		{
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x000253B0 File Offset: 0x000235B0
		// (set) Token: 0x060008E9 RID: 2281 RVA: 0x000253B8 File Offset: 0x000235B8
		public int? Precision { get; set; }
	}
}
