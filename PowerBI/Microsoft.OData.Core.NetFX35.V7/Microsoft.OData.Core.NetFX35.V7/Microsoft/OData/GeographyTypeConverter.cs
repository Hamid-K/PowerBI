using System;
using System.IO;
using System.Xml;
using Microsoft.OData.Json;
using Microsoft.Spatial;

namespace Microsoft.OData
{
	// Token: 0x0200000D RID: 13
	internal sealed class GeographyTypeConverter : IPrimitiveTypeConverter
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002D6B File Offset: 0x00000F6B
		public void WriteAtom(object instance, XmlWriter writer)
		{
			((Geography)instance).SendTo(GmlFormatter.Create().CreateWriter(writer));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D88 File Offset: 0x00000F88
		public void WriteAtom(object instance, TextWriter writer)
		{
			((Geography)instance).SendTo(WellKnownTextSqlFormatter.Create().CreateWriter(writer));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public void WriteJsonLight(object instance, IJsonWriter jsonWriter)
		{
			IGeoJsonWriter geoJsonWriter = new GeoJsonWriterAdapter(jsonWriter);
			((Geography)instance).SendTo(GeoJsonObjectFormatter.Create().CreateWriter(geoJsonWriter));
		}
	}
}
