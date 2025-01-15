using System;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001974 RID: 6516
	internal sealed class PartitionStackFrameExtendedInfo : IPartitionStackFrameExtendedInfo
	{
		// Token: 0x0600A55E RID: 42334 RVA: 0x00223857 File Offset: 0x00221A57
		public IGetStackFrameExtendedInfo GetPartitionStackFrameExtendedInfo(IPartitionedDocument document, IPartitionKey partitionKey)
		{
			return new StackFrameExtendedInfo(document);
		}
	}
}
