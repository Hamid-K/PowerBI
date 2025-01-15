using System;
using System.Linq;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000064 RID: 100
	public class AddFunction : Function
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x000154B4 File Offset: 0x000136B4
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectAllNodes<Number>(base.Arguments, this, base.Location);
			return new Number((from Number d in base.Arguments
				select d.Value).Aggregate(0.0, (double a, double b) => a + b));
		}
	}
}
