using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x02000108 RID: 264
	[LayoutRenderer("xml-encode")]
	[AmbientProperty("XmlEncode")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class XmlEncodeLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x06000E5A RID: 3674 RVA: 0x00023A3D File Offset: 0x00021C3D
		public XmlEncodeLayoutRendererWrapper()
		{
			this.XmlEncode = true;
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x00023A4C File Offset: 0x00021C4C
		// (set) Token: 0x06000E5C RID: 3676 RVA: 0x00023A54 File Offset: 0x00021C54
		[DefaultValue(true)]
		public bool XmlEncode { get; set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00023A5D File Offset: 0x00021C5D
		// (set) Token: 0x06000E5E RID: 3678 RVA: 0x00023A65 File Offset: 0x00021C65
		[DefaultValue(false)]
		public bool XmlEncodeNewlines { get; set; }

		// Token: 0x06000E5F RID: 3679 RVA: 0x00023A70 File Offset: 0x00021C70
		protected override void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
		{
			base.Inner.RenderAppendBuilder(logEvent, builder, false);
			if (this.XmlEncode && this.RequiresXmlEncode(builder, orgLength))
			{
				string text = builder.ToString(orgLength, builder.Length - orgLength);
				builder.Length = orgLength;
				XmlHelper.EscapeXmlString(text, this.XmlEncodeNewlines, builder);
			}
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00023AC0 File Offset: 0x00021CC0
		protected override string Transform(string text)
		{
			if (this.XmlEncode)
			{
				return XmlHelper.EscapeXmlString(text, this.XmlEncodeNewlines, null);
			}
			return text;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00023ADC File Offset: 0x00021CDC
		private bool RequiresXmlEncode(StringBuilder target, int startPos = 0)
		{
			for (int i = startPos; i < target.Length; i++)
			{
				char c = target[i];
				if (c <= '"')
				{
					if (c != '\n' && c != '\r')
					{
						if (c == '"')
						{
							return true;
						}
					}
					else if (this.XmlEncodeNewlines)
					{
						return true;
					}
				}
				else if (c <= '\'')
				{
					if (c == '&' || c == '\'')
					{
						return true;
					}
				}
				else if (c == '<' || c == '>')
				{
					return true;
				}
			}
			return false;
		}
	}
}
