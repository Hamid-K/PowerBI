using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableManagers;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001CF RID: 463
	internal sealed class AggregateInputStack
	{
		// Token: 0x0600103D RID: 4157 RVA: 0x0004360D File Offset: 0x0004180D
		internal AggregateInputStack()
		{
			this.m_slots = new Stack<AggregateInputStack.Slot>();
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00043620 File Offset: 0x00041820
		public void PushSlot(AggregateRewriteMode mode, bool aggregateIgnoresNulls, RowResultSetType rowResultSetType)
		{
			this.m_slots.Push(new AggregateInputStack.Slot(mode, aggregateIgnoresNulls, rowResultSetType));
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00043635 File Offset: 0x00041835
		public AggregateInputStack.Slot PopSlot()
		{
			return this.m_slots.Pop();
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00043642 File Offset: 0x00041842
		public AggregateInputStack.Slot GetActiveSlot()
		{
			if (this.m_slots.Count == 0)
			{
				return null;
			}
			return this.m_slots.Peek();
		}

		// Token: 0x04000799 RID: 1945
		private readonly Stack<AggregateInputStack.Slot> m_slots;

		// Token: 0x02000314 RID: 788
		internal sealed class Slot
		{
			// Token: 0x0600174B RID: 5963 RVA: 0x00052943 File Offset: 0x00050B43
			internal Slot(AggregateRewriteMode mode, bool aggregateIgnoresNulls, RowResultSetType rowResultSetType)
			{
				this.Mode = mode;
				this.AggregateIgnoresNulls = aggregateIgnoresNulls;
				this.RowResultSetType = rowResultSetType;
			}

			// Token: 0x1700041E RID: 1054
			// (get) Token: 0x0600174C RID: 5964 RVA: 0x00052960 File Offset: 0x00050B60
			// (set) Token: 0x0600174D RID: 5965 RVA: 0x00052968 File Offset: 0x00050B68
			public AggregateRewriteMode Mode { get; private set; }

			// Token: 0x1700041F RID: 1055
			// (get) Token: 0x0600174E RID: 5966 RVA: 0x00052971 File Offset: 0x00050B71
			// (set) Token: 0x0600174F RID: 5967 RVA: 0x00052979 File Offset: 0x00050B79
			public bool AggregateIgnoresNulls { get; private set; }

			// Token: 0x17000420 RID: 1056
			// (get) Token: 0x06001750 RID: 5968 RVA: 0x00052982 File Offset: 0x00050B82
			// (set) Token: 0x06001751 RID: 5969 RVA: 0x0005298A File Offset: 0x00050B8A
			public RowResultSetType RowResultSetType { get; private set; }

			// Token: 0x17000421 RID: 1057
			// (get) Token: 0x06001752 RID: 5970 RVA: 0x00052993 File Offset: 0x00050B93
			// (set) Token: 0x06001753 RID: 5971 RVA: 0x0005299B File Offset: 0x00050B9B
			public IScope Scope { get; private set; }

			// Token: 0x17000422 RID: 1058
			// (get) Token: 0x06001754 RID: 5972 RVA: 0x000529A4 File Offset: 0x00050BA4
			// (set) Token: 0x06001755 RID: 5973 RVA: 0x000529AC File Offset: 0x00050BAC
			public IAggregateInputTable Table { get; private set; }

			// Token: 0x17000423 RID: 1059
			// (get) Token: 0x06001756 RID: 5974 RVA: 0x000529B5 File Offset: 0x00050BB5
			// (set) Token: 0x06001757 RID: 5975 RVA: 0x000529BD File Offset: 0x00050BBD
			public bool HasCalculationReference { get; set; }

			// Token: 0x06001758 RID: 5976 RVA: 0x000529C6 File Offset: 0x00050BC6
			public void Fill(IScope scope, IAggregateInputTable table)
			{
				this.Scope = scope;
				this.Table = table;
			}
		}
	}
}
