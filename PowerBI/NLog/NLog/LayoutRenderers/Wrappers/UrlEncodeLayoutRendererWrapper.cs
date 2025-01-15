using System;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000102 RID: 258
	[LayoutRenderer("url-encode")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class UrlEncodeLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x06000E2A RID: 3626 RVA: 0x0002352D File Offset: 0x0002172D
		public UrlEncodeLayoutRendererWrapper()
		{
			this.SpaceAsPlus = true;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x0002353C File Offset: 0x0002173C
		// (set) Token: 0x06000E2C RID: 3628 RVA: 0x00023544 File Offset: 0x00021744
		public bool SpaceAsPlus { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0002354D File Offset: 0x0002174D
		// (set) Token: 0x06000E2E RID: 3630 RVA: 0x00023555 File Offset: 0x00021755
		public bool EscapeDataRfc3986 { get; set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0002355E File Offset: 0x0002175E
		// (set) Token: 0x06000E30 RID: 3632 RVA: 0x00023566 File Offset: 0x00021766
		public bool EscapeDataNLogLegacy { get; set; }

		// Token: 0x06000E31 RID: 3633 RVA: 0x00023570 File Offset: 0x00021770
		protected override string Transform(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				UrlHelper.EscapeEncodingOptions uriStringEncodingFlags = UrlHelper.GetUriStringEncodingFlags(this.EscapeDataNLogLegacy, this.SpaceAsPlus, this.EscapeDataRfc3986);
				StringBuilder stringBuilder = new StringBuilder(text.Length + 20);
				UrlHelper.EscapeDataEncode(text, stringBuilder, uriStringEncodingFlags);
				return stringBuilder.ToString();
			}
			return string.Empty;
		}
	}
}
