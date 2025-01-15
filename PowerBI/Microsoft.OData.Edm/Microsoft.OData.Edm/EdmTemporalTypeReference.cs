using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000D0 RID: 208
	public class EdmTemporalTypeReference : EdmPrimitiveTypeReference, IEdmTemporalTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060004E0 RID: 1248 RVA: 0x0000C485 File Offset: 0x0000A685
		public EdmTemporalTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, new int?(0))
		{
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000C495 File Offset: 0x0000A695
		public EdmTemporalTypeReference(IEdmPrimitiveType definition, bool isNullable, int? precision)
			: base(definition, isNullable)
		{
			this.precision = precision;
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0000C4A6 File Offset: 0x0000A6A6
		public int? Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x040001AC RID: 428
		private readonly int? precision;
	}
}
