using System;
using System.Xml;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData
{
	// Token: 0x020001DF RID: 479
	internal interface IPrimitiveTypeConverter
	{
		// Token: 0x06000E02 RID: 3586
		object TokenizeFromXml(XmlReader reader);

		// Token: 0x06000E03 RID: 3587
		void WriteAtom(object instance, XmlWriter writer);

		// Token: 0x06000E04 RID: 3588
		void WriteVerboseJson(object instance, IJsonWriter jsonWriter, string typeName, ODataVersion odataVersion);

		// Token: 0x06000E05 RID: 3589
		void WriteJsonLight(object instance, IJsonWriter jsonWriter, ODataVersion odataVersion);
	}
}
