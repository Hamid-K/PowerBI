using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData
{
	// Token: 0x02000036 RID: 54
	internal sealed class HttpHeaderValue : Dictionary<string, HttpHeaderValueElement>
	{
		// Token: 0x060001DF RID: 479 RVA: 0x0000535B File Offset: 0x0000355B
		internal HttpHeaderValue()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005368 File Offset: 0x00003568
		public override string ToString()
		{
			if (base.Count != 0)
			{
				return string.Join(",", base.Values.Select((HttpHeaderValueElement element) => element.ToString()).ToArray<string>());
			}
			return null;
		}
	}
}
