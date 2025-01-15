using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azure.Core.GeoJson
{
	// Token: 0x020000B2 RID: 178
	[NullableContext(2)]
	[Nullable(0)]
	[JsonConverter(typeof(GeoJsonConverter))]
	public abstract class GeoObject
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0001194A File Offset: 0x0000FB4A
		[Nullable(new byte[] { 1, 1, 2 })]
		internal IReadOnlyDictionary<string, object> CustomProperties
		{
			[return: Nullable(new byte[] { 1, 1, 2 })]
			get;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00011952 File Offset: 0x0000FB52
		internal GeoObject(GeoBoundingBox boundingBox, [Nullable(new byte[] { 1, 1, 2 })] IReadOnlyDictionary<string, object> customProperties)
		{
			Argument.AssertNotNull<IReadOnlyDictionary<string, object>>(customProperties, "customProperties");
			this.BoundingBox = boundingBox;
			this.CustomProperties = customProperties;
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600058E RID: 1422
		public abstract GeoObjectType Type { get; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00011973 File Offset: 0x0000FB73
		public GeoBoundingBox BoundingBox { get; }

		// Token: 0x06000590 RID: 1424 RVA: 0x0001197B File Offset: 0x0000FB7B
		[NullableContext(1)]
		public bool TryGetCustomProperty(string name, [Nullable(2)] out object value)
		{
			return this.CustomProperties.TryGetValue(name, out value);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001198C File Offset: 0x0000FB8C
		[NullableContext(1)]
		public override string ToString()
		{
			string @string;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, default(JsonWriterOptions)))
				{
					GeoJsonConverter.Write(utf8JsonWriter, this);
					utf8JsonWriter.Flush();
					@string = Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
			return @string;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00011A00 File Offset: 0x0000FC00
		[NullableContext(1)]
		public static GeoObject Parse(string json)
		{
			GeoObject geoObject;
			using (JsonDocument jsonDocument = JsonDocument.Parse(json, default(JsonDocumentOptions)))
			{
				geoObject = GeoJsonConverter.Read(jsonDocument.RootElement);
			}
			return geoObject;
		}

		// Token: 0x04000247 RID: 583
		[Nullable(new byte[] { 1, 1, 2 })]
		internal static readonly IReadOnlyDictionary<string, object> DefaultProperties = new ReadOnlyDictionary<string, object>(new Dictionary<string, object>());
	}
}
