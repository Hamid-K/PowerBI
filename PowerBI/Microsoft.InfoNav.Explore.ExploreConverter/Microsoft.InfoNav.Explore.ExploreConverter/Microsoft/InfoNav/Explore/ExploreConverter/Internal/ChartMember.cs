using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000090 RID: 144
	internal sealed class ChartMember : Member
	{
		// Token: 0x060002DA RID: 730 RVA: 0x0000CD0A File Offset: 0x0000AF0A
		internal ChartMember(Group group, List<SortExpression> sortExpressions, Label label, List<ChartMember> chartMembers)
			: base(group, sortExpressions)
		{
			this._label = label;
			this._chartMembers = chartMembers;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000CD23 File Offset: 0x0000AF23
		internal Label Label
		{
			get
			{
				return this._label;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000CD2B File Offset: 0x0000AF2B
		internal List<ChartMember> ChartMembers
		{
			get
			{
				return this._chartMembers;
			}
		}

		// Token: 0x040001E5 RID: 485
		private readonly Label _label;

		// Token: 0x040001E6 RID: 486
		private readonly List<ChartMember> _chartMembers;
	}
}
