using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000126 RID: 294
	public sealed class ODataMediaTypeFormat
	{
		// Token: 0x06000B1D RID: 2845 RVA: 0x00029084 File Offset: 0x00027284
		public ODataMediaTypeFormat(ODataMediaType mediaType, ODataFormat format)
		{
			this.MediaType = mediaType;
			this.Format = format;
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0002909A File Offset: 0x0002729A
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x000290A2 File Offset: 0x000272A2
		public ODataMediaType MediaType { get; internal set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x000290AB File Offset: 0x000272AB
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x000290B3 File Offset: 0x000272B3
		public ODataFormat Format { get; internal set; }
	}
}
