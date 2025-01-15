using System;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000106 RID: 262
	public abstract class WrapperLayoutRendererBase : LayoutRenderer
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x000238DB File Offset: 0x00021ADB
		// (set) Token: 0x06000E4B RID: 3659 RVA: 0x000238E3 File Offset: 0x00021AE3
		[DefaultParameter]
		public Layout Inner { get; set; }

		// Token: 0x06000E4C RID: 3660 RVA: 0x000238EC File Offset: 0x00021AEC
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			Layout inner = this.Inner;
			if (inner == null)
			{
				return;
			}
			inner.Initialize(base.LoggingConfiguration);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0002390C File Offset: 0x00021B0C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (this.Inner == null)
			{
				InternalLogger.Warn<WrapperLayoutRendererBase>("{0} has no configured Inner-Layout, so skipping", this);
				return;
			}
			int length = builder.Length;
			try
			{
				this.RenderInnerAndTransform(logEvent, builder, length);
			}
			catch
			{
				builder.Length = length;
				throw;
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0002395C File Offset: 0x00021B5C
		protected virtual void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
		{
			string text = this.RenderInner(logEvent);
			builder.Append(this.Transform(logEvent, text));
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00023980 File Offset: 0x00021B80
		protected virtual string Transform(LogEventInfo logEvent, string text)
		{
			return this.Transform(text);
		}

		// Token: 0x06000E50 RID: 3664
		protected abstract string Transform(string text);

		// Token: 0x06000E51 RID: 3665 RVA: 0x00023989 File Offset: 0x00021B89
		protected virtual string RenderInner(LogEventInfo logEvent)
		{
			Layout inner = this.Inner;
			return ((inner != null) ? inner.Render(logEvent) : null) ?? string.Empty;
		}
	}
}
