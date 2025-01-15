using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E00 RID: 7680
	public interface IPartitionStackFrameExtendedInfo
	{
		// Token: 0x0600BDB8 RID: 48568
		IGetStackFrameExtendedInfo GetPartitionStackFrameExtendedInfo(IPartitionedDocument document, IPartitionKey partitionKey);
	}
}
