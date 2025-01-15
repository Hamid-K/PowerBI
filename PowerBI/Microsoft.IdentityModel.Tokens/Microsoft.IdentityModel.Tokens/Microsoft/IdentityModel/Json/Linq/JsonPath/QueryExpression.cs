using System;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Linq.JsonPath
{
	// Token: 0x020000D8 RID: 216
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class QueryExpression
	{
		// Token: 0x06000C16 RID: 3094 RVA: 0x00030A15 File Offset: 0x0002EC15
		public QueryExpression(QueryOperator @operator)
		{
			this.Operator = @operator;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00030A24 File Offset: 0x0002EC24
		public bool IsMatch(JToken root, JToken t)
		{
			return this.IsMatch(root, t, null);
		}

		// Token: 0x06000C18 RID: 3096
		public abstract bool IsMatch(JToken root, JToken t, [Nullable(2)] JsonSelectSettings settings);

		// Token: 0x040003E3 RID: 995
		internal QueryOperator Operator;
	}
}
