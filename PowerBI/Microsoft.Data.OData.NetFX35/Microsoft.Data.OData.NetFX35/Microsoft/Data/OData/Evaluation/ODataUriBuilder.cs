using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000139 RID: 313
	internal abstract class ODataUriBuilder
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x0001AC68 File Offset: 0x00018E68
		internal virtual Uri BuildBaseUri()
		{
			return null;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001AC6B File Offset: 0x00018E6B
		internal virtual Uri BuildEntitySetUri(Uri baseUri, string entitySetName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entitySetName, "entitySetName");
			return null;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001AC79 File Offset: 0x00018E79
		internal virtual Uri BuildEntityInstanceUri(Uri baseUri, ICollection<KeyValuePair<string, object>> keyProperties, string entityTypeName)
		{
			ExceptionUtils.CheckArgumentNotNull<ICollection<KeyValuePair<string, object>>>(keyProperties, "keyProperties");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entityTypeName, "entityTypeName");
			return null;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001AC92 File Offset: 0x00018E92
		internal virtual Uri BuildStreamEditLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001ACA0 File Offset: 0x00018EA0
		internal virtual Uri BuildStreamReadLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001ACAE File Offset: 0x00018EAE
		internal virtual Uri BuildNavigationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001ACBC File Offset: 0x00018EBC
		internal virtual Uri BuildAssociationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001ACCA File Offset: 0x00018ECA
		internal virtual Uri BuildOperationTargetUri(Uri baseUri, string operationName, string bindingParameterTypeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001ACD8 File Offset: 0x00018ED8
		internal virtual Uri AppendTypeSegment(Uri baseUri, string typeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(typeName, "typeName");
			return null;
		}
	}
}
