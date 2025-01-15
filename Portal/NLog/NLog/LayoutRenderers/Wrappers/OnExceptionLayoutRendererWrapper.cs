using System;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000F8 RID: 248
	[LayoutRenderer("onexception")]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class OnExceptionLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x06000DE3 RID: 3555 RVA: 0x00022D4D File Offset: 0x00020F4D
		protected override void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
		{
			if (logEvent.Exception != null)
			{
				base.Inner.RenderAppendBuilder(logEvent, builder, false);
			}
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00022D65 File Offset: 0x00020F65
		protected override string Transform(string text)
		{
			return text;
		}
	}
}
