using System;
using System.IO;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000C4 RID: 196
	public abstract class RowsetStreamBase : Stream
	{
		// Token: 0x06000365 RID: 869
		public abstract bool TryGetHasRowset(out bool hasRowset);
	}
}
