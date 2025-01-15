using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000073 RID: 115
	internal sealed class GuidTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		internal override object Parse(string text)
		{
			return new Guid(text);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000E84A File Offset: 0x0000CA4A
		internal override string ToString(object instance)
		{
			return instance.ToString();
		}
	}
}
