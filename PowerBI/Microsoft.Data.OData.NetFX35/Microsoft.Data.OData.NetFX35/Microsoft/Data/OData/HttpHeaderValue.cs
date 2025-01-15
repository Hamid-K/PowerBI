using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData
{
	// Token: 0x02000122 RID: 290
	internal sealed class HttpHeaderValue : Dictionary<string, HttpHeaderValueElement>
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x00019705 File Offset: 0x00017905
		internal HttpHeaderValue()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001971C File Offset: 0x0001791C
		public override string ToString()
		{
			if (base.Count != 0)
			{
				return string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<HttpHeaderValueElement, string>(base.Values, (HttpHeaderValueElement element) => element.ToString())));
			}
			return null;
		}
	}
}
