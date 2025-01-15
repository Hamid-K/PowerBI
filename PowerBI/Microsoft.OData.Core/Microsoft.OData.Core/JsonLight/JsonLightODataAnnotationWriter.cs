using System;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000231 RID: 561
	internal sealed class JsonLightODataAnnotationWriter
	{
		// Token: 0x06001875 RID: 6261 RVA: 0x00046314 File Offset: 0x00044514
		public JsonLightODataAnnotationWriter(IJsonWriter jsonWriter, bool enableWritingODataAnnotationWithoutPrefix, ODataVersion? odataVersion)
		{
			this.jsonWriter = jsonWriter;
			this.enableWritingODataAnnotationWithoutPrefix = enableWritingODataAnnotationWithoutPrefix;
			this.odataVersion = odataVersion ?? ODataVersion.V4;
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x00046350 File Offset: 0x00044550
		public void WriteODataTypeInstanceAnnotation(string typeName, bool writeRawValue = false)
		{
			this.WriteInstanceAnnotationName("odata.type");
			if (writeRawValue)
			{
				this.jsonWriter.WriteValue(typeName);
				return;
			}
			this.jsonWriter.WriteValue(WriterUtils.PrefixTypeNameForWriting(typeName, this.odataVersion));
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00046384 File Offset: 0x00044584
		public void WriteODataTypePropertyAnnotation(string propertyName, string typeName)
		{
			this.WritePropertyAnnotationName(propertyName, "odata.type");
			this.jsonWriter.WriteValue(WriterUtils.PrefixTypeNameForWriting(typeName, this.odataVersion));
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x000463A9 File Offset: 0x000445A9
		public void WritePropertyAnnotationName(string propertyName, string annotationName)
		{
			this.jsonWriter.WritePropertyAnnotationName(propertyName, this.SimplifyODataAnnotationName(annotationName));
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x000463BE File Offset: 0x000445BE
		public void WriteInstanceAnnotationName(string annotationName)
		{
			this.jsonWriter.WriteInstanceAnnotationName(this.SimplifyODataAnnotationName(annotationName));
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x000463D2 File Offset: 0x000445D2
		private string SimplifyODataAnnotationName(string annotationName)
		{
			if (!this.enableWritingODataAnnotationWithoutPrefix)
			{
				return annotationName;
			}
			return annotationName.Substring(JsonLightODataAnnotationWriter.ODataAnnotationPrefixLength);
		}

		// Token: 0x04000B01 RID: 2817
		private static readonly int ODataAnnotationPrefixLength = "odata.".Length;

		// Token: 0x04000B02 RID: 2818
		private readonly IJsonWriter jsonWriter;

		// Token: 0x04000B03 RID: 2819
		private readonly bool enableWritingODataAnnotationWithoutPrefix;

		// Token: 0x04000B04 RID: 2820
		private readonly ODataVersion odataVersion;
	}
}
