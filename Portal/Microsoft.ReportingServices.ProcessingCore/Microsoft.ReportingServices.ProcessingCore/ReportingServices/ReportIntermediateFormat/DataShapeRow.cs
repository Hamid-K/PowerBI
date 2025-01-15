using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003CE RID: 974
	[SkipStaticValidation]
	[NonPersistent]
	internal sealed class DataShapeRow : Row
	{
		// Token: 0x0600276C RID: 10092 RVA: 0x000BAA25 File Offset: 0x000B8C25
		internal DataShapeRow()
		{
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000BAA2D File Offset: 0x000B8C2D
		internal DataShapeRow(int id)
			: base(id)
		{
		}

		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x0600276E RID: 10094 RVA: 0x000BAA36 File Offset: 0x000B8C36
		internal override CellList Cells
		{
			get
			{
				return this.m_intersections;
			}
		}

		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x000BAA3E File Offset: 0x000B8C3E
		// (set) Token: 0x06002770 RID: 10096 RVA: 0x000BAA46 File Offset: 0x000B8C46
		internal DataShapeIntersectionList DataShapeIntersections
		{
			get
			{
				return this.m_intersections;
			}
			set
			{
				this.m_intersections = value;
			}
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x000BAA4F File Offset: 0x000B8C4F
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(false, "Deserialize should never be called for data shape processing.");
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x000BAA61 File Offset: 0x000B8C61
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			Global.Tracer.Assert(false, "GetObjectType should never be called for data shape processing.");
			throw new InvalidOperationException();
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000BAA78 File Offset: 0x000B8C78
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "ResolveReferences should never be called for data shape processing.");
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000BAA8A File Offset: 0x000B8C8A
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			Global.Tracer.Assert(false, "Serialize should never be called for data shape processing.");
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000BAA9C File Offset: 0x000B8C9C
		internal override void SetupCriRenderItemDef(ReportItem reportItem)
		{
			Global.Tracer.Assert(false, "SetupCriRenderItemDef should never be called for data shape processing.");
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x000BAAAE File Offset: 0x000B8CAE
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Global.Tracer.Assert(false, "PublishClone should never be called for data shape processing.");
			throw new InvalidOperationException();
		}

		// Token: 0x04001683 RID: 5763
		private DataShapeIntersectionList m_intersections;
	}
}
