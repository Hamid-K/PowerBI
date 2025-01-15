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
	// Token: 0x020003E6 RID: 998
	[Serializable]
	internal sealed class GaugeMember : ReportHierarchyNode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600291F RID: 10527 RVA: 0x000C0B10 File Offset: 0x000BED10
		internal GaugeMember()
		{
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x000C0B18 File Offset: 0x000BED18
		internal GaugeMember(int id, GaugePanel crItem)
			: base(id, crItem)
		{
		}

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x06002921 RID: 10529 RVA: 0x000C0B22 File Offset: 0x000BED22
		internal override string RdlElementName
		{
			get
			{
				return "GaugeMember";
			}
		}

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x06002922 RID: 10530 RVA: 0x000C0B29 File Offset: 0x000BED29
		internal override HierarchyNodeList InnerHierarchy
		{
			get
			{
				return this.m_innerMembers;
			}
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x06002923 RID: 10531 RVA: 0x000C0B31 File Offset: 0x000BED31
		// (set) Token: 0x06002924 RID: 10532 RVA: 0x000C0B57 File Offset: 0x000BED57
		internal GaugeMember ChildGaugeMember
		{
			get
			{
				if (this.m_innerMembers != null && this.m_innerMembers.Count > 0)
				{
					return this.m_innerMembers[0];
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					return;
				}
				if (this.m_innerMembers == null)
				{
					this.m_innerMembers = new GaugeMemberList();
				}
				else
				{
					this.m_innerMembers.Clear();
				}
				this.m_innerMembers.Add(value);
			}
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x000C0B8A File Offset: 0x000BED8A
		internal void SetIsCategoryMember(bool value)
		{
			this.m_isColumn = value;
			if (this.ChildGaugeMember != null)
			{
				this.ChildGaugeMember.SetIsCategoryMember(value);
			}
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x000C0BA7 File Offset: 0x000BEDA7
		protected override void DataGroupStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder builder)
		{
			builder.DataGroupStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.GaugePanel, this.m_isColumn);
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x000C0BB6 File Offset: 0x000BEDB6
		protected override int DataGroupEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder builder)
		{
			return builder.DataGroupEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.GaugePanel, this.m_isColumn);
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x000C0BC8 File Offset: 0x000BEDC8
		internal override bool Initialize(InitializationContext context)
		{
			if (!this.m_isColumn)
			{
				if (this.m_grouping != null)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidRowGaugeMemberCannotBeDynamic, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, context.TablixName, "GaugeMember", new string[]
					{
						"Group",
						this.m_grouping.Name
					});
				}
				if (this.m_innerMembers != null)
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidRowGaugeMemberCannotContainChildMember, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, context.TablixName, "GaugeMember", Array.Empty<string>());
				}
			}
			else if (this.m_innerMembers != null && this.m_innerMembers.OriginalNodeCount > 1)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsInvalidColumnGaugeMemberCannotContainMultipleChildMember, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, context.TablixName, "GaugeMember", Array.Empty<string>());
			}
			return base.Initialize(context);
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x000C0C8C File Offset: 0x000BEE8C
		internal override object PublishClone(AutomaticSubtotalContext context, DataRegion newContainingRegion)
		{
			GaugeMember gaugeMember = (GaugeMember)base.PublishClone(context, newContainingRegion);
			if (this.ChildGaugeMember != null)
			{
				gaugeMember.ChildGaugeMember = (GaugeMember)this.ChildGaugeMember.PublishClone(context, newContainingRegion);
			}
			return gaugeMember;
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000C0CC8 File Offset: 0x000BEEC8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeMember, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportHierarchyNode, new List<MemberInfo>
			{
				new ReadOnlyMemberInfo(MemberName.GaugeMember, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeMember),
				new MemberInfo(MemberName.ColumnMembers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeMember)
			});
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x000C0D14 File Offset: 0x000BEF14
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(GaugeMember.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.ColumnMembers)
				{
					writer.Write(this.m_innerMembers);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x000C0D6C File Offset: 0x000BEF6C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(GaugeMember.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.ColumnMembers)
				{
					if (memberName == MemberName.GaugeMember)
					{
						this.ChildGaugeMember = (GaugeMember)reader.ReadRIFObject();
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					this.m_innerMembers = reader.ReadListOfRIFObjects<GaugeMemberList>();
				}
			}
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x000C0DDF File Offset: 0x000BEFDF
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x000C0DE9 File Offset: 0x000BEFE9
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeMember;
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x000C0DF0 File Offset: 0x000BEFF0
		internal override void SetExprHost(IMemberNode memberExprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(memberExprHost != null && reportObjectModel != null);
			base.MemberNodeSetExprHost(memberExprHost, reportObjectModel);
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x000C0E0E File Offset: 0x000BF00E
		internal override void MemberContentsSetExprHost(ObjectModelImpl reportObjectModel, bool traverseDataRegions)
		{
		}

		// Token: 0x040016F2 RID: 5874
		private GaugeMemberList m_innerMembers;

		// Token: 0x040016F3 RID: 5875
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = GaugeMember.GetDeclaration();
	}
}
