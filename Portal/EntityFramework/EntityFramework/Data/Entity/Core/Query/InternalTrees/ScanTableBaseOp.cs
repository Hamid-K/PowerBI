using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003DF RID: 991
	internal abstract class ScanTableBaseOp : RelOp
	{
		// Token: 0x06002EEE RID: 12014 RVA: 0x000952ED File Offset: 0x000934ED
		protected ScanTableBaseOp(OpType opType, Table table)
			: base(opType)
		{
			this.m_table = table;
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x000952FD File Offset: 0x000934FD
		protected ScanTableBaseOp(OpType opType)
			: base(opType)
		{
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x00095306 File Offset: 0x00093506
		internal Table Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x04000FD3 RID: 4051
		private readonly Table m_table;
	}
}
