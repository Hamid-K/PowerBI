using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;

namespace Microsoft.Mashup.Engine1.Library.WebBrowserContents
{
	// Token: 0x0200026E RID: 622
	public sealed class WebBrowserContentsModule : Module45
	{
		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x00034C7B File Offset: 0x00032E7B
		public override string Name
		{
			get
			{
				return "WebBrowserContents";
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x00034C82 File Offset: 0x00032E82
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "Web.BrowserContents";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x040007A2 RID: 1954
		public const string WebBrowserContentsName = "Web.BrowserContents";

		// Token: 0x040007A3 RID: 1955
		private Keys exportKeys;

		// Token: 0x0200026F RID: 623
		private enum Exports
		{
			// Token: 0x040007A5 RID: 1957
			Web_BrowserContents,
			// Token: 0x040007A6 RID: 1958
			Count
		}
	}
}
