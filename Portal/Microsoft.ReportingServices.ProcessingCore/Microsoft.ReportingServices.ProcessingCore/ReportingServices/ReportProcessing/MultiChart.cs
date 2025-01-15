using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000703 RID: 1795
	[Serializable]
	internal sealed class MultiChart : ReportHierarchyNode
	{
		// Token: 0x06006423 RID: 25635 RVA: 0x0018D368 File Offset: 0x0018B568
		internal MultiChart()
		{
		}

		// Token: 0x06006424 RID: 25636 RVA: 0x0018D370 File Offset: 0x0018B570
		internal MultiChart(int id, Chart chartDef)
			: base(id, chartDef)
		{
		}

		// Token: 0x17002371 RID: 9073
		// (get) Token: 0x06006425 RID: 25637 RVA: 0x0018D37A File Offset: 0x0018B57A
		// (set) Token: 0x06006426 RID: 25638 RVA: 0x0018D382 File Offset: 0x0018B582
		internal MultiChart.Layouts Layout
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

		// Token: 0x17002372 RID: 9074
		// (get) Token: 0x06006427 RID: 25639 RVA: 0x0018D38B File Offset: 0x0018B58B
		// (set) Token: 0x06006428 RID: 25640 RVA: 0x0018D393 File Offset: 0x0018B593
		internal int MaxCount
		{
			get
			{
				return this.m_maxCount;
			}
			set
			{
				this.m_maxCount = value;
			}
		}

		// Token: 0x17002373 RID: 9075
		// (get) Token: 0x06006429 RID: 25641 RVA: 0x0018D39C File Offset: 0x0018B59C
		// (set) Token: 0x0600642A RID: 25642 RVA: 0x0018D3A4 File Offset: 0x0018B5A4
		internal bool SyncScale
		{
			get
			{
				return this.m_syncScale;
			}
			set
			{
				this.m_syncScale = value;
			}
		}

		// Token: 0x0600642B RID: 25643 RVA: 0x0018D3AD File Offset: 0x0018B5AD
		internal void SetExprHost(MultiChartExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			exprHost.SetReportObjectModel(reportObjectModel);
			base.ReportHierarchyNodeSetExprHost(exprHost.GroupingHost, null, reportObjectModel);
		}

		// Token: 0x0600642C RID: 25644 RVA: 0x0018D3D8 File Offset: 0x0018B5D8
		internal new void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MultiChartStart();
			base.Initialize(context);
			context.ExprHostBuilder.MultiChartEnd();
		}

		// Token: 0x0600642D RID: 25645 RVA: 0x0018D3FC File Offset: 0x0018B5FC
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportHierarchyNode, new MemberInfoList
			{
				new MemberInfo(MemberName.Layout, Token.Enum),
				new MemberInfo(MemberName.MaxCount, Token.Int32),
				new MemberInfo(MemberName.SyncScale, Token.Boolean)
			});
		}

		// Token: 0x04003240 RID: 12864
		private MultiChart.Layouts m_layout;

		// Token: 0x04003241 RID: 12865
		private int m_maxCount;

		// Token: 0x04003242 RID: 12866
		private bool m_syncScale;

		// Token: 0x02000CD3 RID: 3283
		internal enum Layouts
		{
			// Token: 0x04004EEC RID: 20204
			Automatic,
			// Token: 0x04004EED RID: 20205
			Horizontal,
			// Token: 0x04004EEE RID: 20206
			Vertical
		}
	}
}
