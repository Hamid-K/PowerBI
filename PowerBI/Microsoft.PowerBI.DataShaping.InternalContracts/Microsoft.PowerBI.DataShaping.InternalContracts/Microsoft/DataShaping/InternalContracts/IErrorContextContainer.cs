using System;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x0200001B RID: 27
	internal interface IErrorContextContainer
	{
		// Token: 0x06000061 RID: 97
		bool HasMessages();

		// Token: 0x06000062 RID: 98
		string SummarizeMessages();
	}
}
