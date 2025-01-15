using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000070 RID: 112
	public class ComplementFunction : HslColorFunctionBase
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x00015971 File Offset: 0x00013B71
		protected override Node EvalHsl(HslColor color)
		{
			base.WarnNotSupportedByLessJS("complement(color)");
			color.Hue += 0.5;
			return color.ToRgbColor();
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001599A File Offset: 0x00013B9A
		protected override Node EditHsl(HslColor color, Number number)
		{
			return null;
		}
	}
}
