using System;
using System.Xml;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003EC RID: 1004
	internal static class MessageId
	{
		// Token: 0x0600234A RID: 9034 RVA: 0x0006C67C File Offset: 0x0006A87C
		public static UniqueId Create()
		{
			return SequencedIdGenerator.DefaultGenerator.Next;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x0006C688 File Offset: 0x0006A888
		public static UniqueId Create(string id)
		{
			UniqueId uniqueId = SequencedIdGenerator.Create(id);
			if (uniqueId == null)
			{
				uniqueId = new StandaloneId(id);
			}
			return uniqueId;
		}
	}
}
