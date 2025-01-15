using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000130 RID: 304
	public class EdmDecimalTypeReference : EdmPrimitiveTypeReference, IEdmDecimalTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060005E6 RID: 1510 RVA: 0x0000E25C File Offset: 0x0000C45C
		public EdmDecimalTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, default(int?), new int?(0))
		{
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000E280 File Offset: 0x0000C480
		public EdmDecimalTypeReference(IEdmPrimitiveType definition, bool isNullable, int? precision, int? scale)
			: base(definition, isNullable)
		{
			this.precision = precision;
			this.scale = scale;
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0000E299 File Offset: 0x0000C499
		public int? Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0000E2A1 File Offset: 0x0000C4A1
		public int? Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x04000233 RID: 563
		private readonly int? precision;

		// Token: 0x04000234 RID: 564
		private readonly int? scale;
	}
}
