using System;

namespace Microsoft.DataShaping.Processing.Utils
{
	// Token: 0x02000018 RID: 24
	internal interface IKeyGenerator
	{
		// Token: 0x060000A9 RID: 169
		string GetKey(object[] values);

		// Token: 0x060000AA RID: 170
		string GetKey(object value);
	}
}
