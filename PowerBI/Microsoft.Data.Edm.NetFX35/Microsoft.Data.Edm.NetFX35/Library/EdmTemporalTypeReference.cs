using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x0200010C RID: 268
	public class EdmTemporalTypeReference : EdmPrimitiveTypeReference, IEdmTemporalTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		public EdmTemporalTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, default(int?))
		{
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000CBDA File Offset: 0x0000ADDA
		public EdmTemporalTypeReference(IEdmPrimitiveType definition, bool isNullable, int? precision)
			: base(definition, isNullable)
		{
			this.precision = precision;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0000CBEB File Offset: 0x0000ADEB
		public int? Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x040001E5 RID: 485
		private readonly int? precision;
	}
}
