using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Linq.JsonPath
{
	// Token: 0x020000D8 RID: 216
	internal class CompositeExpression : QueryExpression
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x000302DF File Offset: 0x0002E4DF
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x000302E7 File Offset: 0x0002E4E7
		public List<QueryExpression> Expressions { get; set; }

		// Token: 0x06000C0E RID: 3086 RVA: 0x000302F0 File Offset: 0x0002E4F0
		public CompositeExpression(QueryOperator @operator)
			: base(@operator)
		{
			this.Expressions = new List<QueryExpression>();
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00030304 File Offset: 0x0002E504
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
