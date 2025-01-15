using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006FB RID: 1787
	[Serializable]
	internal sealed class OWCChart : DataRegion, IRunningValueHolder
	{
		// Token: 0x0600636C RID: 25452 RVA: 0x0018AEB4 File Offset: 0x001890B4
		internal OWCChart(ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x0600636D RID: 25453 RVA: 0x0018AEBD File Offset: 0x001890BD
		internal OWCChart(int id, ReportItem parent)
			: base(id, parent)
		{
			this.m_chartData = new ChartColumnList();
			this.m_detailRunningValues = new RunningValueInfoList();
			this.m_runningValues = new RunningValueInfoList();
		}

		// Token: 0x17002327 RID: 8999
		// (get) Token: 0x0600636E RID: 25454 RVA: 0x0018AEE8 File Offset: 0x001890E8
		internal override ObjectType ObjectType
		{
			get
			{
				return ObjectType.OWCChart;
			}
		}

		// Token: 0x17002328 RID: 9000
		// (get) Token: 0x0600636F RID: 25455 RVA: 0x0018AEEC File Offset: 0x001890EC
		// (set) Token: 0x06006370 RID: 25456 RVA: 0x0018AEF4 File Offset: 0x001890F4
		internal ChartColumnList ChartData
		{
			get
			{
				return this.m_chartData;
			}
			set
			{
				this.m_chartData = value;
			}
		}

		// Token: 0x17002329 RID: 9001
		// (get) Token: 0x06006371 RID: 25457 RVA: 0x0018AEFD File Offset: 0x001890FD
		// (set) Token: 0x06006372 RID: 25458 RVA: 0x0018AF05 File Offset: 0x00189105
		internal string ChartDefinition
		{
			get
			{
				return this.m_chartDefinition;
			}
			set
			{
				this.m_chartDefinition = value;
			}
		}

		// Token: 0x1700232A RID: 9002
		// (get) Token: 0x06006373 RID: 25459 RVA: 0x0018AF0E File Offset: 0x0018910E
		// (set) Token: 0x06006374 RID: 25460 RVA: 0x0018AF16 File Offset: 0x00189116
		internal RunningValueInfoList DetailRunningValues
		{
			get
			{
				return this.m_detailRunningValues;
			}
			set
			{
				this.m_detailRunningValues = value;
			}
		}

		// Token: 0x1700232B RID: 9003
		// (get) Token: 0x06006375 RID: 25461 RVA: 0x0018AF1F File Offset: 0x0018911F
		// (set) Token: 0x06006376 RID: 25462 RVA: 0x0018AF27 File Offset: 0x00189127
		internal RunningValueInfoList RunningValues
		{
			get
			{
				return this.m_runningValues;
			}
			set
			{
				this.m_runningValues = value;
			}
		}

		// Token: 0x1700232C RID: 9004
		// (get) Token: 0x06006377 RID: 25463 RVA: 0x0018AF30 File Offset: 0x00189130
		internal OWCChartExprHost OWCChartExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x1700232D RID: 9005
		// (get) Token: 0x06006378 RID: 25464 RVA: 0x0018AF38 File Offset: 0x00189138
		protected override DataRegionExprHost DataRegionExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06006379 RID: 25465 RVA: 0x0018AF40 File Offset: 0x00189140
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			context.RegisterDataRegion(this);
			this.InternalInitialize(context);
			context.UnRegisterDataRegion(this);
			return false;
		}

		// Token: 0x0600637A RID: 25466 RVA: 0x0018AF74 File Offset: 0x00189174
		private void InternalInitialize(InitializationContext context)
		{
			context.Location = context.Location | LocationFlags.InDataSet | LocationFlags.InDataRegion;
			context.ExprHostBuilder.OWCChartStart(this.m_name);
			base.Initialize(context);
			context.Location &= ~LocationFlags.InMatrixCellTopLevelItem;
			context.RegisterRunningValues(this.m_runningValues);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, false, false);
			}
			context.UnRegisterRunningValues(this.m_runningValues);
			context.RegisterRunningValues(this.m_detailRunningValues);
			if (this.m_chartData != null)
			{
				context.ExprHostBuilder.OWCChartColumnsStart();
				for (int i = 0; i < this.m_chartData.Count; i++)
				{
					this.m_chartData[i].Initialize(context);
				}
				context.ExprHostBuilder.OWCChartColumnsEnd();
			}
			context.UnRegisterRunningValues(this.m_detailRunningValues);
			base.ExprHostID = context.ExprHostBuilder.OWCChartEnd();
		}

		// Token: 0x0600637B RID: 25467 RVA: 0x0018B064 File Offset: 0x00189264
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.OWCChartHostsRemotable[base.ExprHostID];
				base.DataRegionSetExprHost(this.m_exprHost, reportObjectModel);
			}
		}

		// Token: 0x0600637C RID: 25468 RVA: 0x0018B0B2 File Offset: 0x001892B2
		RunningValueInfoList IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		// Token: 0x0600637D RID: 25469 RVA: 0x0018B0BC File Offset: 0x001892BC
		void IRunningValueHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_detailRunningValues != null);
			if (this.m_detailRunningValues.Count == 0)
			{
				this.m_detailRunningValues = null;
			}
			Global.Tracer.Assert(this.m_runningValues != null);
			if (this.m_runningValues.Count == 0)
			{
				this.m_runningValues = null;
			}
		}

		// Token: 0x0600637E RID: 25470 RVA: 0x0018B118 File Offset: 0x00189318
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataRegion, new MemberInfoList
			{
				new MemberInfo(MemberName.ChartData, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ChartColumnList),
				new MemberInfo(MemberName.ChartDefinition, Token.String),
				new MemberInfo(MemberName.DetailRunningValues, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.RunningValueInfoList),
				new MemberInfo(MemberName.RunningValues, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.RunningValueInfoList)
			});
		}

		// Token: 0x04003203 RID: 12803
		private ChartColumnList m_chartData;

		// Token: 0x04003204 RID: 12804
		private string m_chartDefinition;

		// Token: 0x04003205 RID: 12805
		private RunningValueInfoList m_detailRunningValues;

		// Token: 0x04003206 RID: 12806
		private RunningValueInfoList m_runningValues;

		// Token: 0x04003207 RID: 12807
		[NonSerialized]
		private OWCChartExprHost m_exprHost;
	}
}
