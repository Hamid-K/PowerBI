using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006E3 RID: 1763
	public abstract class DbUnaryExpression : DbExpression
	{
		// Token: 0x0600518E RID: 20878 RVA: 0x00123BD6 File Offset: 0x00121DD6
		internal DbUnaryExpression()
		{
		}

		// Token: 0x0600518F RID: 20879 RVA: 0x00123BDE File Offset: 0x00121DDE
		internal DbUnaryExpression(DbExpressionKind kind, TypeUsage resultType, DbExpression argument)
			: base(kind, resultType, true)
		{
			this._argument = argument;
		}

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06005190 RID: 20880 RVA: 0x00123BF0 File Offset: 0x00121DF0
		public virtual DbExpression Argument
		{
			get
			{
				return this._argument;
			}
		}

		// Token: 0x04001DD0 RID: 7632
		private readonly DbExpression _argument;
	}
}
