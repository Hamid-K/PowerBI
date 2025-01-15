using System;
using AngleSharp.Dom.Css;

namespace AngleSharp.Services.Styling
{
	// Token: 0x0200003A RID: 58
	public interface ICssStyleEngine : IStyleEngine
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000142 RID: 322
		ICssStyleSheet Default { get; }

		// Token: 0x06000143 RID: 323
		ICssStyleDeclaration ParseDeclaration(string source, StyleOptions options);

		// Token: 0x06000144 RID: 324
		IMediaList ParseMedia(string source, StyleOptions options);
	}
}
