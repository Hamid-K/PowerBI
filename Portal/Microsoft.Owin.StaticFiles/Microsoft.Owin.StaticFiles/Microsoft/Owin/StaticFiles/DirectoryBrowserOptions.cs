using System;
using Microsoft.Owin.StaticFiles.DirectoryFormatters;
using Microsoft.Owin.StaticFiles.Infrastructure;

namespace Microsoft.Owin.StaticFiles
{
	// Token: 0x0200000B RID: 11
	public class DirectoryBrowserOptions : SharedOptionsBase<DirectoryBrowserOptions>
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002656 File Offset: 0x00000856
		public DirectoryBrowserOptions()
			: this(new SharedOptions())
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002663 File Offset: 0x00000863
		public DirectoryBrowserOptions(SharedOptions sharedOptions)
			: base(sharedOptions)
		{
			this.Formatter = new HtmlDirectoryFormatter();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002677 File Offset: 0x00000877
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000267F File Offset: 0x0000087F
		public IDirectoryFormatter Formatter { get; set; }
	}
}
