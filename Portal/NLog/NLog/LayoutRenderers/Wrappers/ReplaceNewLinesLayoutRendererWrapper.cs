using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000FC RID: 252
	[LayoutRenderer("replace-newlines")]
	[AmbientProperty("ReplaceNewLines")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class ReplaceNewLinesLayoutRendererWrapper : WrapperLayoutRendererBuilderBase
	{
		// Token: 0x06000E00 RID: 3584 RVA: 0x00023074 File Offset: 0x00021274
		public ReplaceNewLinesLayoutRendererWrapper()
		{
			this.Replacement = " ";
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00023087 File Offset: 0x00021287
		// (set) Token: 0x06000E02 RID: 3586 RVA: 0x0002308F File Offset: 0x0002128F
		[DefaultValue(" ")]
		public string Replacement { get; set; }

		// Token: 0x06000E03 RID: 3587 RVA: 0x00023098 File Offset: 0x00021298
		protected override void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
		{
			base.Inner.RenderAppendBuilder(logEvent, builder, false);
			if (builder.Length > orgLength)
			{
				string newLine = Environment.NewLine;
				if (!string.IsNullOrEmpty(newLine) && builder.IndexOf(newLine[newLine.Length - 1], orgLength) >= 0)
				{
					string text = builder.ToString(orgLength, builder.Length - orgLength);
					text = text.Replace(newLine, this.Replacement);
					if (newLine != "\n" && !ReplaceNewLinesLayoutRendererWrapper.HasUnixNewline(this.Replacement) && ReplaceNewLinesLayoutRendererWrapper.HasUnixNewline(text))
					{
						text = text.Replace("\n", this.Replacement);
					}
					builder.Length = orgLength;
					builder.Append(text);
				}
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00023146 File Offset: 0x00021346
		private static bool HasUnixNewline(string str)
		{
			return str != null && str.IndexOf('\n') >= 0;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0002315B File Offset: 0x0002135B
		[Obsolete("Inherit from WrapperLayoutRendererBase and override RenderInnerAndTransform() instead. Marked obsolete in NLog 4.6")]
		protected override void TransformFormattedMesssage(StringBuilder target)
		{
		}
	}
}
