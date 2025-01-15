using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200009D RID: 157
	public class PercentageFunction : NumberFunctionBase
	{
		// Token: 0x060004E2 RID: 1250 RVA: 0x00016D36 File Offset: 0x00014F36
		protected override Node Eval(Env env, Number number, Node[] args)
		{
			return new Number(number.Value * 100.0, "%");
		}
	}
}
