using System;

namespace Microsoft.ReportingServices.Portal.Interfaces.SoapProxy
{
	// Token: 0x02000089 RID: 137
	public class RenderResults
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00004A39 File Offset: 0x00002C39
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x00004A41 File Offset: 0x00002C41
		public byte[] ByteContents { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00004A4A File Offset: 0x00002C4A
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x00004A52 File Offset: 0x00002C52
		public string Extension { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00004A5B File Offset: 0x00002C5B
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x00004A63 File Offset: 0x00002C63
		public string MimeType { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00004A6C File Offset: 0x00002C6C
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x00004A74 File Offset: 0x00002C74
		public string Encoding { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00004A7D File Offset: 0x00002C7D
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x00004A85 File Offset: 0x00002C85
		public string[] StreamIds { get; set; }
	}
}
