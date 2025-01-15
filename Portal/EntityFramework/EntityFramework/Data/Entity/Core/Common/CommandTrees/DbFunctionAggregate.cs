using System;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006BC RID: 1724
	public sealed class DbFunctionAggregate : DbAggregate
	{
		// Token: 0x060050B9 RID: 20665 RVA: 0x00121DD5 File Offset: 0x0011FFD5
		internal DbFunctionAggregate(TypeUsage resultType, DbExpressionList arguments, EdmFunction function, bool isDistinct)
			: base(resultType, arguments)
		{
			this._aggregateFunction = function;
			this._distinct = isDistinct;
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x060050BA RID: 20666 RVA: 0x00121DEE File Offset: 0x0011FFEE
		public bool Distinct
		{
			get
			{
				return this._distinct;
			}
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x060050BB RID: 20667 RVA: 0x00121DF6 File Offset: 0x0011FFF6
		public EdmFunction Function
		{
			get
			{
				return this._aggregateFunction;
			}
		}

		// Token: 0x04001D8E RID: 7566
		private readonly bool _distinct;

		// Token: 0x04001D8F RID: 7567
		private readonly EdmFunction _aggregateFunction;
	}
}
