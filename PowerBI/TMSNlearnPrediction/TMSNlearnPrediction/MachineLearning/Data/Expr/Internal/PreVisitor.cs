using System;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x020001A4 RID: 420
	internal abstract class PreVisitor : NodeVisitor
	{
		// Token: 0x060008DE RID: 2270
		public abstract void Visit(LambdaNode node);

		// Token: 0x060008DF RID: 2271
		public abstract void Visit(UnaryOpNode node);

		// Token: 0x060008E0 RID: 2272
		public abstract void Visit(BinaryOpNode node);

		// Token: 0x060008E1 RID: 2273
		public abstract void Visit(ConditionalNode node);

		// Token: 0x060008E2 RID: 2274
		public abstract void Visit(CompareNode node);

		// Token: 0x060008E3 RID: 2275
		public abstract void Visit(CallNode node);

		// Token: 0x060008E4 RID: 2276
		public abstract void Visit(ListNode node);

		// Token: 0x060008E5 RID: 2277
		public abstract void Visit(WithNode node);

		// Token: 0x060008E6 RID: 2278
		public abstract void Visit(WithLocalNode node);

		// Token: 0x060008E7 RID: 2279 RVA: 0x0003227C File Offset: 0x0003047C
		public override bool PreVisit(LambdaNode node)
		{
			this.Visit(node);
			return false;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00032286 File Offset: 0x00030486
		public override bool PreVisit(UnaryOpNode node)
		{
			this.Visit(node);
			return false;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00032290 File Offset: 0x00030490
		public override bool PreVisit(BinaryOpNode node)
		{
			this.Visit(node);
			return false;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0003229A File Offset: 0x0003049A
		public override bool PreVisit(ConditionalNode node)
		{
			this.Visit(node);
			return false;
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x000322A4 File Offset: 0x000304A4
		public override bool PreVisit(CompareNode node)
		{
			this.Visit(node);
			return false;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x000322AE File Offset: 0x000304AE
		public override bool PreVisit(CallNode node)
		{
			this.Visit(node);
			return false;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x000322B8 File Offset: 0x000304B8
		public override bool PreVisit(ListNode node)
		{
			this.Visit(node);
			return false;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x000322C2 File Offset: 0x000304C2
		public override bool PreVisit(WithNode node)
		{
			this.Visit(node);
			return false;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x000322CC File Offset: 0x000304CC
		public override bool PreVisit(WithLocalNode node)
		{
			this.Visit(node);
			return false;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x000322D6 File Offset: 0x000304D6
		public override void PostVisit(LambdaNode node)
		{
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x000322D8 File Offset: 0x000304D8
		public override void PostVisit(UnaryOpNode node)
		{
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x000322DA File Offset: 0x000304DA
		public override void PostVisit(BinaryOpNode node)
		{
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x000322DC File Offset: 0x000304DC
		public override void PostVisit(ConditionalNode node)
		{
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000322DE File Offset: 0x000304DE
		public override void PostVisit(CompareNode node)
		{
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x000322E0 File Offset: 0x000304E0
		public override void PostVisit(CallNode node)
		{
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000322E2 File Offset: 0x000304E2
		public override void PostVisit(ListNode node)
		{
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x000322E4 File Offset: 0x000304E4
		public override void PostVisit(WithNode node)
		{
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000322E6 File Offset: 0x000304E6
		public override void PostVisit(WithLocalNode node)
		{
		}
	}
}
