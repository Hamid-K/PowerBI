using System;
using System.IO;
using System.Xml;
using Microsoft.OData.Json;
using Microsoft.Spatial;

namespace Microsoft.OData
{
	// Token: 0x02000035 RID: 53
	internal sealed class GeometryTypeConverter : IPrimitiveTypeConverter
	{
		// Token: 0x060001DB RID: 475 RVA: 0x000052EF File Offset: 0x000034EF
		public void WriteAtom(object instance, XmlWriter writer)
		{
			((Geometry)instance).SendTo(GmlFormatter.Create().CreateWriter(writer));
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000530C File Offset: 0x0000350C
		public void WriteAtom(object instance, TextWriter writer)
		{
			((Geometry)instance).SendTo(WellKnownTextSqlFormatter.Create().CreateWriter(writer));
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000532C File Offset: 0x0000352C
		public void WriteJsonLight(object instance, IJsonWriter jsonWriter)
		{
			IGeoJsonWriter geoJsonWriter = new GeoJsonWriterAdapter(jsonWriter);
			((Geometry)instance).SendTo(GeoJsonObjectFormatter.Create().CreateWriter(geoJsonWriter));
		}
	}
}
