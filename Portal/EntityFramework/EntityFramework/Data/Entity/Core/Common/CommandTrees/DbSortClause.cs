using System;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006E0 RID: 1760
	public sealed class DbSortClause
	{
		// Token: 0x06005182 RID: 20866 RVA: 0x00123B17 File Offset: 0x00121D17
		internal DbSortClause(DbExpression key, bool asc, string collation)
		{
			this._expr = key;
			this._asc = asc;
			this._coll = collation;
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06005183 RID: 20867 RVA: 0x00123B34 File Offset: 0x00121D34
		public bool Ascending
		{
			get
			{
				return this._asc;
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06005184 RID: 20868 RVA: 0x00123B3C File Offset: 0x00121D3C
		public string Collation
		{
			get
			{
				return this._coll;
			}
		}

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06005185 RID: 20869 RVA: 0x00123B44 File Offset: 0x00121D44
		public DbExpression Expression
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x04001DCB RID: 7627
		private readonly DbExpression _expr;

		// Token: 0x04001DCC RID: 7628
		private readonly bool _asc;

		// Token: 0x04001DCD RID: 7629
		private readonly string _coll;
	}
}
