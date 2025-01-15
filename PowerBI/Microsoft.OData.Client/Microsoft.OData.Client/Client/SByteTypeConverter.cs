using System;
using System.Xml;

namespace Microsoft.OData.Client
{
	// Token: 0x02000079 RID: 121
	internal sealed class SByteTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x0000E93D File Offset: 0x0000CB3D
		internal override object Parse(string text)
		{
			return XmlConvert.ToSByte(text);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000E94A File Offset: 0x0000CB4A
		internal override string ToString(object instance)
		{
			return XmlConvert.ToString((sbyte)instance);
		}
	}
}
