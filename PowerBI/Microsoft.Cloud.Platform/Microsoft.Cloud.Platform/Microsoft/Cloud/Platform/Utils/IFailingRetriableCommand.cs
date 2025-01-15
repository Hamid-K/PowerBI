using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000238 RID: 568
	public interface IFailingRetriableCommand : IRetriableCommand
	{
		// Token: 0x06000EC7 RID: 3783
		bool IsPermanentError(Exception ex);
	}
}
