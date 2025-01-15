using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000083 RID: 131
	public abstract class GaugeScaleExprHost : StyleExprHost
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000337E File Offset: 0x0000157E
		internal IList<ScaleRangeExprHost> ScaleRangesHostsRemotable
		{
			get
			{
				return this.m_scaleRangesHostsRemotable;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00003386 File Offset: 0x00001586
		internal IList<CustomLabelExprHost> CustomLabelsHostsRemotable
		{
			get
			{
				return this.m_customLabelsHostsRemotable;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000338E File Offset: 0x0000158E
		public virtual object IntervalExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00003391 File Offset: 0x00001591
		public virtual object IntervalOffsetExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00003394 File Offset: 0x00001594
		public virtual object LogarithmicExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00003397 File Offset: 0x00001597
		public virtual object LogarithmicBaseExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000339A File Offset: 0x0000159A
		public virtual object MultiplierExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000339D File Offset: 0x0000159D
		public virtual object ReversedExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000033A0 File Offset: 0x000015A0
		public virtual object TickMarksOnTopExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000033A3 File Offset: 0x000015A3
		public virtual object ToolTipExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000033A6 File Offset: 0x000015A6
		public virtual object HiddenExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060002AE RID: 686 RVA: 0x000033A9 File Offset: 0x000015A9
		public virtual object WidthExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000D4 RID: 212
		[CLSCompliant(false)]
		protected IList<ScaleRangeExprHost> m_scaleRangesHostsRemotable;

		// Token: 0x040000D5 RID: 213
		[CLSCompliant(false)]
		protected IList<CustomLabelExprHost> m_customLabelsHostsRemotable;

		// Token: 0x040000D6 RID: 214
		public GaugeInputValueExprHost MaximumValueHost;

		// Token: 0x040000D7 RID: 215
		public GaugeInputValueExprHost MinimumValueHost;

		// Token: 0x040000D8 RID: 216
		public GaugeTickMarksExprHost GaugeMajorTickMarksHost;

		// Token: 0x040000D9 RID: 217
		public GaugeTickMarksExprHost GaugeMinorTickMarksHost;

		// Token: 0x040000DA RID: 218
		public ScalePinExprHost MaximumPinHost;

		// Token: 0x040000DB RID: 219
		public ScalePinExprHost MinimumPinHost;

		// Token: 0x040000DC RID: 220
		public ScaleLabelsExprHost ScaleLabelsHost;

		// Token: 0x040000DD RID: 221
		public ActionInfoExprHost ActionInfoHost;
	}
}
