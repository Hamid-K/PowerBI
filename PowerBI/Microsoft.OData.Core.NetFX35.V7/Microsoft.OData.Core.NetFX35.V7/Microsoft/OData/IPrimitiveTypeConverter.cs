using System;
using System.IO;
using System.Xml;
using Microsoft.OData.Json;

namespace Microsoft.OData
{
	// Token: 0x0200001A RID: 26
	internal interface IPrimitiveTypeConverter
	{
		// Token: 0x060000A6 RID: 166
		void WriteAtom(object instance, XmlWriter writer);

		// Token: 0x060000A7 RID: 167
		void WriteAtom(object instance, TextWriter writer);

		// Token: 0x060000A8 RID: 168
		void WriteJsonLight(object instance, IJsonWriter jsonWriter);
	}
}
