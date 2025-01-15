using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000186 RID: 390
	internal sealed class QueryFunctionExpression : QueryExpression
	{
		// Token: 0x06001521 RID: 5409 RVA: 0x0003B890 File Offset: 0x00039A90
		internal QueryFunctionExpression(EdmFunction function, IEnumerable<QueryExpression> arguments)
			: base(function.ConceptualReturnType)
		{
			this._function = ArgumentValidation.CheckNotNull<EdmFunction>(function, "function");
			this._arguments = ArgumentValidation.CheckNotNull<IEnumerable<QueryExpression>>(arguments, "arguments").ToReadOnlyCollection<QueryExpression>();
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x0003B8C5 File Offset: 0x00039AC5
		public EdmFunction Function
		{
			get
			{
				return this._function;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x0003B8CD File Offset: 0x00039ACD
		public ReadOnlyCollection<QueryExpression> Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0003B8D5 File Offset: 0x00039AD5
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0003B8E8 File Offset: 0x00039AE8
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryFunctionExpression queryFunctionExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryFunctionExpression>(this, other, out flag, out queryFunctionExpression))
			{
				return flag;
			}
			return this.Function.Equals(queryFunctionExpression.Function) && this.Arguments.SequenceEqual(queryFunctionExpression.Arguments, QueryExpression.Comparer);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0003B92F File Offset: 0x00039B2F
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Function.GetHashCode(), Hashing.CombineHashReadonly<QueryExpression>(this.Arguments, QueryExpression.Comparer));
		}

		// Token: 0x04000B4C RID: 2892
		private readonly EdmFunction _function;

		// Token: 0x04000B4D RID: 2893
		private readonly ReadOnlyCollection<QueryExpression> _arguments;
	}
}
