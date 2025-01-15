using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x02000082 RID: 130
	internal abstract class ODataUriBuilder
	{
		// Token: 0x0600053C RID: 1340 RVA: 0x000132C0 File Offset: 0x000114C0
		internal virtual Uri BuildBaseUri()
		{
			return null;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000132C3 File Offset: 0x000114C3
		internal virtual Uri BuildEntitySetUri(Uri baseUri, string entitySetName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entitySetName, "entitySetName");
			return null;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000132D1 File Offset: 0x000114D1
		internal virtual Uri BuildEntityInstanceUri(Uri baseUri, ICollection<KeyValuePair<string, object>> keyProperties, string entityTypeName)
		{
			ExceptionUtils.CheckArgumentNotNull<ICollection<KeyValuePair<string, object>>>(keyProperties, "keyProperties");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entityTypeName, "entityTypeName");
			return null;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000132EA File Offset: 0x000114EA
		internal virtual Uri BuildStreamEditLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000132F8 File Offset: 0x000114F8
		internal virtual Uri BuildStreamReadLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00013306 File Offset: 0x00011506
		internal virtual Uri BuildNavigationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00013314 File Offset: 0x00011514
		internal virtual Uri BuildAssociationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00013322 File Offset: 0x00011522
		internal virtual Uri BuildOperationTargetUri(Uri baseUri, string operationName, string bindingParameterTypeName, string parameterNames)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00013330 File Offset: 0x00011530
		internal virtual Uri AppendTypeSegment(Uri baseUri, string typeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(typeName, "typeName");
			return null;
		}
	}
}
