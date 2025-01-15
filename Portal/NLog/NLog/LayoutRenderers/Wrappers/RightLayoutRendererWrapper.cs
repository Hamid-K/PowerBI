using System;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000FD RID: 253
	[LayoutRenderer("right")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class RightLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0002315D File Offset: 0x0002135D
		// (set) Token: 0x06000E07 RID: 3591 RVA: 0x00023165 File Offset: 0x00021365
		[RequiredParameter]
		public int Length { get; set; }

		// Token: 0x06000E08 RID: 3592 RVA: 0x00023170 File Offset: 0x00021370
		protected override void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
		{
			if (this.Length <= 0)
			{
				return;
			}
			base.Inner.RenderAppendBuilder(logEvent, builder, false);
			if (builder.Length - orgLength > this.Length)
			{
				string text = builder.ToString(builder.Length - this.Length, this.Length);
				builder.Length = orgLength;
				builder.Append(text);
			}
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x000231CE File Offset: 0x000213CE
		protected override string Transform(string text)
		{
			throw new NotSupportedException();
		}
	}
}
