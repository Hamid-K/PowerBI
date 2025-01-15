using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000DE RID: 222
	internal sealed class IntermediateQueryTransformTableColumn
	{
		// Token: 0x060007B4 RID: 1972 RVA: 0x0001D0D4 File Offset: 0x0001B2D4
		internal IntermediateQueryTransformTableColumn(Identifier id, ExpressionNode expression, string role, TransformTableColumnActAs actAs, string formatString, IConceptualColumn underlyingConceptualColumn, ResolvedQueryExpression underlyingExpression, bool? isScalar)
		{
			this._id = id;
			this._expression = expression;
			this._role = role;
			this._actAs = actAs;
			this._formatString = formatString;
			this._underlyingConceptualColumn = underlyingConceptualColumn;
			this._underlyingExpression = underlyingExpression;
			this._isScalar = isScalar;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0001D124 File Offset: 0x0001B324
		internal Identifier Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0001D12C File Offset: 0x0001B32C
		internal ExpressionNode Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0001D134 File Offset: 0x0001B334
		internal string Role
		{
			get
			{
				return this._role;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001D13C File Offset: 0x0001B33C
		internal string FormatString
		{
			get
			{
				return this._formatString;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001D144 File Offset: 0x0001B344
		internal IConceptualColumn UnderlyingConceptualColumn
		{
			get
			{
				return this._underlyingConceptualColumn;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0001D14C File Offset: 0x0001B34C
		internal ResolvedQueryExpression UnderlyingExpression
		{
			get
			{
				return this._underlyingExpression;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0001D154 File Offset: 0x0001B354
		internal TransformTableColumnActAs ActAs
		{
			get
			{
				return this._actAs;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0001D15C File Offset: 0x0001B35C
		internal IntermediateQueryTransformTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0001D164 File Offset: 0x0001B364
		internal bool? IsScalar
		{
			get
			{
				return this._isScalar;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0001D16C File Offset: 0x0001B36C
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x0001D174 File Offset: 0x0001B374
		public bool OmitFromOutput
		{
			get
			{
				return this._omitFromOutput;
			}
			set
			{
				this._omitFromOutput = value;
			}
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001D17D File Offset: 0x0001B37D
		internal void SetTable(IntermediateQueryTransformTable table)
		{
			this._table = table;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001D186 File Offset: 0x0001B386
		internal void SetRole(string role)
		{
			this._role = role;
		}

		// Token: 0x04000403 RID: 1027
		private readonly Identifier _id;

		// Token: 0x04000404 RID: 1028
		private readonly ExpressionNode _expression;

		// Token: 0x04000405 RID: 1029
		private readonly TransformTableColumnActAs _actAs;

		// Token: 0x04000406 RID: 1030
		private readonly string _formatString;

		// Token: 0x04000407 RID: 1031
		private readonly IConceptualColumn _underlyingConceptualColumn;

		// Token: 0x04000408 RID: 1032
		private readonly ResolvedQueryExpression _underlyingExpression;

		// Token: 0x04000409 RID: 1033
		private readonly bool? _isScalar;

		// Token: 0x0400040A RID: 1034
		private IntermediateQueryTransformTable _table;

		// Token: 0x0400040B RID: 1035
		private string _role;

		// Token: 0x0400040C RID: 1036
		private bool _omitFromOutput;
	}
}
