using System;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200021F RID: 543
	internal static class ODataJsonLightWriterUtils
	{
		// Token: 0x06001614 RID: 5652 RVA: 0x00043FC8 File Offset: 0x000421C8
		internal static void WriteValuePropertyName(this IJsonWriter jsonWriter)
		{
			jsonWriter.WriteName("value");
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x00043FD5 File Offset: 0x000421D5
		internal static void WritePropertyAnnotationName(this IJsonWriter jsonWriter, string propertyName, string annotationName)
		{
			jsonWriter.WriteName(propertyName + "@" + annotationName);
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00043FE9 File Offset: 0x000421E9
		internal static void WriteInstanceAnnotationName(this IJsonWriter jsonWriter, string annotationName)
		{
			jsonWriter.WriteName("@" + annotationName);
		}
	}
}
