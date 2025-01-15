using System;
using Microsoft.Owin.FileSystems;

namespace Microsoft.Owin.StaticFiles
{
	// Token: 0x02000014 RID: 20
	public class StaticFileResponseContext
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003471 File Offset: 0x00001671
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003479 File Offset: 0x00001679
		public IOwinContext OwinContext { get; internal set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003482 File Offset: 0x00001682
		// (set) Token: 0x06000064 RID: 100 RVA: 0x0000348A File Offset: 0x0000168A
		public IFileInfo File { get; internal set; }
	}
}
