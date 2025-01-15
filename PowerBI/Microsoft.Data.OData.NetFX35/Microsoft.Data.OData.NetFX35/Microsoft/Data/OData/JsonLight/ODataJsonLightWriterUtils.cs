using System;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x020001B1 RID: 433
	internal static class ODataJsonLightWriterUtils
	{
		// Token: 0x06000CBD RID: 3261 RVA: 0x0002C7C0 File Offset: 0x0002A9C0
		internal static void WriteODataTypeInstanceAnnotation(IJsonWriter jsonWriter, string typeName)
		{
			jsonWriter.WriteName("odata.type");
			jsonWriter.WriteValue(typeName);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0002C7D4 File Offset: 0x0002A9D4
		internal static void WriteODataTypePropertyAnnotation(IJsonWriter jsonWriter, string propertyName, string typeName)
		{
			jsonWriter.WritePropertyAnnotationName(propertyName, "odata.type");
			jsonWriter.WriteValue(typeName);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002C7E9 File Offset: 0x0002A9E9
		internal static void WriteValuePropertyName(this IJsonWriter jsonWriter)
		{
			jsonWriter.WriteName("value");
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002C7F6 File Offset: 0x0002A9F6
		internal static void WritePropertyAnnotationName(this IJsonWriter jsonWriter, string propertyName, string annotationName)
		{
			jsonWriter.WriteName(propertyName + '@' + annotationName);
		}
	}
}
