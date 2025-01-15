using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200007B RID: 123
	internal sealed class CharArrayTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x0000E971 File Offset: 0x0000CB71
		internal override object Parse(string text)
		{
			return text.ToCharArray();
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000E979 File Offset: 0x0000CB79
		internal override string ToString(object instance)
		{
			return new string((char[])instance);
		}
	}
}
