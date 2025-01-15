using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000CC RID: 204
	public class EdmDecimalTypeReference : EdmPrimitiveTypeReference, IEdmDecimalTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
		public EdmDecimalTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, null, new int?(0))
		{
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000C3E4 File Offset: 0x0000A5E4
		public EdmDecimalTypeReference(IEdmPrimitiveType definition, bool isNullable, int? precision, int? scale)
			: base(definition, isNullable)
		{
			this.precision = precision;
			this.scale = scale;
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0000C3FD File Offset: 0x0000A5FD
		public int? Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000C405 File Offset: 0x0000A605
		public int? Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x040001A7 RID: 423
		private readonly int? precision;

		// Token: 0x040001A8 RID: 424
		private readonly int? scale;
	}
}
