using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000033 RID: 51
	internal sealed class ResourceSetWithoutExpectedTypeValidator
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x000051E2 File Offset: 0x000033E2
		public ResourceSetWithoutExpectedTypeValidator(IEdmType memberType)
		{
			this.itemType = memberType;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000051F4 File Offset: 0x000033F4
		internal void ValidateResource(IEdmType itemType)
		{
			if (this.itemType == null || this.itemType.TypeKind == EdmTypeKind.Untyped)
			{
				return;
			}
			if (this.itemType.IsEquivalentTo(itemType))
			{
				return;
			}
			IEdmStructuredType edmStructuredType = itemType as IEdmStructuredType;
			IEdmStructuredType edmStructuredType2 = this.itemType as IEdmStructuredType;
			if (edmStructuredType == null || edmStructuredType2 == null)
			{
				throw new ODataException(Strings.ResourceSetWithoutExpectedTypeValidator_IncompatibleTypes(itemType.FullTypeName(), this.itemType.FullTypeName()));
			}
			if (!this.itemType.IsAssignableFrom(itemType))
			{
				throw new ODataException(Strings.ResourceSetWithoutExpectedTypeValidator_IncompatibleTypes(itemType.FullTypeName(), this.itemType.FullTypeName()));
			}
		}

		// Token: 0x04000094 RID: 148
		private IEdmType itemType;
	}
}
