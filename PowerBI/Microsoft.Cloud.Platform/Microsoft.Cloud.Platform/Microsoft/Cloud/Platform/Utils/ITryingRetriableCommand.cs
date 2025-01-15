using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200023A RID: 570
	public interface ITryingRetriableCommand : IRetriableCommand
	{
		// Token: 0x06000EC9 RID: 3785
		bool IsRetryRequired();
	}
}
