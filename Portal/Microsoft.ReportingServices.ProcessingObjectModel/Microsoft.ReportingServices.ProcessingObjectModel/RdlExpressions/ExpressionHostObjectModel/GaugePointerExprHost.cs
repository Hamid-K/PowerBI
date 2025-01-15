using System;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000082 RID: 130
	public abstract class GaugePointerExprHost : StyleExprHost
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00003358 File Offset: 0x00001558
		public virtual object BarStartExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000335B File Offset: 0x0000155B
		public virtual object DistanceFromScaleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000335E File Offset: 0x0000155E
		public virtual object MarkerLengthExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00003361 File Offset: 0x00001561
		public virtual object MarkerStyleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00003364 File Offset: 0x00001564
		public virtual object PlacementExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00003367 File Offset: 0x00001567
		public virtual object SnappingEnabledExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000336A File Offset: 0x0000156A
		public virtual object SnappingIntervalExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000336D File Offset: 0x0000156D
		public virtual object ToolTipExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00003370 File Offset: 0x00001570
		public virtual object HiddenExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00003373 File Offset: 0x00001573
		public virtual object WidthExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000D1 RID: 209
		public GaugeInputValueExprHost GaugeInputValueHost;

		// Token: 0x040000D2 RID: 210
		public PointerImageExprHost PointerImageHost;

		// Token: 0x040000D3 RID: 211
		public ActionInfoExprHost ActionInfoHost;
	}
}
