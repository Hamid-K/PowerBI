using System;
using System.Reflection;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000139 RID: 313
	public class PrimitivePropertyConfiguration : StructuralPropertyConfiguration
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x000282A7 File Offset: 0x000264A7
		public PrimitivePropertyConfiguration(PropertyInfo property, StructuralTypeConfiguration declaringType)
			: base(property, declaringType)
		{
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0002C0C3 File Offset: 0x0002A2C3
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x0002C0CB File Offset: 0x0002A2CB
		public string DefaultValueString { get; set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x000102A1 File Offset: 0x0000E4A1
		public override PropertyKind Kind
		{
			get
			{
				return PropertyKind.Primitive;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x000282C2 File Offset: 0x000264C2
		public override Type RelatedClrType
		{
			get
			{
				return base.PropertyInfo.PropertyType;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0002C0D4 File Offset: 0x0002A2D4
		// (set) Token: 0x06000AFF RID: 2815 RVA: 0x0002C0DC File Offset: 0x0002A2DC
		public EdmPrimitiveTypeKind? TargetEdmTypeKind { get; internal set; }

		// Token: 0x06000B00 RID: 2816 RVA: 0x000282CF File Offset: 0x000264CF
		public PrimitivePropertyConfiguration IsOptional()
		{
			base.OptionalProperty = true;
			return this;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000282D9 File Offset: 0x000264D9
		public PrimitivePropertyConfiguration IsRequired()
		{
			base.OptionalProperty = false;
			return this;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000282E3 File Offset: 0x000264E3
		public PrimitivePropertyConfiguration IsConcurrencyToken()
		{
			base.ConcurrencyToken = true;
			return this;
		}
	}
}
