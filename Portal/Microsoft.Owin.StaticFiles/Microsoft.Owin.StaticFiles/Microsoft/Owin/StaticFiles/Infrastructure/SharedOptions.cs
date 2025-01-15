using System;
using Microsoft.Owin.FileSystems;

namespace Microsoft.Owin.StaticFiles.Infrastructure
{
	// Token: 0x02000017 RID: 23
	public class SharedOptions
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00003A17 File Offset: 0x00001C17
		public SharedOptions()
		{
			this.RequestPath = PathString.Empty;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003A2A File Offset: 0x00001C2A
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003A32 File Offset: 0x00001C32
		public PathString RequestPath
		{
			get
			{
				return this._requestPath;
			}
			set
			{
				if (value.HasValue && value.Value.EndsWith("/", StringComparison.Ordinal))
				{
					throw new ArgumentException("Request path must not end in a slash");
				}
				this._requestPath = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003A63 File Offset: 0x00001C63
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003A6B File Offset: 0x00001C6B
		public IFileSystem FileSystem { get; set; }

		// Token: 0x0400004D RID: 77
		private PathString _requestPath;
	}
}
