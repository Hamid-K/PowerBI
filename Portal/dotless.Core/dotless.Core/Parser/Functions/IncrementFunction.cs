using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000085 RID: 133
	public class IncrementFunction : NumberFunctionBase
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x0001664F File Offset: 0x0001484F
		protected override Node Eval(Env env, Number number, Node[] args)
		{
			return new Number(number.Value + 1.0, number.Unit);
		}
	}
}
