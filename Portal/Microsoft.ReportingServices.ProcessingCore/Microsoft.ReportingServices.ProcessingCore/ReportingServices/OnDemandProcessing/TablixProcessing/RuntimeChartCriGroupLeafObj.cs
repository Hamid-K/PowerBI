using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008E9 RID: 2281
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeChartCriGroupLeafObj : RuntimeDataTablixGroupLeafObj
	{
		// Token: 0x06007D06 RID: 32006 RVA: 0x00203B24 File Offset: 0x00201D24
		internal RuntimeChartCriGroupLeafObj()
		{
		}

		// Token: 0x06007D07 RID: 32007 RVA: 0x00203B2C File Offset: 0x00201D2C
		internal RuntimeChartCriGroupLeafObj(RuntimeDataTablixGroupRootObjReference groupRootRef, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(groupRootRef, objectType)
		{
		}

		// Token: 0x06007D08 RID: 32008 RVA: 0x00203B36 File Offset: 0x00201D36
		internal override RuntimeDataTablixObjReference GetNestedDataRegion(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion rifDataRegion)
		{
			Global.Tracer.Assert(false, "This type of group leaf does not support nested data regions.");
			throw new InvalidOperationException();
		}

		// Token: 0x06007D09 RID: 32009 RVA: 0x00203B4D File Offset: 0x00201D4D
		protected override void ConstructOutermostCellContents(Cell cell)
		{
		}

		// Token: 0x06007D0A RID: 32010 RVA: 0x00203B50 File Offset: 0x00201D50
		internal override void CreateCell(RuntimeCells cellsCollection, int collectionKey, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode outerGroupingMember, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode innerGroupingMember, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef)
		{
			RuntimeCell runtimeCell = new RuntimeChartCriCell((RuntimeChartCriGroupLeafObjReference)this.m_selfReference, outerGroupingMember, innerGroupingMember, !this.m_hasInnerHierarchy);
			if (runtimeCell.SelfReference == null)
			{
				cellsCollection.AddCell(collectionKey, runtimeCell);
				return;
			}
			IReference<RuntimeCell> selfReference = runtimeCell.SelfReference;
			selfReference.UnPinValue();
			cellsCollection.AddCell(collectionKey, selfReference);
		}

		// Token: 0x06007D0B RID: 32011 RVA: 0x00203BA6 File Offset: 0x00201DA6
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeChartCriGroupLeafObj.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007D0C RID: 32012 RVA: 0x00203BDE File Offset: 0x00201DDE
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeChartCriGroupLeafObj.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007D0D RID: 32013 RVA: 0x00203C16 File Offset: 0x00201E16
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007D0E RID: 32014 RVA: 0x00203C20 File Offset: 0x00201E20
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriGroupLeafObj;
		}

		// Token: 0x06007D0F RID: 32015 RVA: 0x00203C24 File Offset: 0x00201E24
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeChartCriGroupLeafObj.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriGroupLeafObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupLeafObj, list);
			}
			return RuntimeChartCriGroupLeafObj.m_declaration;
		}

		// Token: 0x04003DD9 RID: 15833
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeChartCriGroupLeafObj.GetDeclaration();
	}
}
