using System;
using System.Reflection;
using Microsoft.AspNet.OData.Formatter;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200013F RID: 319
	public abstract class StructuralPropertyConfiguration : PropertyConfiguration
	{
		// Token: 0x06000B7C RID: 2940 RVA: 0x0002CAFC File Offset: 0x0002ACFC
		protected StructuralPropertyConfiguration(PropertyInfo property, StructuralTypeConfiguration declaringType)
			: base(property, declaringType)
		{
			this.OptionalProperty = EdmLibHelpers.IsNullable(property.PropertyType);
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0002CB17 File Offset: 0x0002AD17
		// (set) Token: 0x06000B7E RID: 2942 RVA: 0x0002CB1F File Offset: 0x0002AD1F
		public bool OptionalProperty { get; set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0002CB28 File Offset: 0x0002AD28
		// (set) Token: 0x06000B80 RID: 2944 RVA: 0x0002CB30 File Offset: 0x0002AD30
		public bool ConcurrencyToken { get; set; }
	}
}
