using System;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200008D RID: 141
	public abstract class IsDimensionUnitFunction : IsTypeFunction<Number>
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004BD RID: 1213
		protected abstract string Unit { get; }

		// Token: 0x060004BE RID: 1214 RVA: 0x000166F6 File Offset: 0x000148F6
		protected override bool IsEvaluator(Node node)
		{
			return base.IsEvaluator(node) && ((Number)node).Unit == this.Unit;
		}
	}
}
