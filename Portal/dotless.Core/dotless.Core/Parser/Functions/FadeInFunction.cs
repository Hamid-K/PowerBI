using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000065 RID: 101
	public class FadeInFunction : ColorFunctionBase
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x0001553D File Offset: 0x0001373D
		protected override Node Eval(Color color)
		{
			return new Number(color.Alpha);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001554C File Offset: 0x0001374C
		protected override Node EditColor(Color color, Number number)
		{
			double num = number.Value / 100.0;
			return new Color(color.R, color.G, color.B, this.ProcessAlpha(color.Alpha, num));
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001558E File Offset: 0x0001378E
		protected virtual double ProcessAlpha(double originalAlpha, double newAlpha)
		{
			return originalAlpha + newAlpha;
		}
	}
}
