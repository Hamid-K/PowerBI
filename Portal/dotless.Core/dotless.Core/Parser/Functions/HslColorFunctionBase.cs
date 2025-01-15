using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000081 RID: 129
	public abstract class HslColorFunctionBase : ColorFunctionBase
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x00016580 File Offset: 0x00014780
		protected override Node Eval(Color color)
		{
			HslColor hslColor = HslColor.FromRgbColor(color);
			return this.EvalHsl(hslColor);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001659C File Offset: 0x0001479C
		protected override Node EditColor(Color color, Number number)
		{
			HslColor hslColor = HslColor.FromRgbColor(color);
			return this.EditHsl(hslColor, number);
		}

		// Token: 0x060004A8 RID: 1192
		protected abstract Node EvalHsl(HslColor color);

		// Token: 0x060004A9 RID: 1193
		protected abstract Node EditHsl(HslColor color, Number number);
	}
}
