using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001FA RID: 506
	internal sealed class BaseConflictEnumerator : BaseEnumerator
	{
		// Token: 0x0600107E RID: 4222 RVA: 0x00037005 File Offset: 0x00035205
		internal BaseConflictEnumerator(BaseConflictNode conflictNode)
		{
			this._conflictNode = conflictNode;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00037014 File Offset: 0x00035214
		protected override object NextData(ref int Index)
		{
			return this._conflictNode.NextData(ref Index);
		}

		// Token: 0x04000ACB RID: 2763
		private readonly BaseConflictNode _conflictNode;
	}
}
