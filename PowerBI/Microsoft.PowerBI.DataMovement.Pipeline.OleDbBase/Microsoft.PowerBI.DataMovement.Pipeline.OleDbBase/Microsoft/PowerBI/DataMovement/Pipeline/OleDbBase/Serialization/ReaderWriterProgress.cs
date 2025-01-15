using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000E4 RID: 228
	internal class ReaderWriterProgress : IProgress
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x0000E172 File Offset: 0x0000C372
		internal ReaderWriterProgress()
		{
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000E188 File Offset: 0x0000C388
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

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000E1CC File Offset: 0x0000C3CC
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

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000E210 File Offset: 0x0000C410
		public void OnRows(int rows, int errorRows)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.rows += (long)rows;
				this.errorsRows += (long)errorRows;
			}
		}

		// Token: 0x040003F0 RID: 1008
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private readonly object syncRoot = new object();

		// Token: 0x040003F1 RID: 1009
		private long rows;

		// Token: 0x040003F2 RID: 1010
		private long errorsRows;
	}
}
