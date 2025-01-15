using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000A9 RID: 169
	internal sealed class RdmTextRun
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0000D351 File Offset: 0x0000B551
		internal RdmTextRun(string value, Style style)
		{
			this._style = style;
			this._value = value;
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000D367 File Offset: 0x0000B567
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000D36F File Offset: 0x0000B56F
		public Style Style
		{
			get
			{
				return this._style;
			}
		}

		// Token: 0x0400022B RID: 555
		private readonly string _value;

		// Token: 0x0400022C RID: 556
		private readonly Style _style;
	}
}
