using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006A7 RID: 1703
	public abstract class DbBinaryExpression : DbExpression
	{
		// Token: 0x06004FEA RID: 20458 RVA: 0x001214E8 File Offset: 0x0011F6E8
		internal DbBinaryExpression()
		{
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x001214F0 File Offset: 0x0011F6F0
		internal DbBinaryExpression(DbExpressionKind kind, TypeUsage type, DbExpression left, DbExpression right)
			: base(kind, type, true)
		{
			this._left = left;
			this._right = right;
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06004FEC RID: 20460 RVA: 0x0012150A File Offset: 0x0011F70A
		public virtual DbExpression Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06004FED RID: 20461 RVA: 0x00121512 File Offset: 0x0011F712
		public virtual DbExpression Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x04001D37 RID: 7479
		private readonly DbExpression _left;

		// Token: 0x04001D38 RID: 7480
		private readonly DbExpression _right;
	}
}
