using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000048 RID: 72
	public class EdmBinaryTypeReference : EdmPrimitiveTypeReference, IEdmBinaryTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x00009C00 File Offset: 0x00007E00
		public EdmBinaryTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, false, default(int?))
		{
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00009C1F File Offset: 0x00007E1F
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

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00009C4F File Offset: 0x00007E4F
		public bool IsUnbounded
		{
			get
			{
				return this.isUnbounded;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00009C57 File Offset: 0x00007E57
		public int? MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x0400006F RID: 111
		private readonly bool isUnbounded;

		// Token: 0x04000070 RID: 112
		private readonly int? maxLength;
	}
}
