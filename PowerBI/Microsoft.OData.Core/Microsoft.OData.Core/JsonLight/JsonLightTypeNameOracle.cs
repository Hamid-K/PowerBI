using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000232 RID: 562
	internal abstract class JsonLightTypeNameOracle : TypeNameOracle
	{
		// Token: 0x0600187C RID: 6268
		internal abstract string GetResourceSetTypeNameForWriting(string expectedResourceTypeName, ODataResourceSet resourceSet, bool isUndeclared);

		// Token: 0x0600187D RID: 6269
		internal abstract string GetResourceTypeNameForWriting(string expectedTypeName, ODataResourceBase resource, bool isUndeclared);

		// Token: 0x0600187E RID: 6270
		internal abstract string GetValueTypeNameForWriting(ODataValue value, IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, bool isOpenProperty);

		// Token: 0x0600187F RID: 6271
		internal abstract string GetValueTypeNameForWriting(ODataValue value, PropertySerializationInfo propertyInfo, bool isOpenProperty);
	}
}
