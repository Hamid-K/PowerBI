using System;
using Microsoft.OData.Core.Json;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000B1 RID: 177
	internal sealed class JsonLightODataAnnotationWriter
	{
		// Token: 0x06000660 RID: 1632 RVA: 0x0001636C File Offset: 0x0001456C
		public JsonLightODataAnnotationWriter(IJsonWriter jsonWriter, bool odataSimplified)
		{
			this.jsonWriter = jsonWriter;
			this.odataSimplified = odataSimplified;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00016382 File Offset: 0x00014582
		public void WriteODataTypeInstanceAnnotation(string typeName)
		{
			this.WriteInstanceAnnotationName("odata.type");
			this.jsonWriter.WriteValue(JsonLightODataAnnotationWriter.PrefixTypeName(WriterUtils.RemoveEdmPrefixFromTypeName(typeName)));
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000163A5 File Offset: 0x000145A5
		public void WriteODataTypePropertyAnnotation(string propertyName, string typeName)
		{
			this.WritePropertyAnnotationName(propertyName, "odata.type");
			this.jsonWriter.WriteValue(JsonLightODataAnnotationWriter.PrefixTypeName(WriterUtils.RemoveEdmPrefixFromTypeName(typeName)));
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000163C9 File Offset: 0x000145C9
		public void WritePropertyAnnotationName(string propertyName, string annotationName)
		{
			this.jsonWriter.WritePropertyAnnotationName(propertyName, this.SimplifyODataAnnotationName(annotationName));
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000163DE File Offset: 0x000145DE
		public void WriteInstanceAnnotationName(string annotationName)
		{
			this.jsonWriter.WriteInstanceAnnotationName(this.SimplifyODataAnnotationName(annotationName));
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000163F2 File Offset: 0x000145F2
		private static string PrefixTypeName(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				return typeName;
			}
			return "#" + typeName;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00016409 File Offset: 0x00014609
		private string SimplifyODataAnnotationName(string annotationName)
		{
			if (!this.odataSimplified)
			{
				return annotationName;
			}
			return annotationName.Substring(JsonLightODataAnnotationWriter.ODataAnnotationPrefixLength);
		}

		// Token: 0x040002FC RID: 764
		private static readonly int ODataAnnotationPrefixLength = "odata.".Length;

		// Token: 0x040002FD RID: 765
		private readonly IJsonWriter jsonWriter;

		// Token: 0x040002FE RID: 766
		private readonly bool odataSimplified;
	}
}
