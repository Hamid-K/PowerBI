using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000105 RID: 261
	[LayoutRenderer("wrapline")]
	[AmbientProperty("WrapLine")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class WrapLineLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x06000E46 RID: 3654 RVA: 0x0002381E File Offset: 0x00021A1E
		public WrapLineLayoutRendererWrapper()
		{
			this.WrapLine = 80;
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0002382E File Offset: 0x00021A2E
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x00023836 File Offset: 0x00021A36
		[DefaultValue(80)]
		public int WrapLine { get; set; }

		// Token: 0x06000E49 RID: 3657 RVA: 0x00023840 File Offset: 0x00021A40
		protected override string Transform(string text)
		{
			if (this.WrapLine <= 0)
			{
				return text;
			}
			int num = this.WrapLine;
			if (text.Length <= num)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Length + text.Length / num * Environment.NewLine.Length);
			for (int i = 0; i < text.Length; i += num)
			{
				if (num + i > text.Length)
				{
					num = text.Length - i;
				}
				stringBuilder.Append(text.Substring(i, num));
				if (num + i < text.Length)
				{
					stringBuilder.Append(Environment.NewLine);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
