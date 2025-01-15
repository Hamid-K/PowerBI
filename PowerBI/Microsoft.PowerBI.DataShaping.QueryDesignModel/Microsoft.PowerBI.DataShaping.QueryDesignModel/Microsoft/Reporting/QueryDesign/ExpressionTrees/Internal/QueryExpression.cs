using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000178 RID: 376
	internal abstract class QueryExpression : IEquatable<QueryExpression>
	{
		// Token: 0x06001494 RID: 5268 RVA: 0x0003B207 File Offset: 0x00039407
		protected QueryExpression(ConceptualResultType conceptualResultType)
		{
			this.ConceptualResultType = conceptualResultType;
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x0003B216 File Offset: 0x00039416
		public ConceptualResultType ConceptualResultType { get; }

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x0003B21E File Offset: 0x0003941E
		public static IEqualityComparer<QueryExpression> Comparer
		{
			get
			{
				return EqualityComparer<QueryExpression>.Default;
			}
		}

		// Token: 0x06001497 RID: 5271
		public abstract TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor);

		// Token: 0x06001498 RID: 5272 RVA: 0x0003B225 File Offset: 0x00039425
		public sealed override bool Equals(object obj)
		{
			return this.Equals(obj as QueryExpression);
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0003B233 File Offset: 0x00039433
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x0600149A RID: 5274
		public abstract bool Equals(QueryExpression other);

		// Token: 0x0600149B RID: 5275 RVA: 0x0003B240 File Offset: 0x00039440
		protected static bool CheckReferenceAndTypeEquality<TExpression>(TExpression @this, QueryExpression other, out bool isEqual, out TExpression otherTyped) where TExpression : QueryExpression
		{
			if (@this == other)
			{
				isEqual = true;
				otherTyped = default(TExpression);
				return true;
			}
			if (@this == null || other == null || @this.GetType() != other.GetType())
			{
				isEqual = false;
				otherTyped = default(TExpression);
				return true;
			}
			otherTyped = other as TExpression;
			if (otherTyped == null)
			{
				isEqual = false;
				return true;
			}
			if (@this.ConceptualResultType != null && !@this.ConceptualResultType.Equals(other.ConceptualResultType))
			{
				isEqual = false;
				return true;
			}
			isEqual = false;
			return false;
		}
	}
}
