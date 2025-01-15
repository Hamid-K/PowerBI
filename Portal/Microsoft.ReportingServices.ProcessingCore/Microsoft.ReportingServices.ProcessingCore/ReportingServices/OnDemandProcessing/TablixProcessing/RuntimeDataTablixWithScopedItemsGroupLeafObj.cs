using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008E6 RID: 2278
	[PersistedWithinRequestOnly]
	internal abstract class RuntimeDataTablixWithScopedItemsGroupLeafObj : RuntimeDataTablixGroupLeafObj
	{
		// Token: 0x06007CDB RID: 31963 RVA: 0x00203569 File Offset: 0x00201769
		internal RuntimeDataTablixWithScopedItemsGroupLeafObj()
		{
		}

		// Token: 0x06007CDC RID: 31964 RVA: 0x00203571 File Offset: 0x00201771
		internal RuntimeDataTablixWithScopedItemsGroupLeafObj(RuntimeDataTablixGroupRootObjReference groupRootRef, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(groupRootRef, objectType)
		{
		}

		// Token: 0x06007CDD RID: 31965
		protected abstract List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> GetGroupScopedContents(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode member);

		// Token: 0x06007CDE RID: 31966
		protected abstract List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> GetCellContents(Cell cell);

		// Token: 0x06007CDF RID: 31967
		protected abstract RuntimeCell CreateCell(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode outerGroupingMember, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode innerGroupingMember);

		// Token: 0x06007CE0 RID: 31968 RVA: 0x0020357C File Offset: 0x0020177C
		protected override void InitializeGroupScopedItems(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode member, ref DataActions innerDataAction)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> groupScopedContents = this.GetGroupScopedContents(member);
			if (groupScopedContents != null)
			{
				if (this.m_groupScopedItems == null)
				{
					this.m_groupScopedItems = new RuntimeRICollection(groupScopedContents.Count);
				}
				this.m_groupScopedItems.AddItems(this.m_selfReference, groupScopedContents, ref innerDataAction, this.m_odpContext);
			}
		}

		// Token: 0x06007CE1 RID: 31969 RVA: 0x002035C8 File Offset: 0x002017C8
		protected override void ConstructOutermostCellContents(Cell cell)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> cellContents = this.GetCellContents(cell);
			if (cellContents != null)
			{
				DataActions dataActions = DataActions.None;
				if (this.m_groupScopedItems == null)
				{
					this.m_groupScopedItems = new RuntimeRICollection(cellContents.Count);
				}
				this.m_groupScopedItems.AddItems(this.m_selfReference, cellContents, ref dataActions, this.m_odpContext);
			}
		}

		// Token: 0x06007CE2 RID: 31970 RVA: 0x00203618 File Offset: 0x00201818
		internal override void CreateCell(RuntimeCells cellsCollection, int collectionKey, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode outerGroupingMember, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode innerGroupingMember, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef)
		{
			RuntimeCell runtimeCell = this.CreateCell(outerGroupingMember, innerGroupingMember);
			if (runtimeCell.SelfReference == null)
			{
				cellsCollection.AddCell(collectionKey, runtimeCell);
				return;
			}
			IReference<RuntimeCell> selfReference = runtimeCell.SelfReference;
			selfReference.UnPinValue();
			cellsCollection.AddCell(collectionKey, selfReference);
		}

		// Token: 0x06007CE3 RID: 31971 RVA: 0x0020365B File Offset: 0x0020185B
		protected override void SendToInner()
		{
			base.SendToInner();
			if ((base.IsOuterGrouping || !this.m_odpContext.PeerOuterGroupProcessing) && this.m_groupScopedItems != null)
			{
				this.m_groupScopedItems.FirstPassNextDataRow(this.m_odpContext);
			}
		}

		// Token: 0x06007CE4 RID: 31972 RVA: 0x00203691 File Offset: 0x00201891
		protected override void TraverseStaticContents(ProcessingStages operation, AggregateUpdateContext context)
		{
			if (this.m_groupScopedItems != null)
			{
				if (operation == ProcessingStages.SortAndFilter)
				{
					this.m_groupScopedItems.SortAndFilter(context);
					return;
				}
				if (operation == ProcessingStages.UpdateAggregates)
				{
					this.m_groupScopedItems.UpdateAggregates(context);
					return;
				}
				Global.Tracer.Assert(false, "Unknown operation in TraverseStaticContents.");
			}
		}

		// Token: 0x06007CE5 RID: 31973 RVA: 0x002036D0 File Offset: 0x002018D0
		protected override void CalculateRunningValuesForStaticContents(AggregateUpdateContext aggContext)
		{
			if (this.m_processHeading)
			{
				RuntimeDataTablixGroupRootObjReference runtimeDataTablixGroupRootObjReference = (RuntimeDataTablixGroupRootObjReference)this.m_hierarchyRoot;
				using (runtimeDataTablixGroupRootObjReference.PinValue())
				{
					Dictionary<string, IReference<RuntimeGroupRootObj>> groupCollection = runtimeDataTablixGroupRootObjReference.Value().GroupCollection;
					RuntimeGroupRootObjReference runtimeGroupRootObjReference = runtimeDataTablixGroupRootObjReference;
					if (this.m_groupScopedItems != null)
					{
						this.m_groupScopedItems.CalculateRunningValues(groupCollection, runtimeGroupRootObjReference, aggContext);
					}
				}
			}
		}

		// Token: 0x06007CE6 RID: 31974 RVA: 0x00203738 File Offset: 0x00201938
		protected override void CreateInstanceHeadingContents()
		{
			if (base.MemberDef.InScopeEventSources != null)
			{
				UserSortFilterContext.ProcessEventSources(this.m_odpContext, this, base.MemberDef.InScopeEventSources);
			}
			if (this.m_groupScopedItems != null)
			{
				List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> groupScopedContents = this.GetGroupScopedContents(base.MemberDef);
				if (groupScopedContents != null)
				{
					this.m_groupScopedItems.CreateInstances(this.m_memberInstance, this.m_odpContext, this.m_selfReference, groupScopedContents);
				}
			}
		}

		// Token: 0x06007CE7 RID: 31975 RVA: 0x002037A0 File Offset: 0x002019A0
		protected override void CreateOutermostStaticCellContents(Cell cell, DataCellInstance cellInstance)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> cellContents = this.GetCellContents(cell);
			if (this.m_groupScopedItems != null && cellContents != null)
			{
				this.m_groupScopedItems.CreateInstances(cellInstance, this.m_odpContext, this.m_selfReference, cellContents);
			}
		}

		// Token: 0x06007CE8 RID: 31976 RVA: 0x002037D9 File Offset: 0x002019D9
		internal override RuntimeDataTablixObjReference GetNestedDataRegion(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion rifDataRegion)
		{
			Global.Tracer.Assert(this.m_groupScopedItems != null, "Cannot find data region.");
			return this.m_groupScopedItems.GetDataRegionObj(rifDataRegion);
		}

		// Token: 0x06007CE9 RID: 31977 RVA: 0x002037FF File Offset: 0x002019FF
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixWithScopedItemsGroupLeafObj;
		}

		// Token: 0x06007CEA RID: 31978 RVA: 0x00203808 File Offset: 0x00201A08
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeDataTablixWithScopedItemsGroupLeafObj.m_declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.GroupScopedItems)
				{
					writer.Write(this.m_groupScopedItems);
				}
				else
				{
					Global.Tracer.Assert(false, "Unsupported member name: " + writer.CurrentMember.MemberName.ToString() + ".");
				}
			}
		}

		// Token: 0x06007CEB RID: 31979 RVA: 0x0020388C File Offset: 0x00201A8C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeDataTablixWithScopedItemsGroupLeafObj.m_declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.GroupScopedItems)
				{
					this.m_groupScopedItems = (RuntimeRICollection)reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false, "Unsupported member name: " + reader.CurrentMember.MemberName.ToString() + ".");
				}
			}
		}

		// Token: 0x06007CEC RID: 31980 RVA: 0x00203912 File Offset: 0x00201B12
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007CED RID: 31981 RVA: 0x0020391C File Offset: 0x00201B1C
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeDataTablixWithScopedItemsGroupLeafObj.m_declaration == null)
			{
				RuntimeDataTablixWithScopedItemsGroupLeafObj.m_declaration = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixWithScopedItemsGroupLeafObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupLeafObj, new List<MemberInfo>
				{
					new MemberInfo(MemberName.GroupScopedItems, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeRICollection)
				});
			}
			return RuntimeDataTablixWithScopedItemsGroupLeafObj.m_declaration;
		}

		// Token: 0x170028BE RID: 10430
		// (get) Token: 0x06007CEE RID: 31982 RVA: 0x00203962 File Offset: 0x00201B62
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.SizeOf(this.m_groupScopedItems);
			}
		}

		// Token: 0x04003DD5 RID: 15829
		private RuntimeRICollection m_groupScopedItems;

		// Token: 0x04003DD6 RID: 15830
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeDataTablixWithScopedItemsGroupLeafObj.GetDeclaration();
	}
}
