using System;
using System.IO;

namespace AngleSharp
{
	// Token: 0x02000012 RID: 18
	public interface IStyleFormattable
	{
		// Token: 0x06000073 RID: 115
		void ToCss(TextWriter writer, IStyleFormatter formatter);
	}
}
