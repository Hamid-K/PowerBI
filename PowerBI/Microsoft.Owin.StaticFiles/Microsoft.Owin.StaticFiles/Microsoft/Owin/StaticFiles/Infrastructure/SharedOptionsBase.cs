using System;
using Microsoft.Owin.FileSystems;

namespace Microsoft.Owin.StaticFiles.Infrastructure
{
	// Token: 0x02000018 RID: 24
	public abstract class SharedOptionsBase<T>
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003A74 File Offset: 0x00001C74
		protected SharedOptionsBase(SharedOptions sharedOptions)
		{
			if (sharedOptions == null)
			{
				throw new ArgumentNullException("sharedOptions");
			}
			this.SharedOptions = sharedOptions;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003A91 File Offset: 0x00001C91
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003A99 File Offset: 0x00001C99
		private protected SharedOptions SharedOptions { protected get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003AA2 File Offset: 0x00001CA2
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003AAF File Offset: 0x00001CAF
		public PathString RequestPath
		{
			get
			{
				return this.SharedOptions.RequestPath;
			}
			set
			{
				this.SharedOptions.RequestPath = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003ABD File Offset: 0x00001CBD
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003ACA File Offset: 0x00001CCA
		public IFileSystem FileSystem
		{
			get
			{
				return this.SharedOptions.FileSystem;
			}
			set
			{
				this.SharedOptions.FileSystem = value;
			}
		}
	}
}
