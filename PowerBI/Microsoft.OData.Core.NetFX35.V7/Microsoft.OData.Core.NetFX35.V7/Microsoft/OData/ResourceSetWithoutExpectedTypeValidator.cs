using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x0200000C RID: 12
	internal sealed class ResourceSetWithoutExpectedTypeValidator
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002D08 File Offset: 0x00000F08
		internal void ValidateResource(IEdmStructuredType resourceType)
		{
			if (this.itemType == null)
			{
				this.itemType = resourceType;
			}
			if (this.itemType.IsEquivalentTo(resourceType))
			{
				return;
			}
			IEdmType commonBaseType = this.itemType.GetCommonBaseType(resourceType);
			if (commonBaseType == null)
			{
				throw new ODataException(Strings.ResourceSetWithoutExpectedTypeValidator_IncompatibleTypes(resourceType.FullTypeName(), this.itemType.FullTypeName()));
			}
			this.itemType = (IEdmStructuredType)commonBaseType;
		}

		// Token: 0x04000024 RID: 36
		private IEdmStructuredType itemType;
	}
}
