using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Client
{
	// Token: 0x0200000A RID: 10
	internal abstract class ODataResourceMetadataBuilder
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003422 File Offset: 0x00001622
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000342A File Offset: 0x0000162A
		internal ODataResourceMetadataBuilder ParentMetadataBuilder { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003433 File Offset: 0x00001633
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000343B File Offset: 0x0000163B
		internal bool IsFromCollection { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003444 File Offset: 0x00001644
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000344C File Offset: 0x0000164C
		internal string NameAsProperty { get; set; }

		// Token: 0x0600003A RID: 58
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal abstract Uri GetEditLink();

		// Token: 0x0600003B RID: 59
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal abstract Uri GetReadLink();

		// Token: 0x0600003C RID: 60
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal abstract Uri GetId();

		// Token: 0x0600003D RID: 61
		internal abstract bool TryGetIdForSerialization(out Uri id);

		// Token: 0x0600003E RID: 62
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal abstract string GetETag();

		// Token: 0x0600003F RID: 63 RVA: 0x00003455 File Offset: 0x00001655
		internal virtual Uri GetStreamEditLink(string streamPropertyName)
		{
			Util.CheckArgumentNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003455 File Offset: 0x00001655
		internal virtual Uri GetStreamReadLink(string streamPropertyName)
		{
			Util.CheckArgumentNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003463 File Offset: 0x00001663
		internal virtual Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNestedResourceInfoUrl)
		{
			Util.CheckArgumentNullAndEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003463 File Offset: 0x00001663
		internal virtual Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			Util.CheckArgumentNullAndEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003471 File Offset: 0x00001671
		internal virtual Uri GetOperationTargetUri(string operationName, string bindingParameterTypeName, string parameterNames)
		{
			Util.CheckArgumentNullAndEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003471 File Offset: 0x00001671
		internal virtual string GetOperationTitle(string operationName)
		{
			Util.CheckArgumentNullAndEmpty(operationName, "operationName");
			return null;
		}
	}
}
