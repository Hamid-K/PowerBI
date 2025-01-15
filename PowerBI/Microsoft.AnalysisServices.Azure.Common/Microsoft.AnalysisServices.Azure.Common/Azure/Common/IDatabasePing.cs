using System;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.Storage;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000041 RID: 65
	public interface IDatabasePing
	{
		// Token: 0x06000368 RID: 872
		Task PingAsync(StorageOperationMode mode);
	}
}
