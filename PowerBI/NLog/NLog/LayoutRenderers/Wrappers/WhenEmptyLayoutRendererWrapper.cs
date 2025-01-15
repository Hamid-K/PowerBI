using System;
using System.Text;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000103 RID: 259
	[LayoutRenderer("whenEmpty")]
	[AmbientProperty("WhenEmpty")]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class WhenEmptyLayoutRendererWrapper : WrapperLayoutRendererBuilderBase, IRawValue, IStringValueRenderer
	{
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x000235C0 File Offset: 0x000217C0
		// (set) Token: 0x06000E33 RID: 3635 RVA: 0x000235C8 File Offset: 0x000217C8
		[RequiredParameter]
		public Layout WhenEmpty { get; set; }

		// Token: 0x06000E34 RID: 3636 RVA: 0x000235D4 File Offset: 0x000217D4
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			Layout whenEmpty = this.WhenEmpty;
			if (whenEmpty != null)
			{
				whenEmpty.Initialize(base.LoggingConfiguration);
			}
			SimpleLayout simpleLayout;
			SimpleLayout simpleLayout2;
			this._skipStringValueRenderer = !this.TryGetStringValue(out simpleLayout, out simpleLayout2);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00023611 File Offset: 0x00021811
		protected override void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
		{
			base.Inner.RenderAppendBuilder(logEvent, builder, false);
			if (builder.Length > orgLength)
			{
				return;
			}
			this.WhenEmpty.RenderAppendBuilder(logEvent, builder, false);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0002363C File Offset: 0x0002183C
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			if (this._skipStringValueRenderer)
			{
				return null;
			}
			SimpleLayout simpleLayout;
			SimpleLayout simpleLayout2;
			if (!this.TryGetStringValue(out simpleLayout, out simpleLayout2))
			{
				this._skipStringValueRenderer = true;
				return null;
			}
			string text = simpleLayout.Render(logEvent);
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return simpleLayout2.Render(logEvent);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00023681 File Offset: 0x00021881
		private bool TryGetStringValue(out SimpleLayout innerLayout, out SimpleLayout whenEmptyLayout)
		{
			whenEmptyLayout = this.WhenEmpty as SimpleLayout;
			innerLayout = base.Inner as SimpleLayout;
			return WhenEmptyLayoutRendererWrapper.IsStringLayout(innerLayout) && WhenEmptyLayoutRendererWrapper.IsStringLayout(whenEmptyLayout);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000236AF File Offset: 0x000218AF
		private static bool IsStringLayout(SimpleLayout innerLayout)
		{
			return innerLayout != null && (innerLayout.IsFixedText || innerLayout.IsSimpleStringText);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x000236C8 File Offset: 0x000218C8
		bool IRawValue.TryGetRawValue(LogEventInfo logEvent, out object value)
		{
			object obj;
			if (base.Inner.TryGetRawValue(logEvent, out obj))
			{
				if (obj != null && !obj.Equals(string.Empty))
				{
					value = obj;
					return true;
				}
			}
			else if (!string.IsNullOrEmpty(base.Inner.Render(logEvent)))
			{
				value = null;
				return false;
			}
			return this.WhenEmpty.TryGetRawValue(logEvent, out value);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0002371F File Offset: 0x0002191F
		[Obsolete("Inherit from WrapperLayoutRendererBase and override RenderInnerAndTransform() instead. Marked obsolete in NLog 4.6")]
		protected override void TransformFormattedMesssage(StringBuilder target)
		{
		}

		// Token: 0x040003D4 RID: 980
		private bool _skipStringValueRenderer;
	}
}
