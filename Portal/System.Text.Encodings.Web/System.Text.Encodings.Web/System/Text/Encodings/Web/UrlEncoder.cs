using System;
using System.Runtime.CompilerServices;
using System.Text.Unicode;

namespace System.Text.Encodings.Web
{
	// Token: 0x02000030 RID: 48
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class UrlEncoder : TextEncoder
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00006371 File Offset: 0x00004571
		public static UrlEncoder Default
		{
			get
			{
				return DefaultUrlEncoder.BasicLatinSingleton;
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006378 File Offset: 0x00004578
		public static UrlEncoder Create(TextEncoderSettings settings)
		{
			return new DefaultUrlEncoder(settings);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006380 File Offset: 0x00004580
		public static UrlEncoder Create(params UnicodeRange[] allowedRanges)
		{
			return new DefaultUrlEncoder(new TextEncoderSettings(allowedRanges));
		}
	}
}
