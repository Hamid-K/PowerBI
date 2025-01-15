using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000B1 RID: 177
	public abstract class ChartAxisExprHost : StyleExprHost
	{
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000387E File Offset: 0x00001A7E
		internal IList<DataValueExprHost> CustomPropertyHostsRemotable
		{
			get
			{
				return this.m_customPropertyHostsRemotable;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00003886 File Offset: 0x00001A86
		internal IList<ChartStripLineExprHost> ChartStripLinesHostsRemotable
		{
			get
			{
				return this.m_stripLinesHostsRemotable;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000388E File Offset: 0x00001A8E
		public virtual object AxisMinExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00003891 File Offset: 0x00001A91
		public virtual object AxisMaxExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00003894 File Offset: 0x00001A94
		public virtual object AxisCrossAtExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00003897 File Offset: 0x00001A97
		public virtual object VisibleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000389A File Offset: 0x00001A9A
		public virtual object MarginExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000389D File Offset: 0x00001A9D
		public virtual object IntervalExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x000038A0 File Offset: 0x00001AA0
		public virtual object IntervalTypeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x000038A3 File Offset: 0x00001AA3
		public virtual object IntervalOffsetExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x000038A6 File Offset: 0x00001AA6
		public virtual object IntervalOffsetTypeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x000038A9 File Offset: 0x00001AA9
		public virtual object MarksAlwaysAtPlotEdgeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x000038AC File Offset: 0x00001AAC
		public virtual object ReverseExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x000038AF File Offset: 0x00001AAF
		public virtual object LocationExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x000038B2 File Offset: 0x00001AB2
		public virtual object InterlacedExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x000038B5 File Offset: 0x00001AB5
		public virtual object InterlacedColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000038B8 File Offset: 0x00001AB8
		public virtual object LogScaleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x000038BB File Offset: 0x00001ABB
		public virtual object LogBaseExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x000038BE File Offset: 0x00001ABE
		public virtual object HideLabelsExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x000038C1 File Offset: 0x00001AC1
		public virtual object AngleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x000038C4 File Offset: 0x00001AC4
		public virtual object ArrowsExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x000038C7 File Offset: 0x00001AC7
		public virtual object PreventFontShrinkExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000038CA File Offset: 0x00001ACA
		public virtual object PreventFontGrowExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x000038CD File Offset: 0x00001ACD
		public virtual object PreventLabelOffsetExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000038D0 File Offset: 0x00001AD0
		public virtual object PreventWordWrapExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x000038D3 File Offset: 0x00001AD3
		public virtual object AllowLabelRotationExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x000038D6 File Offset: 0x00001AD6
		public virtual object IncludeZeroExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x000038D9 File Offset: 0x00001AD9
		public virtual object LabelsAutoFitDisabledExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x000038DC File Offset: 0x00001ADC
		public virtual object MinFontSizeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x000038DF File Offset: 0x00001ADF
		public virtual object MaxFontSizeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x000038E2 File Offset: 0x00001AE2
		public virtual object OffsetLabelsExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000038E5 File Offset: 0x00001AE5
		public virtual object HideEndLabelsExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x000038E8 File Offset: 0x00001AE8
		public virtual object VariableAutoIntervalExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x000038EB File Offset: 0x00001AEB
		public virtual object LabelIntervalExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x000038EE File Offset: 0x00001AEE
		public virtual object LabelIntervalTypeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x000038F1 File Offset: 0x00001AF1
		public virtual object LabelIntervalOffsetExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x000038F4 File Offset: 0x00001AF4
		public virtual object LabelIntervalOffsetTypeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000125 RID: 293
		public ChartAxisTitleExprHost TitleHost;

		// Token: 0x04000126 RID: 294
		public ChartGridLinesExprHost MajorGridLinesHost;

		// Token: 0x04000127 RID: 295
		public ChartGridLinesExprHost MinorGridLinesHost;

		// Token: 0x04000128 RID: 296
		[CLSCompliant(false)]
		protected IList<DataValueExprHost> m_customPropertyHostsRemotable;

		// Token: 0x04000129 RID: 297
		[CLSCompliant(false)]
		protected IList<ChartStripLineExprHost> m_stripLinesHostsRemotable;

		// Token: 0x0400012A RID: 298
		public ChartTickMarksExprHost MajorTickMarksHost;

		// Token: 0x0400012B RID: 299
		public ChartTickMarksExprHost MinorTickMarksHost;

		// Token: 0x0400012C RID: 300
		public ChartAxisScaleBreakExprHost AxisScaleBreakHost;
	}
}
