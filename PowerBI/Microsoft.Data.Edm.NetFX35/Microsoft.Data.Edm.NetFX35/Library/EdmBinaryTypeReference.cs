using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020000E8 RID: 232
	public class EdmBinaryTypeReference : EdmPrimitiveTypeReference, IEdmBinaryTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		public EdmBinaryTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, false, default(int?), default(bool?))
		{
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		public EdmBinaryTypeReference(IEdmPrimitiveType definition, bool isNullable, bool isUnbounded, int? maxLength, bool? isFixedLength)
			: base(definition, isNullable)
		{
			if (isUnbounded && maxLength != null)
			{
				throw new InvalidOperationException(Strings.EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull);
			}
			this.isUnbounded = isUnbounded;
			this.maxLength = maxLength;
			this.isFixedLength = isFixedLength;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000C31C File Offset: 0x0000A51C
		public bool? IsFixedLength
		{
			get
			{
				return this.isFixedLength;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000C324 File Offset: 0x0000A524
		public bool IsUnbounded
		{
			get
			{
				return this.isUnbounded;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000C32C File Offset: 0x0000A52C
		public int? MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x040001BC RID: 444
		private readonly bool isUnbounded;

		// Token: 0x040001BD RID: 445
		private readonly int? maxLength;

		// Token: 0x040001BE RID: 446
		private readonly bool? isFixedLength;
	}
}
