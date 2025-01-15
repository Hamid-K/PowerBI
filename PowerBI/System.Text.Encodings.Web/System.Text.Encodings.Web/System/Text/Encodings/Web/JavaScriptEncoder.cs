using System;
using System.Runtime.CompilerServices;
using System.Text.Unicode;

namespace System.Text.Encodings.Web
{
	// Token: 0x0200002B RID: 43
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JavaScriptEncoder : TextEncoder
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00005909 File Offset: 0x00003B09
		public static JavaScriptEncoder Default
		{
			get
			{
				return DefaultJavaScriptEncoder.BasicLatinSingleton;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00005910 File Offset: 0x00003B10
		public static JavaScriptEncoder UnsafeRelaxedJsonEscaping
		{
			get
			{
				return DefaultJavaScriptEncoder.UnsafeRelaxedEscapingSingleton;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005917 File Offset: 0x00003B17
		public static JavaScriptEncoder Create(TextEncoderSettings settings)
		{
			return new DefaultJavaScriptEncoder(settings);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000591F File Offset: 0x00003B1F
		public static JavaScriptEncoder Create(params UnicodeRange[] allowedRanges)
		{
			return new DefaultJavaScriptEncoder(new TextEncoderSettings(allowedRanges));
		}
	}
}
