using System;
using System.Reflection;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x020000FD RID: 253
	public class LengthPropertyConfiguration : PrimitivePropertyConfiguration
	{
		// Token: 0x060008E4 RID: 2276 RVA: 0x00025395 File Offset: 0x00023595
		public LengthPropertyConfiguration(PropertyInfo property, StructuralTypeConfiguration declaringType)
			: base(property, declaringType)
		{
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0002539F File Offset: 0x0002359F
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x000253A7 File Offset: 0x000235A7
		public int? MaxLength { get; set; }
	}
}
