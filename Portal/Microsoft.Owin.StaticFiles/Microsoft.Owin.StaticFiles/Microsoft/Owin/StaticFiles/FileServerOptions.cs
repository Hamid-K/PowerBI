using System;
using Microsoft.Owin.StaticFiles.Infrastructure;

namespace Microsoft.Owin.StaticFiles
{
	// Token: 0x0200000C RID: 12
	public class FileServerOptions : SharedOptionsBase<FileServerOptions>
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002688 File Offset: 0x00000888
		public FileServerOptions()
			: base(new SharedOptions())
		{
			this.StaticFileOptions = new StaticFileOptions(base.SharedOptions);
			this.DirectoryBrowserOptions = new DirectoryBrowserOptions(base.SharedOptions);
			this.DefaultFilesOptions = new DefaultFilesOptions(base.SharedOptions);
			this.EnableDefaultFiles = true;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000026DA File Offset: 0x000008DA
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000026E2 File Offset: 0x000008E2
		public StaticFileOptions StaticFileOptions { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000026EB File Offset: 0x000008EB
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000026F3 File Offset: 0x000008F3
		public DirectoryBrowserOptions DirectoryBrowserOptions { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000026FC File Offset: 0x000008FC
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002704 File Offset: 0x00000904
		public DefaultFilesOptions DefaultFilesOptions { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000270D File Offset: 0x0000090D
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002715 File Offset: 0x00000915
		public bool EnableDirectoryBrowsing { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000271E File Offset: 0x0000091E
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002726 File Offset: 0x00000926
		public bool EnableDefaultFiles { get; set; }
	}
}
