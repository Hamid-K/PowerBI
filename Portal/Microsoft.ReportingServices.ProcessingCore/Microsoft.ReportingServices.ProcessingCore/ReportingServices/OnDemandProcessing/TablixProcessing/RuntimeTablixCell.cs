using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008B9 RID: 2233
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeTablixCell : RuntimeCellWithContents
	{
		// Token: 0x060079F0 RID: 31216 RVA: 0x001F6FA6 File Offset: 0x001F51A6
		internal RuntimeTablixCell()
		{
		}

		// Token: 0x060079F1 RID: 31217 RVA: 0x001F6FAE File Offset: 0x001F51AE
		internal RuntimeTablixCell(RuntimeTablixGroupLeafObjReference owner, TablixMember outerGroupingMember, TablixMember innerGroupingMember, bool innermost)
			: base(owner, outerGroupingMember, innerGroupingMember, innermost)
		{
		}

		// Token: 0x060079F2 RID: 31218 RVA: 0x001F6FBC File Offset: 0x001F51BC
		protected override List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> GetCellContents(Cell cell)
		{
			TablixCell tablixCell = cell as TablixCell;
			if (tablixCell != null && tablixCell.HasInnerGroupTreeHierarchy)
			{
				return tablixCell.CellContentCollection;
			}
			return null;
		}

		// Token: 0x060079F3 RID: 31219 RVA: 0x001F6FE3 File Offset: 0x001F51E3
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixCell;
		}

		// Token: 0x060079F4 RID: 31220 RVA: 0x001F6FE7 File Offset: 0x001F51E7
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
		}

		// Token: 0x060079F5 RID: 31221 RVA: 0x001F6FF0 File Offset: 0x001F51F0
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
		}

		// Token: 0x060079F6 RID: 31222 RVA: 0x001F6FF9 File Offset: 0x001F51F9
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, base.GetType().Name + " should not resolve references");
		}

		// Token: 0x060079F7 RID: 31223 RVA: 0x001F701C File Offset: 0x001F521C
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeTablixCell.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				RuntimeTablixCell.m_declaration = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeTablixCell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCellWithContents, list);
			}
			return RuntimeTablixCell.m_declaration;
		}

		// Token: 0x04003D23 RID: 15651
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeTablixCell.GetDeclaration();
	}
}
