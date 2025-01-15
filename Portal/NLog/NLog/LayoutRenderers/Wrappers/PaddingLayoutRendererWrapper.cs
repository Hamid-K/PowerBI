using System;
using System.ComponentModel;
using NLog.Config;

namespace NLog.LayoutRenderers.Wrappers
{
	// Token: 0x020000FA RID: 250
	[LayoutRenderer("pad")]
	[AmbientProperty("Padding")]
	[AmbientProperty("PadCharacter")]
	[AmbientProperty("FixedLength")]
	[AmbientProperty("AlignmentOnTruncation")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class PaddingLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x06000DE6 RID: 3558 RVA: 0x00022D70 File Offset: 0x00020F70
		public PaddingLayoutRendererWrapper()
		{
			this.PadCharacter = ' ';
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x00022D80 File Offset: 0x00020F80
		// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x00022D88 File Offset: 0x00020F88
		public int Padding { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00022D91 File Offset: 0x00020F91
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x00022D99 File Offset: 0x00020F99
		[DefaultValue(' ')]
		public char PadCharacter { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00022DA2 File Offset: 0x00020FA2
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x00022DAA File Offset: 0x00020FAA
		[DefaultValue(false)]
		public bool FixedLength { get; set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00022DB3 File Offset: 0x00020FB3
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x00022DBB File Offset: 0x00020FBB
		[DefaultValue(PaddingHorizontalAlignment.Left)]
		public PaddingHorizontalAlignment AlignmentOnTruncation { get; set; }

		// Token: 0x06000DEF RID: 3567 RVA: 0x00022DC4 File Offset: 0x00020FC4
		protected override string Transform(string text)
		{
			string text2 = text ?? string.Empty;
			if (this.Padding != 0)
			{
				if (this.Padding > 0)
				{
					text2 = text2.PadLeft(this.Padding, this.PadCharacter);
				}
				else
				{
					text2 = text2.PadRight(-this.Padding, this.PadCharacter);
				}
				int num = this.Padding;
				if (num < 0)
				{
					num = -num;
				}
				if (this.FixedLength && text2.Length > num)
				{
					if (this.AlignmentOnTruncation == PaddingHorizontalAlignment.Right)
					{
						text2 = text2.Substring(text2.Length - num);
					}
					else
					{
						text2 = text2.Substring(0, num);
					}
				}
			}
			return text2;
		}
	}
}
