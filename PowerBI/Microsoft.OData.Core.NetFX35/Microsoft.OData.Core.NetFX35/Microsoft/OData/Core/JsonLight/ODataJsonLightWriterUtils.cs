using System;
using Microsoft.OData.Core.Json;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x02000104 RID: 260
	internal static class ODataJsonLightWriterUtils
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x00023E06 File Offset: 0x00022006
		internal static void WriteValuePropertyName(this IJsonWriter jsonWriter)
		{
			jsonWriter.WriteName("value");
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00023E13 File Offset: 0x00022013
		internal static void WritePropertyAnnotationName(this IJsonWriter jsonWriter, string propertyName, string annotationName)
		{
			jsonWriter.WriteName(propertyName + '@' + annotationName);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00023E29 File Offset: 0x00022029
		internal static void WriteInstanceAnnotationName(this IJsonWriter jsonWriter, string annotationName)
		{
			jsonWriter.WriteName('@' + annotationName);
		}
	}
}
