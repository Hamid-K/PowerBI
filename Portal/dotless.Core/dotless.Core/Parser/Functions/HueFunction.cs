using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000083 RID: 131
	public class HueFunction : HslColorFunctionBase
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x00016607 File Offset: 0x00014807
		protected override Node EvalHsl(HslColor color)
		{
			return color.GetHueInDegrees();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001660F File Offset: 0x0001480F
		protected override Node EditHsl(HslColor color, Number number)
		{
			base.WarnNotSupportedByLessJS("hue(color, number)");
			color.Hue += number.Value / 360.0;
			return color.ToRgbColor();
		}
	}
}
