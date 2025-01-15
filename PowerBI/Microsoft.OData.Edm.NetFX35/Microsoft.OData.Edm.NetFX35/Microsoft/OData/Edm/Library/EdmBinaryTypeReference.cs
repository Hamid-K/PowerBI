using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000124 RID: 292
	public class EdmBinaryTypeReference : EdmPrimitiveTypeReference, IEdmBinaryTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x0000E078 File Offset: 0x0000C278
		public EdmBinaryTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, false, default(int?))
		{
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000E097 File Offset: 0x0000C297
		public EdmBinaryTypeReference(IEdmPrimitiveType definition, bool isNullable, bool isUnbounded, int? maxLength)
			: base(definition, isNullable)
		{
			if (isUnbounded && maxLength != null)
			{
				throw new InvalidOperationException(Strings.EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull);
			}
			this.isUnbounded = isUnbounded;
			this.maxLength = maxLength;
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0000E0C7 File Offset: 0x0000C2C7
		public bool IsUnbounded
		{
			get
			{
				return this.isUnbounded;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0000E0CF File Offset: 0x0000C2CF
		public int? MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x0400022C RID: 556
		private readonly bool isUnbounded;

		// Token: 0x0400022D RID: 557
		private readonly int? maxLength;
	}
}
