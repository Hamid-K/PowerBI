using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x020000A2 RID: 162
	public class RoundFunction : NumberFunctionBase
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x00016F94 File Offset: 0x00015194
		protected override Node Eval(Env env, Number number, Node[] args)
		{
			if (args.Length == 0)
			{
				return new Number(Math.Round(number.Value, MidpointRounding.AwayFromZero), number.Unit);
			}
			Guard.ExpectNode<Number>(args[0], this, args[0].Location);
			return new Number(Math.Round(number.Value, (int)((Number)args[0]).Value, MidpointRounding.AwayFromZero), number.Unit);
		}
	}
}
