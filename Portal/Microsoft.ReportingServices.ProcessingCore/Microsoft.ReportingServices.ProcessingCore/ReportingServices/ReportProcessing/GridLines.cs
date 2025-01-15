using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000707 RID: 1799
	[Serializable]
	internal sealed class GridLines
	{
		// Token: 0x17002398 RID: 9112
		// (get) Token: 0x06006480 RID: 25728 RVA: 0x0018DD47 File Offset: 0x0018BF47
		// (set) Token: 0x06006481 RID: 25729 RVA: 0x0018DD4F File Offset: 0x0018BF4F
		internal bool ShowGridLines
		{
			get
			{
				return this.m_showGridLines;
			}
			set
			{
				this.m_showGridLines = value;
			}
		}

		// Token: 0x17002399 RID: 9113
		// (get) Token: 0x06006482 RID: 25730 RVA: 0x0018DD58 File Offset: 0x0018BF58
		// (set) Token: 0x06006483 RID: 25731 RVA: 0x0018DD60 File Offset: 0x0018BF60
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

		// Token: 0x06006484 RID: 25732 RVA: 0x0018DD69 File Offset: 0x0018BF69
		internal void Initialize(InitializationContext context)
		{
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
		}

		// Token: 0x06006485 RID: 25733 RVA: 0x0018DD7F File Offset: 0x0018BF7F
		internal void SetExprHost(StyleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_styleClass != null)
			{
				this.m_styleClass.SetStyleExprHost(exprHost);
			}
		}

		// Token: 0x06006486 RID: 25734 RVA: 0x0018DDB0 File Offset: 0x0018BFB0
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.ShowGridLines, Token.Boolean),
				new MemberInfo(MemberName.StyleClass, ObjectType.Style)
			});
		}

		// Token: 0x04003267 RID: 12903
		private bool m_showGridLines;

		// Token: 0x04003268 RID: 12904
		private Style m_styleClass;
	}
}
