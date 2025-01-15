using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008B8 RID: 2232
	[PersistedWithinRequestOnly]
	internal abstract class RuntimeCellWithContents : RuntimeCell
	{
		// Token: 0x060079DF RID: 31199 RVA: 0x001F6C74 File Offset: 0x001F4E74
		internal RuntimeCellWithContents()
		{
		}

		// Token: 0x060079E0 RID: 31200 RVA: 0x001F6C7C File Offset: 0x001F4E7C
		internal RuntimeCellWithContents(RuntimeDataTablixGroupLeafObjReference owner, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode outerGroupingMember, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode innerGroupingMember, bool innermost)
			: base(owner, outerGroupingMember, innerGroupingMember, innermost)
		{
		}

		// Token: 0x060079E1 RID: 31201
		protected abstract List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> GetCellContents(Cell cell);

		// Token: 0x060079E2 RID: 31202 RVA: 0x001F6C8C File Offset: 0x001F4E8C
		protected override void ConstructCellContents(Cell cell, ref DataActions dataAction)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> cellContents = this.GetCellContents(cell);
			if (cellContents != null)
			{
				this.m_owner.Value().OdpContext.TablixProcessingScalabilityCache.AllocateAndPin<RuntimeCell>(this, this.m_outerGroupDynamicIndex);
				if (this.m_cellContents == null)
				{
					this.m_cellContents = new RuntimeRICollection(cellContents.Count);
				}
				this.m_cellContents.AddItems(this.m_selfReference, cellContents, ref dataAction, this.m_owner.Value().OdpContext);
			}
		}

		// Token: 0x060079E3 RID: 31203 RVA: 0x001F6D04 File Offset: 0x001F4F04
		internal override bool NextRow()
		{
			bool flag = base.NextRow();
			if (this.m_cellContents != null)
			{
				OnDemandProcessingContext odpContext = this.m_owner.Value().OdpContext;
				this.m_cellContents.FirstPassNextDataRow(odpContext);
			}
			return flag;
		}

		// Token: 0x060079E4 RID: 31204 RVA: 0x001F6D3C File Offset: 0x001F4F3C
		protected override void TraverseCellContents(ProcessingStages operation, AggregateUpdateContext context)
		{
			if (this.m_cellContents != null)
			{
				if (operation == ProcessingStages.SortAndFilter)
				{
					this.m_cellContents.SortAndFilter(context);
					return;
				}
				if (operation == ProcessingStages.UpdateAggregates)
				{
					this.m_cellContents.UpdateAggregates(context);
					return;
				}
				Global.Tracer.Assert(false, "Invalid operation for TraverseCellContents.");
			}
		}

		// Token: 0x060079E5 RID: 31205 RVA: 0x001F6D7A File Offset: 0x001F4F7A
		protected override void CalculateInnerRunningValues(Dictionary<string, IReference<RuntimeGroupRootObj>> groupCol, IReference<RuntimeGroupRootObj> lastGroup, AggregateUpdateContext aggContext)
		{
			if (this.m_cellContents != null)
			{
				this.m_cellContents.CalculateRunningValues(groupCol, lastGroup, aggContext);
			}
		}

		// Token: 0x060079E6 RID: 31206 RVA: 0x001F6D94 File Offset: 0x001F4F94
		protected override void CreateInstanceCellContents(Cell cell, DataCellInstance cellInstance, OnDemandProcessingContext odpContext)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> cellContents = this.GetCellContents(cell);
			if (cellContents != null && this.m_cellContents != null)
			{
				this.m_cellContents.CreateInstances(cellInstance, odpContext, this.m_selfReference, cellContents);
			}
		}

		// Token: 0x060079E7 RID: 31207 RVA: 0x001F6DC8 File Offset: 0x001F4FC8
		public override IOnDemandMemberOwnerInstanceReference GetDataRegionInstance(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion rifDataRegion)
		{
			Global.Tracer.Assert(this.m_cellContents != null, "Cannot find data region.");
			return this.m_cellContents.GetDataRegionObj(rifDataRegion);
		}

		// Token: 0x060079E8 RID: 31208 RVA: 0x001F6DF0 File Offset: 0x001F4FF0
		public override IReference<IDataCorrelation> GetIdcReceiver(IRIFReportDataScope scope)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = scope as Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion;
			Global.Tracer.Assert(dataRegion != null, "Invalid scope.");
			return this.m_cellContents.GetDataRegionObj(dataRegion);
		}

		// Token: 0x060079E9 RID: 31209 RVA: 0x001F6E23 File Offset: 0x001F5023
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCellWithContents;
		}

		// Token: 0x060079EA RID: 31210 RVA: 0x001F6E2C File Offset: 0x001F502C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeCellWithContents.m_declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.CellContents)
				{
					writer.Write(this.m_cellContents);
				}
				else
				{
					Global.Tracer.Assert(false, "Unsupported member name: " + writer.CurrentMember.MemberName.ToString() + ".");
				}
			}
		}

		// Token: 0x060079EB RID: 31211 RVA: 0x001F6EB0 File Offset: 0x001F50B0
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeCellWithContents.m_declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.CellContents)
				{
					this.m_cellContents = (RuntimeRICollection)reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false, "Unsupported member name: " + reader.CurrentMember.MemberName.ToString() + ".");
				}
			}
		}

		// Token: 0x060079EC RID: 31212 RVA: 0x001F6F36 File Offset: 0x001F5136
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x060079ED RID: 31213 RVA: 0x001F6F40 File Offset: 0x001F5140
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeCellWithContents.m_declaration == null)
			{
				RuntimeCellWithContents.m_declaration = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCellWithContents, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCell, new List<MemberInfo>
				{
					new MemberInfo(MemberName.CellContents, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeRICollection)
				});
			}
			return RuntimeCellWithContents.m_declaration;
		}

		// Token: 0x1700283B RID: 10299
		// (get) Token: 0x060079EE RID: 31214 RVA: 0x001F6F86 File Offset: 0x001F5186
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.SizeOf(this.m_cellContents);
			}
		}

		// Token: 0x04003D21 RID: 15649
		private RuntimeRICollection m_cellContents;

		// Token: 0x04003D22 RID: 15650
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeCellWithContents.GetDeclaration();
	}
}
