using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000271 RID: 625
	internal sealed class QueryTableColumn : IEquatable<QueryTableColumn>
	{
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x0004C11A File Offset: 0x0004A31A
		public static QueryTableColumnExpressionComparer DefaultExpressionComparer
		{
			get
			{
				return QueryTableColumnExpressionComparer.Instance;
			}
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0004C121 File Offset: 0x0004A321
		internal QueryTableColumn(string name, QueryExpression expression)
		{
			this.Name = name;
			this.Expression = expression;
			this.ConceptualResultType = expression.ConceptualResultType;
			this.Column = ((ConceptualPrimitiveResultType)this.ConceptualResultType).Column(name, null);
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0004C15B File Offset: 0x0004A35B
		internal QueryTableColumn(string name, ConceptualPrimitiveResultType conceptualResultType)
		{
			this.Name = name;
			this.ConceptualResultType = conceptualResultType;
			this.Column = conceptualResultType.Column(name, null);
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x0004C17F File Offset: 0x0004A37F
		public string Name { get; }

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x0004C187 File Offset: 0x0004A387
		public QueryExpression Expression { get; }

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x0004C18F File Offset: 0x0004A38F
		public ConceptualResultType ConceptualResultType { get; }

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x0004C197 File Offset: 0x0004A397
		public ConceptualTypeColumn Column { get; }

		// Token: 0x06001B1A RID: 6938 RVA: 0x0004C19F File Offset: 0x0004A39F
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QueryTableColumn);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0004C1AD File Offset: 0x0004A3AD
		public bool Equals(QueryTableColumn other)
		{
			return other != null && QueryNamingContext.NameComparer.Compare(this.Name, other.Name) == 0;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0004C1CD File Offset: 0x0004A3CD
		public override int GetHashCode()
		{
			return QueryNamingContext.NameComparer.GetHashCode(this.Name);
		}
	}
}
