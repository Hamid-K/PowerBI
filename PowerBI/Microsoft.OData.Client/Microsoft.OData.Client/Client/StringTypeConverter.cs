using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000078 RID: 120
	internal sealed class StringTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x0000E7CC File Offset: 0x0000C9CC
		internal override object Parse(string text)
		{
			return text;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000E935 File Offset: 0x0000CB35
		internal override string ToString(object instance)
		{
			return (string)instance;
		}
	}
}
