using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x020000A3 RID: 163
	public class SaturateFunction : HslColorFunctionBase
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x00016FFC File Offset: 0x000151FC
		protected override Node EvalHsl(HslColor color)
		{
			return color.GetSaturation();
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00017004 File Offset: 0x00015204
		protected override Node EditHsl(HslColor color, Number number)
		{
			color.Saturation += number.Value / 100.0;
			return color.ToRgbColor();
		}
	}
}
