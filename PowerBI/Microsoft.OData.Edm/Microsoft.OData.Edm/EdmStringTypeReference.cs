using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000CF RID: 207
	public class EdmStringTypeReference : EdmPrimitiveTypeReference, IEdmStringTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x0000C410 File Offset: 0x0000A610
		public EdmStringTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, false, null, new bool?(true))
		{
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000C435 File Offset: 0x0000A635
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

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000C46D File Offset: 0x0000A66D
		public bool IsUnbounded
		{
			get
			{
				return this.isUnbounded;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000C475 File Offset: 0x0000A675
		public int? MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000C47D File Offset: 0x0000A67D
		public bool? IsUnicode
		{
			get
			{
				return this.isUnicode;
			}
		}

		// Token: 0x040001A9 RID: 425
		private readonly bool isUnbounded;

		// Token: 0x040001AA RID: 426
		private readonly int? maxLength;

		// Token: 0x040001AB RID: 427
		private readonly bool? isUnicode;
	}
}
