using System;

namespace Microsoft.Mashup.Engine1.Library.Uris.Internal
{
	// Token: 0x020002C6 RID: 710
	internal class UriSectionInternal
	{
		// Token: 0x06001C2C RID: 7212 RVA: 0x00043566 File Offset: 0x00041766
		public static UriSectionInternal GetSection()
		{
			return new UriSectionInternal();
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x00002105 File Offset: 0x00000305
		public UriIdnScope IdnScope
		{
			get
			{
				return UriIdnScope.None;
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06001C2E RID: 7214 RVA: 0x00002105 File Offset: 0x00000305
		public bool IriParsing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x000020FA File Offset: 0x000002FA
		public SchemeSettingInternal GetSchemeSetting(string scheme)
		{
			return null;
		}
	}
}
