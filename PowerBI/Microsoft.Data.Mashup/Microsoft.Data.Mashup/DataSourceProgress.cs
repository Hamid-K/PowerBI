using System;
using System.Threading;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000014 RID: 20
	public sealed class DataSourceProgress
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x000059D2 File Offset: 0x00003BD2
		internal DataSourceProgress(string kind, string identity)
		{
			this.kind = kind;
			this.identity = identity;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000059F7 File Offset: 0x00003BF7
		[Obsolete("Please use DataSourceKind and DataSourceIdentity separately instead of DataSource", false)]
		public DataSource DataSource
		{
			get
			{
				return new DataSource(this.kind, this.identity);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005A0A File Offset: 0x00003C0A
		public string DataSourceKind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00005A12 File Offset: 0x00003C12
		public string DataSourceIdentity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005A1C File Offset: 0x00003C1C
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00005A4B File Offset: 0x00003C4B
		public int? Rows
		{
			get
			{
				int num = Interlocked.CompareExchange(ref this.rows, 0, 0);
				if (num != -1)
				{
					return new int?(num);
				}
				return null;
			}
			internal set
			{
				Interlocked.Exchange(ref this.rows, value.GetValueOrDefault(-1));
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005A64 File Offset: 0x00003C64
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00005A92 File Offset: 0x00003C92
		public long? Bytes
		{
			get
			{
				long num = Interlocked.Read(ref this.bytes);
				if (num != -1L)
				{
					return new long?(num);
				}
				return null;
			}
			internal set
			{
				Interlocked.Exchange(ref this.bytes, value.GetValueOrDefault(-1L));
			}
		}

		// Token: 0x0400006E RID: 110
		private readonly string kind;

		// Token: 0x0400006F RID: 111
		private readonly string identity;

		// Token: 0x04000070 RID: 112
		private int rows = -1;

		// Token: 0x04000071 RID: 113
		private long bytes = -1L;
	}
}
