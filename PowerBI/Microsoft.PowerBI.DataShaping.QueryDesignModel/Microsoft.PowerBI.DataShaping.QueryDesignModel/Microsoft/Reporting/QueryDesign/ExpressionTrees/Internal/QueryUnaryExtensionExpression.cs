using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200017E RID: 382
	internal abstract class QueryUnaryExtensionExpression : QueryExtensionExpressionBase
	{
		// Token: 0x060014F5 RID: 5365 RVA: 0x0003B41E File Offset: 0x0003961E
		protected QueryUnaryExtensionExpression(ConceptualResultType conceptualResultType, QueryExpression argument)
			: base(conceptualResultType)
		{
			this._argument = ArgumentValidation.CheckNotNull<QueryExpression>(argument, "argument");
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0003B438 File Offset: 0x00039638
		public QueryExpression Argument
		{
			get
			{
				return this._argument;
			}
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0003B440 File Offset: 0x00039640
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryUnaryExtensionExpression queryUnaryExtensionExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryUnaryExtensionExpression>(this, other, out flag, out queryUnaryExtensionExpression))
			{
				return flag;
			}
			return this.Argument.Equals(queryUnaryExtensionExpression.Argument);
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0003B46D File Offset: 0x0003966D
		public override int GetHashCode()
		{
			return Hashing.CombineHash(base.GetHashCode(), this.Argument.GetHashCode());
		}

		// Token: 0x04000B3D RID: 2877
		private readonly QueryExpression _argument;
	}
}
