using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200014F RID: 335
	internal sealed class ParameterAliasValueAccessor
	{
		// Token: 0x06000EC5 RID: 3781 RVA: 0x0002ABD5 File Offset: 0x00028DD5
		public ParameterAliasValueAccessor(IDictionary<string, string> parameterAliasValueExpressions)
		{
			ExceptionUtils.CheckArgumentNotNull<IDictionary<string, string>>(parameterAliasValueExpressions, "parameterAliasValueExpressions");
			this.ParameterAliasValueExpressions = new Dictionary<string, string>(parameterAliasValueExpressions, StringComparer.Ordinal);
			this.ParameterAliasValueNodesCached = new Dictionary<string, SingleValueNode>(StringComparer.Ordinal);
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x0002AC0A File Offset: 0x00028E0A
		// (set) Token: 0x06000EC7 RID: 3783 RVA: 0x0002AC12 File Offset: 0x00028E12
		public IDictionary<string, SingleValueNode> ParameterAliasValueNodesCached { get; private set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x0002AC1B File Offset: 0x00028E1B
		// (set) Token: 0x06000EC9 RID: 3785 RVA: 0x0002AC23 File Offset: 0x00028E23
		internal IDictionary<string, string> ParameterAliasValueExpressions { get; private set; }

		// Token: 0x06000ECA RID: 3786 RVA: 0x0002AC2C File Offset: 0x00028E2C
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
