using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009AB RID: 2475
	public class DrdaRowsCopiedEventArgs : EventArgs
	{
		// Token: 0x06004CB0 RID: 19632 RVA: 0x0013299E File Offset: 0x00130B9E
		public DrdaRowsCopiedEventArgs(long rowsCopied)
		{
			this._rowsCopied = rowsCopied;
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06004CB1 RID: 19633 RVA: 0x001329AD File Offset: 0x00130BAD
		public long RowsCopied
		{
			get
			{
				return this._rowsCopied;
			}
		}

		// Token: 0x04003CA3 RID: 15523
		private long _rowsCopied;
	}
}
