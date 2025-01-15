using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000075 RID: 117
	public class EdmStringTypeReference : EdmPrimitiveTypeReference, IEdmStringTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x0000C220 File Offset: 0x0000A420
		public EdmStringTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, false, default(int?), new bool?(true))
		{
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000C245 File Offset: 0x0000A445
		public EdmStringTypeReference(IEdmPrimitiveType definition, bool isNullable, bool isUnbounded, int? maxLength, bool? isUnicode)
			: base(definition, isNullable)
		{
			if (isUnbounded && maxLength != null)
			{
				throw new InvalidOperationException(Strings.EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull);
			}
			this.isUnbounded = isUnbounded;
			this.maxLength = maxLength;
			this.isUnicode = isUnicode;
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000C27D File Offset: 0x0000A47D
		public bool IsUnbounded
		{
			get
			{
				return this.isUnbounded;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000C285 File Offset: 0x0000A485
		public int? MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000C28D File Offset: 0x0000A48D
		public bool? IsUnicode
		{
			get
			{
				return this.isUnicode;
			}
		}

		// Token: 0x04000101 RID: 257
		private readonly bool isUnbounded;

		// Token: 0x04000102 RID: 258
		private readonly int? maxLength;

		// Token: 0x04000103 RID: 259
		private readonly bool? isUnicode;
	}
}
