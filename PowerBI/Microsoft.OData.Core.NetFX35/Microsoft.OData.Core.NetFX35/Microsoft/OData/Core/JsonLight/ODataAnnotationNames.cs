using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000B6 RID: 182
	internal static class ODataAnnotationNames
	{
		// Token: 0x06000677 RID: 1655 RVA: 0x00016549 File Offset: 0x00014749
		internal static bool IsODataAnnotationName(string annotationName)
		{
			return annotationName.StartsWith("odata.", 4);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001655C File Offset: 0x0001475C
		internal static bool IsUnknownODataAnnotationName(string annotationName)
		{
			return ODataAnnotationNames.IsODataAnnotationName(annotationName) && !ODataAnnotationNames.KnownODataAnnotationNames.Contains(annotationName);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00016576 File Offset: 0x00014776
		internal static void ValidateIsCustomAnnotationName(string annotationName)
		{
			if (ODataAnnotationNames.KnownODataAnnotationNames.Contains(annotationName))
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(annotationName));
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00016591 File Offset: 0x00014791
		internal static string RemoveAnnotationPrefix(string annotationName)
		{
			if (!string.IsNullOrEmpty(annotationName) && annotationName.get_Chars(0) == '@')
			{
				return annotationName.Substring(1);
			}
			return annotationName;
		}

		// Token: 0x040002FF RID: 767
		internal const string ODataContext = "odata.context";

		// Token: 0x04000300 RID: 768
		internal const string ODataNull = "odata.null";

		// Token: 0x04000301 RID: 769
		internal const string ODataType = "odata.type";

		// Token: 0x04000302 RID: 770
		internal const string ODataId = "odata.id";

		// Token: 0x04000303 RID: 771
		internal const string ODataETag = "odata.etag";

		// Token: 0x04000304 RID: 772
		internal const string ODataEditLink = "odata.editLink";

		// Token: 0x04000305 RID: 773
		internal const string ODataReadLink = "odata.readLink";

		// Token: 0x04000306 RID: 774
		internal const string ODataMediaEditLink = "odata.mediaEditLink";

		// Token: 0x04000307 RID: 775
		internal const string ODataMediaReadLink = "odata.mediaReadLink";

		// Token: 0x04000308 RID: 776
		internal const string ODataMediaContentType = "odata.mediaContentType";

		// Token: 0x04000309 RID: 777
		internal const string ODataMediaETag = "odata.mediaEtag";

		// Token: 0x0400030A RID: 778
		internal const string ODataCount = "odata.count";

		// Token: 0x0400030B RID: 779
		internal const string ODataNextLink = "odata.nextLink";

		// Token: 0x0400030C RID: 780
		internal const string ODataNavigationLinkUrl = "odata.navigationLink";

		// Token: 0x0400030D RID: 781
		internal const string ODataBind = "odata.bind";

		// Token: 0x0400030E RID: 782
		internal const string ODataAssociationLinkUrl = "odata.associationLink";

		// Token: 0x0400030F RID: 783
		internal const string ODataDeltaLink = "odata.deltaLink";

		// Token: 0x04000310 RID: 784
		internal static readonly HashSet<string> KnownODataAnnotationNames = new HashSet<string>(new string[]
		{
			"odata.context", "odata.null", "odata.type", "odata.id", "odata.etag", "odata.editLink", "odata.readLink", "odata.mediaEditLink", "odata.mediaReadLink", "odata.mediaContentType",
			"odata.mediaEtag", "odata.count", "odata.nextLink", "odata.bind", "odata.associationLink", "odata.navigationLink", "odata.deltaLink"
		}, StringComparer.Ordinal);
	}
}
