using System;
using AngleSharp.Attributes;
using AngleSharp.Network;

namespace AngleSharp.Dom
{
	// Token: 0x0200018D RID: 397
	[DomNoInterfaceObject]
	public interface ILoadableElement
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000E48 RID: 3656
		IDownload CurrentDownload { get; }
	}
}
