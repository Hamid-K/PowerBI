using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x020000A1 RID: 161
	public class RgbFunction : RgbaFunction
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x00016F4C File Offset: 0x0001514C
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectNumArguments(3, base.Arguments.Count, this, base.Location);
			base.Arguments.Add(new Number(1.0, ""));
			return base.Evaluate(env);
		}
	}
}
