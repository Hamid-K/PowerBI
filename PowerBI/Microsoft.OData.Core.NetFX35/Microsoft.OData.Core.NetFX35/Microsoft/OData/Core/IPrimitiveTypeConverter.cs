using System;
using System.IO;
using System.Xml;
using Microsoft.OData.Core.Json;

namespace Microsoft.OData.Core
{
	// Token: 0x02000092 RID: 146
	internal interface IPrimitiveTypeConverter
	{
		// Token: 0x060005B5 RID: 1461
		object TokenizeFromXml(XmlReader reader);

		// Token: 0x060005B6 RID: 1462
		void WriteAtom(object instance, XmlWriter writer);

		// Token: 0x060005B7 RID: 1463
		void WriteAtom(object instance, TextWriter writer);

		// Token: 0x060005B8 RID: 1464
		void WriteJsonLight(object instance, IJsonWriter jsonWriter);
	}
}
