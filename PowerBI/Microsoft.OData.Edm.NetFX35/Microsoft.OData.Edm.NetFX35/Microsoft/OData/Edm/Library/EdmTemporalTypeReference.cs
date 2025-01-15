using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200014A RID: 330
	public class EdmTemporalTypeReference : EdmPrimitiveTypeReference, IEdmTemporalTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000654 RID: 1620 RVA: 0x0000E9E2 File Offset: 0x0000CBE2
		public EdmTemporalTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, new int?(0))
		{
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0000E9F2 File Offset: 0x0000CBF2
		public EdmTemporalTypeReference(IEdmPrimitiveType definition, bool isNullable, int? precision)
			: base(definition, isNullable)
		{
			this.precision = precision;
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x0000EA03 File Offset: 0x0000CC03
		public int? Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x04000257 RID: 599
		private readonly int? precision;
	}
}
