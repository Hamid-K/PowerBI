using System;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D0 RID: 208
	[LayoutRenderer("literal")]
	[ThreadAgnostic]
	[ThreadSafe]
	[AppDomainFixedOutput]
	public class LiteralLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000C9B RID: 3227 RVA: 0x00020396 File Offset: 0x0001E596
		public LiteralLayoutRenderer()
		{
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0002039E File Offset: 0x0001E59E
		public LiteralLayoutRenderer(string text)
		{
			this.Text = text;
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x000203AD File Offset: 0x0001E5AD
		// (set) Token: 0x06000C9E RID: 3230 RVA: 0x000203B5 File Offset: 0x0001E5B5
		public string Text { get; set; }

		// Token: 0x06000C9F RID: 3231 RVA: 0x000203BE File Offset: 0x0001E5BE
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(this.Text);
		}
	}
}
