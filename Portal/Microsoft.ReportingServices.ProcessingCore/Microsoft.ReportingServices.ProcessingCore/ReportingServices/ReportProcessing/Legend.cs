using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000700 RID: 1792
	[Serializable]
	internal sealed class Legend
	{
		// Token: 0x17002359 RID: 9049
		// (get) Token: 0x060063E8 RID: 25576 RVA: 0x0018CC3B File Offset: 0x0018AE3B
		// (set) Token: 0x060063E9 RID: 25577 RVA: 0x0018CC43 File Offset: 0x0018AE43
		internal bool Visible
		{
			get
			{
				return this.m_visible;
			}
			set
			{
				this.m_visible = value;
			}
		}

		// Token: 0x1700235A RID: 9050
		// (get) Token: 0x060063EA RID: 25578 RVA: 0x0018CC4C File Offset: 0x0018AE4C
		// (set) Token: 0x060063EB RID: 25579 RVA: 0x0018CC54 File Offset: 0x0018AE54
		internal Style StyleClass
		{
			get
			{
				return this.m_styleClass;
			}
			set
			{
				this.m_styleClass = value;
			}
		}

		// Token: 0x1700235B RID: 9051
		// (get) Token: 0x060063EC RID: 25580 RVA: 0x0018CC5D File Offset: 0x0018AE5D
		// (set) Token: 0x060063ED RID: 25581 RVA: 0x0018CC65 File Offset: 0x0018AE65
		internal Legend.Positions Position
		{
			get
			{
				return this.m_position;
			}
			set
			{
				this.m_position = value;
			}
		}

		// Token: 0x1700235C RID: 9052
		// (get) Token: 0x060063EE RID: 25582 RVA: 0x0018CC6E File Offset: 0x0018AE6E
		// (set) Token: 0x060063EF RID: 25583 RVA: 0x0018CC76 File Offset: 0x0018AE76
		internal Legend.LegendLayout Layout
		{
			get
			{
				return this.m_layout;
			}
			set
			{
				this.m_layout = value;
			}
		}

		// Token: 0x1700235D RID: 9053
		// (get) Token: 0x060063F0 RID: 25584 RVA: 0x0018CC7F File Offset: 0x0018AE7F
		// (set) Token: 0x060063F1 RID: 25585 RVA: 0x0018CC87 File Offset: 0x0018AE87
		internal bool InsidePlotArea
		{
			get
			{
				return this.m_insidePlotArea;
			}
			set
			{
				this.m_insidePlotArea = value;
			}
		}

		// Token: 0x060063F2 RID: 25586 RVA: 0x0018CC90 File Offset: 0x0018AE90
		internal void SetExprHost(StyleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_styleClass != null)
			{
				this.m_styleClass.SetStyleExprHost(exprHost);
			}
		}

		// Token: 0x060063F3 RID: 25587 RVA: 0x0018CCC1 File Offset: 0x0018AEC1
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartLegendStart();
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
			context.ExprHostBuilder.ChartLegendEnd();
		}

		// Token: 0x060063F4 RID: 25588 RVA: 0x0018CCF0 File Offset: 0x0018AEF0
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Visible, Token.Boolean),
				new MemberInfo(MemberName.StyleClass, ObjectType.Style),
				new MemberInfo(MemberName.Position, Token.Enum),
				new MemberInfo(MemberName.Layout, Token.Enum),
				new MemberInfo(MemberName.InsidePlotArea, Token.Boolean)
			});
		}

		// Token: 0x04003229 RID: 12841
		private bool m_visible;

		// Token: 0x0400322A RID: 12842
		private Style m_styleClass;

		// Token: 0x0400322B RID: 12843
		private Legend.Positions m_position;

		// Token: 0x0400322C RID: 12844
		private Legend.LegendLayout m_layout;

		// Token: 0x0400322D RID: 12845
		private bool m_insidePlotArea;

		// Token: 0x02000CCF RID: 3279
		internal enum LegendLayout
		{
			// Token: 0x04004EC8 RID: 20168
			Column,
			// Token: 0x04004EC9 RID: 20169
			Row,
			// Token: 0x04004ECA RID: 20170
			Table
		}

		// Token: 0x02000CD0 RID: 3280
		internal enum Positions
		{
			// Token: 0x04004ECC RID: 20172
			RightTop,
			// Token: 0x04004ECD RID: 20173
			TopLeft,
			// Token: 0x04004ECE RID: 20174
			TopCenter,
			// Token: 0x04004ECF RID: 20175
			TopRight,
			// Token: 0x04004ED0 RID: 20176
			LeftTop,
			// Token: 0x04004ED1 RID: 20177
			LeftCenter,
			// Token: 0x04004ED2 RID: 20178
			LeftBottom,
			// Token: 0x04004ED3 RID: 20179
			RightCenter,
			// Token: 0x04004ED4 RID: 20180
			RightBottom,
			// Token: 0x04004ED5 RID: 20181
			BottomLeft,
			// Token: 0x04004ED6 RID: 20182
			BottomCenter,
			// Token: 0x04004ED7 RID: 20183
			BottomRight
		}
	}
}
