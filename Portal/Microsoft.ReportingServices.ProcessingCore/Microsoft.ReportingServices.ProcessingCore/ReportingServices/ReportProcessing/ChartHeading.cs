using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006FE RID: 1790
	[Serializable]
	internal sealed class ChartHeading : PivotHeading, IRunningValueHolder
	{
		// Token: 0x060063C9 RID: 25545 RVA: 0x0018C62C File Offset: 0x0018A82C
		internal ChartHeading()
		{
		}

		// Token: 0x060063CA RID: 25546 RVA: 0x0018C634 File Offset: 0x0018A834
		internal ChartHeading(int id, Chart chartDef)
			: base(id, chartDef)
		{
			this.m_runningValues = new RunningValueInfoList();
		}

		// Token: 0x1700234F RID: 9039
		// (get) Token: 0x060063CB RID: 25547 RVA: 0x0018C649 File Offset: 0x0018A849
		// (set) Token: 0x060063CC RID: 25548 RVA: 0x0018C656 File Offset: 0x0018A856
		internal new ChartHeading SubHeading
		{
			get
			{
				return (ChartHeading)this.m_innerHierarchy;
			}
			set
			{
				this.m_innerHierarchy = value;
			}
		}

		// Token: 0x17002350 RID: 9040
		// (get) Token: 0x060063CD RID: 25549 RVA: 0x0018C65F File Offset: 0x0018A85F
		// (set) Token: 0x060063CE RID: 25550 RVA: 0x0018C667 File Offset: 0x0018A867
		internal ExpressionInfoList Labels
		{
			get
			{
				return this.m_labels;
			}
			set
			{
				this.m_labels = value;
			}
		}

		// Token: 0x17002351 RID: 9041
		// (get) Token: 0x060063CF RID: 25551 RVA: 0x0018C670 File Offset: 0x0018A870
		// (set) Token: 0x060063D0 RID: 25552 RVA: 0x0018C678 File Offset: 0x0018A878
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

		// Token: 0x17002352 RID: 9042
		// (get) Token: 0x060063D1 RID: 25553 RVA: 0x0018C681 File Offset: 0x0018A881
		// (set) Token: 0x060063D2 RID: 25554 RVA: 0x0018C689 File Offset: 0x0018A889
		internal bool ChartGroupExpression
		{
			get
			{
				return this.m_chartGroupExpression;
			}
			set
			{
				this.m_chartGroupExpression = value;
			}
		}

		// Token: 0x17002353 RID: 9043
		// (get) Token: 0x060063D3 RID: 25555 RVA: 0x0018C692 File Offset: 0x0018A892
		// (set) Token: 0x060063D4 RID: 25556 RVA: 0x0018C69A File Offset: 0x0018A89A
		internal BoolList PlotTypesLine
		{
			get
			{
				return this.m_plotTypesLine;
			}
			set
			{
				this.m_plotTypesLine = value;
			}
		}

		// Token: 0x17002354 RID: 9044
		// (get) Token: 0x060063D5 RID: 25557 RVA: 0x0018C6A3 File Offset: 0x0018A8A3
		internal ChartDynamicGroupExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x060063D6 RID: 25558 RVA: 0x0018C6AB File Offset: 0x0018A8AB
		RunningValueInfoList IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		// Token: 0x060063D7 RID: 25559 RVA: 0x0018C6B3 File Offset: 0x0018A8B3
		void IRunningValueHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_runningValues != null);
			if (this.m_runningValues.Count == 0)
			{
				this.m_runningValues = null;
			}
		}

		// Token: 0x060063D8 RID: 25560 RVA: 0x0018C6DC File Offset: 0x0018A8DC
		internal void SetExprHost(ChartDynamicGroupExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null && base.HasExprHost);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			base.ReportHierarchyNodeSetExprHost(this.m_exprHost, reportObjectModel);
		}

		// Token: 0x060063D9 RID: 25561 RVA: 0x0018C718 File Offset: 0x0018A918
		internal void LabelCollectionInitialize(InitializationContext context, bool registerRunningValues, bool isStatic)
		{
			if (isStatic)
			{
				if (this.m_isColumn)
				{
					context.ExprHostBuilder.ChartStaticColumnLabelsStart();
				}
				else
				{
					context.ExprHostBuilder.ChartStaticRowLabelsStart();
				}
			}
			if (registerRunningValues)
			{
				context.RegisterRunningValues(this.m_runningValues);
			}
			Global.Tracer.Assert(this.m_labels != null);
			for (int i = 0; i < this.m_labels.Count; i++)
			{
				Global.Tracer.Assert(this.m_labels[i] != null);
				this.m_labels[i].Initialize("Label", context);
				if (isStatic)
				{
					context.ExprHostBuilder.ChartStaticColumnRowLabel(this.m_labels[i]);
				}
				else
				{
					context.ExprHostBuilder.ChartHeadingLabel(this.m_labels[i]);
				}
			}
			if (registerRunningValues)
			{
				context.UnRegisterRunningValues(this.m_runningValues);
			}
			if (isStatic)
			{
				if (this.m_isColumn)
				{
					context.ExprHostBuilder.ChartStaticColumnLabelsEnd();
					return;
				}
				context.ExprHostBuilder.ChartStaticRowLabelsEnd();
			}
		}

		// Token: 0x060063DA RID: 25562 RVA: 0x0018C81C File Offset: 0x0018AA1C
		internal int DynamicInitialize(bool column, int level, InitializationContext context)
		{
			this.m_level = level;
			this.m_isColumn = column;
			if (this.m_grouping == null)
			{
				if (this.SubHeading != null)
				{
					this.SubHeading.DynamicInitialize(column, ++level, context);
				}
				return 1;
			}
			context.ExprHostBuilder.ChartDynamicGroupStart(this.m_grouping.Name);
			if (this.m_subtotal != null)
			{
				this.m_subtotal.RegisterReportItems(context);
				this.m_subtotal.Initialize(context);
			}
			context.Location |= LocationFlags.InGrouping;
			context.RegisterGroupingScope(this.m_grouping.Name, this.m_grouping.SimpleGroupExpressions, this.m_grouping.Aggregates, this.m_grouping.PostSortAggregates, this.m_grouping.RecursiveAggregates, this.m_grouping);
			ObjectType objectType = context.ObjectType;
			string objectName = context.ObjectName;
			context.ObjectType = ObjectType.Grouping;
			context.ObjectName = this.m_grouping.Name;
			base.Initialize(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, true, false);
			}
			if (this.SubHeading != null)
			{
				this.m_subtotalSpan = this.SubHeading.DynamicInitialize(column, ++level, context);
			}
			else
			{
				this.m_subtotalSpan = 1;
			}
			if (this.m_labels != null)
			{
				this.LabelCollectionInitialize(context, true, false);
			}
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
			context.ObjectType = objectType;
			context.ObjectName = objectName;
			context.UnRegisterGroupingScope(this.m_grouping.Name);
			if (this.m_subtotal != null)
			{
				this.m_subtotal.UnregisterReportItems(context);
			}
			this.m_hasExprHost = context.ExprHostBuilder.ChartDynamicGroupEnd(column);
			return this.m_subtotalSpan + 1;
		}

		// Token: 0x060063DB RID: 25563 RVA: 0x0018C9D0 File Offset: 0x0018ABD0
		internal int StaticInitialize(InitializationContext context)
		{
			if (this.m_grouping != null)
			{
				int num = 1;
				if (this.SubHeading != null)
				{
					context.Location |= LocationFlags.InGrouping;
					context.RegisterGroupingScope(this.m_grouping.Name, this.m_grouping.SimpleGroupExpressions, this.m_aggregates, this.m_postSortAggregates, this.m_recursiveAggregates, this.m_grouping);
					num = this.SubHeading.StaticInitialize(context);
					context.UnRegisterGroupingScope(this.m_grouping.Name);
				}
				return num + 1;
			}
			if (this.SubHeading != null)
			{
				this.m_subtotalSpan = this.SubHeading.StaticInitialize(context);
			}
			else
			{
				this.m_subtotalSpan = 1;
			}
			if (this.m_labels != null)
			{
				this.LabelCollectionInitialize(context, true, true);
			}
			return 0;
		}

		// Token: 0x060063DC RID: 25564 RVA: 0x0018CA8C File Offset: 0x0018AC8C
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.PivotHeading, new MemberInfoList
			{
				new MemberInfo(MemberName.Labels, ObjectType.ExpressionInfoList),
				new MemberInfo(MemberName.RunningValues, ObjectType.RunningValueInfoList),
				new MemberInfo(MemberName.ChartGroupExpression, Token.Boolean),
				new MemberInfo(MemberName.PlotTypesLine, ObjectType.BoolList)
			});
		}

		// Token: 0x04003220 RID: 12832
		private ExpressionInfoList m_labels;

		// Token: 0x04003221 RID: 12833
		private RunningValueInfoList m_runningValues;

		// Token: 0x04003222 RID: 12834
		private bool m_chartGroupExpression;

		// Token: 0x04003223 RID: 12835
		private BoolList m_plotTypesLine;

		// Token: 0x04003224 RID: 12836
		[NonSerialized]
		private ChartDynamicGroupExprHost m_exprHost;
	}
}
