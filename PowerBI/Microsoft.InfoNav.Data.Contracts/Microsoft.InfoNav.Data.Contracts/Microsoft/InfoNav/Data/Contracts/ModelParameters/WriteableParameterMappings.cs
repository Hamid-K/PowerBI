using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.ModelParameters
{
	// Token: 0x020000EC RID: 236
	public sealed class WriteableParameterMappings : ParameterMappings
	{
		// Token: 0x06000639 RID: 1593 RVA: 0x0000D048 File Offset: 0x0000B248
		public void AddMapping(ParameterMapping parameterMapping)
		{
			this.AddMapping(parameterMapping.ParameterName, parameterMapping.Values, parameterMapping.IsListType, parameterMapping.IsSelectAllFilter);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000D068 File Offset: 0x0000B268
		internal void AddMapping(string mappedParameterName, ISet<ResolvedQueryLiteralExpression> resolvedQueryLiteralExpressions, bool isListType, bool isSelectAllFilter)
		{
			ParameterMapping parameterMapping;
			if (this._parameterMappings.TryGetValue(mappedParameterName, out parameterMapping))
			{
				parameterMapping.IntersectWith(resolvedQueryLiteralExpressions, isSelectAllFilter);
				return;
			}
			this._parameterMappings.Add(mappedParameterName, new ParameterMapping(mappedParameterName, resolvedQueryLiteralExpressions, isListType, isSelectAllFilter));
		}
	}
}
