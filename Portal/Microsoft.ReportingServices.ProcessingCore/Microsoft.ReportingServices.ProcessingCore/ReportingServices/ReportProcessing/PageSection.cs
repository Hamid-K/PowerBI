using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006DC RID: 1756
	[Serializable]
	internal sealed class PageSection : ReportItem
	{
		// Token: 0x06005F3E RID: 24382 RVA: 0x00181C78 File Offset: 0x0017FE78
		internal PageSection(bool isHeader, int id, int idForReportItems, Report report)
			: base(id, report)
		{
			this.m_reportItems = new ReportItemCollection(idForReportItems, true);
			this.m_isHeader = isHeader;
		}

		// Token: 0x06005F3F RID: 24383 RVA: 0x00181C97 File Offset: 0x0017FE97
		internal PageSection(bool isHeader, ReportItem parent)
			: base(parent)
		{
			this.m_isHeader = isHeader;
		}

		// Token: 0x1700217A RID: 8570
		// (get) Token: 0x06005F40 RID: 24384 RVA: 0x00181CA7 File Offset: 0x0017FEA7
		internal override ObjectType ObjectType
		{
			get
			{
				if (!this.m_isHeader)
				{
					return ObjectType.PageFooter;
				}
				return ObjectType.PageHeader;
			}
		}

		// Token: 0x1700217B RID: 8571
		// (get) Token: 0x06005F41 RID: 24385 RVA: 0x00181CB4 File Offset: 0x0017FEB4
		// (set) Token: 0x06005F42 RID: 24386 RVA: 0x00181CBC File Offset: 0x0017FEBC
		internal bool PrintOnFirstPage
		{
			get
			{
				return this.m_printOnFirstPage;
			}
			set
			{
				this.m_printOnFirstPage = value;
			}
		}

		// Token: 0x1700217C RID: 8572
		// (get) Token: 0x06005F43 RID: 24387 RVA: 0x00181CC5 File Offset: 0x0017FEC5
		// (set) Token: 0x06005F44 RID: 24388 RVA: 0x00181CCD File Offset: 0x0017FECD
		internal bool PrintOnLastPage
		{
			get
			{
				return this.m_printOnLastPage;
			}
			set
			{
				this.m_printOnLastPage = value;
			}
		}

		// Token: 0x1700217D RID: 8573
		// (get) Token: 0x06005F45 RID: 24389 RVA: 0x00181CD6 File Offset: 0x0017FED6
		// (set) Token: 0x06005F46 RID: 24390 RVA: 0x00181CDE File Offset: 0x0017FEDE
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

		// Token: 0x1700217E RID: 8574
		// (get) Token: 0x06005F47 RID: 24391 RVA: 0x00181CE7 File Offset: 0x0017FEE7
		// (set) Token: 0x06005F48 RID: 24392 RVA: 0x00181CEF File Offset: 0x0017FEEF
		internal bool PostProcessEvaluate
		{
			get
			{
				return this.m_postProcessEvaluate;
			}
			set
			{
				this.m_postProcessEvaluate = value;
			}
		}

		// Token: 0x06005F49 RID: 24393 RVA: 0x00181CF8 File Offset: 0x0017FEF8
		internal override bool Initialize(InitializationContext context)
		{
			context.Location |= LocationFlags.InPageSection;
			context.ObjectType = this.ObjectType;
			context.ObjectName = null;
			context.ExprHostBuilder.PageSectionStart();
			base.Initialize(context);
			this.m_reportItems.Initialize(context, true);
			base.ExprHostID = context.ExprHostBuilder.PageSectionEnd();
			return false;
		}

		// Token: 0x06005F4A RID: 24394 RVA: 0x00181D60 File Offset: 0x0017FF60
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.PageSectionHostsRemotable[base.ExprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
				if (this.m_styleClass != null)
				{
					this.m_styleClass.SetStyleExprHost(this.m_exprHost);
				}
			}
		}

		// Token: 0x06005F4B RID: 24395 RVA: 0x00181DC6 File Offset: 0x0017FFC6
		protected override void DataRendererInitialize(InitializationContext context)
		{
		}

		// Token: 0x06005F4C RID: 24396 RVA: 0x00181DC8 File Offset: 0x0017FFC8
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItem, new MemberInfoList
			{
				new MemberInfo(MemberName.PrintOnFirstPage, Token.Boolean),
				new MemberInfo(MemberName.PrintOnLastPage, Token.Boolean),
				new MemberInfo(MemberName.ReportItems, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemCollection),
				new MemberInfo(MemberName.PostProcessEvaluate, Token.Boolean)
			});
		}

		// Token: 0x04003082 RID: 12418
		private bool m_printOnFirstPage;

		// Token: 0x04003083 RID: 12419
		private bool m_printOnLastPage;

		// Token: 0x04003084 RID: 12420
		private ReportItemCollection m_reportItems;

		// Token: 0x04003085 RID: 12421
		private bool m_postProcessEvaluate;

		// Token: 0x04003086 RID: 12422
		[NonSerialized]
		private bool m_isHeader;

		// Token: 0x04003087 RID: 12423
		[NonSerialized]
		private StyleExprHost m_exprHost;
	}
}
