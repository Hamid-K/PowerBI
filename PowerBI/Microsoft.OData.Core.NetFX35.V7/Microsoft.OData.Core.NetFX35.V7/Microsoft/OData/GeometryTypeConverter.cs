using System;
using System.IO;
using System.Xml;
using Microsoft.OData.Json;
using Microsoft.Spatial;

namespace Microsoft.OData
{
	// Token: 0x0200000E RID: 14
	internal sealed class GeometryTypeConverter : IPrimitiveTypeConverter
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002DD7 File Offset: 0x00000FD7
		public void WriteAtom(object instance, XmlWriter writer)
		{
			((Geometry)instance).SendTo(GmlFormatter.Create().CreateWriter(writer));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public void WriteAtom(object instance, TextWriter writer)
		{
			((Geometry)instance).SendTo(WellKnownTextSqlFormatter.Create().CreateWriter(writer));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002E14 File Offset: 0x00001014
		public void WriteJsonLight(object instance, IJsonWriter jsonWriter)
		{
			IGeoJsonWriter geoJsonWriter = new GeoJsonWriterAdapter(jsonWriter);
			((Geometry)instance).SendTo(GeoJsonObjectFormatter.Create().CreateWriter(geoJsonWriter));
		}
	}
}
