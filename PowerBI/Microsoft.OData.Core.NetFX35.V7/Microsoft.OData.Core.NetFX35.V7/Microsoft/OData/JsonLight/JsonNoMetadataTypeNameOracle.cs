using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x020001FD RID: 509
	internal sealed class JsonNoMetadataTypeNameOracle : JsonLightTypeNameOracle
	{
		// Token: 0x060013BF RID: 5055 RVA: 0x0000B41B File Offset: 0x0000961B
		internal override string GetResourceSetTypeNameForForWriting(string expectedResourceTypeName, ODataResourceSet resourceSet, bool isUndeclared)
		{
			return null;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0000B41B File Offset: 0x0000961B
		internal override string GetResourceTypeNameForWriting(string expectedTypeName, ODataResource resource, bool isUndeclared = false)
		{
			return null;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0000B41B File Offset: 0x0000961B
		internal override string GetValueTypeNameForWriting(ODataValue value, IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, bool isOpenProperty)
		{
			return null;
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0000B41B File Offset: 0x0000961B
		internal override string GetValueTypeNameForWriting(ODataValue value, PropertySerializationInfo propertyInfo, bool isOpenProperty)
		{
			return null;
		}
	}
}
