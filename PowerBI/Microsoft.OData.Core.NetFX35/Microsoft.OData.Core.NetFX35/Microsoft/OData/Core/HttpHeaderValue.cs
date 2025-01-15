using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Core
{
	// Token: 0x02000095 RID: 149
	internal sealed class HttpHeaderValue : Dictionary<string, HttpHeaderValueElement>
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x00014DD7 File Offset: 0x00012FD7
		internal HttpHeaderValue()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00014DEC File Offset: 0x00012FEC
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
