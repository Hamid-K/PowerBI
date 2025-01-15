using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000045 RID: 69
	public class SqlRowsCopiedEventArgs : EventArgs
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x0000FFD1 File Offset: 0x0000E1D1
		public SqlRowsCopiedEventArgs(long rowsCopied)
		{
			this._rowsCopied = rowsCopied;
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
		public bool Abort
		{
			get
			{
				return this._abort;
			}
			set
			{
				this._abort = value;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0000FFF1 File Offset: 0x0000E1F1
		public long RowsCopied
		{
			get
			{
				return this._rowsCopied;
			}
		}

		// Token: 0x040000E0 RID: 224
		private bool _abort;

		// Token: 0x040000E1 RID: 225
		private long _rowsCopied;
	}
}
