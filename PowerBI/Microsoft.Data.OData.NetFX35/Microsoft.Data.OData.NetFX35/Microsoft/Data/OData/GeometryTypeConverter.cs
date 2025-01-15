using System;
using System.Collections.Generic;
using System.Spatial;
using System.Xml;
using Microsoft.Data.OData.Atom;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData
{
	// Token: 0x020001E0 RID: 480
	internal sealed class GeometryTypeConverter : IPrimitiveTypeConverter
	{
		// Token: 0x06000E06 RID: 3590 RVA: 0x00031FD0 File Offset: 0x000301D0
		public object TokenizeFromXml(XmlReader reader)
		{
			reader.ReadStartElement();
			Geometry geometry = GmlFormatter.Create().Read<Geometry>(reader);
			reader.SkipInsignificantNodes();
			return geometry;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00031FF6 File Offset: 0x000301F6
		public void WriteAtom(object instance, XmlWriter writer)
		{
			((Geometry)instance).SendTo(GmlFormatter.Create().CreateWriter(writer));
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003202C File Offset: 0x0003022C
		public void WriteVerboseJson(object instance, IJsonWriter jsonWriter, string typeName, ODataVersion odataVersion)
		{
			IDictionary<string, object> dictionary = GeoJsonObjectFormatter.Create().Write((ISpatial)instance);
			jsonWriter.WriteJsonObjectValue(dictionary, delegate(IJsonWriter jw)
			{
				ODataJsonWriterUtils.WriteMetadataWithTypeName(jw, typeName);
			}, odataVersion);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0003206C File Offset: 0x0003026C
		public void WriteJsonLight(object instance, IJsonWriter jsonWriter, ODataVersion odataVersion)
		{
			IDictionary<string, object> dictionary = GeoJsonObjectFormatter.Create().Write((ISpatial)instance);
			jsonWriter.WriteJsonObjectValue(dictionary, null, odataVersion);
		}
	}
}
