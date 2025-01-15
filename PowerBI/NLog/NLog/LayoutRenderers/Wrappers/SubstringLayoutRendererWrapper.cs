using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000FF RID: 255
	[LayoutRenderer("substring")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class SubstringLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x06000E13 RID: 3603 RVA: 0x000232CE File Offset: 0x000214CE
		public SubstringLayoutRendererWrapper()
		{
			this.Start = 0;
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x000232DD File Offset: 0x000214DD
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x000232E5 File Offset: 0x000214E5
		[DefaultValue(0)]
		public int Start { get; set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x000232EE File Offset: 0x000214EE
		// (set) Token: 0x06000E17 RID: 3607 RVA: 0x000232F6 File Offset: 0x000214F6
		[DefaultValue(null)]
		[RequiredParameter]
		public int? Length { get; set; }

		// Token: 0x06000E18 RID: 3608 RVA: 0x00023300 File Offset: 0x00021500
		protected override void RenderInnerAndTransform(LogEventInfo logEvent, StringBuilder builder, int orgLength)
		{
			int? length = this.Length;
			int num = 0;
			if ((length.GetValueOrDefault() == num) & (length != null))
			{
				return;
			}
			base.Inner.RenderAppendBuilder(logEvent, builder, false);
			int num2 = builder.Length - orgLength;
			if (num2 > 0)
			{
				int num3 = this.CalcStart(num2);
				int num4 = this.CalcLength(num2, num3);
				string text = builder.ToString(orgLength + num3, num4);
				builder.Length = orgLength;
				builder.Append(text);
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00023375 File Offset: 0x00021575
		protected override string Transform(string text)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0002337C File Offset: 0x0002157C
		private int CalcStart(int textLength)
		{
			int num = this.Start;
			if (num > textLength)
			{
				num = textLength;
			}
			if (num < 0)
			{
				num = textLength + num;
				if (num < 0)
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x000233A8 File Offset: 0x000215A8
		private int CalcLength(int textLength, int start)
		{
			int num = textLength - start;
			if (this.Length != null && textLength > this.Length.Value + start)
			{
				num = this.Length.Value;
			}
			if (num < 0)
			{
				num = 0;
			}
			return num;
		}
	}
}
