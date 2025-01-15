using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003C5 RID: 965
	[SkipStaticValidation]
	[NonPersistent]
	internal sealed class DataShapeIntersection : Cell
	{
		// Token: 0x0600271A RID: 10010 RVA: 0x000BA424 File Offset: 0x000B8624
		internal DataShapeIntersection()
		{
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000BA42C File Offset: 0x000B862C
		internal DataShapeIntersection(int id, DataRegion dataRegion)
			: base(id, dataRegion)
		{
		}

		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x000BA436 File Offset: 0x000B8636
		protected override bool IsDataRegionBodyCell
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x000BA439 File Offset: 0x000B8639
		// (set) Token: 0x0600271E RID: 10014 RVA: 0x000BA441 File Offset: 0x000B8641
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x0600271F RID: 10015 RVA: 0x000BA44A File Offset: 0x000B864A
		// (set) Token: 0x06002720 RID: 10016 RVA: 0x000BA452 File Offset: 0x000B8652
		internal List<Calculation> Calculations
		{
			get
			{
				return this.m_calculations;
			}
			set
			{
				this.m_calculations = value;
			}
		}

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x06002721 RID: 10017 RVA: 0x000BA45B File Offset: 0x000B865B
		// (set) Token: 0x06002722 RID: 10018 RVA: 0x000BA463 File Offset: 0x000B8663
		internal List<ReportItem> DataShapes
		{
			get
			{
				return this.m_dataShapes;
			}
			set
			{
				this.m_dataShapes = value;
			}
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x06002723 RID: 10019 RVA: 0x000BA46C File Offset: 0x000B866C
		public override Microsoft.ReportingServices.ReportProcessing.ObjectType DataScopeObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.DataShapeIntersection;
			}
		}

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x000BA470 File Offset: 0x000B8670
		protected override Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode ExprHostDataRegionMode
		{
			get
			{
				return Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.DataShape;
			}
		}

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x06002725 RID: 10021 RVA: 0x000BA473 File Offset: 0x000B8673
		internal override List<ReportItem> CellContentCollection
		{
			get
			{
				return this.m_dataShapes;
			}
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000BA47C File Offset: 0x000B867C
		internal override void InternalInitialize(int parentRowID, int parentColumnID, int rowindex, int colIndex, InitializationContext context)
		{
			this.m_hasInnerGroupTreeHierarchy = this.m_dataShapes != null && this.m_dataShapes.Count > 0;
			if (this.m_dataShapes != null)
			{
				for (int i = 0; i < this.m_dataShapes.Count; i++)
				{
					this.m_dataShapes[i].Initialize(context);
				}
			}
			if (this.m_calculations != null)
			{
				for (int j = 0; j < this.m_calculations.Count; j++)
				{
					this.m_calculations[j].Initialize(context);
				}
			}
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000BA50B File Offset: 0x000B870B
		internal void SetParentColumnIDOwner(IDOwner parentColumnIDOwner)
		{
			this.m_parentColumnIDOwner = parentColumnIDOwner;
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x000BA514 File Offset: 0x000B8714
		internal void DetermineGroupingExprValueCount(InitializationContext context, int groupingExprCount)
		{
			if (this.m_dataShapes != null)
			{
				for (int i = 0; i < this.m_dataShapes.Count; i++)
				{
					this.m_dataShapes[i].DetermineGroupingExprValueCount(context, groupingExprCount);
				}
			}
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x000BA552 File Offset: 0x000B8752
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Global.Tracer.Assert(false, "PublishClone should never be called for data shape processing.");
			throw new InvalidOperationException();
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x000BA569 File Offset: 0x000B8769
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(false, "Deserialize should never be called for data shape processing.");
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x000BA57B File Offset: 0x000B877B
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			Global.Tracer.Assert(false, "GetObjectType should never be called for data shape processing.");
			throw new InvalidOperationException();
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x000BA592 File Offset: 0x000B8792
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "ResolveReferences should never be called for data shape processing.");
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x000BA5A4 File Offset: 0x000B87A4
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			Global.Tracer.Assert(false, "Serialize should never be called for data shape processing.");
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x000BA5B6 File Offset: 0x000B87B6
		internal override void SetupCriRenderItemDef(ReportItem reportItem)
		{
			Global.Tracer.Assert(false, "SetupCriRenderItemDef should never be called for data shape processing.");
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x000BA5C8 File Offset: 0x000B87C8
		protected override void TraverseNestedScopes(IRIFScopeVisitor visitor)
		{
			if (this.m_dataShapes != null)
			{
				for (int i = 0; i < this.m_dataShapes.Count; i++)
				{
					this.m_dataShapes[i].TraverseScopes(visitor);
				}
			}
		}

		// Token: 0x04001673 RID: 5747
		private string m_name;

		// Token: 0x04001674 RID: 5748
		private List<Calculation> m_calculations;

		// Token: 0x04001675 RID: 5749
		private List<ReportItem> m_dataShapes;
	}
}
