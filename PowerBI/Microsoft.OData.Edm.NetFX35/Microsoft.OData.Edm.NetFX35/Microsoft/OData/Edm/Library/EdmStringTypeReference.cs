using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000147 RID: 327
	public class EdmStringTypeReference : EdmPrimitiveTypeReference, IEdmStringTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x0600064B RID: 1611 RVA: 0x0000E8E4 File Offset: 0x0000CAE4
		public EdmStringTypeReference(IEdmPrimitiveType definition, bool isNullable)
			: this(definition, isNullable, false, default(int?), new bool?(true))
		{
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000E909 File Offset: 0x0000CB09
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

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0000E941 File Offset: 0x0000CB41
		public bool IsUnbounded
		{
			get
			{
				return this.isUnbounded;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0000E949 File Offset: 0x0000CB49
		public int? MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0000E951 File Offset: 0x0000CB51
		public bool? IsUnicode
		{
			get
			{
				return this.isUnicode;
			}
		}

		// Token: 0x04000253 RID: 595
		private readonly bool isUnbounded;

		// Token: 0x04000254 RID: 596
		private readonly int? maxLength;

		// Token: 0x04000255 RID: 597
		private readonly bool? isUnicode;
	}
}
