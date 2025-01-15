using System;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x020001F8 RID: 504
	internal sealed class JsonLightODataAnnotationWriter
	{
		// Token: 0x060013A4 RID: 5028 RVA: 0x00038561 File Offset: 0x00036761
		public JsonLightODataAnnotationWriter(IJsonWriter jsonWriter, bool enableWritingODataAnnotationWithoutPrefix)
		{
			this.jsonWriter = jsonWriter;
			this.enableWritingODataAnnotationWithoutPrefix = enableWritingODataAnnotationWithoutPrefix;
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00038577 File Offset: 0x00036777
		public void WriteODataTypeInstanceAnnotation(string typeName, bool writeRawValue = false)
		{
			this.WriteInstanceAnnotationName("odata.type");
			if (writeRawValue)
			{
				this.jsonWriter.WriteValue(typeName);
				return;
			}
			this.jsonWriter.WriteValue(JsonLightODataAnnotationWriter.PrefixTypeName(WriterUtils.RemoveEdmPrefixFromTypeName(typeName)));
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x000385AA File Offset: 0x000367AA
		public void WriteODataTypePropertyAnnotation(string propertyName, string typeName)
		{
			this.WritePropertyAnnotationName(propertyName, "odata.type");
			this.jsonWriter.WriteValue(JsonLightODataAnnotationWriter.PrefixTypeName(WriterUtils.RemoveEdmPrefixFromTypeName(typeName)));
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000385CE File Offset: 0x000367CE
		public void WritePropertyAnnotationName(string propertyName, string annotationName)
		{
			this.jsonWriter.WritePropertyAnnotationName(propertyName, this.SimplifyODataAnnotationName(annotationName));
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x000385E3 File Offset: 0x000367E3
		public void WriteInstanceAnnotationName(string annotationName)
		{
			this.jsonWriter.WriteInstanceAnnotationName(this.SimplifyODataAnnotationName(annotationName));
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x000385F7 File Offset: 0x000367F7
		private static string PrefixTypeName(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				return typeName;
			}
			return "#" + typeName;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0003860E File Offset: 0x0003680E
		private string SimplifyODataAnnotationName(string annotationName)
		{
			if (!this.enableWritingODataAnnotationWithoutPrefix)
			{
				return annotationName;
			}
			return annotationName.Substring(JsonLightODataAnnotationWriter.ODataAnnotationPrefixLength);
		}

		// Token: 0x040009E7 RID: 2535
		private static readonly int ODataAnnotationPrefixLength = "odata.".Length;

		// Token: 0x040009E8 RID: 2536
		private readonly IJsonWriter jsonWriter;

		// Token: 0x040009E9 RID: 2537
		private readonly bool enableWritingODataAnnotationWithoutPrefix;
	}
}
