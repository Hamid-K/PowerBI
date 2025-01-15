using System;
using System.Linq;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200007B RID: 123
	public class GreyscaleFunction : ColorFunctionBase
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x00016346 File Offset: 0x00014546
		protected override Node Eval(Color color)
		{
			double num = (color.RGB.Max() + color.RGB.Min()) / 2.0;
			return new Color(num, num, num);
		}
	}
}
