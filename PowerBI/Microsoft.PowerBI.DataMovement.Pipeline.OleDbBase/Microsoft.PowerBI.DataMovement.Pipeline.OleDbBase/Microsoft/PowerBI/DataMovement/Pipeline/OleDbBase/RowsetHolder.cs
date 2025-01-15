using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000BF RID: 191
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public struct RowsetHolder : IDisposable
	{
		// Token: 0x06000349 RID: 841 RVA: 0x00009925 File Offset: 0x00007B25
		public RowsetHolder(IRowset rowset)
		{
			this.rowset = rowset;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000992E File Offset: 0x00007B2E
		public IRowset Rowset
		{
			get
			{
				return this.rowset;
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00009938 File Offset: 0x00007B38
		void IDisposable.Dispose()
		{
			IDisposable disposable = this.rowset as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x04000376 RID: 886
		private IRowset rowset;
	}
}
