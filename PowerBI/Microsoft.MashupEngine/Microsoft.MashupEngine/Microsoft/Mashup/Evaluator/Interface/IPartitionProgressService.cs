using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DFF RID: 7679
	public interface IPartitionProgressService
	{
		// Token: 0x0600BDB7 RID: 48567
		IProgressService2 GetPartitionProgressService(IPartitionKey partitionKey);
	}
}
