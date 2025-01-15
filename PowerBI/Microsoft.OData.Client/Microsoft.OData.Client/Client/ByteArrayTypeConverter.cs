using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200006F RID: 111
	internal sealed class ByteArrayTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x0000E80B File Offset: 0x0000CA0B
		internal override object Parse(string text)
		{
			return Convert.FromBase64String(text);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000E813 File Offset: 0x0000CA13
		internal override string ToString(object instance)
		{
			return Convert.ToBase64String((byte[])instance);
		}
	}
}
