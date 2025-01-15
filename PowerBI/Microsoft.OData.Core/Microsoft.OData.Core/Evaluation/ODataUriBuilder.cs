using System;
using System.Collections.Generic;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000268 RID: 616
	internal abstract class ODataUriBuilder
	{
		// Token: 0x06001BD5 RID: 7125 RVA: 0x0000360D File Offset: 0x0000180D
		internal virtual Uri BuildBaseUri()
		{
			return null;
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x00055A20 File Offset: 0x00053C20
		internal virtual Uri BuildEntitySetUri(Uri baseUri, string entitySetName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entitySetName, "entitySetName");
			return null;
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x00055A2E File Offset: 0x00053C2E
		internal virtual Uri BuildEntityInstanceUri(Uri baseUri, ICollection<KeyValuePair<string, object>> keyProperties, string entityTypeName)
		{
			ExceptionUtils.CheckArgumentNotNull<ICollection<KeyValuePair<string, object>>>(keyProperties, "keyProperties");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entityTypeName, "entityTypeName");
			return null;
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x00055A48 File Offset: 0x00053C48
		internal virtual Uri BuildStreamEditLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x00055A48 File Offset: 0x00053C48
		internal virtual Uri BuildStreamReadLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x00055A56 File Offset: 0x00053C56
		internal virtual Uri BuildNavigationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x00055A56 File Offset: 0x00053C56
		internal virtual Uri BuildAssociationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x00055A64 File Offset: 0x00053C64
		internal virtual Uri BuildOperationTargetUri(Uri baseUri, string operationName, string bindingParameterTypeName, string parameterNames)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x00055A72 File Offset: 0x00053C72
		internal virtual Uri AppendTypeSegment(Uri baseUri, string typeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(typeName, "typeName");
			return null;
		}
	}
}
