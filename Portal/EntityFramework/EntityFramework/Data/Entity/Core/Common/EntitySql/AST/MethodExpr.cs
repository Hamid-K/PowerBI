using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200068D RID: 1677
	internal sealed class MethodExpr : GroupAggregateExpr
	{
		// Token: 0x06004F55 RID: 20309 RVA: 0x001205D5 File Offset: 0x0011E7D5
		internal MethodExpr(Node expr, DistinctKind distinctKind, NodeList<Node> args)
			: this(expr, distinctKind, args, null)
		{
		}

		// Token: 0x06004F56 RID: 20310 RVA: 0x001205E1 File Offset: 0x0011E7E1
		internal MethodExpr(Node expr, DistinctKind distinctKind, NodeList<Node> args, NodeList<RelshipNavigationExpr> relationships)
			: base(distinctKind)
		{
			this._expr = expr;
			this._args = args;
			this._relationships = relationships;
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06004F57 RID: 20311 RVA: 0x00120600 File Offset: 0x0011E800
		internal Node Expr
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06004F58 RID: 20312 RVA: 0x00120608 File Offset: 0x0011E808
		internal NodeList<Node> Args
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06004F59 RID: 20313 RVA: 0x00120610 File Offset: 0x0011E810
		internal bool HasRelationships
		{
			get
			{
				return this._relationships != null && this._relationships.Count > 0;
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06004F5A RID: 20314 RVA: 0x0012062A File Offset: 0x0011E82A
		internal NodeList<RelshipNavigationExpr> Relationships
		{
			get
			{
				return this._relationships;
			}
		}

		// Token: 0x04001D03 RID: 7427
		private readonly Node _expr;

		// Token: 0x04001D04 RID: 7428
		private readonly NodeList<Node> _args;

		// Token: 0x04001D05 RID: 7429
		private readonly NodeList<RelshipNavigationExpr> _relationships;
	}
}
