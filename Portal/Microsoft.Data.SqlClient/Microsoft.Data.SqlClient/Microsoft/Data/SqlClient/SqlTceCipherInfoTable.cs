using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000104 RID: 260
	internal class SqlTceCipherInfoTable
	{
		// Token: 0x06001565 RID: 5477 RVA: 0x0005E344 File Offset: 0x0005C544
		internal SqlTceCipherInfoTable(int tabSize)
		{
			this.keyList = new SqlTceCipherInfoEntry[tabSize];
		}

		// Token: 0x170008EA RID: 2282
		internal SqlTceCipherInfoEntry this[int index]
		{
			get
			{
				return this.keyList[index];
			}
			set
			{
				this.keyList[index] = value;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x0005E36D File Offset: 0x0005C56D
		internal int Size
		{
			get
			{
				return this.keyList.Length;
			}
		}

		// Token: 0x0400085E RID: 2142
		private readonly SqlTceCipherInfoEntry[] keyList;
	}
}
