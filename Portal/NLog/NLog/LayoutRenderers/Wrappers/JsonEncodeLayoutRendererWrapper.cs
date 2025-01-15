using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;
using NLog.Targets;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000F4 RID: 244
	[LayoutRenderer("json-encode")]
	[AmbientProperty("JsonEncode")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class JsonEncodeLayoutRendererWrapper : WrapperLayoutRendererBuilderBase
	{
		// Token: 0x06000DC7 RID: 3527 RVA: 0x00022B39 File Offset: 0x00020D39
		public JsonEncodeLayoutRendererWrapper()
		{
			this.JsonEncode = true;
			this.EscapeUnicode = true;
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00022B4F File Offset: 0x00020D4F
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x00022B57 File Offset: 0x00020D57
		[DefaultValue(true)]
		public bool JsonEncode { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00022B60 File Offset: 0x00020D60
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x00022B68 File Offset: 0x00020D68
		[DefaultValue(true)]
		public bool EscapeUnicode { get; set; }

		// Token: 0x06000DCC RID: 3532 RVA: 0x00022B74 File Offset: 0x00020D74
		protected override void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
		{
			base.Inner.RenderAppendBuilder(logEvent, builder, false);
			if (this.JsonEncode && builder.Length > orgLength && this.RequiresJsonEncode(builder, orgLength))
			{
				string text = builder.ToString(orgLength, builder.Length - orgLength);
				builder.Length = orgLength;
				DefaultJsonSerializer.AppendStringEscape(builder, text, this.EscapeUnicode);
			}
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00022BCE File Offset: 0x00020DCE
		[Obsolete("Inherit from WrapperLayoutRendererBase and override RenderInnerAndTransform() instead. Marked obsolete in NLog 4.6")]
		protected override void TransformFormattedMesssage(StringBuilder target)
		{
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00022BD0 File Offset: 0x00020DD0
		private bool RequiresJsonEncode(StringBuilder target, int startPos = 0)
		{
			for (int i = startPos; i < target.Length; i++)
			{
				if (DefaultJsonSerializer.RequiresJsonEscape(target[i], this.EscapeUnicode))
				{
					return true;
				}
			}
			return false;
		}
	}
}
