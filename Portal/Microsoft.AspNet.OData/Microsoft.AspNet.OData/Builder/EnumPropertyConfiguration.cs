using System;
using System.Reflection;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000118 RID: 280
	public class EnumPropertyConfiguration : StructuralPropertyConfiguration
	{
		// Token: 0x06000998 RID: 2456 RVA: 0x000282A7 File Offset: 0x000264A7
		public EnumPropertyConfiguration(PropertyInfo property, StructuralTypeConfiguration declaringType)
			: base(property, declaringType)
		{
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x000282B1 File Offset: 0x000264B1
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x000282B9 File Offset: 0x000264B9
		public string DefaultValueString { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0000605C File Offset: 0x0000425C
		public override PropertyKind Kind
		{
			get
			{
				return PropertyKind.Enum;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x000282C2 File Offset: 0x000264C2
		public override Type RelatedClrType
		{
			get
			{
				return base.PropertyInfo.PropertyType;
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000282CF File Offset: 0x000264CF
		public EnumPropertyConfiguration IsOptional()
		{
			base.OptionalProperty = true;
			return this;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000282D9 File Offset: 0x000264D9
		public EnumPropertyConfiguration IsRequired()
		{
			base.OptionalProperty = false;
			return this;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x000282E3 File Offset: 0x000264E3
		public EnumPropertyConfiguration IsConcurrencyToken()
		{
			base.ConcurrencyToken = true;
			return this;
		}
	}
}
