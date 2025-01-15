using System;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Network;

namespace AngleSharp.Services.Styling
{
	// Token: 0x0200003B RID: 59
	public interface IStyleEngine
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000145 RID: 325
		string Type { get; }

		// Token: 0x06000146 RID: 326
		Task<IStyleSheet> ParseStylesheetAsync(IResponse response, StyleOptions options, CancellationToken cancel);
	}
}
