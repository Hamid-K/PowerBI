using System;
using System.IO;
using System.Xml;
using Microsoft.Data.Spatial;
using Microsoft.OData.Core.Atom;
using Microsoft.OData.Core.Json;
using Microsoft.Spatial;

namespace Microsoft.OData.Core
{
	// Token: 0x02000093 RID: 147
	internal sealed class GeographyTypeConverter : IPrimitiveTypeConverter
	{
		// Token: 0x060005B9 RID: 1465 RVA: 0x00014CA8 File Offset: 0x00012EA8
		public object TokenizeFromXml(XmlReader reader)
		{
			reader.ReadStartElement();
			Geography geography = GmlFormatter.Create().Read<Geography>(reader);
			reader.SkipInsignificantNodes();
			return geography;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00014CCE File Offset: 0x00012ECE
		public void WriteAtom(object instance, XmlWriter writer)
		{
			((Geography)instance).SendTo(GmlFormatter.Create().CreateWriter(writer));
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00014CEB File Offset: 0x00012EEB
		public void WriteAtom(object instance, TextWriter writer)
		{
			((Geography)instance).SendTo(WellKnownTextSqlFormatter.Create().CreateWriter(writer));
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00014D08 File Offset: 0x00012F08
		public void WriteJsonLight(object instance, IJsonWriter jsonWriter)
		{
			IGeoJsonWriter geoJsonWriter = new GeoJsonWriterAdapter(jsonWriter);
			((Geography)instance).SendTo(GeoJsonObjectFormatter.Create().CreateWriter(geoJsonWriter));
		}
	}
}
