using System;
using System.Threading.Tasks;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000239 RID: 569
	public interface IFailingRetriableCommandV2 : IRetriableCommand
	{
		// Token: 0x06000EC8 RID: 3784
		Task<bool> IsPermanentError(Exception ex, RetrierContext retryContext);
	}
}
