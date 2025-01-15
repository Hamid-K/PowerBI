using System;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x0200004E RID: 78
	internal sealed class DataIndexException : Exception
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00007CD4 File Offset: 0x00005ED4
		public DataIndexException(string message, Exception ex)
			: base(message, ex)
		{
		}
	}
}
