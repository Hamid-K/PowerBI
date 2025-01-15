using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000082 RID: 130
	public class HslFunction : HslaFunction
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x000165C0 File Offset: 0x000147C0
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectNumArguments(3, base.Arguments.Count, this, base.Location);
			base.Arguments.Add(new Number(1.0, ""));
			return base.Evaluate(env);
		}
	}
}
