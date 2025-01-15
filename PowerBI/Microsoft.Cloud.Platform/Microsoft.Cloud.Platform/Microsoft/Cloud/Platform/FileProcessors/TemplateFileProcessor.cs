using System;
using System.Text;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000F0 RID: 240
	public abstract class TemplateFileProcessor : BaseFileContentProcessor
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x00017EA2 File Offset: 0x000160A2
		protected TemplateFileProcessor(string placeholderStartMarker, string placeholderEndMarker, Encoding fileContentEncoding, CacheUsage cacheUsage)
			: base(cacheUsage)
		{
			this.PlaceholderStartMarker = placeholderStartMarker;
			this.PlaceholderEndMarker = placeholderEndMarker;
			this.FileContentEncoding = fileContentEncoding;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00017EC1 File Offset: 0x000160C1
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x00017EC9 File Offset: 0x000160C9
		private protected string PlaceholderStartMarker { protected get; private set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00017ED2 File Offset: 0x000160D2
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x00017EDA File Offset: 0x000160DA
		private protected string PlaceholderEndMarker { protected get; private set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00017EE3 File Offset: 0x000160E3
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x00017EEB File Offset: 0x000160EB
		private protected Encoding FileContentEncoding { protected get; private set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00017EF4 File Offset: 0x000160F4
		protected static Encoding DefaultFileContentEncoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}
	}
}
