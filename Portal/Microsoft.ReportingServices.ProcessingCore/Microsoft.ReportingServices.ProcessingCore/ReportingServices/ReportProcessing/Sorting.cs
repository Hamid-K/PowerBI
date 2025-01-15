using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006F0 RID: 1776
	[Serializable]
	internal sealed class Sorting
	{
		// Token: 0x06006208 RID: 25096 RVA: 0x001877D8 File Offset: 0x001859D8
		internal Sorting(ConstructionPhase phase)
		{
			if (phase == ConstructionPhase.Publishing)
			{
				this.m_sortExpressions = new ExpressionInfoList();
				this.m_sortDirections = new BoolList();
			}
		}

		// Token: 0x1700229B RID: 8859
		// (get) Token: 0x06006209 RID: 25097 RVA: 0x001877F9 File Offset: 0x001859F9
		// (set) Token: 0x0600620A RID: 25098 RVA: 0x00187801 File Offset: 0x00185A01
		internal ExpressionInfoList SortExpressions
		{
			get
			{
				return this.m_sortExpressions;
			}
			set
			{
				this.m_sortExpressions = value;
			}
		}

		// Token: 0x1700229C RID: 8860
		// (get) Token: 0x0600620B RID: 25099 RVA: 0x0018780A File Offset: 0x00185A0A
		// (set) Token: 0x0600620C RID: 25100 RVA: 0x00187812 File Offset: 0x00185A12
		internal BoolList SortDirections
		{
			get
			{
				return this.m_sortDirections;
			}
			set
			{
				this.m_sortDirections = value;
			}
		}

		// Token: 0x1700229D RID: 8861
		// (get) Token: 0x0600620D RID: 25101 RVA: 0x0018781B File Offset: 0x00185A1B
		internal SortingExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x0600620E RID: 25102 RVA: 0x00187824 File Offset: 0x00185A24
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.SortingStart();
			if (this.m_sortExpressions != null)
			{
				for (int i = 0; i < this.m_sortExpressions.Count; i++)
				{
					ExpressionInfo expressionInfo = this.m_sortExpressions[i];
					expressionInfo.Initialize("SortExpression", context);
					context.ExprHostBuilder.SortingExpression(expressionInfo);
				}
			}
			context.ExprHostBuilder.SortingEnd();
		}

		// Token: 0x0600620F RID: 25103 RVA: 0x0018788D File Offset: 0x00185A8D
		internal void SetExprHost(SortingExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06006210 RID: 25104 RVA: 0x001878B8 File Offset: 0x00185AB8
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.SortExpressions, ObjectType.ExpressionInfoList),
				new MemberInfo(MemberName.SortDirections, ObjectType.BoolList)
			});
		}

		// Token: 0x04003189 RID: 12681
		private ExpressionInfoList m_sortExpressions;

		// Token: 0x0400318A RID: 12682
		private BoolList m_sortDirections;

		// Token: 0x0400318B RID: 12683
		[NonSerialized]
		private SortingExprHost m_exprHost;
	}
}
