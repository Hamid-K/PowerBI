using System;
using dotless.Core.Exceptions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200007F RID: 127
	public class HexFunction : NumberFunctionBase
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x00016480 File Offset: 0x00014680
		protected override Node Eval(Env env, Number number, Node[] args)
		{
			base.WarnNotSupportedByLessJS("hex(number)");
			if (!string.IsNullOrEmpty(number.Unit))
			{
				throw new ParsingException(string.Format("Expected unitless number in function 'hex', found {0}", number.ToCSS(env)), number.Location);
			}
			number.Value = HexFunction.Clamp(number.Value, 255.0, 0.0);
			return new TextNode(((int)number.Value).ToString("X2"));
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000164FE File Offset: 0x000146FE
		private static double Clamp(double value, double max, double min)
		{
			if (value < min)
			{
				value = min;
			}
			if (value > max)
			{
				value = max;
			}
			return value;
		}
	}
}
