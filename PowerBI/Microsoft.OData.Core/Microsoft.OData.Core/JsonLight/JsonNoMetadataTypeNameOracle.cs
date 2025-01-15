using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000236 RID: 566
	internal sealed class JsonNoMetadataTypeNameOracle : JsonLightTypeNameOracle
	{
		// Token: 0x0600188D RID: 6285 RVA: 0x0000360D File Offset: 0x0000180D
		internal override string GetResourceSetTypeNameForWriting(string expectedResourceTypeName, ODataResourceSet resourceSet, bool isUndeclared)
		{
			return null;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0000360D File Offset: 0x0000180D
		internal override string GetResourceTypeNameForWriting(string expectedTypeName, ODataResourceBase resource, bool isUndeclared = false)
		{
			return null;
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0000360D File Offset: 0x0000180D
		internal override string GetValueTypeNameForWriting(ODataValue value, IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, bool isOpenProperty)
		{
			return null;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0000360D File Offset: 0x0000180D
		internal override string GetValueTypeNameForWriting(ODataValue value, PropertySerializationInfo propertyInfo, bool isOpenProperty)
		{
			return null;
		}
	}
}
