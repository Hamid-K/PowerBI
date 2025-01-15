using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200006C RID: 108
	public class CeilFunction : NumberFunctionBase
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x000156B0 File Offset: 0x000138B0
		protected override Node Eval(Env env, Number number, Node[] args)
		{
			return new Number(Math.Ceiling(number.Value), number.Unit);
		}
	}
}
