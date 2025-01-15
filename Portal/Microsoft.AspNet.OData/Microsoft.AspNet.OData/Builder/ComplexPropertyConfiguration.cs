using System;
using System.Reflection;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200012B RID: 299
	public class ComplexPropertyConfiguration : StructuralPropertyConfiguration
	{
		// Token: 0x06000A4B RID: 2635 RVA: 0x000282A7 File Offset: 0x000264A7
		public ComplexPropertyConfiguration(PropertyInfo property, StructuralTypeConfiguration declaringType)
			: base(property, declaringType)
		{
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x000032B9 File Offset: 0x000014B9
		public override PropertyKind Kind
		{
			get
			{
				return PropertyKind.Complex;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x000282C2 File Offset: 0x000264C2
		public override Type RelatedClrType
		{
			get
			{
				return base.PropertyInfo.PropertyType;
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x000282CF File Offset: 0x000264CF
		public ComplexPropertyConfiguration IsOptional()
		{
			base.OptionalProperty = true;
			return this;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000282D9 File Offset: 0x000264D9
		public ComplexPropertyConfiguration IsRequired()
		{
			base.OptionalProperty = false;
			return this;
		}
	}
}
