using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000196 RID: 406
	internal sealed class QueryIsAfterExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001579 RID: 5497 RVA: 0x0003C1CA File Offset: 0x0003A3CA
		internal QueryIsAfterExpression(ConceptualResultType conceptualResultType, IEnumerable<QueryIsOnOrAfterArgument> arguments)
			: base(conceptualResultType)
		{
			this.Arguments = arguments.ToReadOnlyCollection<QueryIsOnOrAfterArgument>();
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0003C1DF File Offset: 0x0003A3DF
		public ReadOnlyCollection<QueryIsOnOrAfterArgument> Arguments { get; }

		// Token: 0x0600157B RID: 5499 RVA: 0x0003C1E7 File Offset: 0x0003A3E7
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0003C1F0 File Offset: 0x0003A3F0
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryIsAfterExpression queryIsAfterExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryIsAfterExpression>(this, other, out flag, out queryIsAfterExpression))
			{
				return flag;
			}
			return this.Arguments.SequenceEqual(queryIsAfterExpression.Arguments);
		}
	}
}
