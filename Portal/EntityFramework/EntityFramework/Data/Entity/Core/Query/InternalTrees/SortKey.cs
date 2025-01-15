using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003EE RID: 1006
	internal class SortKey
	{
		// Token: 0x06002F2C RID: 12076 RVA: 0x000956F2 File Offset: 0x000938F2
		internal SortKey(Var v, bool asc, string collation)
		{
			this.Var = v;
			this.m_asc = asc;
			this.m_collation = collation;
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x0009570F File Offset: 0x0009390F
		// (set) Token: 0x06002F2E RID: 12078 RVA: 0x00095717 File Offset: 0x00093917
		internal Var Var { get; set; }

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x00095720 File Offset: 0x00093920
		internal bool AscendingSort
		{
			get
			{
				return this.m_asc;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06002F30 RID: 12080 RVA: 0x00095728 File Offset: 0x00093928
		internal string Collation
		{
			get
			{
				return this.m_collation;
			}
		}

		// Token: 0x04000FE4 RID: 4068
		private readonly bool m_asc;

		// Token: 0x04000FE5 RID: 4069
		private readonly string m_collation;
	}
}
