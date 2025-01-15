using System;

namespace Microsoft.OData
{
	// Token: 0x02000024 RID: 36
	public sealed class ODataMediaTypeFormat
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00005052 File Offset: 0x00003252
		public ODataMediaTypeFormat(ODataMediaType mediaType, ODataFormat format)
		{
			this.MediaType = mediaType;
			this.Format = format;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005068 File Offset: 0x00003268
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00005070 File Offset: 0x00003270
		public ODataMediaType MediaType { get; internal set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00005079 File Offset: 0x00003279
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00005081 File Offset: 0x00003281
		public ODataFormat Format { get; internal set; }
	}
}
