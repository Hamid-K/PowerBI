using System;
using System.Xml;
using Microsoft.Spatial;

namespace Microsoft.OData.Client
{
	// Token: 0x02000086 RID: 134
	internal sealed class GeographyTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x0000EA60 File Offset: 0x0000CC60
		internal override PrimitiveParserToken TokenizeFromXml(XmlReader reader)
		{
			reader.ReadStartElement();
			return new InstancePrimitiveParserToken<Geography>(GmlFormatter.Create().Read<Geography>(reader));
		}
	}
}
