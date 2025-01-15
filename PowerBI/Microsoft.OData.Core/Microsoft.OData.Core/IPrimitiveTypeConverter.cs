using System;
using System.IO;
using System.Xml;
using Microsoft.OData.Json;

namespace Microsoft.OData
{
	// Token: 0x02000043 RID: 67
	internal interface IPrimitiveTypeConverter
	{
		// Token: 0x0600022F RID: 559
		void WriteAtom(object instance, XmlWriter writer);

		// Token: 0x06000230 RID: 560
		void WriteAtom(object instance, TextWriter writer);

		// Token: 0x06000231 RID: 561
		void WriteJsonLight(object instance, IJsonWriter jsonWriter);
	}
}
