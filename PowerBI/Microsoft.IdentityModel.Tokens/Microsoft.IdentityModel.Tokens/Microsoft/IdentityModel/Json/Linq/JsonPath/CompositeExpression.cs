using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Linq.JsonPath
{
	// Token: 0x020000D9 RID: 217
	[NullableContext(1)]
	[Nullable(0)]
	internal class CompositeExpression : QueryExpression
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x00030A2F File Offset: 0x0002EC2F
		// (set) Token: 0x06000C1A RID: 3098 RVA: 0x00030A37 File Offset: 0x0002EC37
		public List<QueryExpression> Expressions { get; set; }

		// Token: 0x06000C1B RID: 3099 RVA: 0x00030A40 File Offset: 0x0002EC40
		public CompositeExpression(QueryOperator @operator)
			: base(@operator)
		{
			this.Expressions = new List<QueryExpression>();
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00030A54 File Offset: 0x0002EC54
		public override bool IsMatch(JToken root, JToken t, [Nullable(2)] JsonSelectSettings settings)
		{
			QueryOperator @operator = this.Operator;
			if (@operator == QueryOperator.And)
			{
				using (List<QueryExpression>.Enumerator enumerator = this.Expressions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.IsMatch(root, t, settings))
						{
							return false;
						}
					}
				}
				return true;
			}
			if (@operator != QueryOperator.Or)
			{
				throw new ArgumentOutOfRangeException();
			}
			using (List<QueryExpression>.Enumerator enumerator = this.Expressions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsMatch(root, t, settings))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
