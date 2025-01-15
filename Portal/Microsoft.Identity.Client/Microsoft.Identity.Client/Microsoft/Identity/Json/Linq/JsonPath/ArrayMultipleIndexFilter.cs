using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Linq.JsonPath
{
	// Token: 0x020000D0 RID: 208
	internal class ArrayMultipleIndexFilter : PathFilter
	{
		// Token: 0x06000BDF RID: 3039 RVA: 0x0002EF30 File Offset: 0x0002D130
		public ArrayMultipleIndexFilter(List<int> indexes)
		{
			this.Indexes = indexes;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002EF3F File Offset: 0x0002D13F
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			foreach (JToken t in current)
			{
				foreach (int num in this.Indexes)
				{
					JToken tokenIndex = PathFilter.GetTokenIndex(t, settings, num);
					if (tokenIndex != null)
					{
						yield return tokenIndex;
					}
				}
				List<int>.Enumerator enumerator2 = default(List<int>.Enumerator);
				t = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040003AF RID: 943
		internal List<int> Indexes;
	}
}
