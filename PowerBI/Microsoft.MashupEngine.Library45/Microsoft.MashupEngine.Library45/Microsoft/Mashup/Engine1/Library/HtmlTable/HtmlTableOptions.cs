using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.HtmlTable
{
	// Token: 0x02000039 RID: 57
	internal class HtmlTableOptions
	{
		// Token: 0x06000133 RID: 307 RVA: 0x00006C5F File Offset: 0x00004E5F
		public HtmlTableOptions(OptionsRecord options)
		{
			this.options = options;
			this.options.TryGetString("RowSelector", out this.rowSelector);
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00006C85 File Offset: 0x00004E85
		public string RowSelector
		{
			get
			{
				return this.rowSelector;
			}
		}

		// Token: 0x040000D5 RID: 213
		private readonly OptionsRecord options;

		// Token: 0x040000D6 RID: 214
		private readonly string rowSelector;
	}
}
