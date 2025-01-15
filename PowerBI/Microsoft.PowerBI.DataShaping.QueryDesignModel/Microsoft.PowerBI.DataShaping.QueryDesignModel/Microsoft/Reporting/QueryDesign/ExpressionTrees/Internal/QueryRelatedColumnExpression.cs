using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001B0 RID: 432
	internal sealed class QueryRelatedColumnExpression : QueryExtensionExpressionBase
	{
		// Token: 0x060015E6 RID: 5606 RVA: 0x0003CE6E File Offset: 0x0003B06E
		internal QueryRelatedColumnExpression(ConceptualResultType conceptualResultType, EdmFieldInstance field, IConceptualColumn column = null)
			: base(conceptualResultType)
		{
			this._field = field;
			this._column = column;
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x0003CE85 File Offset: 0x0003B085
		internal EdmFieldInstance Field
		{
			get
			{
				return this._field;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0003CE8D File Offset: 0x0003B08D
		internal IConceptualColumn Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x0003CE95 File Offset: 0x0003B095
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x0003CEA8 File Offset: 0x0003B0A8
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryRelatedColumnExpression queryRelatedColumnExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryRelatedColumnExpression>(this, other, out flag, out queryRelatedColumnExpression))
			{
				return flag;
			}
			return object.Equals(this.Field, queryRelatedColumnExpression.Field) && object.Equals(this.Column, queryRelatedColumnExpression.Column);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0003CEF4 File Offset: 0x0003B0F4
		public override int GetHashCode()
		{
			return Microsoft.DataShaping.Common.Hashing.CombineHash(Microsoft.DataShaping.Common.Hashing.GetHashCode<EdmFieldInstance>(this.Field, null), Microsoft.DataShaping.Common.Hashing.GetHashCode<IConceptualColumn>(this.Column, null));
		}

		// Token: 0x04000BB0 RID: 2992
		private readonly EdmFieldInstance _field;

		// Token: 0x04000BB1 RID: 2993
		private readonly IConceptualColumn _column;
	}
}
