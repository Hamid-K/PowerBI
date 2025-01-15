using System;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000087 RID: 135
	public abstract class IsTypeFunction<T> : IsFunction where T : Node
	{
		// Token: 0x060004B6 RID: 1206 RVA: 0x000166BB File Offset: 0x000148BB
		protected override bool IsEvaluator(Node node)
		{
			return node is T;
		}
	}
}
