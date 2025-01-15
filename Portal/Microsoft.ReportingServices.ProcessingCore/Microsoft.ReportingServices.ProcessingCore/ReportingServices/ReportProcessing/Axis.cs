using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000706 RID: 1798
	[Serializable]
	internal sealed class Axis
	{
		// Token: 0x17002382 RID: 9090
		// (get) Token: 0x06006451 RID: 25681 RVA: 0x0018D71F File Offset: 0x0018B91F
		// (set) Token: 0x06006452 RID: 25682 RVA: 0x0018D727 File Offset: 0x0018B927
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

		// Token: 0x17002383 RID: 9091
		// (get) Token: 0x06006453 RID: 25683 RVA: 0x0018D730 File Offset: 0x0018B930
		// (set) Token: 0x06006454 RID: 25684 RVA: 0x0018D738 File Offset: 0x0018B938
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

		// Token: 0x17002384 RID: 9092
		// (get) Token: 0x06006455 RID: 25685 RVA: 0x0018D741 File Offset: 0x0018B941
		// (set) Token: 0x06006456 RID: 25686 RVA: 0x0018D749 File Offset: 0x0018B949
		internal ChartTitle Title
		{
			get
			{
				return this.m_title;
			}
			set
			{
				this.m_title = value;
			}
		}

		// Token: 0x17002385 RID: 9093
		// (get) Token: 0x06006457 RID: 25687 RVA: 0x0018D752 File Offset: 0x0018B952
		// (set) Token: 0x06006458 RID: 25688 RVA: 0x0018D75A File Offset: 0x0018B95A
		internal bool Margin
		{
			get
			{
				return this.m_margin;
			}
			set
			{
				this.m_margin = value;
			}
		}

		// Token: 0x17002386 RID: 9094
		// (get) Token: 0x06006459 RID: 25689 RVA: 0x0018D763 File Offset: 0x0018B963
		// (set) Token: 0x0600645A RID: 25690 RVA: 0x0018D76B File Offset: 0x0018B96B
		internal Axis.TickMarks MajorTickMarks
		{
			get
			{
				return this.m_majorTickMarks;
			}
			set
			{
				this.m_majorTickMarks = value;
			}
		}

		// Token: 0x17002387 RID: 9095
		// (get) Token: 0x0600645B RID: 25691 RVA: 0x0018D774 File Offset: 0x0018B974
		// (set) Token: 0x0600645C RID: 25692 RVA: 0x0018D77C File Offset: 0x0018B97C
		internal Axis.TickMarks MinorTickMarks
		{
			get
			{
				return this.m_minorTickMarks;
			}
			set
			{
				this.m_minorTickMarks = value;
			}
		}

		// Token: 0x17002388 RID: 9096
		// (get) Token: 0x0600645D RID: 25693 RVA: 0x0018D785 File Offset: 0x0018B985
		// (set) Token: 0x0600645E RID: 25694 RVA: 0x0018D78D File Offset: 0x0018B98D
		internal GridLines MajorGridLines
		{
			get
			{
				return this.m_majorGridLines;
			}
			set
			{
				this.m_majorGridLines = value;
			}
		}

		// Token: 0x17002389 RID: 9097
		// (get) Token: 0x0600645F RID: 25695 RVA: 0x0018D796 File Offset: 0x0018B996
		// (set) Token: 0x06006460 RID: 25696 RVA: 0x0018D79E File Offset: 0x0018B99E
		internal GridLines MinorGridLines
		{
			get
			{
				return this.m_minorGridLines;
			}
			set
			{
				this.m_minorGridLines = value;
			}
		}

		// Token: 0x1700238A RID: 9098
		// (get) Token: 0x06006461 RID: 25697 RVA: 0x0018D7A7 File Offset: 0x0018B9A7
		// (set) Token: 0x06006462 RID: 25698 RVA: 0x0018D7AF File Offset: 0x0018B9AF
		internal ExpressionInfo MajorInterval
		{
			get
			{
				return this.m_majorInterval;
			}
			set
			{
				this.m_majorInterval = value;
			}
		}

		// Token: 0x1700238B RID: 9099
		// (get) Token: 0x06006463 RID: 25699 RVA: 0x0018D7B8 File Offset: 0x0018B9B8
		// (set) Token: 0x06006464 RID: 25700 RVA: 0x0018D7C0 File Offset: 0x0018B9C0
		internal ExpressionInfo MinorInterval
		{
			get
			{
				return this.m_minorInterval;
			}
			set
			{
				this.m_minorInterval = value;
			}
		}

		// Token: 0x1700238C RID: 9100
		// (get) Token: 0x06006465 RID: 25701 RVA: 0x0018D7C9 File Offset: 0x0018B9C9
		// (set) Token: 0x06006466 RID: 25702 RVA: 0x0018D7D1 File Offset: 0x0018B9D1
		internal bool Reverse
		{
			get
			{
				return this.m_reverse;
			}
			set
			{
				this.m_reverse = value;
			}
		}

		// Token: 0x1700238D RID: 9101
		// (get) Token: 0x06006467 RID: 25703 RVA: 0x0018D7DA File Offset: 0x0018B9DA
		// (set) Token: 0x06006468 RID: 25704 RVA: 0x0018D7E2 File Offset: 0x0018B9E2
		internal ExpressionInfo CrossAt
		{
			get
			{
				return this.m_crossAt;
			}
			set
			{
				this.m_crossAt = value;
			}
		}

		// Token: 0x1700238E RID: 9102
		// (get) Token: 0x06006469 RID: 25705 RVA: 0x0018D7EB File Offset: 0x0018B9EB
		// (set) Token: 0x0600646A RID: 25706 RVA: 0x0018D7F3 File Offset: 0x0018B9F3
		internal bool AutoCrossAt
		{
			get
			{
				return this.m_autoCrossAt;
			}
			set
			{
				this.m_autoCrossAt = value;
			}
		}

		// Token: 0x1700238F RID: 9103
		// (get) Token: 0x0600646B RID: 25707 RVA: 0x0018D7FC File Offset: 0x0018B9FC
		// (set) Token: 0x0600646C RID: 25708 RVA: 0x0018D804 File Offset: 0x0018BA04
		internal bool Interlaced
		{
			get
			{
				return this.m_interlaced;
			}
			set
			{
				this.m_interlaced = value;
			}
		}

		// Token: 0x17002390 RID: 9104
		// (get) Token: 0x0600646D RID: 25709 RVA: 0x0018D80D File Offset: 0x0018BA0D
		// (set) Token: 0x0600646E RID: 25710 RVA: 0x0018D815 File Offset: 0x0018BA15
		internal bool Scalar
		{
			get
			{
				return this.m_scalar;
			}
			set
			{
				this.m_scalar = value;
			}
		}

		// Token: 0x17002391 RID: 9105
		// (get) Token: 0x0600646F RID: 25711 RVA: 0x0018D81E File Offset: 0x0018BA1E
		// (set) Token: 0x06006470 RID: 25712 RVA: 0x0018D826 File Offset: 0x0018BA26
		internal ExpressionInfo Min
		{
			get
			{
				return this.m_min;
			}
			set
			{
				this.m_min = value;
			}
		}

		// Token: 0x17002392 RID: 9106
		// (get) Token: 0x06006471 RID: 25713 RVA: 0x0018D82F File Offset: 0x0018BA2F
		// (set) Token: 0x06006472 RID: 25714 RVA: 0x0018D837 File Offset: 0x0018BA37
		internal ExpressionInfo Max
		{
			get
			{
				return this.m_max;
			}
			set
			{
				this.m_max = value;
			}
		}

		// Token: 0x17002393 RID: 9107
		// (get) Token: 0x06006473 RID: 25715 RVA: 0x0018D840 File Offset: 0x0018BA40
		// (set) Token: 0x06006474 RID: 25716 RVA: 0x0018D848 File Offset: 0x0018BA48
		internal bool AutoScaleMin
		{
			get
			{
				return this.m_autoScaleMin;
			}
			set
			{
				this.m_autoScaleMin = value;
			}
		}

		// Token: 0x17002394 RID: 9108
		// (get) Token: 0x06006475 RID: 25717 RVA: 0x0018D851 File Offset: 0x0018BA51
		// (set) Token: 0x06006476 RID: 25718 RVA: 0x0018D859 File Offset: 0x0018BA59
		internal bool AutoScaleMax
		{
			get
			{
				return this.m_autoScaleMax;
			}
			set
			{
				this.m_autoScaleMax = value;
			}
		}

		// Token: 0x17002395 RID: 9109
		// (get) Token: 0x06006477 RID: 25719 RVA: 0x0018D862 File Offset: 0x0018BA62
		// (set) Token: 0x06006478 RID: 25720 RVA: 0x0018D86A File Offset: 0x0018BA6A
		internal bool LogScale
		{
			get
			{
				return this.m_logScale;
			}
			set
			{
				this.m_logScale = value;
			}
		}

		// Token: 0x17002396 RID: 9110
		// (get) Token: 0x06006479 RID: 25721 RVA: 0x0018D873 File Offset: 0x0018BA73
		// (set) Token: 0x0600647A RID: 25722 RVA: 0x0018D87B File Offset: 0x0018BA7B
		internal DataValueList CustomProperties
		{
			get
			{
				return this.m_customProperties;
			}
			set
			{
				this.m_customProperties = value;
			}
		}

		// Token: 0x17002397 RID: 9111
		// (get) Token: 0x0600647B RID: 25723 RVA: 0x0018D884 File Offset: 0x0018BA84
		internal AxisExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x0600647C RID: 25724 RVA: 0x0018D88C File Offset: 0x0018BA8C
		internal void SetExprHost(AxisExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_title != null && this.m_exprHost.TitleHost != null)
			{
				this.m_title.SetExprHost(this.m_exprHost.TitleHost, reportObjectModel);
			}
			if (this.m_styleClass != null)
			{
				this.m_styleClass.SetStyleExprHost(this.m_exprHost);
			}
			if (this.m_majorGridLines != null && this.m_majorGridLines.StyleClass != null && this.m_exprHost.MajorGridLinesHost != null)
			{
				this.m_majorGridLines.SetExprHost(this.m_exprHost.MajorGridLinesHost, reportObjectModel);
			}
			if (this.m_minorGridLines != null && this.m_minorGridLines.StyleClass != null && this.m_exprHost.MinorGridLinesHost != null)
			{
				this.m_minorGridLines.SetExprHost(this.m_exprHost.MinorGridLinesHost, reportObjectModel);
			}
			if (this.m_exprHost.CustomPropertyHostsRemotable != null)
			{
				Global.Tracer.Assert(this.m_customProperties != null);
				this.m_customProperties.SetExprHost(this.m_exprHost.CustomPropertyHostsRemotable, reportObjectModel);
			}
		}

		// Token: 0x0600647D RID: 25725 RVA: 0x0018D9B0 File Offset: 0x0018BBB0
		internal void Initialize(InitializationContext context, Axis.Mode mode)
		{
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
			if (this.m_title != null)
			{
				this.m_title.Initialize(context);
			}
			if (this.m_minorGridLines != null)
			{
				context.ExprHostBuilder.MinorGridLinesStyleStart();
				this.m_minorGridLines.Initialize(context);
				context.ExprHostBuilder.MinorGridLinesStyleEnd();
			}
			if (this.m_majorGridLines != null)
			{
				context.ExprHostBuilder.MajorGridLinesStyleStart();
				this.m_majorGridLines.Initialize(context);
				context.ExprHostBuilder.MajorGridLinesStyleEnd();
			}
			string text = mode.ToString();
			if (this.m_min != null)
			{
				this.m_min.Initialize(text + ".Min", context);
				context.ExprHostBuilder.AxisMin(this.m_min);
			}
			if (this.m_max != null)
			{
				this.m_max.Initialize(text + ".Max", context);
				context.ExprHostBuilder.AxisMax(this.m_max);
			}
			if (this.m_crossAt != null)
			{
				this.m_crossAt.Initialize(text + ".CrossAt", context);
				context.ExprHostBuilder.AxisCrossAt(this.m_crossAt);
			}
			if (this.m_majorInterval != null)
			{
				this.m_majorInterval.Initialize(text + ".MajorInterval", context);
				context.ExprHostBuilder.AxisMajorInterval(this.m_majorInterval);
			}
			if (this.m_minorInterval != null)
			{
				this.m_minorInterval.Initialize(text + ".MinorInterval", context);
				context.ExprHostBuilder.AxisMinorInterval(this.m_minorInterval);
			}
			if (this.m_customProperties != null)
			{
				this.m_customProperties.Initialize(text + ".", true, context);
			}
		}

		// Token: 0x0600647E RID: 25726 RVA: 0x0018DB60 File Offset: 0x0018BD60
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Visible, Token.Boolean),
				new MemberInfo(MemberName.StyleClass, ObjectType.Style),
				new MemberInfo(MemberName.Title, ObjectType.ChartTitle),
				new MemberInfo(MemberName.Margin, Token.Boolean),
				new MemberInfo(MemberName.MajorTickMarks, Token.Enum),
				new MemberInfo(MemberName.MinorTickMarks, Token.Enum),
				new MemberInfo(MemberName.MajorGridLines, ObjectType.GridLines),
				new MemberInfo(MemberName.MinorGridLines, ObjectType.GridLines),
				new MemberInfo(MemberName.MajorInterval, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MinorInterval, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Reverse, Token.Boolean),
				new MemberInfo(MemberName.CrossAt, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.AutoCrossAt, Token.Boolean),
				new MemberInfo(MemberName.Interlaced, Token.Boolean),
				new MemberInfo(MemberName.Scalar, Token.Boolean),
				new MemberInfo(MemberName.Min, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Max, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.AutoScaleMin, Token.Boolean),
				new MemberInfo(MemberName.AutoScaleMax, Token.Boolean),
				new MemberInfo(MemberName.LogScale, Token.Boolean),
				new MemberInfo(MemberName.CustomProperties, ObjectType.DataValueList)
			});
		}

		// Token: 0x04003251 RID: 12881
		private bool m_visible;

		// Token: 0x04003252 RID: 12882
		private Style m_styleClass;

		// Token: 0x04003253 RID: 12883
		private ChartTitle m_title;

		// Token: 0x04003254 RID: 12884
		private bool m_margin;

		// Token: 0x04003255 RID: 12885
		private Axis.TickMarks m_majorTickMarks;

		// Token: 0x04003256 RID: 12886
		private Axis.TickMarks m_minorTickMarks;

		// Token: 0x04003257 RID: 12887
		private GridLines m_majorGridLines;

		// Token: 0x04003258 RID: 12888
		private GridLines m_minorGridLines;

		// Token: 0x04003259 RID: 12889
		private ExpressionInfo m_majorInterval;

		// Token: 0x0400325A RID: 12890
		private ExpressionInfo m_minorInterval;

		// Token: 0x0400325B RID: 12891
		private bool m_reverse;

		// Token: 0x0400325C RID: 12892
		private ExpressionInfo m_crossAt;

		// Token: 0x0400325D RID: 12893
		private bool m_autoCrossAt = true;

		// Token: 0x0400325E RID: 12894
		private bool m_interlaced;

		// Token: 0x0400325F RID: 12895
		private bool m_scalar;

		// Token: 0x04003260 RID: 12896
		private ExpressionInfo m_min;

		// Token: 0x04003261 RID: 12897
		private ExpressionInfo m_max;

		// Token: 0x04003262 RID: 12898
		private bool m_autoScaleMin = true;

		// Token: 0x04003263 RID: 12899
		private bool m_autoScaleMax = true;

		// Token: 0x04003264 RID: 12900
		private bool m_logScale;

		// Token: 0x04003265 RID: 12901
		private DataValueList m_customProperties;

		// Token: 0x04003266 RID: 12902
		[NonSerialized]
		private AxisExprHost m_exprHost;

		// Token: 0x02000CD6 RID: 3286
		internal enum TickMarks
		{
			// Token: 0x04004EF9 RID: 20217
			None,
			// Token: 0x04004EFA RID: 20218
			Inside,
			// Token: 0x04004EFB RID: 20219
			Outside,
			// Token: 0x04004EFC RID: 20220
			Cross
		}

		// Token: 0x02000CD7 RID: 3287
		internal enum Mode
		{
			// Token: 0x04004EFE RID: 20222
			CategoryAxis,
			// Token: 0x04004EFF RID: 20223
			CategoryAxisSecondary,
			// Token: 0x04004F00 RID: 20224
			ValueAxis,
			// Token: 0x04004F01 RID: 20225
			ValueAxisSecondary
		}

		// Token: 0x02000CD8 RID: 3288
		internal enum ExpressionType
		{
			// Token: 0x04004F03 RID: 20227
			Min,
			// Token: 0x04004F04 RID: 20228
			Max,
			// Token: 0x04004F05 RID: 20229
			CrossAt,
			// Token: 0x04004F06 RID: 20230
			MajorInterval,
			// Token: 0x04004F07 RID: 20231
			MinorInterval
		}
	}
}
