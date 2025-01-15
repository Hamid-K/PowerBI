using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000063 RID: 99
	public class AbsFunction : NumberFunctionBase
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x00015489 File Offset: 0x00013689
		protected override Node Eval(Env env, Number number, Node[] args)
		{
			base.WarnNotSupportedByLessJS("abs(number)");
			return new Number(Math.Abs(number.Value), number.Unit);
		}
	}
}
