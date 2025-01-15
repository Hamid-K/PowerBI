using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200019B RID: 411
	internal sealed class ParameterAliasValueAccessor
	{
		// Token: 0x060013D6 RID: 5078 RVA: 0x0003A9F1 File Offset: 0x00038BF1
		public ParameterAliasValueAccessor(IDictionary<string, string> parameterAliasValueExpressions)
		{
			ExceptionUtils.CheckArgumentNotNull<IDictionary<string, string>>(parameterAliasValueExpressions, "parameterAliasValueExpressions");
			this.ParameterAliasValueExpressions = new Dictionary<string, string>(parameterAliasValueExpressions, StringComparer.Ordinal);
			this.ParameterAliasValueNodesCached = new Dictionary<string, SingleValueNode>(StringComparer.Ordinal);
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0003AA26 File Offset: 0x00038C26
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x0003AA2E File Offset: 0x00038C2E
		public IDictionary<string, SingleValueNode> ParameterAliasValueNodesCached { get; private set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0003AA37 File Offset: 0x00038C37
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x0003AA3F File Offset: 0x00038C3F
		internal IDictionary<string, string> ParameterAliasValueExpressions { get; private set; }

		// Token: 0x060013DB RID: 5083 RVA: 0x0003AA48 File Offset: 0x00038C48
		public string GetAliasValueExpression(string alias)
		{
			string text = null;
			if (this.ParameterAliasValueExpressions.TryGetValue(alias, out text))
			{
				return text;
			}
			return null;
		}
	}
}
