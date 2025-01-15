using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000092 RID: 146
	public class LightenFunction : HslColorFunctionBase
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x00016764 File Offset: 0x00014964
		protected override Node EvalHsl(HslColor color)
		{
			return color.GetLightness();
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001676C File Offset: 0x0001496C
		protected override Node EditHsl(HslColor color, Number number)
		{
			color.Lightness += number.Value / 100.0;
			return color.ToRgbColor();
		}
	}
}
