using System;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x02000083 RID: 131
	internal sealed class UInt16TypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x0000EA11 File Offset: 0x0000CC11
		internal override object Parse(string text)
		{
			return XmlConvert.ToUInt16(text);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000EA1E File Offset: 0x0000CC1E
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((ushort)instance);
		}
	}
}
