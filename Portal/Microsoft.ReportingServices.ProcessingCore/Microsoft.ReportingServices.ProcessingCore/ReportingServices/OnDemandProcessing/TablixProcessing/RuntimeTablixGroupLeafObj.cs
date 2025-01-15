using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008E7 RID: 2279
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeTablixGroupLeafObj : RuntimeDataTablixWithScopedItemsGroupLeafObj
	{
		// Token: 0x06007CF0 RID: 31984 RVA: 0x00203982 File Offset: 0x00201B82
		internal RuntimeTablixGroupLeafObj()
		{
		}

		// Token: 0x06007CF1 RID: 31985 RVA: 0x0020398A File Offset: 0x00201B8A
		internal RuntimeTablixGroupLeafObj(RuntimeDataTablixGroupRootObjReference groupRootRef, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(groupRootRef, objectType)
		{
		}

		// Token: 0x06007CF2 RID: 31986 RVA: 0x00203994 File Offset: 0x00201B94
		protected override List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> GetGroupScopedContents(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode member)
		{
			return ((TablixMember)member).GroupScopedContentsForProcessing;
		}

		// Token: 0x06007CF3 RID: 31987 RVA: 0x002039A4 File Offset: 0x00201BA4
		protected override List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> GetCellContents(Cell cell)
		{
			TablixCell tablixCell = cell as TablixCell;
			if (tablixCell != null && tablixCell.HasInnerGroupTreeHierarchy)
			{
				return tablixCell.CellContentCollection;
			}
			return null;
		}

		// Token: 0x06007CF4 RID: 31988 RVA: 0x002039CB File Offset: 0x00201BCB
		protected override RuntimeCell CreateCell(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode outerGroupingMember, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode innerGroupingMember)
		{
			return new RuntimeTablixCell((RuntimeTablixGroupLeafObjReference)this.m_selfReference, (TablixMember)outerGroupingMember, (TablixMember)innerGroupingMember, !this.m_hasInnerHierarchy);
		}

		// Token: 0x06007CF5 RID: 31989 RVA: 0x002039F2 File Offset: 0x00201BF2
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixGroupLeafObj;
		}

		// Token: 0x06007CF6 RID: 31990 RVA: 0x002039F6 File Offset: 0x00201BF6
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
		}

		// Token: 0x06007CF7 RID: 31991 RVA: 0x002039FF File Offset: 0x00201BFF
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
		}

		// Token: 0x06007CF8 RID: 31992 RVA: 0x00203A08 File Offset: 0x00201C08
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007CF9 RID: 31993 RVA: 0x00203A14 File Offset: 0x00201C14
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeTablixGroupLeafObj.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				RuntimeTablixGroupLeafObj.m_declaration = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixGroupLeafObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixWithScopedItemsGroupLeafObj, list);
			}
			return RuntimeTablixGroupLeafObj.m_declaration;
		}

		// Token: 0x04003DD7 RID: 15831
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeTablixGroupLeafObj.GetDeclaration();
	}
}
