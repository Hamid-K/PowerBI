using System;
using System.Linq;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200009E RID: 158
	public class PowFunction : Function
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x00016D5C File Offset: 0x00014F5C
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectMinArguments(2, base.Arguments.Count, this, base.Location);
			Guard.ExpectMaxArguments(2, base.Arguments.Count, this, base.Location);
			Guard.ExpectAllNodes<Number>(base.Arguments, this, base.Location);
			base.WarnNotSupportedByLessJS("pow(number, number)");
			Number number = base.Arguments.Cast<Number>().First<Number>();
			Number number2 = base.Arguments.Cast<Number>().ElementAt(1);
			return new Number(Math.Pow(number.Value, number2.Value));
		}
	}
}
