using System;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000046 RID: 70
	internal sealed class JoinCondition
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x0000602C File Offset: 0x0000422C
		internal JoinCondition(ExpressionNode primaryKey, ExpressionNode secondaryKey, SortDirection sortDirection)
		{
			this._primaryKey = primaryKey;
			this._secondaryKey = secondaryKey;
			this._sortDirection = sortDirection;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00006049 File Offset: 0x00004249
		internal ExpressionNode PrimaryKey
		{
			get
			{
				return this._primaryKey;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00006051 File Offset: 0x00004251
		internal ExpressionNode SecondaryKey
		{
			get
			{
				return this._secondaryKey;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00006059 File Offset: 0x00004259
		internal SortDirection SortDirection
		{
			get
			{
				return this._sortDirection;
			}
		}

		// Token: 0x04000126 RID: 294
		private readonly ExpressionNode _primaryKey;

		// Token: 0x04000127 RID: 295
		private readonly ExpressionNode _secondaryKey;

		// Token: 0x04000128 RID: 296
		private readonly SortDirection _sortDirection;
	}
}
