using System;
using System.IO;

namespace AngleSharp
{
	// Token: 0x02000010 RID: 16
	public interface IMarkupFormattable
	{
		// Token: 0x0600006B RID: 107
		void ToHtml(TextWriter writer, IMarkupFormatter formatter);
	}
}
