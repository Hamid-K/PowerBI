using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000051 RID: 81
	public class EdmDecimalTypeReference : EdmPrimitiveTypeReference, IEdmDecimalTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000323 RID: 803 RVA: 0x0000A6AC File Offset: 0x000088AC
		public EdmDecimalTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, default(int?), new int?(0))
		{
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000A6D0 File Offset: 0x000088D0
		public EdmDecimalTypeReference(IEdmPrimitiveType definition, bool isNullable, int? precision, int? scale)
			: base(definition, isNullable)
		{
			this.precision = precision;
			this.scale = scale;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000A6E9 File Offset: 0x000088E9
		public int? Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000A6F1 File Offset: 0x000088F1
		public int? Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x040000A6 RID: 166
		private readonly int? precision;

		// Token: 0x040000A7 RID: 167
		private readonly int? scale;
	}
}
