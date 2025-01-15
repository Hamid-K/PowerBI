using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000705 RID: 1797
	[Serializable]
	internal sealed class PlotArea
	{
		// Token: 0x17002380 RID: 9088
		// (get) Token: 0x06006449 RID: 25673 RVA: 0x0018D658 File Offset: 0x0018B858
		// (set) Token: 0x0600644A RID: 25674 RVA: 0x0018D660 File Offset: 0x0018B860
		internal PlotArea.Origins Origin
		{
			get
			{
				return this.m_origin;
			}
			set
			{
				this.m_origin = value;
			}
		}

		// Token: 0x17002381 RID: 9089
		// (get) Token: 0x0600644B RID: 25675 RVA: 0x0018D669 File Offset: 0x0018B869
		// (set) Token: 0x0600644C RID: 25676 RVA: 0x0018D671 File Offset: 0x0018B871
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

		// Token: 0x0600644D RID: 25677 RVA: 0x0018D67A File Offset: 0x0018B87A
		internal void SetExprHost(StyleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_styleClass != null)
			{
				this.m_styleClass.SetStyleExprHost(exprHost);
			}
		}

		// Token: 0x0600644E RID: 25678 RVA: 0x0018D6AB File Offset: 0x0018B8AB
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartPlotAreaStart();
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
			context.ExprHostBuilder.ChartPlotAreaEnd();
		}

		// Token: 0x0600644F RID: 25679 RVA: 0x0018D6DC File Offset: 0x0018B8DC
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Origin, Token.Enum),
				new MemberInfo(MemberName.StyleClass, ObjectType.Style)
			});
		}

		// Token: 0x0400324F RID: 12879
		private PlotArea.Origins m_origin;

		// Token: 0x04003250 RID: 12880
		private Style m_styleClass;

		// Token: 0x02000CD5 RID: 3285
		internal enum Origins
		{
			// Token: 0x04004EF4 RID: 20212
			BottomLeft,
			// Token: 0x04004EF5 RID: 20213
			TopLeft,
			// Token: 0x04004EF6 RID: 20214
			TopRight,
			// Token: 0x04004EF7 RID: 20215
			BottomRight
		}
	}
}
