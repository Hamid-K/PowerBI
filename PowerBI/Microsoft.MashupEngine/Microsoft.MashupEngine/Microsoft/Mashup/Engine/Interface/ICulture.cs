using System;
using System.Globalization;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000037 RID: 55
	public interface ICulture
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600012D RID: 301
		string Name { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600012E RID: 302
		CultureInfo Value { get; }
	}
}
