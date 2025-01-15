using System;
using System.Collections.Generic;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x020001FE RID: 510
	internal static class ODataAnnotationNames
	{
		// Token: 0x060013C4 RID: 5060 RVA: 0x00038814 File Offset: 0x00036A14
		internal static bool IsODataAnnotationName(string annotationName)
		{
			return annotationName.StartsWith("odata.", 4);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00038827 File Offset: 0x00036A27
		internal static bool IsUnknownODataAnnotationName(string annotationName)
		{
			return ODataAnnotationNames.IsODataAnnotationName(annotationName) && !ODataAnnotationNames.KnownODataAnnotationNames.Contains(annotationName);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00038841 File Offset: 0x00036A41
		internal static void ValidateIsCustomAnnotationName(string annotationName)
		{
			if (ODataAnnotationNames.KnownODataAnnotationNames.Contains(annotationName))
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(annotationName));
			}
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0003885C File Offset: 0x00036A5C
		internal static string RemoveAnnotationPrefix(string annotationName)
		{
			if (!string.IsNullOrEmpty(annotationName) && annotationName.get_Chars(0) == '@')
			{
				return annotationName.Substring(1);
			}
			return annotationName;
		}

		// Token: 0x040009EA RID: 2538
		internal static readonly HashSet<string> KnownODataAnnotationNames = new HashSet<string>(new string[]
		{
			"odata.context", "odata.type", "odata.id", "odata.etag", "odata.editLink", "odata.readLink", "odata.mediaEditLink", "odata.mediaReadLink", "odata.mediaContentType", "odata.mediaEtag",
			"odata.count", "odata.nextLink", "odata.bind", "odata.associationLink", "odata.navigationLink", "odata.deltaLink"
		}, StringComparer.Ordinal);

		// Token: 0x040009EB RID: 2539
		internal const string ODataContext = "odata.context";

		// Token: 0x040009EC RID: 2540
		internal const string ODataType = "odata.type";

		// Token: 0x040009ED RID: 2541
		internal const string ODataId = "odata.id";

		// Token: 0x040009EE RID: 2542
		internal const string ODataETag = "odata.etag";

		// Token: 0x040009EF RID: 2543
		internal const string ODataEditLink = "odata.editLink";

		// Token: 0x040009F0 RID: 2544
		internal const string ODataReadLink = "odata.readLink";

		// Token: 0x040009F1 RID: 2545
		internal const string ODataMediaEditLink = "odata.mediaEditLink";

		// Token: 0x040009F2 RID: 2546
		internal const string ODataMediaReadLink = "odata.mediaReadLink";

		// Token: 0x040009F3 RID: 2547
		internal const string ODataMediaContentType = "odata.mediaContentType";

		// Token: 0x040009F4 RID: 2548
		internal const string ODataMediaETag = "odata.mediaEtag";

		// Token: 0x040009F5 RID: 2549
		internal const string ODataCount = "odata.count";

		// Token: 0x040009F6 RID: 2550
		internal const string ODataNextLink = "odata.nextLink";

		// Token: 0x040009F7 RID: 2551
		internal const string ODataNavigationLinkUrl = "odata.navigationLink";

		// Token: 0x040009F8 RID: 2552
		internal const string ODataBind = "odata.bind";

		// Token: 0x040009F9 RID: 2553
		internal const string ODataAssociationLinkUrl = "odata.associationLink";

		// Token: 0x040009FA RID: 2554
		internal const string ODataDeltaLink = "odata.deltaLink";
	}
}
