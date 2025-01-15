using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200050E RID: 1294
	[Serializable]
	internal sealed class Rectangle : Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem, IPageBreakOwner, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060044C0 RID: 17600 RVA: 0x0011F463 File Offset: 0x0011D663
		internal Rectangle(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x060044C1 RID: 17601 RVA: 0x0011F473 File Offset: 0x0011D673
		internal Rectangle(int id, int idForReportItems, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(id, parent)
		{
			this.m_reportItems = new Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection(idForReportItems, true);
		}

		// Token: 0x17001CE3 RID: 7395
		// (get) Token: 0x060044C2 RID: 17602 RVA: 0x0011F491 File Offset: 0x0011D691
		internal override Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Rectangle;
			}
		}

		// Token: 0x17001CE4 RID: 7396
		// (get) Token: 0x060044C3 RID: 17603 RVA: 0x0011F494 File Offset: 0x0011D694
		// (set) Token: 0x060044C4 RID: 17604 RVA: 0x0011F49C File Offset: 0x0011D69C
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection ReportItems
		{
			get
			{
				return this.m_reportItems;
			}
			set
			{
				this.m_reportItems = value;
			}
		}

		// Token: 0x17001CE5 RID: 7397
		// (get) Token: 0x060044C5 RID: 17605 RVA: 0x0011F4A5 File Offset: 0x0011D6A5
		// (set) Token: 0x060044C6 RID: 17606 RVA: 0x0011F4AD File Offset: 0x0011D6AD
		internal int LinkToChild
		{
			get
			{
				return this.m_linkToChild;
			}
			set
			{
				this.m_linkToChild = value;
			}
		}

		// Token: 0x17001CE6 RID: 7398
		// (get) Token: 0x060044C7 RID: 17607 RVA: 0x0011F4B6 File Offset: 0x0011D6B6
		// (set) Token: 0x060044C8 RID: 17608 RVA: 0x0011F4BE File Offset: 0x0011D6BE
		internal bool KeepTogether
		{
			get
			{
				return this.m_keepTogether;
			}
			set
			{
				this.m_keepTogether = value;
			}
		}

		// Token: 0x17001CE7 RID: 7399
		// (get) Token: 0x060044C9 RID: 17609 RVA: 0x0011F4C7 File Offset: 0x0011D6C7
		// (set) Token: 0x060044CA RID: 17610 RVA: 0x0011F4CF File Offset: 0x0011D6CF
		internal bool OmitBorderOnPageBreak
		{
			get
			{
				return this.m_omitBorderOnPageBreak;
			}
			set
			{
				this.m_omitBorderOnPageBreak = value;
			}
		}

		// Token: 0x17001CE8 RID: 7400
		// (get) Token: 0x060044CB RID: 17611 RVA: 0x0011F4D8 File Offset: 0x0011D6D8
		// (set) Token: 0x060044CC RID: 17612 RVA: 0x0011F4E0 File Offset: 0x0011D6E0
		internal bool IsSimple
		{
			get
			{
				return this.m_isSimple;
			}
			set
			{
				this.m_isSimple = value;
			}
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x0011F4E9 File Offset: 0x0011D6E9
		internal override void CalculateSizes(double width, double height, InitializationContext context, bool overwrite)
		{
			base.CalculateSizes(width, height, context, overwrite);
			this.m_reportItems.CalculateSizes(context, false);
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x0011F504 File Offset: 0x0011D704
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			context.ExprHostBuilder.RectangleStart(this.m_name);
			this.m_isSimple = this.m_toolTip == null && this.m_documentMapLabel == null && this.m_bookmark == null && this.m_styleClass == null && this.m_visibility == null;
			base.Initialize(context);
			context.InitializeAbsolutePosition(this);
			if (this.m_pageBreak != null)
			{
				this.m_pageBreak.Initialize(context);
			}
			if (this.m_pageName != null)
			{
				this.m_pageName.Initialize("PageName", context);
				context.ExprHostBuilder.PageName(this.m_pageName);
			}
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context);
			}
			bool flag = context.RegisterVisibility(this.m_visibility, this);
			context.IsTopLevelCellContents = false;
			this.m_reportItems.Initialize(context);
			if (flag)
			{
				context.UnRegisterVisibility(this.m_visibility, this);
			}
			base.ExprHostID = context.ExprHostBuilder.RectangleEnd();
			return false;
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x0011F618 File Offset: 0x0011D818
		internal override void TraverseScopes(IRIFScopeVisitor visitor)
		{
			if (this.m_reportItems != null)
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem in this.m_reportItems)
				{
					reportItem.TraverseScopes(visitor);
				}
			}
		}

		// Token: 0x17001CE9 RID: 7401
		// (get) Token: 0x060044D0 RID: 17616 RVA: 0x0011F66C File Offset: 0x0011D86C
		internal override DataElementOutputTypes DataElementOutputDefault
		{
			get
			{
				return DataElementOutputTypes.ContentsOnly;
			}
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x0011F66F File Offset: 0x0011D86F
		internal override void InitializeRVDirectionDependentItems(InitializationContext context)
		{
			if (this.m_reportItems != null)
			{
				this.m_reportItems.InitializeRVDirectionDependentItems(context);
			}
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x0011F685 File Offset: 0x0011D885
		internal override void DetermineGroupingExprValueCount(InitializationContext context, int groupingExprCount)
		{
			if (this.m_reportItems != null)
			{
				this.m_reportItems.DetermineGroupingExprValueCount(context, groupingExprCount);
			}
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x0011F69C File Offset: 0x0011D89C
		internal bool ContainsDataRegionOrSubReport()
		{
			for (int i = 0; i < this.m_reportItems.Count; i++)
			{
				if (this.m_reportItems[i].IsOrContainsDataRegionOrSubReport())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17001CEA RID: 7402
		// (get) Token: 0x060044D4 RID: 17620 RVA: 0x0011F6D5 File Offset: 0x0011D8D5
		// (set) Token: 0x060044D5 RID: 17621 RVA: 0x0011F6DD File Offset: 0x0011D8DD
		internal ExpressionInfo PageName
		{
			get
			{
				return this.m_pageName;
			}
			set
			{
				this.m_pageName = value;
			}
		}

		// Token: 0x17001CEB RID: 7403
		// (get) Token: 0x060044D6 RID: 17622 RVA: 0x0011F6E6 File Offset: 0x0011D8E6
		// (set) Token: 0x060044D7 RID: 17623 RVA: 0x0011F6EE File Offset: 0x0011D8EE
		internal PageBreak PageBreak
		{
			get
			{
				return this.m_pageBreak;
			}
			set
			{
				this.m_pageBreak = value;
			}
		}

		// Token: 0x17001CEC RID: 7404
		// (get) Token: 0x060044D8 RID: 17624 RVA: 0x0011F6F7 File Offset: 0x0011D8F7
		// (set) Token: 0x060044D9 RID: 17625 RVA: 0x0011F6FF File Offset: 0x0011D8FF
		PageBreak IPageBreakOwner.PageBreak
		{
			get
			{
				return this.m_pageBreak;
			}
			set
			{
				this.m_pageBreak = value;
			}
		}

		// Token: 0x17001CED RID: 7405
		// (get) Token: 0x060044DA RID: 17626 RVA: 0x0011F708 File Offset: 0x0011D908
		Microsoft.ReportingServices.ReportProcessing.ObjectType IPageBreakOwner.ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Rectangle;
			}
		}

		// Token: 0x17001CEE RID: 7406
		// (get) Token: 0x060044DB RID: 17627 RVA: 0x0011F70B File Offset: 0x0011D90B
		string IPageBreakOwner.ObjectName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17001CEF RID: 7407
		// (get) Token: 0x060044DC RID: 17628 RVA: 0x0011F713 File Offset: 0x0011D913
		IInstancePath IPageBreakOwner.InstancePath
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060044DD RID: 17629 RVA: 0x0011F718 File Offset: 0x0011D918
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Rectangle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ReportItems, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItemCollection),
				new ReadOnlyMemberInfo(MemberName.PageBreakLocation, Token.Enum),
				new MemberInfo(MemberName.LinkToChild, Token.Int32),
				new MemberInfo(MemberName.OmitBorderOnPageBreak, Token.Boolean),
				new MemberInfo(MemberName.KeepTogether, Token.Boolean),
				new MemberInfo(MemberName.IsSimple, Token.Boolean),
				new MemberInfo(MemberName.PageBreak, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PageBreak),
				new MemberInfo(MemberName.PageName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060044DE RID: 17630 RVA: 0x0011F7E0 File Offset: 0x0011D9E0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.LinkToChild)
				{
					if (memberName == MemberName.ReportItems)
					{
						writer.Write(this.m_reportItems);
						continue;
					}
					if (memberName == MemberName.KeepTogether)
					{
						writer.Write(this.m_keepTogether);
						continue;
					}
					if (memberName == MemberName.LinkToChild)
					{
						writer.Write(this.m_linkToChild);
						continue;
					}
				}
				else if (memberName <= MemberName.IsSimple)
				{
					if (memberName == MemberName.OmitBorderOnPageBreak)
					{
						writer.Write(this.m_omitBorderOnPageBreak);
						continue;
					}
					if (memberName == MemberName.IsSimple)
					{
						writer.Write(this.m_isSimple);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.PageBreak)
					{
						writer.Write(this.m_pageBreak);
						continue;
					}
					if (memberName == MemberName.PageName)
					{
						writer.Write(this.m_pageName);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060044DF RID: 17631 RVA: 0x0011F8E4 File Offset: 0x0011DAE4
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.PageBreakLocation)
				{
					if (memberName <= MemberName.KeepTogether)
					{
						if (memberName == MemberName.ReportItems)
						{
							this.m_reportItems = (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.KeepTogether)
						{
							this.m_keepTogether = reader.ReadBoolean();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.LinkToChild)
						{
							this.m_linkToChild = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.PageBreakLocation)
						{
							this.m_pageBreak = new PageBreak();
							this.m_pageBreak.BreakLocation = (PageBreakLocation)reader.ReadEnum();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.IsSimple)
				{
					if (memberName == MemberName.OmitBorderOnPageBreak)
					{
						this.m_omitBorderOnPageBreak = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.IsSimple)
					{
						this.m_isSimple = reader.ReadBoolean();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.PageBreak)
					{
						this.m_pageBreak = (PageBreak)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.PageName)
					{
						this.m_pageName = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060044E0 RID: 17632 RVA: 0x0011FA3F File Offset: 0x0011DC3F
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x060044E1 RID: 17633 RVA: 0x0011FA49 File Offset: 0x0011DC49
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Rectangle;
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x0011FA50 File Offset: 0x0011DC50
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle rectangle = (Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)base.PublishClone(context);
			if (this.m_reportItems != null)
			{
				rectangle.m_reportItems = (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection)this.m_reportItems.PublishClone(context);
			}
			if (this.m_pageBreak != null)
			{
				rectangle.m_pageBreak = (PageBreak)this.m_pageBreak.PublishClone(context);
			}
			if (this.m_pageName != null)
			{
				rectangle.m_pageName = (ExpressionInfo)this.m_pageName.PublishClone(context);
			}
			return rectangle;
		}

		// Token: 0x060044E3 RID: 17635 RVA: 0x0011FAC8 File Offset: 0x0011DCC8
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
				this.m_exprHost = reportExprHost.RectangleHostsRemotable[base.ExprHostID];
				base.ReportItemSetExprHost(this.m_exprHost, reportObjectModel);
				if (this.m_pageBreak != null && this.m_exprHost.PageBreakExprHost != null)
				{
					this.m_pageBreak.SetExprHost(this.m_exprHost.PageBreakExprHost, reportObjectModel);
				}
			}
		}

		// Token: 0x060044E4 RID: 17636 RVA: 0x0011FB47 File Offset: 0x0011DD47
		internal string EvaluatePageName(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, romInstance);
			return context.ReportRuntime.EvaluateRectanglePageNameExpression(this, this.m_pageName, this.m_name);
		}

		// Token: 0x04001F1B RID: 7963
		private Microsoft.ReportingServices.ReportIntermediateFormat.ReportItemCollection m_reportItems;

		// Token: 0x04001F1C RID: 7964
		private PageBreak m_pageBreak;

		// Token: 0x04001F1D RID: 7965
		private ExpressionInfo m_pageName;

		// Token: 0x04001F1E RID: 7966
		private int m_linkToChild = -1;

		// Token: 0x04001F1F RID: 7967
		private bool m_keepTogether;

		// Token: 0x04001F20 RID: 7968
		private bool m_omitBorderOnPageBreak;

		// Token: 0x04001F21 RID: 7969
		private bool m_isSimple;

		// Token: 0x04001F22 RID: 7970
		[NonSerialized]
		private ReportItemExprHost m_exprHost;

		// Token: 0x04001F23 RID: 7971
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle.GetDeclaration();
	}
}
