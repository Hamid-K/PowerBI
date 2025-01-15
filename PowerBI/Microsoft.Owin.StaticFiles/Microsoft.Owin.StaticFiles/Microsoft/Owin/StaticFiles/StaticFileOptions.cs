using System;
using Microsoft.Owin.StaticFiles.ContentTypes;
using Microsoft.Owin.StaticFiles.Infrastructure;

namespace Microsoft.Owin.StaticFiles
{
	// Token: 0x02000013 RID: 19
	public class StaticFileOptions : SharedOptionsBase<StaticFileOptions>
	{
		// Token: 0x06000057 RID: 87 RVA: 0x000033E7 File Offset: 0x000015E7
		public StaticFileOptions()
			: this(new SharedOptions())
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000033F4 File Offset: 0x000015F4
		public StaticFileOptions(SharedOptions sharedOptions)
			: base(sharedOptions)
		{
			this.ContentTypeProvider = new FileExtensionContentTypeProvider();
			this.OnPrepareResponse = delegate(StaticFileResponseContext _)
			{
			};
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000342D File Offset: 0x0000162D
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003435 File Offset: 0x00001635
		public IContentTypeProvider ContentTypeProvider { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000343E File Offset: 0x0000163E
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003446 File Offset: 0x00001646
		public string DefaultContentType { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000344F File Offset: 0x0000164F
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003457 File Offset: 0x00001657
		public bool ServeUnknownFileTypes { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003460 File Offset: 0x00001660
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003468 File Offset: 0x00001668
		public Action<StaticFileResponseContext> OnPrepareResponse { get; set; }
	}
}
