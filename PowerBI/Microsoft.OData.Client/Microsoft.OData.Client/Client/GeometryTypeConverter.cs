using System;
using System.Xml;
using Microsoft.Spatial;

namespace Microsoft.OData.Client
{
	// Token: 0x02000087 RID: 135
	internal sealed class GeometryTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x0000EA88 File Offset: 0x0000CC88
		internal override PrimitiveParserToken TokenizeFromXml(XmlReader reader)
		{
			reader.ReadStartElement();
			return new InstancePrimitiveParserToken<Geometry>(GmlFormatter.Create().Read<Geometry>(reader));
		}
	}
}
