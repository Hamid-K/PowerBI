using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000073 RID: 115
	public class DefaultFunction : Function
	{
		// Token: 0x06000476 RID: 1142 RVA: 0x00015CFB File Offset: 0x00013EFB
		protected override Node Evaluate(Env env)
		{
			return new TextNode("default()");
		}
	}
}
