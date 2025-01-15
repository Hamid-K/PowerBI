using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000091 RID: 145
	public class LengthFunction : ListFunctionBase
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x00016751 File Offset: 0x00014951
		protected override Node Eval(Env env, Node[] list, Node[] args)
		{
			return new Number((double)list.Length);
		}
	}
}
