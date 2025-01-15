using System;
using System.Collections.Generic;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000237 RID: 567
	internal static class ODataAnnotationNames
	{
		// Token: 0x06001892 RID: 6290 RVA: 0x000465D8 File Offset: 0x000447D8
		internal static bool IsODataAnnotationName(string annotationName)
		{
			return annotationName.StartsWith("odata.", StringComparison.Ordinal);
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x000465EB File Offset: 0x000447EB
		internal static bool IsUnknownODataAnnotationName(string annotationName)
		{
			return ODataAnnotationNames.IsODataAnnotationName(annotationName) && !ODataAnnotationNames.KnownODataAnnotationNames.Contains(annotationName);
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x00046605 File Offset: 0x00044805
		internal static void ValidateIsCustomAnnotationName(string annotationName)
		{
			if (ODataAnnotationNames.KnownODataAnnotationNames.Contains(annotationName))
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(annotationName));
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00046620 File Offset: 0x00044820
		internal static string RemoveAnnotationPrefix(string annotationName)
		{
			if (!string.IsNullOrEmpty(annotationName) && annotationName[0] == '@')
			{
				return annotationName.Substring(1);
			}
			return annotationName;
		}

		// Token: 0x04000B05 RID: 2821
		internal static readonly HashSet<string> KnownODataAnnotationNames = new HashSet<string>(new string[]
		{
			"odata.context", "odata.type", "odata.id", "odata.etag", "odata.editLink", "odata.readLink", "odata.mediaEditLink", "odata.mediaReadLink", "odata.mediaContentType", "odata.mediaEtag",
			"odata.count", "odata.nextLink", "odata.bind", "odata.associationLink", "odata.navigationLink", "odata.deltaLink", "odata.removed", "odata.delta", "odata.null"
		}, StringComparer.Ordinal);

		// Token: 0x04000B06 RID: 2822
		internal const string ODataContext = "odata.context";

		// Token: 0x04000B07 RID: 2823
		internal const string ODataType = "odata.type";

		// Token: 0x04000B08 RID: 2824
		internal const string ODataId = "odata.id";

		// Token: 0x04000B09 RID: 2825
		internal const string ODataETag = "odata.etag";

		// Token: 0x04000B0A RID: 2826
		internal const string ODataEditLink = "odata.editLink";

		// Token: 0x04000B0B RID: 2827
		internal const string ODataReadLink = "odata.readLink";

		// Token: 0x04000B0C RID: 2828
		internal const string ODataMediaEditLink = "odata.mediaEditLink";

		// Token: 0x04000B0D RID: 2829
		internal const string ODataMediaReadLink = "odata.mediaReadLink";

		// Token: 0x04000B0E RID: 2830
		internal const string ODataMediaContentType = "odata.mediaContentType";

		// Token: 0x04000B0F RID: 2831
		internal const string ODataMediaETag = "odata.mediaEtag";

		// Token: 0x04000B10 RID: 2832
		internal const string ODataCount = "odata.count";

		// Token: 0x04000B11 RID: 2833
		internal const string ODataNextLink = "odata.nextLink";

		// Token: 0x04000B12 RID: 2834
		internal const string ODataNavigationLinkUrl = "odata.navigationLink";

		// Token: 0x04000B13 RID: 2835
		internal const string ODataBind = "odata.bind";

		// Token: 0x04000B14 RID: 2836
		internal const string ODataAssociationLinkUrl = "odata.associationLink";

		// Token: 0x04000B15 RID: 2837
		internal const string ODataDeltaLink = "odata.deltaLink";

		// Token: 0x04000B16 RID: 2838
		internal const string ODataRemoved = "odata.removed";

		// Token: 0x04000B17 RID: 2839
		internal const string ODataDelta = "odata.delta";

		// Token: 0x04000B18 RID: 2840
		internal const string ODataNull = "odata.null";
	}
}
