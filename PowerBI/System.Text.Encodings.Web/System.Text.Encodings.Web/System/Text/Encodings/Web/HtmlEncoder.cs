using System;
using System.Runtime.CompilerServices;
using System.Text.Unicode;

namespace System.Text.Encodings.Web
{
	// Token: 0x02000029 RID: 41
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class HtmlEncoder : TextEncoder
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00005809 File Offset: 0x00003A09
		public static HtmlEncoder Default
		{
			get
			{
				return DefaultHtmlEncoder.BasicLatinSingleton;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005810 File Offset: 0x00003A10
		public static HtmlEncoder Create(TextEncoderSettings settings)
		{
			return new DefaultHtmlEncoder(settings);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005818 File Offset: 0x00003A18
		public static HtmlEncoder Create(params UnicodeRange[] allowedRanges)
		{
			return new DefaultHtmlEncoder(new TextEncoderSettings(allowedRanges));
		}
	}
}
