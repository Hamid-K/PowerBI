using System;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FC7 RID: 8135
	public class ReaderWriterProgress : IProgress
	{
		// Token: 0x0600C6D5 RID: 50901 RVA: 0x00279E66 File Offset: 0x00278066
		public ReaderWriterProgress()
		{
			this.syncRoot = new object();
		}

		// Token: 0x1700302A RID: 12330
		// (get) Token: 0x0600C6D6 RID: 50902 RVA: 0x00279E7C File Offset: 0x0027807C
		public long Rows
		{
			get
			{
				object obj = this.syncRoot;
				long num;
				lock (obj)
				{
					num = this.rows;
				}
				return num;
			}
		}

		// Token: 0x1700302B RID: 12331
		// (get) Token: 0x0600C6D7 RID: 50903 RVA: 0x00279EC0 File Offset: 0x002780C0
		public long ExceptionRows
		{
			get
			{
				object obj = this.syncRoot;
				long num;
				lock (obj)
				{
					num = this.errorsRows;
				}
				return num;
			}
		}

		// Token: 0x0600C6D8 RID: 50904 RVA: 0x00279F04 File Offset: 0x00278104
		public void OnRows(int rows, int errorRows)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.rows += (long)rows;
				this.errorsRows += (long)errorRows;
			}
		}

		// Token: 0x0400657B RID: 25979
		private readonly object syncRoot;

		// Token: 0x0400657C RID: 25980
		private long rows;

		// Token: 0x0400657D RID: 25981
		private long errorsRows;
	}
}
