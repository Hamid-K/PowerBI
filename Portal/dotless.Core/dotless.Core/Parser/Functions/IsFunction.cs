using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000086 RID: 134
	public abstract class IsFunction : Function
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x00016674 File Offset: 0x00014874
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectNumArguments(1, base.Arguments.Count, this, base.Location);
			return new Keyword(this.IsEvaluator(base.Arguments[0]) ? "true" : "false");
		}

		// Token: 0x060004B4 RID: 1204
		protected abstract bool IsEvaluator(Node node);
	}
}
