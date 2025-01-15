using System;
using System.ComponentModel;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200011C RID: 284
	[ImmutableObject(true)]
	public sealed class InflectionResult
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x0000A9AF File Offset: 0x00008BAF
		public InflectionResult(string inflection, string adjunct = null)
		{
			this._inflection = inflection;
			this._adjunct = adjunct;
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000A9C5 File Offset: 0x00008BC5
		public string Inflection
		{
			get
			{
				return this._inflection;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0000A9CD File Offset: 0x00008BCD
		public string Adjunct
		{
			get
			{
				return this._adjunct;
			}
		}

		// Token: 0x040005D4 RID: 1492
		private readonly string _inflection;

		// Token: 0x040005D5 RID: 1493
		private readonly string _adjunct;
	}
}
