using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x02000180 RID: 384
	internal sealed class QueryExtensionFunctionExpression : QueryExtensionExpressionBase
	{
		// Token: 0x060014FB RID: 5371 RVA: 0x0003B4A1 File Offset: 0x000396A1
		internal QueryExtensionFunctionExpression(ConceptualResultType conceptualResultType, string functionName, IReadOnlyList<QueryExpression> arguments, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Name", "Index" })] IReadOnlyList<global::System.ValueTuple<string, int>> resultColumnLineage)
			: base(conceptualResultType)
		{
			this._functionName = functionName;
			this._arguments = arguments;
			this._resultColumnLineage = resultColumnLineage;
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x0003B4C0 File Offset: 0x000396C0
		public string FunctionName
		{
			get
			{
				return this._functionName;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x0003B4C8 File Offset: 0x000396C8
		public IReadOnlyList<QueryExpression> Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x0003B4D0 File Offset: 0x000396D0
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Name", "Index" })]
		public IReadOnlyList<global::System.ValueTuple<string, int>> ResultColumnLineage
		{
			[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Name", "Index" })]
			get
			{
				return this._resultColumnLineage;
			}
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0003B4D8 File Offset: 0x000396D8
		public override TResultType Accept<TResultType>(QueryExpressionVisitor<TResultType> visitor)
		{
			return ArgumentValidation.CheckNotNull<QueryExpressionVisitor<TResultType>>(visitor, "visitor").Visit(this);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0003B4EC File Offset: 0x000396EC
		public override bool Equals(QueryExpression other)
		{
			bool flag;
			QueryExtensionFunctionExpression queryExtensionFunctionExpression;
			if (QueryExpression.CheckReferenceAndTypeEquality<QueryExtensionFunctionExpression>(this, other, out flag, out queryExtensionFunctionExpression))
			{
				return flag;
			}
			return this.FunctionName.Equals(queryExtensionFunctionExpression.FunctionName, StringComparison.Ordinal) && this.Arguments.SequenceEqual(queryExtensionFunctionExpression.Arguments, QueryExpression.Comparer) && this.ResultColumnLineage.SequenceEqual(queryExtensionFunctionExpression.ResultColumnLineage);
		}

		// Token: 0x04000B3E RID: 2878
		private readonly string _functionName;

		// Token: 0x04000B3F RID: 2879
		private readonly IReadOnlyList<QueryExpression> _arguments;

		// Token: 0x04000B40 RID: 2880
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Name", "Index" })]
		private readonly IReadOnlyList<global::System.ValueTuple<string, int>> _resultColumnLineage;
	}
}
