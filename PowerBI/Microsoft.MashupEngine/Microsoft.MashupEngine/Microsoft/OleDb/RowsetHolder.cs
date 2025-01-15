using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001F24 RID: 7972
	public struct RowsetHolder : IDisposable
	{
		// Token: 0x0600C357 RID: 50007 RVA: 0x00271E9F File Offset: 0x0027009F
		public RowsetHolder(IRowset rowset)
		{
			this.rowset = rowset;
		}

		// Token: 0x17002FB4 RID: 12212
		// (get) Token: 0x0600C358 RID: 50008 RVA: 0x00271EA8 File Offset: 0x002700A8
		public IRowset Rowset
		{
			get
			{
				return this.rowset;
			}
		}

		// Token: 0x0600C359 RID: 50009 RVA: 0x00271EB0 File Offset: 0x002700B0
		void IDisposable.Dispose()
		{
			IDisposable disposable = this.rowset as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x04006476 RID: 25718
		private IRowset rowset;
	}
}
