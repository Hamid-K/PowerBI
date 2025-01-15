using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200021C RID: 540
	[ImmutableObject(true)]
	public sealed class ResolvedQueryDefinition : IEquatable<ResolvedQueryDefinition>
	{
		// Token: 0x06000F99 RID: 3993 RVA: 0x0001DAF8 File Offset: 0x0001BCF8
		internal ResolvedQueryDefinition(IReadOnlyList<ResolvedQueryParameterDeclaration> parameters, IReadOnlyList<ResolvedQueryLetBinding> let, IReadOnlyList<ResolvedQuerySource> from, IReadOnlyList<ResolvedQueryFilter> where, IReadOnlyList<ResolvedQueryTransform> transform, IReadOnlyList<ResolvedQuerySortClause> orderBy, IReadOnlyList<ResolvedQuerySelect> select, IReadOnlyList<ResolvedQueryAxis> visualShape, IReadOnlyList<ResolvedQueryExpression> groupBy, int? top, long? skip = null, string name = null)
		{
			this.Parameters = parameters;
			this.Let = let;
			this.From = from;
			this.Where = where;
			this.Transform = transform;
			this.OrderBy = orderBy;
			this.Select = select;
			this.VisualShape = visualShape;
			this.GroupBy = groupBy;
			this.Top = top;
			this.Skip = skip;
			this.Name = name;
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x0001DB68 File Offset: 0x0001BD68
		public IReadOnlyList<ResolvedQueryParameterDeclaration> Parameters { get; }

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x0001DB70 File Offset: 0x0001BD70
		public IReadOnlyList<ResolvedQueryLetBinding> Let { get; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0001DB78 File Offset: 0x0001BD78
		public IReadOnlyList<ResolvedQuerySource> From { get; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0001DB80 File Offset: 0x0001BD80
		public IReadOnlyList<ResolvedQueryFilter> Where { get; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0001DB88 File Offset: 0x0001BD88
		public IReadOnlyList<ResolvedQueryTransform> Transform { get; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0001DB90 File Offset: 0x0001BD90
		public IReadOnlyList<ResolvedQuerySortClause> OrderBy { get; }

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0001DB98 File Offset: 0x0001BD98
		public IReadOnlyList<ResolvedQuerySelect> Select { get; }

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0001DBA0 File Offset: 0x0001BDA0
		public IReadOnlyList<ResolvedQueryAxis> VisualShape { get; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x0001DBA8 File Offset: 0x0001BDA8
		public IReadOnlyList<ResolvedQueryExpression> GroupBy { get; }

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0001DBB0 File Offset: 0x0001BDB0
		public int? Top { get; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x0001DBB8 File Offset: 0x0001BDB8
		public long? Skip { get; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0001DBC0 File Offset: 0x0001BDC0
		public string Name { get; }

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0001DBC8 File Offset: 0x0001BDC8
		public bool Equals(ResolvedQueryDefinition other)
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0001DBD6 File Offset: 0x0001BDD6
		public override int GetHashCode()
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.GetHashCode(this);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0001DBE3 File Offset: 0x0001BDE3
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQueryDefinition);
		}
	}
}
