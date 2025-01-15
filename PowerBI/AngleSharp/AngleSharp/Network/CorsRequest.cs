using System;
using AngleSharp.Services;

namespace AngleSharp.Network
{
	// Token: 0x0200008B RID: 139
	public class CorsRequest
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x0001BEE1 File Offset: 0x0001A0E1
		public CorsRequest(ResourceRequest request)
		{
			this.Request = request;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
		public ResourceRequest Request { get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0001BEF8 File Offset: 0x0001A0F8
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x0001BF00 File Offset: 0x0001A100
		public CorsSetting Setting { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0001BF09 File Offset: 0x0001A109
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0001BF11 File Offset: 0x0001A111
		public OriginBehavior Behavior { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0001BF1A File Offset: 0x0001A11A
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0001BF22 File Offset: 0x0001A122
		public IIntegrityProvider Integrity { get; set; }
	}
}
