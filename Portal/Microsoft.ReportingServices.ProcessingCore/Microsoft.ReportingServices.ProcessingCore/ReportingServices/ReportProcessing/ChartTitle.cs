using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006FF RID: 1791
	[Serializable]
	internal sealed class ChartTitle
	{
		// Token: 0x17002355 RID: 9045
		// (get) Token: 0x060063DD RID: 25565 RVA: 0x0018CAF6 File Offset: 0x0018ACF6
		// (set) Token: 0x060063DE RID: 25566 RVA: 0x0018CAFE File Offset: 0x0018ACFE
		internal ExpressionInfo Caption
		{
			get
			{
				return this.m_caption;
			}
			set
			{
				this.m_caption = value;
			}
		}

		// Token: 0x17002356 RID: 9046
		// (get) Token: 0x060063DF RID: 25567 RVA: 0x0018CB07 File Offset: 0x0018AD07
		// (set) Token: 0x060063E0 RID: 25568 RVA: 0x0018CB0F File Offset: 0x0018AD0F
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

		// Token: 0x17002357 RID: 9047
		// (get) Token: 0x060063E1 RID: 25569 RVA: 0x0018CB18 File Offset: 0x0018AD18
		// (set) Token: 0x060063E2 RID: 25570 RVA: 0x0018CB20 File Offset: 0x0018AD20
		internal ChartTitle.Positions Position
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

		// Token: 0x17002358 RID: 9048
		// (get) Token: 0x060063E3 RID: 25571 RVA: 0x0018CB29 File Offset: 0x0018AD29
		internal ChartTitleExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x060063E4 RID: 25572 RVA: 0x0018CB34 File Offset: 0x0018AD34
		internal void SetExprHost(ChartTitleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_styleClass != null)
			{
				this.m_styleClass.SetStyleExprHost(this.m_exprHost);
			}
		}

		// Token: 0x060063E5 RID: 25573 RVA: 0x0018CB84 File Offset: 0x0018AD84
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ChartTitleStart();
			if (this.m_caption != null)
			{
				this.m_caption.Initialize("Caption", context);
				context.ExprHostBuilder.ChartCaption(this.m_caption);
			}
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
			context.ExprHostBuilder.ChartTitleEnd();
		}

		// Token: 0x060063E6 RID: 25574 RVA: 0x0018CBE8 File Offset: 0x0018ADE8
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Caption, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.StyleClass, ObjectType.Style),
				new MemberInfo(MemberName.Position, Token.Enum)
			});
		}

		// Token: 0x04003225 RID: 12837
		private ExpressionInfo m_caption;

		// Token: 0x04003226 RID: 12838
		private Style m_styleClass;

		// Token: 0x04003227 RID: 12839
		private ChartTitle.Positions m_position;

		// Token: 0x04003228 RID: 12840
		[NonSerialized]
		private ChartTitleExprHost m_exprHost;

		// Token: 0x02000CCE RID: 3278
		internal enum Positions
		{
			// Token: 0x04004EC4 RID: 20164
			Center,
			// Token: 0x04004EC5 RID: 20165
			Near,
			// Token: 0x04004EC6 RID: 20166
			Far
		}
	}
}
