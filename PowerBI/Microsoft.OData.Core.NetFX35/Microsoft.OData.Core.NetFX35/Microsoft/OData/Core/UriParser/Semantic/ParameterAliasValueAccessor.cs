using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000256 RID: 598
	internal sealed class ParameterAliasValueAccessor
	{
		// Token: 0x06001533 RID: 5427 RVA: 0x0004AEA2 File Offset: 0x000490A2
		public ParameterAliasValueAccessor(IDictionary<string, string> parameterAliasValueExpressions)
		{
			ExceptionUtils.CheckArgumentNotNull<IDictionary<string, string>>(parameterAliasValueExpressions, "parameterAliasValueExpressions");
			this.ParameterAliasValueExpressions = new Dictionary<string, string>(parameterAliasValueExpressions, StringComparer.Ordinal);
			this.ParameterAliasValueNodesCached = new Dictionary<string, SingleValueNode>(StringComparer.Ordinal);
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x0004AED6 File Offset: 0x000490D6
		// (set) Token: 0x06001535 RID: 5429 RVA: 0x0004AEDE File Offset: 0x000490DE
		public IDictionary<string, SingleValueNode> ParameterAliasValueNodesCached { get; private set; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x0004AEE7 File Offset: 0x000490E7
		// (set) Token: 0x06001537 RID: 5431 RVA: 0x0004AEEF File Offset: 0x000490EF
		internal IDictionary<string, string> ParameterAliasValueExpressions { get; private set; }

		// Token: 0x06001538 RID: 5432 RVA: 0x0004AEF8 File Offset: 0x000490F8
		public string GetAliasValueExpression(string alias)
		{
			string text = null;
			if (this.ParameterAliasValueExpressions.TryGetValue(alias, ref text))
			{
				return text;
			}
			return null;
		}
	}
}
