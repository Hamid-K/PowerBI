using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000100 RID: 256
	[LayoutRenderer("trim-whitespace")]
	[AmbientProperty("TrimWhiteSpace")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class TrimWhiteSpaceLayoutRendererWrapper : WrapperLayoutRendererBuilderBase
	{
		// Token: 0x06000E1C RID: 3612 RVA: 0x000233F2 File Offset: 0x000215F2
		public TrimWhiteSpaceLayoutRendererWrapper()
		{
			this.TrimWhiteSpace = true;
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x00023401 File Offset: 0x00021601
		// (set) Token: 0x06000E1E RID: 3614 RVA: 0x00023409 File Offset: 0x00021609
		[DefaultValue(true)]
		public bool TrimWhiteSpace { get; set; }

		// Token: 0x06000E1F RID: 3615 RVA: 0x00023412 File Offset: 0x00021612
		protected override void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
		{
			base.Inner.RenderAppendBuilder(logEvent, builder, false);
			if (this.TrimWhiteSpace && builder.Length > orgLength)
			{
				TrimWhiteSpaceLayoutRendererWrapper.TransformTrimWhiteSpaces(builder, orgLength);
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0002343A File Offset: 0x0002163A
		[Obsolete("Inherit from WrapperLayoutRendererBase and override RenderInnerAndTransform() instead. Marked obsolete in NLog 4.6")]
		protected override void TransformFormattedMesssage(StringBuilder target)
		{
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0002343C File Offset: 0x0002163C
		private static void TransformTrimWhiteSpaces(StringBuilder builder, int startPos)
		{
			builder.TrimRight(startPos);
			if (builder.Length > startPos && char.IsWhiteSpace(builder[startPos]))
			{
				string text = builder.ToString(startPos, builder.Length - startPos);
				builder.Length = startPos;
				builder.Append(text.Trim());
			}
		}
	}
}
