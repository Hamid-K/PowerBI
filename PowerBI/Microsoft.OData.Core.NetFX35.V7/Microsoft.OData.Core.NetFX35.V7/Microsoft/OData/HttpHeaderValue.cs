using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData
{
	// Token: 0x0200000F RID: 15
	internal sealed class HttpHeaderValue : Dictionary<string, HttpHeaderValueElement>
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002E43 File Offset: 0x00001043
		internal HttpHeaderValue()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002E50 File Offset: 0x00001050
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
