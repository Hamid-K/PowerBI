using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Linq.JsonPath
{
	// Token: 0x020000D0 RID: 208
	internal class ArrayIndexFilter : PathFilter
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0002F649 File Offset: 0x0002D849
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x0002F651 File Offset: 0x0002D851
		public int? Index { get; set; }

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002F65A File Offset: 0x0002D85A
		[NullableContext(1)]
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			foreach (JToken jtoken in current)
			{
				if (this.Index != null)
				{
					JToken tokenIndex = PathFilter.GetTokenIndex(jtoken, settings, this.Index.GetValueOrDefault());
					if (tokenIndex != null)
					{
						yield return tokenIndex;
					}
				}
				else if (jtoken is JArray || jtoken is JConstructor)
				{
					foreach (JToken jtoken2 in ((IEnumerable<JToken>)jtoken))
					{
						yield return jtoken2;
					}
					IEnumerator<JToken> enumerator2 = null;
				}
				else if (settings != null && settings.ErrorWhenNoMatch)
				{
					throw new JsonException("Index * not valid on {0}.".FormatWith(CultureInfo.InvariantCulture, jtoken.GetType().Name));
				}
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}
	}
}
