using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200006F RID: 111
	public abstract class ColorMixFunction : Function
	{
		// Token: 0x06000468 RID: 1128 RVA: 0x00015814 File Offset: 0x00013A14
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectNumArguments(2, base.Arguments.Count, this, base.Location);
			Guard.ExpectAllNodes<Color>(base.Arguments, this, base.Location);
			Color color = (Color)base.Arguments[0];
			Color color2 = (Color)base.Arguments[1];
			double num = color2.Alpha + color.Alpha * (1.0 - color2.Alpha);
			return new Color(this.Compose(color, color2, num, (Color c) => c.R), this.Compose(color, color2, num, (Color c) => c.G), this.Compose(color, color2, num, (Color c) => c.B), num);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00015910 File Offset: 0x00013B10
		private double Compose(Color backdrop, Color source, double ar, Func<Color, double> channel)
		{
			double num = channel(backdrop);
			double num2 = channel(source);
			double alpha = backdrop.Alpha;
			double alpha2 = source.Alpha;
			double num3 = this.Operate(num, num2);
			if (ar > 0.0)
			{
				num3 = (alpha2 * num2 + alpha * (num - alpha2 * (num + num2 - num3))) / ar;
			}
			return num3;
		}

		// Token: 0x0600046A RID: 1130
		protected abstract double Operate(double a, double b);
	}
}
