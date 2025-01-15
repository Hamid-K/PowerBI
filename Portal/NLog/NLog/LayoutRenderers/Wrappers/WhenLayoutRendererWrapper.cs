using System;
using System.Text;
using NLog.Conditions;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000104 RID: 260
	[LayoutRenderer("when")]
	[AmbientProperty("When")]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class WhenLayoutRendererWrapper : WrapperLayoutRendererBuilderBase, IRawValue
	{
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x00023729 File Offset: 0x00021929
		// (set) Token: 0x06000E3D RID: 3645 RVA: 0x00023731 File Offset: 0x00021931
		[RequiredParameter]
		public ConditionExpression When { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0002373A File Offset: 0x0002193A
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x00023742 File Offset: 0x00021942
		public Layout Else { get; set; }

		// Token: 0x06000E40 RID: 3648 RVA: 0x0002374C File Offset: 0x0002194C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			int length = builder.Length;
			try
			{
				if (this.ShouldRenderInner(logEvent))
				{
					Layout inner = base.Inner;
					if (inner != null)
					{
						inner.RenderAppendBuilder(logEvent, builder, false);
					}
				}
				else
				{
					Layout @else = this.Else;
					if (@else != null)
					{
						@else.RenderAppendBuilder(logEvent, builder, false);
					}
				}
			}
			catch
			{
				builder.Length = length;
				throw;
			}
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x000237B0 File Offset: 0x000219B0
		private bool ShouldRenderInner(LogEventInfo logEvent)
		{
			return this.When == null || true.Equals(this.When.Evaluate(logEvent));
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x000237DC File Offset: 0x000219DC
		[Obsolete("Inherit from WrapperLayoutRendererBase and override RenderInnerAndTransform() instead. Marked obsolete in NLog 4.6")]
		protected override void TransformFormattedMesssage(StringBuilder target)
		{
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x000237DE File Offset: 0x000219DE
		public bool TryGetRawValue(LogEventInfo logEvent, out object value)
		{
			if (this.ShouldRenderInner(logEvent))
			{
				return WhenLayoutRendererWrapper.TryGetRawValueFromLayout(logEvent, base.Inner, out value);
			}
			return WhenLayoutRendererWrapper.TryGetRawValueFromLayout(logEvent, this.Else, out value);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00023804 File Offset: 0x00021A04
		private static bool TryGetRawValueFromLayout(LogEventInfo logEvent, Layout layout, out object value)
		{
			if (layout == null)
			{
				value = null;
				return false;
			}
			return layout.TryGetRawValue(logEvent, out value);
		}
	}
}
