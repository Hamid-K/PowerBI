using System;
using AngleSharp.Dom;

namespace AngleSharp.Network
{
	// Token: 0x0200009C RID: 156
	public class ResourceRequest
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x0001E6E4 File Offset: 0x0001C8E4
		public ResourceRequest(IElement source, Url target)
		{
			this.Source = source;
			this.Target = target;
			this.Origin = source.Owner.Origin;
			this.IsManualRedirectDesired = false;
			this.IsSameOriginForced = false;
			this.IsCookieBlocked = false;
			this.IsCredentialOmitted = false;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0001E732 File Offset: 0x0001C932
		public IElement Source { get; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0001E73A File Offset: 0x0001C93A
		public Url Target { get; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0001E742 File Offset: 0x0001C942
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x0001E74A File Offset: 0x0001C94A
		public string Origin { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0001E753 File Offset: 0x0001C953
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x0001E75B File Offset: 0x0001C95B
		public bool IsManualRedirectDesired { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0001E764 File Offset: 0x0001C964
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x0001E76C File Offset: 0x0001C96C
		public bool IsSameOriginForced { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0001E775 File Offset: 0x0001C975
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x0001E77D File Offset: 0x0001C97D
		public bool IsCredentialOmitted { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0001E786 File Offset: 0x0001C986
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x0001E78E File Offset: 0x0001C98E
		public bool IsCookieBlocked { get; set; }
	}
}
