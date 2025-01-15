using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006A3 RID: 1699
	public abstract class DbAggregate
	{
		// Token: 0x06004FDB RID: 20443 RVA: 0x001213F4 File Offset: 0x0011F5F4
		internal DbAggregate(TypeUsage resultType, DbExpressionList arguments)
		{
			this._type = resultType;
			this._args = arguments;
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06004FDC RID: 20444 RVA: 0x0012140A File Offset: 0x0011F60A
		public TypeUsage ResultType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06004FDD RID: 20445 RVA: 0x00121412 File Offset: 0x0011F612
		public IList<DbExpression> Arguments
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x04001D32 RID: 7474
		private readonly DbExpressionList _args;

		// Token: 0x04001D33 RID: 7475
		private readonly TypeUsage _type;
	}
}
