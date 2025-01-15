using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;
using NLog.Layouts;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000F7 RID: 247
	[LayoutRenderer("norawvalue")]
	[AmbientProperty("NoRawValue")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class NoRawValueLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00022D11 File Offset: 0x00020F11
		// (set) Token: 0x06000DDF RID: 3551 RVA: 0x00022D19 File Offset: 0x00020F19
		[DefaultValue(true)]
		public bool NoRawValue { get; set; } = true;

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00022D22 File Offset: 0x00020F22
		protected override void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
		{
			Layout inner = base.Inner;
			if (inner == null)
			{
				return;
			}
			inner.RenderAppendBuilder(logEvent, builder, false);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00022D37 File Offset: 0x00020F37
		protected override string Transform(string text)
		{
			throw new NotSupportedException();
		}
	}
}
