using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006E3 RID: 1763
	[Serializable]
	internal sealed class Rectangle : Microsoft.ReportingServices.ReportProcessing.ReportItem, IPageBreakItem, IIndexInto
	{
		// Token: 0x06006004 RID: 24580 RVA: 0x00183ACD File Offset: 0x00181CCD
		internal Rectangle(Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x06006005 RID: 24581 RVA: 0x00183ADD File Offset: 0x00181CDD
		internal Rectangle(int id, int idForReportItems, Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(id, parent)
		{
			this.m_reportItems = new ReportItemCollection(idForReportItems, true);
		}

		// Token: 0x170021C6 RID: 8646
		// (get) Token: 0x06006006 RID: 24582 RVA: 0x00183AFB File Offset: 0x00181CFB
		internal override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Rectangle;
			}
		}

		// Token: 0x170021C7 RID: 8647
		// (get) Token: 0x06006007 RID: 24583 RVA: 0x00183AFE File Offset: 0x00181CFE
		// (set) Token: 0x06006008 RID: 24584 RVA: 0x00183B06 File Offset: 0x00181D06
		internal ReportItemCollection ReportItems
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

		// Token: 0x170021C8 RID: 8648
		// (get) Token: 0x06006009 RID: 24585 RVA: 0x00183B0F File Offset: 0x00181D0F
		// (set) Token: 0x0600600A RID: 24586 RVA: 0x00183B17 File Offset: 0x00181D17
		internal bool PageBreakAtEnd
		{
			get
			{
				return this.m_pageBreakAtEnd;
			}
			set
			{
				this.m_pageBreakAtEnd = value;
			}
		}

		// Token: 0x170021C9 RID: 8649
		// (get) Token: 0x0600600B RID: 24587 RVA: 0x00183B20 File Offset: 0x00181D20
		// (set) Token: 0x0600600C RID: 24588 RVA: 0x00183B28 File Offset: 0x00181D28
		internal bool PageBreakAtStart
		{
			get
			{
				return this.m_pageBreakAtStart;
			}
			set
			{
				this.m_pageBreakAtStart = value;
			}
		}

		// Token: 0x170021CA RID: 8650
		// (get) Token: 0x0600600D RID: 24589 RVA: 0x00183B31 File Offset: 0x00181D31
		// (set) Token: 0x0600600E RID: 24590 RVA: 0x00183B39 File Offset: 0x00181D39
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

		// Token: 0x0600600F RID: 24591 RVA: 0x00183B42 File Offset: 0x00181D42
		internal override void CalculateSizes(double width, double height, InitializationContext context, bool overwrite)
		{
			base.CalculateSizes(width, height, context, overwrite);
			this.m_reportItems.CalculateSizes(context, false);
		}

		// Token: 0x06006010 RID: 24592 RVA: 0x00183B5C File Offset: 0x00181D5C
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			context.ExprHostBuilder.RectangleStart(this.m_name);
			base.Initialize(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, true, false);
			}
			this.m_reportItems.Initialize(context, false);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
			base.ExprHostID = context.ExprHostBuilder.RectangleEnd();
			return false;
		}

		// Token: 0x06006011 RID: 24593 RVA: 0x00183BE8 File Offset: 0x00181DE8
		protected override void DataRendererInitialize(InitializationContext context)
		{
			if (Microsoft.ReportingServices.ReportProcessing.ReportItem.DataElementOutputTypesRDL.Auto == this.m_dataElementOutputRDL)
			{
				this.m_dataElementOutputRDL = Microsoft.ReportingServices.ReportProcessing.ReportItem.DataElementOutputTypesRDL.ContentsOnly;
			}
			base.DataRendererInitialize(context);
		}

		// Token: 0x06006012 RID: 24594 RVA: 0x00183C01 File Offset: 0x00181E01
		internal override void RegisterReceiver(InitializationContext context)
		{
			if (this.m_visibility != null)
			{
				this.m_visibility.RegisterReceiver(context, true);
			}
			this.m_reportItems.RegisterReceiver(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
		}

		// Token: 0x06006013 RID: 24595 RVA: 0x00183C3C File Offset: 0x00181E3C
		internal bool ContainsDataRegionOrSubReport()
		{
			for (int i = 0; i < this.m_reportItems.Count; i++)
			{
				Microsoft.ReportingServices.ReportProcessing.ReportItem reportItem = this.m_reportItems[i];
				if (reportItem is DataRegion)
				{
					return true;
				}
				if (reportItem is SubReport)
				{
					return true;
				}
				if (reportItem is Rectangle && ((Rectangle)reportItem).ContainsDataRegionOrSubReport())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006014 RID: 24596 RVA: 0x00183C98 File Offset: 0x00181E98
		bool IPageBreakItem.IgnorePageBreaks()
		{
			if (this.m_pagebreakState == PageBreakStates.Unknown)
			{
				if (this.m_repeatedSibling || SharedHiddenState.Never != Visibility.GetSharedHidden(this.m_visibility))
				{
					this.m_pagebreakState = PageBreakStates.CanIgnore;
				}
				else
				{
					this.m_pagebreakState = PageBreakStates.CannotIgnore;
				}
			}
			return PageBreakStates.CanIgnore == this.m_pagebreakState;
		}

		// Token: 0x06006015 RID: 24597 RVA: 0x00183CD4 File Offset: 0x00181ED4
		bool IPageBreakItem.HasPageBreaks(bool atStart)
		{
			return (atStart && this.m_pageBreakAtStart) || (!atStart && this.m_pageBreakAtEnd);
		}

		// Token: 0x06006016 RID: 24598 RVA: 0x00183CF0 File Offset: 0x00181EF0
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.RectangleHostsRemotable[base.ExprHostID];
				base.ReportItemSetExprHost(this.m_exprHost, reportObjectModel);
			}
		}

		// Token: 0x06006017 RID: 24599 RVA: 0x00183D40 File Offset: 0x00181F40
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItem, new MemberInfoList
			{
				new MemberInfo(MemberName.ReportItems, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemCollection),
				new MemberInfo(MemberName.PageBreakAtEnd, Token.Boolean),
				new MemberInfo(MemberName.PageBreakAtStart, Token.Boolean),
				new MemberInfo(MemberName.LinkToChild, Token.Int32)
			});
		}

		// Token: 0x06006018 RID: 24600 RVA: 0x00183DA8 File Offset: 0x00181FA8
		internal object SearchChildren(int targetUniqueName, ref NonComputedUniqueNames nonCompNames, ChunkManager.RenderingChunkManager chunkManager)
		{
			if (nonCompNames.ChildrenUniqueNames == null)
			{
				return null;
			}
			NonComputedUniqueNames nonComputedUniqueNames = null;
			int count = this.m_reportItems.Count;
			object obj = null;
			for (int i = 0; i < count; i++)
			{
				nonComputedUniqueNames = nonCompNames.ChildrenUniqueNames[i];
				obj = ((ISearchByUniqueName)this.m_reportItems[i]).Find(targetUniqueName, ref nonComputedUniqueNames, chunkManager);
				if (obj != null)
				{
					break;
				}
			}
			if (obj != null)
			{
				nonCompNames = nonComputedUniqueNames;
				return obj;
			}
			return null;
		}

		// Token: 0x06006019 RID: 24601 RVA: 0x00183E08 File Offset: 0x00182008
		internal override void ProcessDrillthroughAction(ReportProcessing.ProcessingContext processingContext, NonComputedUniqueNames nonCompNames)
		{
			if (nonCompNames == null || nonCompNames.ChildrenUniqueNames == null)
			{
				return;
			}
			int count = this.m_reportItems.Count;
			for (int i = 0; i < count; i++)
			{
				NonComputedUniqueNames nonComputedUniqueNames = nonCompNames.ChildrenUniqueNames[i];
				this.m_reportItems[i].ProcessDrillthroughAction(processingContext, nonComputedUniqueNames);
			}
		}

		// Token: 0x0600601A RID: 24602 RVA: 0x00183E58 File Offset: 0x00182058
		internal int ProcessNavigationChildren(ReportProcessing.NavigationInfo navigationInfo, NonComputedUniqueNames nonCompNames, int startPage)
		{
			if (nonCompNames.ChildrenUniqueNames == null)
			{
				return -1;
			}
			int count = this.m_reportItems.Count;
			int num = -1;
			for (int i = 0; i < count; i++)
			{
				NonComputedUniqueNames nonComputedUniqueNames = nonCompNames.ChildrenUniqueNames[i];
				if (i == this.m_linkToChild)
				{
					num = nonComputedUniqueNames.UniqueName;
				}
				this.m_reportItems[i].ProcessNavigationAction(navigationInfo, nonComputedUniqueNames, startPage);
			}
			return num;
		}

		// Token: 0x0600601B RID: 24603 RVA: 0x00183EB9 File Offset: 0x001820B9
		object IIndexInto.GetChildAt(int index, out NonComputedUniqueNames nonCompNames)
		{
			nonCompNames = null;
			if (index < 0 || index >= this.m_reportItems.Count)
			{
				return null;
			}
			return this.m_reportItems[index];
		}

		// Token: 0x040030CD RID: 12493
		private ReportItemCollection m_reportItems;

		// Token: 0x040030CE RID: 12494
		private bool m_pageBreakAtEnd;

		// Token: 0x040030CF RID: 12495
		private bool m_pageBreakAtStart;

		// Token: 0x040030D0 RID: 12496
		private int m_linkToChild = -1;

		// Token: 0x040030D1 RID: 12497
		[NonSerialized]
		private PageBreakStates m_pagebreakState;

		// Token: 0x040030D2 RID: 12498
		[NonSerialized]
		private ReportItemExprHost m_exprHost;
	}
}
