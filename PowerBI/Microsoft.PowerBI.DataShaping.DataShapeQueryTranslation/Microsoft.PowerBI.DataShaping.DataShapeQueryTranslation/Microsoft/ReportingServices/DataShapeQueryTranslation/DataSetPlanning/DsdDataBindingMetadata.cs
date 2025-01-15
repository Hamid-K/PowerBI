using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x02000100 RID: 256
	internal sealed class DsdDataBindingMetadata
	{
		// Token: 0x06000A1D RID: 2589 RVA: 0x000274BF File Offset: 0x000256BF
		internal DsdDataBindingMetadata(Dictionary<IScope, int> itemBindingIndices, HashSet<IScope> itemsWithEnforcedBinding, List<DataMember> secondaryLeaves, Dictionary<DataMember, DataMember> secondaryStaticToDynamicParentMapping)
		{
			this.m_itemBindingIndices = itemBindingIndices;
			this.m_itemsWithEnforcedBinding = itemsWithEnforcedBinding;
			this.m_secondaryLeaves = secondaryLeaves;
			this.m_secondaryStaticToDynamicParentMapping = secondaryStaticToDynamicParentMapping;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x000274E4 File Offset: 0x000256E4
		internal DataMember GetSecondaryLeaf(int index)
		{
			return this.m_secondaryLeaves[index];
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x000274F2 File Offset: 0x000256F2
		internal DataMember GetSecondaryDynamicParentForStatic(DataMember dataMember)
		{
			return this.m_secondaryStaticToDynamicParentMapping[dataMember];
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00027500 File Offset: 0x00025700
		internal bool TryGetBindingIndex(IScope scope, out int index)
		{
			return this.m_itemBindingIndices.TryGetValue(scope, out index);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002750F File Offset: 0x0002570F
		internal int GetBindingIndex(IScope scope)
		{
			return this.m_itemBindingIndices[scope];
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002751D File Offset: 0x0002571D
		internal bool HasEnforcedBinding(IScope scope)
		{
			return this.m_itemsWithEnforcedBinding.Contains(scope);
		}

		// Token: 0x040004E2 RID: 1250
		private readonly Dictionary<IScope, int> m_itemBindingIndices;

		// Token: 0x040004E3 RID: 1251
		private readonly HashSet<IScope> m_itemsWithEnforcedBinding;

		// Token: 0x040004E4 RID: 1252
		private readonly List<DataMember> m_secondaryLeaves;

		// Token: 0x040004E5 RID: 1253
		private readonly Dictionary<DataMember, DataMember> m_secondaryStaticToDynamicParentMapping;
	}
}
