using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser.ReportObjectModel
{
	// Token: 0x020002C1 RID: 705
	internal abstract class Field
	{
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060015D3 RID: 5587
		internal abstract object Value { get; }

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060015D4 RID: 5588
		internal abstract bool IsMissing { get; }

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060015D5 RID: 5589
		internal abstract string UniqueName { get; }

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060015D6 RID: 5590
		internal abstract string BackgroundColor { get; }

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060015D7 RID: 5591
		internal abstract string Color { get; }

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060015D8 RID: 5592
		internal abstract string FontFamily { get; }

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060015D9 RID: 5593
		internal abstract string FontSize { get; }

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060015DA RID: 5594
		internal abstract string FontWeight { get; }

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060015DB RID: 5595
		internal abstract string FontStyle { get; }

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060015DC RID: 5596
		internal abstract string TextDecoration { get; }

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060015DD RID: 5597
		internal abstract string FormattedValue { get; }

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060015DE RID: 5598
		internal abstract object Key { get; }

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060015DF RID: 5599
		internal abstract int LevelNumber { get; }

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060015E0 RID: 5600
		internal abstract string ParentUniqueName { get; }

		// Token: 0x170006E5 RID: 1765
		[IndexerName("Properties")]
		internal abstract object this[string key] { get; }
	}
}
