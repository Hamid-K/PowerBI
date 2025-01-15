using System;

namespace Microsoft.Mashup.Engine1.Library.Lines
{
	// Token: 0x020009CE RID: 2510
	internal interface ILineReader : IDisposable
	{
		// Token: 0x0600476A RID: 18282
		bool MoveNext();

		// Token: 0x0600476B RID: 18283
		string GetLine();

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x0600476C RID: 18284
		char[] Text { get; }

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x0600476D RID: 18285
		int LineStart { get; }

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x0600476E RID: 18286
		int LineEnd { get; }

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x0600476F RID: 18287
		bool HasQuotes { get; }
	}
}
