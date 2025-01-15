using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000077 RID: 119
	public class FloorFunction : NumberFunctionBase
	{
		// Token: 0x0600047E RID: 1150 RVA: 0x00015D93 File Offset: 0x00013F93
		protected override Node Eval(Env env, Number number, Node[] args)
		{
			return new Number(Math.Floor(number.Value), number.Unit);
		}
	}
}
