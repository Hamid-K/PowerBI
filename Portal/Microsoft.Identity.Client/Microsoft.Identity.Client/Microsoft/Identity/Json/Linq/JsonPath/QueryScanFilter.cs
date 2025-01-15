using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Linq.JsonPath
{
	// Token: 0x020000DB RID: 219
	internal class QueryScanFilter : PathFilter
	{
		// Token: 0x06000C19 RID: 3097 RVA: 0x00030817 File Offset: 0x0002EA17
		public QueryScanFilter(QueryExpression expression)
		{
			this.Expression = expression;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00030826 File Offset: 0x0002EA26
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			foreach (JToken jtoken in current)
			{
				JContainer jcontainer = jtoken as JContainer;
				if (jcontainer != null)
				{
					foreach (JToken jtoken2 in jcontainer.DescendantsAndSelf())
					{
						if (this.Expression.IsMatch(root, jtoken2, settings))
						{
							yield return jtoken2;
						}
					}
					IEnumerator<JToken> enumerator2 = null;
				}
				else if (this.Expression.IsMatch(root, jtoken, settings))
				{
					yield return jtoken;
				}
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040003CC RID: 972
		internal QueryExpression Expression;
	}
}
