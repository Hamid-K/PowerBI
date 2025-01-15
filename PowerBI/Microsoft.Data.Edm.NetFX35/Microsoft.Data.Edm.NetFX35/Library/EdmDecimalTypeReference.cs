using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020000F3 RID: 243
	public class EdmDecimalTypeReference : EdmPrimitiveTypeReference, IEdmDecimalTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		public EdmDecimalTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, default(int?), default(int?))
		{
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000C4E3 File Offset: 0x0000A6E3
		public EdmDecimalTypeReference(IEdmPrimitiveType definition, bool isNullable, int? precision, int? scale)
			: base(definition, isNullable)
		{
			this.precision = precision;
			this.scale = scale;
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000C4FC File Offset: 0x0000A6FC
		public int? Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0000C504 File Offset: 0x0000A704
		public int? Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x040001C4 RID: 452
		private readonly int? precision;

		// Token: 0x040001C5 RID: 453
		private readonly int? scale;
	}
}
