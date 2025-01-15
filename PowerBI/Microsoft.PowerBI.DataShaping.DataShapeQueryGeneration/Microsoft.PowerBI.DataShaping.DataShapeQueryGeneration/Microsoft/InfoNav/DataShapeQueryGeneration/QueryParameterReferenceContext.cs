using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000B8 RID: 184
	internal sealed class QueryParameterReferenceContext
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x0001931A File Offset: 0x0001751A
		internal QueryParameterReferenceContext(IReadOnlyDictionary<string, IntermediateQueryParameter> parametersByName)
		{
			this._parametersByName = parametersByName;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00019329 File Offset: 0x00017529
		internal int Count
		{
			get
			{
				return this._parametersByName.Count;
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00019336 File Offset: 0x00017536
		internal bool TryGetParameter(ResolvedQueryParameterRefExpression expression, out IntermediateQueryParameter parameter)
		{
			return this.TryGetParameter(expression.Declaration.Name, out parameter);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001934A File Offset: 0x0001754A
		internal bool TryGetParameter(string queryName, out IntermediateQueryParameter parameter)
		{
			return this._parametersByName.TryGetValue(queryName, out parameter);
		}

		// Token: 0x04000391 RID: 913
		internal static readonly QueryParameterReferenceContext Empty = new QueryParameterReferenceContext(Util.EmptyReadOnlyDictionary<string, IntermediateQueryParameter>());

		// Token: 0x04000392 RID: 914
		private readonly IReadOnlyDictionary<string, IntermediateQueryParameter> _parametersByName;
	}
}
