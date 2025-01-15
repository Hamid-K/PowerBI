using System;
using AngleSharp.Services.Styling;

namespace AngleSharp.Services
{
	// Token: 0x02000039 RID: 57
	public interface IStylingProvider
	{
		// Token: 0x06000141 RID: 321
		IStyleEngine GetEngine(string mimeType);
	}
}
