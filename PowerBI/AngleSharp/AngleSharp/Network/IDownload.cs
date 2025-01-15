using System;
using AngleSharp.Dom;

namespace AngleSharp.Network
{
	// Token: 0x02000091 RID: 145
	public interface IDownload : ICancellable<IResponse>, ICancellable
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600046C RID: 1132
		Url Target { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600046D RID: 1133
		INode Originator { get; }
	}
}
