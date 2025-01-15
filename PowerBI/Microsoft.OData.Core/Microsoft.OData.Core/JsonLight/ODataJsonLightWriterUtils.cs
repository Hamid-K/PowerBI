using System;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000258 RID: 600
	internal static class ODataJsonLightWriterUtils
	{
		// Token: 0x06001B12 RID: 6930 RVA: 0x000533CC File Offset: 0x000515CC
		internal static void WriteValuePropertyName(this IJsonWriter jsonWriter)
		{
			jsonWriter.WriteName("value");
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x000533D9 File Offset: 0x000515D9
		internal static void WritePropertyAnnotationName(this IJsonWriter jsonWriter, string propertyName, string annotationName)
		{
			jsonWriter.WriteName(propertyName + "@" + annotationName);
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000533ED File Offset: 0x000515ED
		internal static void WriteInstanceAnnotationName(this IJsonWriter jsonWriter, string annotationName)
		{
			jsonWriter.WriteName("@" + annotationName);
		}
	}
}
