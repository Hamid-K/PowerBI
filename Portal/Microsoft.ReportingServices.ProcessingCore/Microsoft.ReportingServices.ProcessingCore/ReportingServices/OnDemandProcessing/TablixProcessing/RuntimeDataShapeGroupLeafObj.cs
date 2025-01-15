using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008E8 RID: 2280
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeDataShapeGroupLeafObj : RuntimeDataTablixWithScopedItemsGroupLeafObj
	{
		// Token: 0x06007CFB RID: 31995 RVA: 0x00203A51 File Offset: 0x00201C51
		internal RuntimeDataShapeGroupLeafObj()
		{
		}

		// Token: 0x06007CFC RID: 31996 RVA: 0x00203A59 File Offset: 0x00201C59
		internal RuntimeDataShapeGroupLeafObj(RuntimeDataTablixGroupRootObjReference groupRootRef, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(groupRootRef, objectType)
		{
		}

		// Token: 0x06007CFD RID: 31997 RVA: 0x00203A63 File Offset: 0x00201C63
		protected override List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> GetGroupScopedContents(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode member)
		{
			return ((DataShapeMember)member).GroupScopedContentsForProcessing;
		}

		// Token: 0x06007CFE RID: 31998 RVA: 0x00203A70 File Offset: 0x00201C70
		protected override List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> GetCellContents(Cell cell)
		{
			DataShapeIntersection dataShapeIntersection = cell as DataShapeIntersection;
			if (dataShapeIntersection != null && dataShapeIntersection.HasInnerGroupTreeHierarchy)
			{
				return dataShapeIntersection.DataShapes;
			}
			return null;
		}

		// Token: 0x06007CFF RID: 31999 RVA: 0x00203A97 File Offset: 0x00201C97
		protected override RuntimeCell CreateCell(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode outerGroupingMember, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode innerGroupingMember)
		{
			return new RuntimeDataShapeIntersection((RuntimeDataShapeGroupLeafObjReference)this.m_selfReference, (DataShapeMember)outerGroupingMember, (DataShapeMember)innerGroupingMember, !this.m_hasInnerHierarchy);
		}

		// Token: 0x06007D00 RID: 32000 RVA: 0x00203ABE File Offset: 0x00201CBE
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeGroupLeafObj;
		}

		// Token: 0x06007D01 RID: 32001 RVA: 0x00203AC5 File Offset: 0x00201CC5
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
		}

		// Token: 0x06007D02 RID: 32002 RVA: 0x00203ACE File Offset: 0x00201CCE
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
		}

		// Token: 0x06007D03 RID: 32003 RVA: 0x00203AD7 File Offset: 0x00201CD7
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007D04 RID: 32004 RVA: 0x00203AE4 File Offset: 0x00201CE4
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeDataShapeGroupLeafObj.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				RuntimeDataShapeGroupLeafObj.m_declaration = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataShapeGroupLeafObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixWithScopedItemsGroupLeafObj, list);
			}
			return RuntimeDataShapeGroupLeafObj.m_declaration;
		}

		// Token: 0x04003DD8 RID: 15832
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeDataShapeGroupLeafObj.GetDeclaration();
	}
}
