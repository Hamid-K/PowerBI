using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008DB RID: 2267
	[PersistedWithinRequestOnly]
	internal abstract class RuntimeDataTablixWithScopedItemsObj : RuntimeDataTablixObj, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007BFC RID: 31740 RVA: 0x001FE30B File Offset: 0x001FC50B
		internal RuntimeDataTablixWithScopedItemsObj()
		{
		}

		// Token: 0x06007BFD RID: 31741 RVA: 0x001FE313 File Offset: 0x001FC513
		internal RuntimeDataTablixWithScopedItemsObj(IReference<IScope> outerScope, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataTablixDef, ref DataActions dataAction, OnDemandProcessingContext odpContext, bool onePassProcess, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(outerScope, dataTablixDef, ref dataAction, odpContext, onePassProcess, objectType)
		{
		}

		// Token: 0x06007BFE RID: 31742
		protected abstract List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> GetDataRegionScopedItems();

		// Token: 0x06007BFF RID: 31743 RVA: 0x001FE324 File Offset: 0x001FC524
		protected override void ConstructRuntimeStructure(ref DataActions innerDataAction, bool onePassProcess)
		{
			this.m_dataRegionScopedItems = null;
			base.ConstructRuntimeStructure(ref innerDataAction, onePassProcess);
			List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> dataRegionScopedItems = this.GetDataRegionScopedItems();
			if (dataRegionScopedItems != null)
			{
				this.m_dataRegionScopedItems = new RuntimeRICollection(this.m_selfReference, dataRegionScopedItems, ref innerDataAction, this.m_odpContext);
			}
		}

		// Token: 0x06007C00 RID: 31744 RVA: 0x001FE363 File Offset: 0x001FC563
		internal override RuntimeDataTablixObjReference GetNestedDataRegion(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion rifDataRegion)
		{
			Global.Tracer.Assert(this.m_dataRegionScopedItems != null, "Cannot find data region.");
			return this.m_dataRegionScopedItems.GetDataRegionObj(rifDataRegion);
		}

		// Token: 0x06007C01 RID: 31745 RVA: 0x001FE389 File Offset: 0x001FC589
		protected override void SendToInner()
		{
			base.SendToInner();
			if (this.m_dataRegionScopedItems != null)
			{
				this.m_dataRegionScopedItems.FirstPassNextDataRow(this.m_odpContext);
			}
		}

		// Token: 0x06007C02 RID: 31746 RVA: 0x001FE3AA File Offset: 0x001FC5AA
		protected override void CalculateRunningValuesForTopLevelStaticContents(Dictionary<string, IReference<RuntimeGroupRootObj>> groupCol, IReference<RuntimeGroupRootObj> lastGroup, AggregateUpdateContext aggContext)
		{
			if (this.m_dataRegionScopedItems != null)
			{
				this.m_dataRegionScopedItems.CalculateRunningValues(groupCol, lastGroup, aggContext);
			}
		}

		// Token: 0x06007C03 RID: 31747 RVA: 0x001FE3C2 File Offset: 0x001FC5C2
		protected override void Traverse(ProcessingStages operation, ITraversalContext context)
		{
			base.Traverse(operation, context);
			if (this.m_dataRegionScopedItems != null)
			{
				this.TraverseDataRegionScopedItems(operation, context);
			}
		}

		// Token: 0x06007C04 RID: 31748 RVA: 0x001FE3DC File Offset: 0x001FC5DC
		private void TraverseDataRegionScopedItems(ProcessingStages operation, ITraversalContext context)
		{
			if (operation == ProcessingStages.SortAndFilter)
			{
				this.m_dataRegionScopedItems.SortAndFilter((AggregateUpdateContext)context);
				return;
			}
			if (operation != ProcessingStages.UpdateAggregates)
			{
				Global.Tracer.Assert(false, "Unknown ProcessingStage for TraverseDataRegionScopedItems");
				return;
			}
			this.m_dataRegionScopedItems.UpdateAggregates((AggregateUpdateContext)context);
		}

		// Token: 0x06007C05 RID: 31749 RVA: 0x001FE41C File Offset: 0x001FC61C
		protected override void CreateDataRegionScopedInstance(DataRegionInstance dataRegionInstance)
		{
			base.CreateDataRegionScopedInstance(dataRegionInstance);
			if (this.m_dataRegionScopedItems != null)
			{
				this.m_dataRegionScopedItems.CreateInstances(dataRegionInstance, this.m_odpContext, this.m_selfReference, this.GetDataRegionScopedItems());
			}
		}

		// Token: 0x06007C06 RID: 31750 RVA: 0x001FE44B File Offset: 0x001FC64B
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixWithScopedItemsObj;
		}

		// Token: 0x06007C07 RID: 31751 RVA: 0x001FE454 File Offset: 0x001FC654
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeDataTablixWithScopedItemsObj.m_declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.DataRegionScopedItems)
				{
					writer.Write(this.m_dataRegionScopedItems);
				}
				else
				{
					Global.Tracer.Assert(false, "Unsupported member name: " + writer.CurrentMember.MemberName.ToString() + ".");
				}
			}
		}

		// Token: 0x06007C08 RID: 31752 RVA: 0x001FE4D8 File Offset: 0x001FC6D8
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeDataTablixWithScopedItemsObj.m_declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.DataRegionScopedItems)
				{
					this.m_dataRegionScopedItems = (RuntimeRICollection)reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false, "Unsupported member name: " + reader.CurrentMember.MemberName.ToString() + ".");
				}
			}
		}

		// Token: 0x06007C09 RID: 31753 RVA: 0x001FE55E File Offset: 0x001FC75E
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007C0A RID: 31754 RVA: 0x001FE568 File Offset: 0x001FC768
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeDataTablixWithScopedItemsObj.m_declaration == null)
			{
				RuntimeDataTablixWithScopedItemsObj.m_declaration = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixWithScopedItemsObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixObj, new List<MemberInfo>
				{
					new MemberInfo(MemberName.DataRegionScopedItems, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeRICollection)
				});
			}
			return RuntimeDataTablixWithScopedItemsObj.m_declaration;
		}

		// Token: 0x1700289A RID: 10394
		// (get) Token: 0x06007C0B RID: 31755 RVA: 0x001FE5AB File Offset: 0x001FC7AB
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.SizeOf(this.m_dataRegionScopedItems);
			}
		}

		// Token: 0x04003D97 RID: 15767
		private RuntimeRICollection m_dataRegionScopedItems;

		// Token: 0x04003D98 RID: 15768
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeDataTablixWithScopedItemsObj.GetDeclaration();
	}
}
