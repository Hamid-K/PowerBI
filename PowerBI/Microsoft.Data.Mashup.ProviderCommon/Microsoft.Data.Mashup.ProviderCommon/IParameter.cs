using System;
using System.Data;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x0200000F RID: 15
	internal interface IParameter
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004D RID: 77
		string ParameterName { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004E RID: 78
		bool TypeWasSet { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004F RID: 79
		DbType DbType { get; }
	}
}
