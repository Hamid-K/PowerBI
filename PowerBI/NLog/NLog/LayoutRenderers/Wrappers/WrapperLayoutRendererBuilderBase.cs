using System;
using System.Text;
using NLog.Internal;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000107 RID: 263
	public abstract class WrapperLayoutRendererBuilderBase : WrapperLayoutRendererBase
	{
		// Token: 0x06000E53 RID: 3667 RVA: 0x000239B0 File Offset: 0x00021BB0
		protected override void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
		{
			using (AppendBuilderCreator appendBuilderCreator = new AppendBuilderCreator(builder, true))
			{
				this.RenderFormattedMessage(logEvent, appendBuilderCreator.Builder);
				this.TransformFormattedMesssage(logEvent, appendBuilderCreator.Builder);
			}
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00023A04 File Offset: 0x00021C04
		[Obsolete("Inherit from WrapperLayoutRendererBase and override RenderInnerAndTransform() instead. Marked obsolete in NLog 4.6")]
		protected virtual void TransformFormattedMesssage(LogEventInfo logEvent, StringBuilder target)
		{
			this.TransformFormattedMesssage(target);
		}

		// Token: 0x06000E55 RID: 3669
		[Obsolete("Inherit from WrapperLayoutRendererBase and override RenderInnerAndTransform() instead. Marked obsolete in NLog 4.6")]
		protected abstract void TransformFormattedMesssage(StringBuilder target);

		// Token: 0x06000E56 RID: 3670 RVA: 0x00023A0D File Offset: 0x00021C0D
		[Obsolete("Inherit from WrapperLayoutRendererBase and override RenderInnerAndTransform() instead. Marked obsolete in NLog 4.6")]
		protected virtual void RenderFormattedMessage(LogEventInfo logEvent, StringBuilder target)
		{
			base.Inner.RenderAppendBuilder(logEvent, target, false);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00023A1D File Offset: 0x00021C1D
		protected sealed override string Transform(string text)
		{
			throw new NotSupportedException("Use TransformFormattedMesssage");
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00023A29 File Offset: 0x00021C29
		protected sealed override string RenderInner(LogEventInfo logEvent)
		{
			throw new NotSupportedException("Use RenderFormattedMessage");
		}
	}
}
