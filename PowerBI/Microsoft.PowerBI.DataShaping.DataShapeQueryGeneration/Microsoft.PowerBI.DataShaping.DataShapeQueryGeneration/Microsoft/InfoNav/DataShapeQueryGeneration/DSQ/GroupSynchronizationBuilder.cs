using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.DSQ
{
	// Token: 0x02000108 RID: 264
	internal sealed class GroupSynchronizationBuilder
	{
		// Token: 0x060008B1 RID: 2225 RVA: 0x00022DCC File Offset: 0x00020FCC
		private GroupSynchronizationBuilder(DataShapeBuilderContext dataShapeBuilderContext, DataShapeBindingAxis axis, DataShapeBuilder dataShapeBuilder, IntermediateDataShapeReferenceSchema schema, IReadOnlyList<DataMemberBuilderPair> primaryDynamics, IReadOnlyList<DataMemberBuilderPair> secondaryDynamics, IFeatureSwitchProvider fsProvider, bool isPrimary, int selectCount)
		{
			this._dataShapeBuilderContext = dataShapeBuilderContext;
			this._bindingAxis = axis;
			this._dataShapeBuilder = dataShapeBuilder;
			this._dataShapeSchema = schema;
			this._primaryDynamics = primaryDynamics;
			this._secondaryDynamics = secondaryDynamics;
			this._featureSwitchProvider = fsProvider;
			this._isPrimary = isPrimary;
			this._selectCount = selectCount;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00022E24 File Offset: 0x00021024
		internal static void BuildGroupSynchronizationDataShapes(DataShapeBuilderContext dataShapeBuilderContext, DataShapeBinding dataShapeBinding, DataShapeBuilder dataShapeBuilder, IReadOnlyList<DataMemberBuilderPair> primaryDynamics, IReadOnlyList<DataMemberBuilderPair> secondaryDynamics, IFeatureSwitchProvider fsProvider, int selectCount)
		{
			if (dataShapeBinding == null)
			{
				return;
			}
			DataShapeBindingAxis primary = dataShapeBinding.Primary;
			if (((primary != null) ? primary.Synchronization : null) == null)
			{
				DataShapeBindingAxis secondary = dataShapeBinding.Secondary;
				if (((secondary != null) ? secondary.Synchronization : null) == null)
				{
					return;
				}
			}
			IntermediateDataShapeReferenceSchema intermediateDataShapeReferenceSchema = dataShapeBuilderContext.CreateReferenceSchema(dataShapeBuilder.Id);
			GroupSynchronizationBuilder.BuildGroupSynchronizationDataShape(dataShapeBinding.Primary, dataShapeBuilderContext, dataShapeBuilder, intermediateDataShapeReferenceSchema, primaryDynamics, secondaryDynamics, fsProvider, true, selectCount);
			GroupSynchronizationBuilder.BuildGroupSynchronizationDataShape(dataShapeBinding.Secondary, dataShapeBuilderContext, dataShapeBuilder, intermediateDataShapeReferenceSchema, primaryDynamics, secondaryDynamics, fsProvider, false, selectCount);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00022EA0 File Offset: 0x000210A0
		private static void BuildGroupSynchronizationDataShape(DataShapeBindingAxis axis, DataShapeBuilderContext dataShapeBuilderContext, DataShapeBuilder dataShapeBuilder, IntermediateDataShapeReferenceSchema dataShapeSchema, IReadOnlyList<DataMemberBuilderPair> primaryDynamics, IReadOnlyList<DataMemberBuilderPair> secondaryDynamics, IFeatureSwitchProvider fsProvider, bool isPrimary, int selectCount)
		{
			if (axis == null || axis.Synchronization.IsNullOrEmptyCollection<DataShapeBindingAxisSynchronizedGroupingBlock>())
			{
				return;
			}
			new GroupSynchronizationBuilder(dataShapeBuilderContext, axis, dataShapeBuilder, dataShapeSchema, primaryDynamics, secondaryDynamics, fsProvider, isPrimary, selectCount).Build();
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00022EDC File Offset: 0x000210DC
		private void Build()
		{
			for (int i = 0; i < this._bindingAxis.Synchronization.Count; i++)
			{
				DataShapeBindingAxisSynchronizedGroupingBlock dataShapeBindingAxisSynchronizedGroupingBlock = this._bindingAxis.Synchronization[i];
				string text = this._dataShapeBuilderContext.CreateGroupSynchronizationDataShapeId();
				DataShapeBuilder dataShapeBuilder = DataShapeBuilder.With(text, null, false, null, DataShapeUsage.Synchronization);
				DataShapeBuilderContext dataShapeBuilderContext = this._dataShapeBuilderContext.CreateSynchronizationContext(this._selectCount, null);
				Dictionary<string, int> dictionary = new Dictionary<string, int>(dataShapeBindingAxisSynchronizedGroupingBlock.Groupings.Count, StringComparer.Ordinal);
				HashSet<int> hashSet = new HashSet<int>();
				this.AddMemberGroups<DataShapeBuilder<DataShape>>(text, dataShapeBindingAxisSynchronizedGroupingBlock, 0, dataShapeBuilder, dataShapeBuilderContext, dictionary, hashSet);
				this.PopulateParentBindingDescriptor(i, text, dataShapeBindingAxisSynchronizedGroupingBlock.Groupings, dataShapeBuilderContext, dictionary, hashSet.AsReadOnlyList<int>());
				this._dataShapeBuilder.WithDataShape(dataShapeBuilder.Parent());
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00022FA0 File Offset: 0x000211A0
		private void AddMemberGroups<TParent>(string syncDataShapeId, DataShapeBindingAxisSynchronizedGroupingBlock syncBlock, int groupIndex, IDataMemberContainer<TParent> parentBuilder, DataShapeBuilderContext groupSyncDataShapeBuilderContext, Dictionary<string, int> syncGroupIndexByMemberId, HashSet<int> visitedSelectIndices)
		{
			if (groupIndex >= syncBlock.Groupings.Count)
			{
				return;
			}
			int num = syncBlock.Groupings[groupIndex];
			if (groupIndex == syncBlock.Groupings.Count - 1)
			{
				this.AddSynchronizationIndex(syncDataShapeId, num);
			}
			IntermediateGroupSchema grouping = this._dataShapeSchema.GetGrouping(num, this._isPrimary);
			string text;
			DataMemberBuilder<TParent> dataMemberBuilder = this.CreateDataMemberBuilder<TParent>(grouping, num, parentBuilder, out text);
			GroupBuilder<DataMemberBuilder<TParent>> groupBuilder = dataMemberBuilder.WithGroup(false);
			string value = dataMemberBuilder.Id.Value;
			groupBuilder.WithSynchronizationGrouping(groupSyncDataShapeBuilderContext, grouping, groupIndex, true, value, text, grouping.SubtotalType, visitedSelectIndices);
			syncGroupIndexByMemberId.Add(value, num);
			this.AddMemberGroups<DataMemberBuilder<TParent>>(syncDataShapeId, syncBlock, groupIndex + 1, dataMemberBuilder, groupSyncDataShapeBuilderContext, syncGroupIndexByMemberId, visitedSelectIndices);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00023048 File Offset: 0x00021248
		private void AddSynchronizationIndex(string syncDataShapeId, int groupIndex)
		{
			DataMemberBuilderPair targetDataMemberBuilderPair = this.GetTargetDataMemberBuilderPair(groupIndex);
			string text = this._dataShapeBuilderContext.AddSynchronizationIndexCalculation<DataMemberBuilder<DataMember>>(targetDataMemberBuilderPair.Dynamic, syncDataShapeId);
			this._dataShapeBuilderContext.AddSynchronizationIndex(this._isPrimary, groupIndex, text);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00023084 File Offset: 0x00021284
		private DataMemberBuilder<TParent> CreateDataMemberBuilder<TParent>(IntermediateGroupSchema groupingSchema, int syncGroupIndex, IDataMemberContainer<TParent> parentBuilder, out string subtotalMemberId)
		{
			DataMemberBuilder<TParent> dataMemberBuilder = null;
			subtotalMemberId = null;
			this.GetTargetDataMemberBuilderPair(syncGroupIndex);
			switch (groupingSchema.SubtotalType)
			{
			case SubtotalType.None:
			{
				string text = this._dataShapeBuilderContext.CreateMemberId();
				dataMemberBuilder = parentBuilder.WithPrimaryMember(text, null);
				break;
			}
			case SubtotalType.Before:
			{
				subtotalMemberId = this._dataShapeBuilderContext.CreateMemberId();
				parentBuilder.WithStaticMember(subtotalMemberId, true, null);
				string text = this._dataShapeBuilderContext.CreateMemberId();
				dataMemberBuilder = parentBuilder.WithPrimaryMember(text, null);
				break;
			}
			case SubtotalType.After:
			{
				string text = this._dataShapeBuilderContext.CreateMemberId();
				dataMemberBuilder = parentBuilder.WithPrimaryMember(text, null);
				subtotalMemberId = this._dataShapeBuilderContext.CreateMemberId();
				parentBuilder.WithStaticMember(subtotalMemberId, true, null);
				break;
			}
			default:
				Contract.RetailAssert(true, "Unexpected SubtotalType.");
				break;
			}
			return dataMemberBuilder;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002317C File Offset: 0x0002137C
		private DataMemberBuilderPair GetTargetDataMemberBuilderPair(int groupIndex)
		{
			if (this._isPrimary)
			{
				return this._primaryDynamics[groupIndex];
			}
			return this._secondaryDynamics[groupIndex];
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x000231A0 File Offset: 0x000213A0
		private void PopulateParentBindingDescriptor(int syncBlockIndex, string dataShapeName, IList<int> syncBlockGroupings, DataShapeBuilderContext groupSyncDataShapeBuilderContext, Dictionary<string, int> syncGroupIndexByMemberId, IReadOnlyList<int> selectIndices)
		{
			this._dataShapeBuilderContext.AddSynchronization(syncBlockIndex, this._isPrimary, dataShapeName, syncBlockGroupings);
			QueryBindingDescriptor queryBindingDescriptor = groupSyncDataShapeBuilderContext.CreateDescriptor(null, this._featureSwitchProvider);
			foreach (DataShapeExpressionsAxisGrouping dataShapeExpressionsAxisGrouping in queryBindingDescriptor.Expressions.Primary.Groupings)
			{
				int num;
				if (syncGroupIndexByMemberId.TryGetValue(dataShapeExpressionsAxisGrouping.Member, out num))
				{
					this._dataShapeBuilderContext.AddSynchronizedGroup(this._isPrimary, num, dataShapeExpressionsAxisGrouping);
				}
			}
			for (int i = 0; i < selectIndices.Count; i++)
			{
				if (i < queryBindingDescriptor.Select.Length)
				{
					SelectBinding selectBinding = queryBindingDescriptor.Select[selectIndices[i]];
					this._dataShapeBuilderContext.AddSynchronizedSelectBinding(selectIndices[i], selectBinding);
				}
			}
		}

		// Token: 0x04000468 RID: 1128
		private readonly DataShapeBuilderContext _dataShapeBuilderContext;

		// Token: 0x04000469 RID: 1129
		private readonly DataShapeBindingAxis _bindingAxis;

		// Token: 0x0400046A RID: 1130
		private readonly DataShapeBuilder _dataShapeBuilder;

		// Token: 0x0400046B RID: 1131
		private readonly IReadOnlyList<DataMemberBuilderPair> _primaryDynamics;

		// Token: 0x0400046C RID: 1132
		private readonly IReadOnlyList<DataMemberBuilderPair> _secondaryDynamics;

		// Token: 0x0400046D RID: 1133
		private readonly bool _isPrimary;

		// Token: 0x0400046E RID: 1134
		private readonly int _selectCount;

		// Token: 0x0400046F RID: 1135
		private readonly IntermediateDataShapeReferenceSchema _dataShapeSchema;

		// Token: 0x04000470 RID: 1136
		private readonly IFeatureSwitchProvider _featureSwitchProvider;
	}
}
