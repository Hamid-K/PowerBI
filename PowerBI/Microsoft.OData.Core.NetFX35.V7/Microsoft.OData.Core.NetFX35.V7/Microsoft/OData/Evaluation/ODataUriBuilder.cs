using System;
using System.Collections.Generic;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x0200022D RID: 557
	internal abstract class ODataUriBuilder
	{
		// Token: 0x060016AB RID: 5803 RVA: 0x0000B41B File Offset: 0x0000961B
		internal virtual Uri BuildBaseUri()
		{
			return null;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00045A68 File Offset: 0x00043C68
		internal virtual Uri BuildEntitySetUri(Uri baseUri, string entitySetName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entitySetName, "entitySetName");
			return null;
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00045A76 File Offset: 0x00043C76
		internal virtual Uri BuildEntityInstanceUri(Uri baseUri, ICollection<KeyValuePair<string, object>> keyProperties, string entityTypeName)
		{
			ExceptionUtils.CheckArgumentNotNull<ICollection<KeyValuePair<string, object>>>(keyProperties, "keyProperties");
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entityTypeName, "entityTypeName");
			return null;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00045A90 File Offset: 0x00043C90
		internal virtual Uri BuildStreamEditLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00045A90 File Offset: 0x00043C90
		internal virtual Uri BuildStreamReadLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00045A9E File Offset: 0x00043C9E
		internal virtual Uri BuildNavigationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00045A9E File Offset: 0x00043C9E
		internal virtual Uri BuildAssociationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00045AAC File Offset: 0x00043CAC
		internal virtual Uri BuildOperationTargetUri(Uri baseUri, string operationName, string bindingParameterTypeName, string parameterNames)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00045ABA File Offset: 0x00043CBA
		internal virtual Uri AppendTypeSegment(Uri baseUri, string typeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(typeName, "typeName");
			return null;
		}
	}
}
