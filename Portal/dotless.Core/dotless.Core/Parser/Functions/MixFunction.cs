using System;
using System.Linq;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000096 RID: 150
	public class MixFunction : Function
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x000168EC File Offset: 0x00014AEC
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectMinArguments(2, base.Arguments.Count, this, base.Location);
			Guard.ExpectMaxArguments(3, base.Arguments.Count, this, base.Location);
			Guard.ExpectAllNodes<Color>(base.Arguments.Take(2), this, base.Location);
			double num = 50.0;
			if (base.Arguments.Count == 3)
			{
				Guard.ExpectNode<Number>(base.Arguments[2], this, base.Location);
				num = ((Number)base.Arguments[2]).Value;
			}
			Color[] array = base.Arguments.Take(2).Cast<Color>().ToArray<Color>();
			return this.Mix(array[0], array[1], num);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000169B0 File Offset: 0x00014BB0
		protected Color Mix(Color color1, Color color2, double weight)
		{
			double num = weight / 100.0;
			double num2 = num * 2.0 - 1.0;
			double num3 = color1.Alpha - color2.Alpha;
			double w1 = (((num2 * num3 == -1.0) ? num2 : ((num2 + num3) / (1.0 + num2 * num3))) + 1.0) / 2.0;
			double w2 = 1.0 - w1;
			double[] array = color1.RGB.Select((double x, int i) => x * w1 + color2.RGB[i] * w2).ToArray<double>();
			double num4 = color1.Alpha * num + color2.Alpha * (1.0 - num);
			return new Color(array[0], array[1], array[2], num4);
		}
	}
}
