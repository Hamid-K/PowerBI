using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004A7 RID: 1191
	[Serializable]
	internal sealed class DataMember : ReportHierarchyNode
	{
		// Token: 0x06003A8B RID: 14987 RVA: 0x000FE39A File Offset: 0x000FC59A
		internal DataMember()
		{
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x000FE3A2 File Offset: 0x000FC5A2
		internal DataMember(int id, CustomReportItem crItem)
			: base(id, crItem)
		{
		}

		// Token: 0x17001945 RID: 6469
		// (get) Token: 0x06003A8D RID: 14989 RVA: 0x000FE3AC File Offset: 0x000FC5AC
		internal override string RdlElementName
		{
			get
			{
				return "DataMember";
			}
		}

		// Token: 0x17001946 RID: 6470
		// (get) Token: 0x06003A8E RID: 14990 RVA: 0x000FE3B3 File Offset: 0x000FC5B3
		internal override HierarchyNodeList InnerHierarchy
		{
			get
			{
				return this.m_dataMembers;
			}
		}

		// Token: 0x17001947 RID: 6471
		// (get) Token: 0x06003A8F RID: 14991 RVA: 0x000FE3BB File Offset: 0x000FC5BB
		// (set) Token: 0x06003A90 RID: 14992 RVA: 0x000FE3C3 File Offset: 0x000FC5C3
		internal DataMemberList SubMembers
		{
			get
			{
				return this.m_dataMembers;
			}
			set
			{
				this.m_dataMembers = value;
			}
		}

		// Token: 0x17001948 RID: 6472
		// (get) Token: 0x06003A91 RID: 14993 RVA: 0x000FE3CC File Offset: 0x000FC5CC
		internal DataGroupExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001949 RID: 6473
		// (get) Token: 0x06003A92 RID: 14994 RVA: 0x000FE3D4 File Offset: 0x000FC5D4
		// (set) Token: 0x06003A93 RID: 14995 RVA: 0x000FE3DC File Offset: 0x000FC5DC
		internal DataMember ParentMember
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

		// Token: 0x1700194A RID: 6474
		// (get) Token: 0x06003A94 RID: 14996 RVA: 0x000FE3E5 File Offset: 0x000FC5E5
		// (set) Token: 0x06003A95 RID: 14997 RVA: 0x000FE3ED File Offset: 0x000FC5ED
		internal bool Subtotal
		{
			get
			{
				return this.m_subtotal;
			}
			set
			{
				this.m_subtotal = value;
			}
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x000FE3F6 File Offset: 0x000FC5F6
		protected override void DataGroupStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder builder)
		{
			builder.DataGroupStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.CustomReportItem, this.m_isColumn);
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x000FE405 File Offset: 0x000FC605
		protected override int DataGroupEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder builder)
		{
			return builder.DataGroupEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.CustomReportItem, this.m_isColumn);
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x000FE414 File Offset: 0x000FC614
		internal override object PublishClone(AutomaticSubtotalContext context, DataRegion newContainingRegion, bool isSubtotal)
		{
			if (isSubtotal && this.m_grouping != null)
			{
				context.RegisterScopeName(this.m_grouping.Name);
			}
			DataMember dataMember = (DataMember)base.PublishClone(context, newContainingRegion, isSubtotal);
			if (this.m_dataMembers != null)
			{
				dataMember.m_dataMembers = new DataMemberList(this.m_dataMembers.Count);
				foreach (object obj in this.m_dataMembers)
				{
					DataMember dataMember2 = (DataMember)((DataMember)obj).PublishClone(context, newContainingRegion, isSubtotal);
					dataMember2.ParentMember = this;
					dataMember.m_dataMembers.Add(dataMember2);
				}
			}
			if (this.m_dataMembers == null && isSubtotal)
			{
				RowList rows = context.CurrentDataRegion.Rows;
				if (this.m_isColumn)
				{
					for (int i = 0; i < rows.Count; i++)
					{
						Cell cell = (Cell)rows[i].Cells[context.CurrentIndex].PublishClone(context);
						context.CellLists[i].Add(cell);
					}
				}
				else
				{
					context.Rows.Add((Row)rows[context.CurrentIndex].PublishClone(context));
				}
				int currentIndex = context.CurrentIndex;
				context.CurrentIndex = currentIndex + 1;
			}
			return dataMember;
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x000FE588 File Offset: 0x000FC788
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataMember, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportHierarchyNode, new List<MemberInfo>
			{
				new MemberInfo(MemberName.DataMembers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataMember)
			});
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000FE5C4 File Offset: 0x000FC7C4
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(DataMember.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.DataMembers)
				{
					writer.Write(this.m_dataMembers);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x000FE61C File Offset: 0x000FC81C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(DataMember.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.DataMembers)
				{
					this.m_dataMembers = reader.ReadListOfRIFObjects<DataMemberList>();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000FE674 File Offset: 0x000FC874
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x000FE67E File Offset: 0x000FC87E
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataMember;
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x000FE688 File Offset: 0x000FC888
		internal override void SetExprHost(IMemberNode memberExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(memberExprHost != null && reportObjectModel != null);
				this.m_exprHost = (DataGroupExprHost)memberExprHost;
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
				base.MemberNodeSetExprHost(this.m_exprHost, reportObjectModel);
			}
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x000FE6D7 File Offset: 0x000FC8D7
		internal override void MemberContentsSetExprHost(ObjectModelImpl reportObjectModel, bool traverseDataRegions)
		{
		}

		// Token: 0x04001BEB RID: 7147
		private DataMemberList m_dataMembers;

		// Token: 0x04001BEC RID: 7148
		[NonSerialized]
		private bool m_subtotal;

		// Token: 0x04001BED RID: 7149
		[NonSerialized]
		private DataMember m_parentMember;

		// Token: 0x04001BEE RID: 7150
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DataMember.GetDeclaration();

		// Token: 0x04001BEF RID: 7151
		[NonSerialized]
		private DataGroupExprHost m_exprHost;
	}
}
