using System;

namespace Microsoft.OData
{
	// Token: 0x0200004C RID: 76
	public sealed class ODataMediaTypeFormat
	{
		// Token: 0x06000266 RID: 614 RVA: 0x00007B77 File Offset: 0x00005D77
		public ODataMediaTypeFormat(ODataMediaType mediaType, ODataFormat format)
		{
			this.MediaType = mediaType;
			this.Format = format;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00007B8D File Offset: 0x00005D8D
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00007B95 File Offset: 0x00005D95
		public ODataMediaType MediaType { get; internal set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00007B9E File Offset: 0x00005D9E
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00007BA6 File Offset: 0x00005DA6
		public ODataFormat Format { get; internal set; }
	}
}
