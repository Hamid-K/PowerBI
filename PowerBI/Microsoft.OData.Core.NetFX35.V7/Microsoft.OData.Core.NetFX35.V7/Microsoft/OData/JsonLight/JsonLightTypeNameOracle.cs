using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x020001F9 RID: 505
	internal abstract class JsonLightTypeNameOracle : TypeNameOracle
	{
		// Token: 0x060013AC RID: 5036
		internal abstract string GetResourceSetTypeNameForForWriting(string expectedResourceTypeName, ODataResourceSet resourceSet, bool isUndeclared);

		// Token: 0x060013AD RID: 5037
		internal abstract string GetResourceTypeNameForWriting(string expectedTypeName, ODataResource resource, bool isUndeclared);

		// Token: 0x060013AE RID: 5038
		internal abstract string GetValueTypeNameForWriting(ODataValue value, IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, bool isOpenProperty);

		// Token: 0x060013AF RID: 5039
		internal abstract string GetValueTypeNameForWriting(ODataValue value, PropertySerializationInfo propertyInfo, bool isOpenProperty);
	}
}
