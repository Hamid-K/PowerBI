using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1;

namespace Microsoft.Mashup.Engine.Host
{
	// Token: 0x02000210 RID: 528
	public static class Engines
	{
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00018D3B File Offset: 0x00016F3B
		public static IEngine Version1
		{
			get
			{
				return Engine.Instance;
			}
		}
	}
}
