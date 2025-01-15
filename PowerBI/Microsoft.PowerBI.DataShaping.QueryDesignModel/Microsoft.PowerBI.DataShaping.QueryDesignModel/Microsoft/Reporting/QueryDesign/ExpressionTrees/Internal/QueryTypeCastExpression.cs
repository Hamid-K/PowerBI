using System;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001C6 RID: 454
	internal sealed class QueryTypeCastExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001665 RID: 5733 RVA: 0x0003DE94 File Offset: 0x0003C094
		internal QueryTypeCastExpression(QueryExpression input, ConceptualResultType conceptualResultType)
			: base(conceptualResultType)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(input, "input");
			ConceptualPrimitiveType? primitiveTypeKind = input.ConceptualResultType.GetPrimitiveTypeKind();
			ConceptualPrimitiveType? primitiveTypeKind2 = base.ConceptualResultType.GetPrimitiveTypeKind();
			ArgumentValidation.CheckCondition(primitiveTypeKind != null, "input");
			ArgumentValidation.CheckCondition(primitiveTypeKind2 != null, "resultType");
			ConceptualPrimitiveType? conceptualPrimitiveType = primitiveTypeKind;
			ConceptualPrimitiveType? conceptualPrimitiveType2 = primitiveTypeKind2;
			ArgumentValidation.CheckCondition(!((conceptualPrimitiveType.GetValueOrDefault() == conceptualPrimitiveType2.GetValueOrDefault()) & (conceptualPrimitiveType != null == (conceptualPrimitiveType2 != null))), "input");
			this._input = input;
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x0003DF27 File Offset: 0x0003C127
		public QueryExpression Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0003DF2F File Offset: 0x0003C12F
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0003DF44 File Offset: 0x0003C144
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryTypeCastExpression queryTypeCastExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryTypeCastExpression>(this, other, out flag, out queryTypeCastExpression))
			{
				return flag;
			}
			return this.Input.Equals(queryTypeCastExpression.Input);
		}

		// Token: 0x04000BF3 RID: 3059
		private readonly QueryExpression _input;
	}
}
