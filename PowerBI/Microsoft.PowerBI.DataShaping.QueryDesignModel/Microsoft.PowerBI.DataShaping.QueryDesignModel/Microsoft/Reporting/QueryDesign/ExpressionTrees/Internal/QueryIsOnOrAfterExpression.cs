using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200019A RID: 410
	internal sealed class QueryIsOnOrAfterExpression : QueryExtensionExpressionBase
	{
		// Token: 0x06001583 RID: 5507 RVA: 0x0003C274 File Offset: 0x0003A474
		internal QueryIsOnOrAfterExpression(ConceptualResultType conceptualResultType, IEnumerable<QueryIsOnOrAfterArgument> arguments)
			: base(conceptualResultType)
		{
			this._arguments = arguments.ToReadOnlyCollection<QueryIsOnOrAfterArgument>();
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x0003C289 File Offset: 0x0003A489
		public ReadOnlyCollection<QueryIsOnOrAfterArgument> Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x0003C291 File Offset: 0x0003A491
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0003C29C File Offset: 0x0003A49C
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryIsOnOrAfterExpression queryIsOnOrAfterExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryIsOnOrAfterExpression>(this, other, out flag, out queryIsOnOrAfterExpression))
			{
				return flag;
			}
			return this.Arguments.SequenceEqual(queryIsOnOrAfterExpression.Arguments);
		}

		// Token: 0x04000B6E RID: 2926
		private ReadOnlyCollection<QueryIsOnOrAfterArgument> _arguments;
	}
}
