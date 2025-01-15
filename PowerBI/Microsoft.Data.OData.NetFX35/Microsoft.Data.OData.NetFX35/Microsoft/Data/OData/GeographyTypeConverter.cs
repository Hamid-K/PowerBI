using System;
using System.Collections.Generic;
using System.Spatial;
using System.Xml;
using Microsoft.Data.OData.Atom;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData
{
	// Token: 0x0200021F RID: 543
	internal sealed class GeographyTypeConverter : IPrimitiveTypeConverter
	{
		// Token: 0x06001015 RID: 4117 RVA: 0x0003CDD0 File Offset: 0x0003AFD0
		public object TokenizeFromXml(XmlReader reader)
		{
			reader.ReadStartElement();
			Geography geography = GmlFormatter.Create().Read<Geography>(reader);
			reader.SkipInsignificantNodes();
			return geography;
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0003CDF6 File Offset: 0x0003AFF6
		public void WriteAtom(object instance, XmlWriter writer)
		{
			((Geography)instance).SendTo(GmlFormatter.Create().CreateWriter(writer));
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0003CE2C File Offset: 0x0003B02C
		public void WriteVerboseJson(object instance, IJsonWriter jsonWriter, string typeName, ODataVersion odataVersion)
		{
			IDictionary<string, object> dictionary = GeoJsonObjectFormatter.Create().Write((ISpatial)instance);
			jsonWriter.WriteJsonObjectValue(dictionary, delegate(IJsonWriter jw)
			{
				ODataJsonWriterUtils.WriteMetadataWithTypeName(jw, typeName);
			}, odataVersion);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0003CE6C File Offset: 0x0003B06C
		public void WriteJsonLight(object instance, IJsonWriter jsonWriter, ODataVersion odataVersion)
		{
			IDictionary<string, object> dictionary = GeoJsonObjectFormatter.Create().Write((ISpatial)instance);
			jsonWriter.WriteJsonObjectValue(dictionary, null, odataVersion);
		}
	}
}
