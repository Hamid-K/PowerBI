using System;
using Microsoft.DataShaping.Processing.Pipeline;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Correlation
{
	// Token: 0x020000AC RID: 172
	internal sealed class ReadOnlyIndexBasedCorrelationGovernor : IndexBasedCorrelationGovernor
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x0000DC21 File Offset: 0x0000BE21
		internal ReadOnlyIndexBasedCorrelationGovernor(IndexBasedCorrelationGovernor existingGovernor)
			: base(existingGovernor)
		{
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000DC2A File Offset: 0x0000BE2A
		internal override void SetCorrelationInfo(int correlationIndex, IReadOnlyRowCache rowCache)
		{
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000DC2C File Offset: 0x0000BE2C
		protected override void EnterMemberInstanceImpl(DataMember member, long rowIndex)
		{
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000DC2E File Offset: 0x0000BE2E
		protected override void IncrementMemberInstanceCount(DataMember member)
		{
			if (!base.IsInSecondaryHierarchy)
			{
				base.IncrementMemberInstanceCount(member);
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000DC3F File Offset: 0x0000BE3F
		internal override void ExitMemberInstance(DataMember member)
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000DC41 File Offset: 0x0000BE41
		internal override void SkipInstance()
		{
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000DC43 File Offset: 0x0000BE43
		internal override CorrelationGovernor ToReadOnly()
		{
			return this;
		}
	}
}
