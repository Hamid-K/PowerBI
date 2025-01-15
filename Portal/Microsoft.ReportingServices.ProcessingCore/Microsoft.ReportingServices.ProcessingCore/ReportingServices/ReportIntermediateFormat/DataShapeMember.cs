using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003CC RID: 972
	[SkipStaticValidation]
	[NonPersistent]
	internal sealed class DataShapeMember : ReportHierarchyNode
	{
		// Token: 0x06002743 RID: 10051 RVA: 0x000BA6DD File Offset: 0x000B88DD
		internal DataShapeMember()
		{
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000BA6E5 File Offset: 0x000B88E5
		internal DataShapeMember(int id, DataShape dataShape)
			: base(id, dataShape)
		{
		}

		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x000BA6EF File Offset: 0x000B88EF
		// (set) Token: 0x06002746 RID: 10054 RVA: 0x000BA6F7 File Offset: 0x000B88F7
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

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x000BA700 File Offset: 0x000B8900
		internal override string RdlElementName
		{
			get
			{
				return "DataShapeMember";
			}
		}

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x06002748 RID: 10056 RVA: 0x000BA707 File Offset: 0x000B8907
		internal override HierarchyNodeList InnerHierarchy
		{
			get
			{
				return this.m_subMembers;
			}
		}

		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x000BA70F File Offset: 0x000B890F
		// (set) Token: 0x0600274A RID: 10058 RVA: 0x000BA717 File Offset: 0x000B8917
		internal DataShapeMemberList SubMembers
		{
			get
			{
				return this.m_subMembers;
			}
			set
			{
				this.m_subMembers = value;
			}
		}

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x000BA720 File Offset: 0x000B8920
		internal DataShapeMemberList SameLevelMembers
		{
			get
			{
				if (this.m_parentMember != null)
				{
					return this.m_parentMember.SubMembers;
				}
				DataShape dataShape = (DataShape)this.m_dataRegionDef;
				if (this.m_isColumn)
				{
					return dataShape.SecondaryHierarchy;
				}
				return dataShape.PrimaryHierarchy;
			}
		}

		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x000BA762 File Offset: 0x000B8962
		// (set) Token: 0x0600274D RID: 10061 RVA: 0x000BA76A File Offset: 0x000B896A
		internal DataShapeMember ParentMember
		{
			get
			{
				return this.m_parentMember;
			}
			set
			{
				this.m_parentMember = value;
			}
		}

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x0600274E RID: 10062 RVA: 0x000BA773 File Offset: 0x000B8973
		// (set) Token: 0x0600274F RID: 10063 RVA: 0x000BA77B File Offset: 0x000B897B
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

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x06002750 RID: 10064 RVA: 0x000BA784 File Offset: 0x000B8984
		// (set) Token: 0x06002751 RID: 10065 RVA: 0x000BA78C File Offset: 0x000B898C
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

		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x06002752 RID: 10066 RVA: 0x000BA795 File Offset: 0x000B8995
		internal override bool IsTablixMember
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x000BA798 File Offset: 0x000B8998
		internal override List<ReportItem> MemberContentCollection
		{
			get
			{
				return this.m_dataShapes;
			}
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000BA7A0 File Offset: 0x000B89A0
		internal override void TraverseMemberScopes(IRIFScopeVisitor visitor)
		{
			if (this.m_dataShapes != null)
			{
				for (int i = 0; i < this.m_dataShapes.Count; i++)
				{
					this.m_dataShapes[i].TraverseScopes(visitor);
				}
			}
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x000BA7DD File Offset: 0x000B89DD
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(false, "Deserialize should never be called for data shape processing.");
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000BA7F0 File Offset: 0x000B89F0
		internal override bool InnerInitialize(InitializationContext context, bool restrictive)
		{
			bool flag = base.InnerInitialize(context, restrictive);
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
			return flag;
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000BA868 File Offset: 0x000B8A68
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Global.Tracer.Assert(false, "PublishClone should never be called for data shape processing.");
			throw new InvalidOperationException();
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000BA87F File Offset: 0x000B8A7F
		internal override object PublishClone(AutomaticSubtotalContext context, DataRegion newContainingRegion)
		{
			Global.Tracer.Assert(false, "PublishClone should never be called for data shape processing.");
			throw new InvalidOperationException();
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000BA896 File Offset: 0x000B8A96
		internal override object PublishClone(AutomaticSubtotalContext context, DataRegion newContainingRegion, bool isSubtotal)
		{
			Global.Tracer.Assert(false, "PublishClone should never be called for data shape processing.");
			throw new InvalidOperationException();
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000BA8AD File Offset: 0x000B8AAD
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "ResolveReferences should never be called for data shape processing.");
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000BA8BF File Offset: 0x000B8ABF
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			Global.Tracer.Assert(false, "Serialize should never be called for data shape processing.");
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000BA8D1 File Offset: 0x000B8AD1
		internal override void SetupCriRenderItemDef(ReportItem reportItem)
		{
			Global.Tracer.Assert(false, "SetupCriRenderItemDef should never be called for data shape processing.");
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x000BA8E3 File Offset: 0x000B8AE3
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			Global.Tracer.Assert(false, "GetObjectType should never be called for data shape processing.");
			throw new InvalidOperationException();
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x000BA8FA File Offset: 0x000B8AFA
		protected override void DataGroupStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder builder)
		{
			builder.DataGroupStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.DataShape, this.m_isColumn);
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000BA909 File Offset: 0x000B8B09
		protected override int DataGroupEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder builder)
		{
			return builder.DataGroupEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.DataShape, this.m_isColumn);
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000BA918 File Offset: 0x000B8B18
		internal override void DetermineGroupingExprValueCount(InitializationContext context, int groupingExprCount)
		{
			if (this.m_dataShapes != null)
			{
				for (int i = 0; i < this.m_dataShapes.Count; i++)
				{
					this.m_dataShapes[i].DetermineGroupingExprValueCount(context, groupingExprCount);
				}
			}
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000BA956 File Offset: 0x000B8B56
		internal override void SetExprHost(IMemberNode memberExprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(false, "SetExprHost should never be called for data member processing.");
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000BA968 File Offset: 0x000B8B68
		internal override void MemberContentsSetExprHost(ObjectModelImpl reportObjectModel, bool traverseDataRegions)
		{
			Global.Tracer.Assert(false, "MemberContentsSetExprHost should never be called for data member processing.");
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x000BA97A File Offset: 0x000B8B7A
		internal override void MoveNextForUserSort(OnDemandProcessingContext odpContext)
		{
			Global.Tracer.Assert(false, "MoveNextForUserSort should never be called for data shape processing.");
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x000BA98C File Offset: 0x000B8B8C
		protected override void AddInScopeTextBox(TextBox textbox)
		{
			Global.Tracer.Assert(false, "AddInScopeTextBox should never be called for data shape processing.");
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000BA99E File Offset: 0x000B8B9E
		internal override void ResetTextBoxImpls(OnDemandProcessingContext context)
		{
			Global.Tracer.Assert(false, "ResetTextBoxImpls should never be called for data shape processing.");
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x000BA9B0 File Offset: 0x000B8BB0
		internal override void SetRecursiveParentIndex(int parentInstanceIndex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x000BA9B7 File Offset: 0x000B8BB7
		internal override void SetInstanceHasRecursiveChildren(bool? hasRecursiveChildren)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400167D RID: 5757
		private string m_name;

		// Token: 0x0400167E RID: 5758
		private DataShapeMemberList m_subMembers;

		// Token: 0x0400167F RID: 5759
		private DataShapeMember m_parentMember;

		// Token: 0x04001680 RID: 5760
		private List<Calculation> m_calculations;

		// Token: 0x04001681 RID: 5761
		private List<ReportItem> m_dataShapes;
	}
}
