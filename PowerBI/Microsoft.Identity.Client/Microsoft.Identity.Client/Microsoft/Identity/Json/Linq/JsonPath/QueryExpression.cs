using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Linq.JsonPath
{
	// Token: 0x020000D7 RID: 215
	internal abstract class QueryExpression
	{
		// Token: 0x06000C09 RID: 3081 RVA: 0x000302C5 File Offset: 0x0002E4C5
		public QueryExpression(QueryOperator @operator)
		{
			this.Operator = @operator;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x000302D4 File Offset: 0x0002E4D4
		public bool IsMatch(JToken root, JToken t)
		{
			return this.IsMatch(root, t, null);
		}

		// Token: 0x06000C0B RID: 3083
		public abstract bool IsMatch(JToken root, JToken t, [Nullable(2)] JsonSelectSettings settings);

		// Token: 0x040003C7 RID: 967
		internal QueryOperator Operator;
	}
}
