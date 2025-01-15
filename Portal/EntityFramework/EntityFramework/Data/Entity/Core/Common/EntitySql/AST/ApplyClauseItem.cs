using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000672 RID: 1650
	internal sealed class ApplyClauseItem : Node
	{
		// Token: 0x06004EF2 RID: 20210 RVA: 0x0011F5A9 File Offset: 0x0011D7A9
		internal ApplyClauseItem(FromClauseItem applyLeft, FromClauseItem applyRight, ApplyKind applyKind)
		{
			this._applyLeft = applyLeft;
			this._applyRight = applyRight;
			this._applyKind = applyKind;
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06004EF3 RID: 20211 RVA: 0x0011F5C6 File Offset: 0x0011D7C6
		internal FromClauseItem LeftExpr
		{
			get
			{
				return this._applyLeft;
			}
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06004EF4 RID: 20212 RVA: 0x0011F5CE File Offset: 0x0011D7CE
		internal FromClauseItem RightExpr
		{
			get
			{
				return this._applyRight;
			}
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06004EF5 RID: 20213 RVA: 0x0011F5D6 File Offset: 0x0011D7D6
		internal ApplyKind ApplyKind
		{
			get
			{
				return this._applyKind;
			}
		}

		// Token: 0x04001C89 RID: 7305
		private readonly FromClauseItem _applyLeft;

		// Token: 0x04001C8A RID: 7306
		private readonly FromClauseItem _applyRight;

		// Token: 0x04001C8B RID: 7307
		private readonly ApplyKind _applyKind;
	}
}
