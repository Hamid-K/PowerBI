using System;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FCB RID: 8139
	public class SinglePageReader : IDisposable
	{
		// Token: 0x0600C6E1 RID: 50913 RVA: 0x0027A062 File Offset: 0x00278262
		public SinglePageReader(IPageReader reader)
		{
			this.reader = reader;
			this.page = reader.CreatePage();
		}

		// Token: 0x1700302C RID: 12332
		// (get) Token: 0x0600C6E2 RID: 50914 RVA: 0x0027A07D File Offset: 0x0027827D
		public TableSchema Schema
		{
			get
			{
				return this.reader.Schema;
			}
		}

		// Token: 0x1700302D RID: 12333
		// (get) Token: 0x0600C6E3 RID: 50915 RVA: 0x0027A08A File Offset: 0x0027828A
		public IPage Page
		{
			get
			{
				return this.page;
			}
		}

		// Token: 0x1700302E RID: 12334
		// (get) Token: 0x0600C6E4 RID: 50916 RVA: 0x0027A092 File Offset: 0x00278292
		public int MaxPageRowCount
		{
			get
			{
				return this.reader.MaxPageRowCount;
			}
		}

		// Token: 0x0600C6E5 RID: 50917 RVA: 0x0027A09F File Offset: 0x0027829F
		public void Read()
		{
			this.reader.Read(this.page);
		}

		// Token: 0x0600C6E6 RID: 50918 RVA: 0x0027A0B2 File Offset: 0x002782B2
		public IPageReader NextResult()
		{
			return this.reader.NextResult();
		}

		// Token: 0x0600C6E7 RID: 50919 RVA: 0x0027A0BF File Offset: 0x002782BF
		public void Dispose()
		{
			this.page.Dispose();
			this.reader.Dispose();
		}

		// Token: 0x04006586 RID: 25990
		private readonly IPageReader reader;

		// Token: 0x04006587 RID: 25991
		private readonly IPage page;
	}
}
