using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x020001C8 RID: 456
	internal abstract class QueryUnaryExpression : QueryExpression
	{
		// Token: 0x0600166F RID: 5743 RVA: 0x0003E02B File Offset: 0x0003C22B
		protected QueryUnaryExpression(ConceptualResultType conceptualResultType, QueryExpression argument)
			: base(conceptualResultType)
		{
			this._argument = ArgumentValidation.CheckNotNull<QueryExpression>(argument, "argument");
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x0003E045 File Offset: 0x0003C245
		public QueryExpression Argument
		{
			get
			{
				return this._argument;
			}
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0003E050 File Offset: 0x0003C250
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryUnaryExpression queryUnaryExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryUnaryExpression>(this, other, out flag, out queryUnaryExpression))
			{
				return flag;
			}
			return this.Argument.Equals(queryUnaryExpression.Argument);
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0003E07D File Offset: 0x0003C27D
		public override int GetHashCode()
		{
			return this.Argument.GetHashCode();
		}

		// Token: 0x04000BF7 RID: 3063
		private readonly QueryExpression _argument;
	}
}
