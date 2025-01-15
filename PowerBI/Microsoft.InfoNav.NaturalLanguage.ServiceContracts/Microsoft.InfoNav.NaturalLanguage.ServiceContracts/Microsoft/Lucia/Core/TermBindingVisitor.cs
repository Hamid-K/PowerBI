using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000111 RID: 273
	public abstract class TermBindingVisitor<T>
	{
		// Token: 0x06000599 RID: 1433
		protected internal abstract T Visit(CompositeTermBinding binding);

		// Token: 0x0600059A RID: 1434
		protected internal abstract T Visit(CoreTermBinding binding);

		// Token: 0x0600059B RID: 1435
		protected internal abstract T Visit(LiteralTermBinding binding);

		// Token: 0x0600059C RID: 1436
		protected internal abstract T Visit(PodTermBinding binding);

		// Token: 0x0600059D RID: 1437
		protected internal abstract T Visit(PropertyTermBinding binding);

		// Token: 0x0600059E RID: 1438
		protected internal abstract T Visit(RangeTermBinding binding);

		// Token: 0x0600059F RID: 1439
		protected internal abstract T Visit(TableTermBinding binding);

		// Token: 0x060005A0 RID: 1440
		protected internal abstract T Visit(ValueTermBinding binding);

		// Token: 0x060005A1 RID: 1441
		protected internal abstract T Visit(VisualizationTypeTermBinding binding);

		// Token: 0x060005A2 RID: 1442
		protected internal abstract T Visit(PhrasingTermBinding binding);

		// Token: 0x060005A3 RID: 1443
		protected internal abstract T Visit(InferredTermBinding binding);

		// Token: 0x060005A4 RID: 1444
		protected internal abstract T Visit(TextualEntityTermBinding binding);
	}
}
