using System;
using System.IO;
using System.Xml;
using Microsoft.OData.Json;
using Microsoft.Spatial;

namespace Microsoft.OData
{
	// Token: 0x02000034 RID: 52
	internal sealed class GeographyTypeConverter : IPrimitiveTypeConverter
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00005286 File Offset: 0x00003486
		public void WriteAtom(object instance, XmlWriter writer)
		{
			((Geography)instance).SendTo(GmlFormatter.Create().CreateWriter(writer));
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000052A3 File Offset: 0x000034A3
		public void WriteAtom(object instance, TextWriter writer)
		{
			((Geography)instance).SendTo(WellKnownTextSqlFormatter.Create().CreateWriter(writer));
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000052C0 File Offset: 0x000034C0
		public void WriteJsonLight(object instance, IJsonWriter jsonWriter)
		{
			IGeoJsonWriter geoJsonWriter = new GeoJsonWriterAdapter(jsonWriter);
			((Geography)instance).SendTo(GeoJsonObjectFormatter.Create().CreateWriter(geoJsonWriter));
		}
	}
}
