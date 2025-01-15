using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000078 RID: 120
	public class EdmTemporalTypeReference : EdmPrimitiveTypeReference, IEdmTemporalTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x0000C4BB File Offset: 0x0000A6BB
		public EdmTemporalTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, new int?(0))
		{
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000C4CB File Offset: 0x0000A6CB
		public EdmTemporalTypeReference(IEdmPrimitiveType definition, bool isNullable, int? precision)
			: base(definition, isNullable)
		{
			this.precision = precision;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000C4DC File Offset: 0x0000A6DC
		public int? Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x0400010B RID: 267
		private readonly int? precision;
	}
}
