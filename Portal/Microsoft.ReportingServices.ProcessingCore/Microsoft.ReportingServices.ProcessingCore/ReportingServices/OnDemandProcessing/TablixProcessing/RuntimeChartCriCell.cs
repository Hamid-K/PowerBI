using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008BB RID: 2235
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeChartCriCell : RuntimeCell
	{
		// Token: 0x06007A02 RID: 31234 RVA: 0x001F7114 File Offset: 0x001F5314
		internal RuntimeChartCriCell()
		{
		}

		// Token: 0x06007A03 RID: 31235 RVA: 0x001F711C File Offset: 0x001F531C
		internal RuntimeChartCriCell(RuntimeChartCriGroupLeafObjReference owner, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode outerGroupingMember, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode innerGroupingMember, bool innermost)
			: base(owner, outerGroupingMember, innerGroupingMember, innermost)
		{
		}

		// Token: 0x06007A04 RID: 31236 RVA: 0x001F7129 File Offset: 0x001F5329
		protected override void ConstructCellContents(Cell cell, ref DataActions dataAction)
		{
		}

		// Token: 0x06007A05 RID: 31237 RVA: 0x001F712B File Offset: 0x001F532B
		protected override void CreateInstanceCellContents(Cell cell, DataCellInstance cellInstance, OnDemandProcessingContext odpContext)
		{
		}

		// Token: 0x06007A06 RID: 31238 RVA: 0x001F712D File Offset: 0x001F532D
		public override IOnDemandMemberOwnerInstanceReference GetDataRegionInstance(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion rifDataRegion)
		{
			Global.Tracer.Assert(false, "This type of cell does not support nested data regions.");
			throw new InvalidOperationException();
		}

		// Token: 0x06007A07 RID: 31239 RVA: 0x001F7144 File Offset: 0x001F5344
		public override IReference<IDataCorrelation> GetIdcReceiver(IRIFReportDataScope scope)
		{
			Global.Tracer.Assert(false, "This type of cell does not support nested data regions.");
			throw new InvalidOperationException();
		}

		// Token: 0x06007A08 RID: 31240 RVA: 0x001F715B File Offset: 0x001F535B
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
		}

		// Token: 0x06007A09 RID: 31241 RVA: 0x001F7164 File Offset: 0x001F5364
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
		}

		// Token: 0x06007A0A RID: 31242 RVA: 0x001F716D File Offset: 0x001F536D
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "RuntimeChartCriCell should not resolve references");
		}

		// Token: 0x06007A0B RID: 31243 RVA: 0x001F717F File Offset: 0x001F537F
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriCell;
		}

		// Token: 0x06007A0C RID: 31244 RVA: 0x001F7184 File Offset: 0x001F5384
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeChartCriCell.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeChartCriCell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCell, list);
			}
			return RuntimeChartCriCell.m_declaration;
		}

		// Token: 0x04003D25 RID: 15653
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeChartCriCell.GetDeclaration();
	}
}
