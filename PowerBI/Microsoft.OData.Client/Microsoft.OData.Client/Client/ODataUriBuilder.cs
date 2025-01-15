using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Client
{
	// Token: 0x0200000B RID: 11
	internal abstract class ODataUriBuilder
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00003487 File Offset: 0x00001687
		internal virtual Uri BuildBaseUri()
		{
			return null;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000348A File Offset: 0x0000168A
		internal virtual Uri BuildEntitySetUri(Uri baseUri, string entitySetName)
		{
			Util.CheckArgumentNullAndEmpty(entitySetName, "entitySetName");
			return null;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003498 File Offset: 0x00001698
		internal virtual Uri BuildEntityInstanceUri(Uri baseUri, IEdmStructuredValue entityInstance)
		{
			Util.CheckArgumentNull<IEdmStructuredValue>(entityInstance, "entityInstance");
			return null;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000034A7 File Offset: 0x000016A7
		internal virtual Uri BuildStreamEditLinkUri(Uri baseUri, string streamPropertyName)
		{
			Util.CheckArgumentNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000034A7 File Offset: 0x000016A7
		internal virtual Uri BuildStreamReadLinkUri(Uri baseUri, string streamPropertyName)
		{
			Util.CheckArgumentNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000034B5 File Offset: 0x000016B5
		internal virtual Uri BuildNavigationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			Util.CheckArgumentNullAndEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000034B5 File Offset: 0x000016B5
		internal virtual Uri BuildAssociationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			Util.CheckArgumentNullAndEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000034C3 File Offset: 0x000016C3
		internal virtual Uri BuildOperationTargetUri(Uri baseUri, string operationName, string bindingParameterTypeName, string parameterNames)
		{
			Util.CheckArgumentNullAndEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000034D1 File Offset: 0x000016D1
		internal virtual Uri AppendTypeSegment(Uri baseUri, string typeName)
		{
			Util.CheckArgumentNullAndEmpty(typeName, "typeName");
			return null;
		}
	}
}
