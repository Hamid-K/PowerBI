using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000C9 RID: 201
	public class EdmBinaryTypeReference : EdmPrimitiveTypeReference, IEdmBinaryTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060004CF RID: 1231 RVA: 0x0000C330 File Offset: 0x0000A530
		public EdmBinaryTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, false, null)
		{
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000C34F File Offset: 0x0000A54F
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

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000C37F File Offset: 0x0000A57F
		public bool IsUnbounded
		{
			get
			{
				return this.isUnbounded;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000C387 File Offset: 0x0000A587
		public int? MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x0400017C RID: 380
		private readonly bool isUnbounded;

		// Token: 0x0400017D RID: 381
		private readonly int? maxLength;
	}
}
