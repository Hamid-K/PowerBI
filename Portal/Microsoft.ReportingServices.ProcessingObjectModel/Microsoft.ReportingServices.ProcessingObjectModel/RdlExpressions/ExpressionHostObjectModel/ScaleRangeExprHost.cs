using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000091 RID: 145
	public abstract class ScaleRangeExprHost : StyleExprHost
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000034EF File Offset: 0x000016EF
		public virtual object DistanceFromScaleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000034F2 File Offset: 0x000016F2
		public virtual object StartWidthExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000034F5 File Offset: 0x000016F5
		public virtual object EndWidthExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000034F8 File Offset: 0x000016F8
		public virtual object InRangeBarPointerColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000034FB File Offset: 0x000016FB
		public virtual object InRangeLabelColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000300 RID: 768 RVA: 0x000034FE File Offset: 0x000016FE
		public virtual object InRangeTickMarksColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00003501 File Offset: 0x00001701
		public virtual object PlacementExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00003504 File Offset: 0x00001704
		public virtual object ToolTipExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00003507 File Offset: 0x00001707
		public virtual object HiddenExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000EC RID: 236
		public GaugeInputValueExprHost StartValueHost;

		// Token: 0x040000ED RID: 237
		public GaugeInputValueExprHost EndValueHost;

		// Token: 0x040000EE RID: 238
		public ActionInfoExprHost ActionInfoHost;
	}
}
