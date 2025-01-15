using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001BF RID: 447
	internal sealed class QueryTableDeclarationExpression : QueryBaseDeclarationExpression
	{
		// Token: 0x0600163D RID: 5693 RVA: 0x0003DA69 File Offset: 0x0003BC69
		internal QueryTableDeclarationExpression(IConceptualEntity entity, QueryExpression expression, QueryVisualShape visualShape, IReadOnlyList<QueryFieldDeclarationExpression> additionalColumns)
			: base(entity.ConceptualResultType)
		{
			this.Entity = entity;
			this.Expression = expression;
			this.VisualShape = visualShape;
			this.AdditionalColumns = additionalColumns;
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0003DA94 File Offset: 0x0003BC94
		public string Name
		{
			get
			{
				return this.Entity.Name;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0003DAA1 File Offset: 0x0003BCA1
		public IConceptualEntity Entity { get; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x0003DAA9 File Offset: 0x0003BCA9
		public QueryExpression Expression { get; }

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x0003DAB1 File Offset: 0x0003BCB1
		public QueryVisualShape VisualShape { get; }

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0003DAB9 File Offset: 0x0003BCB9
		public IReadOnlyList<QueryFieldDeclarationExpression> AdditionalColumns { get; }

		// Token: 0x06001643 RID: 5699 RVA: 0x0003DAC4 File Offset: 0x0003BCC4
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryTableDeclarationExpression queryTableDeclarationExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryTableDeclarationExpression>(this, other, out flag, out queryTableDeclarationExpression))
			{
				return flag;
			}
			return QueryNamingContext.NameComparer.Equals(this.Entity.Name, queryTableDeclarationExpression.Entity.Name) && this.Expression.Equals(queryTableDeclarationExpression.Expression) && object.Equals(this.VisualShape, queryTableDeclarationExpression.VisualShape) && this.AdditionalColumns.SequenceEqualReadOnly(queryTableDeclarationExpression.AdditionalColumns);
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x0003DB3B File Offset: 0x0003BD3B
		public override int GetHashCode()
		{
			return Microsoft.DataShaping.Common.Hashing.CombineHash(QueryNamingContext.NameComparer.GetHashCode(this.Entity.Name), this.Expression.GetHashCode(), Microsoft.DataShaping.Common.Hashing.GetHashCode<QueryVisualShape>(this.VisualShape, null), Microsoft.DataShaping.Common.Hashing.CombineHashReadonly<QueryFieldDeclarationExpression>(this.AdditionalColumns, null));
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x0003DB7A File Offset: 0x0003BD7A
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x0003DB84 File Offset: 0x0003BD84
		internal static bool IsValidTableDeclarationSchema(QueryExpression expression, QueryVisualShape visualShape, IConceptualEntity entity, IReadOnlyList<QueryFieldDeclarationExpression> additionalColumns)
		{
			HashSet<ConceptualTypeColumn> hashSet = new HashSet<ConceptualTypeColumn>(expression.ConceptualResultType.GetRowType().Columns);
			if (visualShape != null && visualShape.IsDensifiedColumnName != null)
			{
				hashSet.Add(new ConceptualTypeColumn(ConceptualPrimitiveResultType.Boolean, visualShape.IsDensifiedColumnName));
			}
			foreach (QueryFieldDeclarationExpression queryFieldDeclarationExpression in additionalColumns)
			{
				hashSet.Add(queryFieldDeclarationExpression.FieldRef.Column);
			}
			return hashSet.SetEquals(entity.ConceptualResultType.RowType.Columns);
		}
	}
}
