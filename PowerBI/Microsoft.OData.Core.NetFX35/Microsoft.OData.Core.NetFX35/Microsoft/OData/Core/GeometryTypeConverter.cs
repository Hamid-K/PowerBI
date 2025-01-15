using System;
using System.IO;
using System.Xml;
using Microsoft.Data.Spatial;
using Microsoft.OData.Core.Atom;
using Microsoft.OData.Core.Json;
using Microsoft.Spatial;

namespace Microsoft.OData.Core
{
	// Token: 0x02000094 RID: 148
	internal sealed class GeometryTypeConverter : IPrimitiveTypeConverter
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x00014D40 File Offset: 0x00012F40
		public object TokenizeFromXml(XmlReader reader)
		{
			reader.ReadStartElement();
			Geometry geometry = GmlFormatter.Create().Read<Geometry>(reader);
			reader.SkipInsignificantNodes();
			return geometry;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00014D66 File Offset: 0x00012F66
		public void WriteAtom(object instance, XmlWriter writer)
		{
			((Geometry)instance).SendTo(GmlFormatter.Create().CreateWriter(writer));
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00014D83 File Offset: 0x00012F83
		public void WriteAtom(object instance, TextWriter writer)
		{
			((Geometry)instance).SendTo(WellKnownTextSqlFormatter.Create().CreateWriter(writer));
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00014DA0 File Offset: 0x00012FA0
		public void WriteJsonLight(object instance, IJsonWriter jsonWriter)
		{
			IGeoJsonWriter geoJsonWriter = new GeoJsonWriterAdapter(jsonWriter);
			((Geometry)instance).SendTo(GeoJsonObjectFormatter.Create().CreateWriter(geoJsonWriter));
		}
	}
}
