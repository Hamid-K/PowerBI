using System;
using System.Text;
using NLog.Config;
using NLog.Layouts;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000FE RID: 254
	[LayoutRenderer("rot13")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class Rot13LayoutRendererWrapper : WrapperLayoutRendererBuilderBase
	{
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x000231DD File Offset: 0x000213DD
		// (set) Token: 0x06000E0C RID: 3596 RVA: 0x000231E5 File Offset: 0x000213E5
		public Layout Text
		{
			get
			{
				return base.Inner;
			}
			set
			{
				base.Inner = value;
			}
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x000231EE File Offset: 0x000213EE
		public static string DecodeRot13(string encodedValue)
		{
			StringBuilder stringBuilder = new StringBuilder(encodedValue.Length);
			stringBuilder.Append(encodedValue);
			Rot13LayoutRendererWrapper.DecodeRot13(stringBuilder, 0);
			return stringBuilder.ToString();
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0002320F File Offset: 0x0002140F
		protected override void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
		{
			base.Inner.RenderAppendBuilder(logEvent, builder, false);
			if (builder.Length > orgLength)
			{
				Rot13LayoutRendererWrapper.DecodeRot13(builder, orgLength);
			}
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0002322F File Offset: 0x0002142F
		[Obsolete("Inherit from WrapperLayoutRendererBase and override RenderInnerAndTransform() instead. Marked obsolete in NLog 4.6")]
		protected override void TransformFormattedMesssage(StringBuilder target)
		{
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00023234 File Offset: 0x00021434
		internal static void DecodeRot13(StringBuilder encodedValue, int startPos)
		{
			if (encodedValue == null)
			{
				return;
			}
			for (int i = startPos; i < encodedValue.Length; i++)
			{
				encodedValue[i] = Rot13LayoutRendererWrapper.DecodeRot13Char(encodedValue[i]);
			}
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0002326C File Offset: 0x0002146C
		private static char DecodeRot13Char(char c)
		{
			if (c >= 'A' && c <= 'M')
			{
				return 'N' + (c - 'A');
			}
			if (c >= 'a' && c <= 'm')
			{
				return 'n' + (c - 'a');
			}
			if (c >= 'N' && c <= 'Z')
			{
				return 'A' + (c - 'N');
			}
			if (c >= 'n' && c <= 'z')
			{
				return 'a' + (c - 'n');
			}
			return c;
		}
	}
}
