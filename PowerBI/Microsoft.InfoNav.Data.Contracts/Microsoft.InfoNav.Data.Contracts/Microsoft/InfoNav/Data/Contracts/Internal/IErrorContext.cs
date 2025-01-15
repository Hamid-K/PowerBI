using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001F2 RID: 498
	public interface IErrorContext
	{
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000D8C RID: 3468
		bool HasError { get; }

		// Token: 0x06000D8D RID: 3469
		void RegisterError(string messageTemplate, params object[] args);

		// Token: 0x06000D8E RID: 3470
		void RegisterWarning(string messageTemplate, params object[] args);
	}
}
