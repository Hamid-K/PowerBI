using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000088 RID: 136
	public abstract class NumericIndicatorExprHost : GaugePanelItemExprHost
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060002BC RID: 700 RVA: 0x000033F6 File Offset: 0x000015F6
		[CLSCompliant(false)]
		public IList<NumericIndicatorRangeExprHost> NumericIndicatorRangesHostsRemotable
		{
			[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
			get
			{
				return this.m_numericIndicatorRangesHostsRemotable;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000033FE File Offset: 0x000015FE
		public virtual object DecimalDigitColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00003401 File Offset: 0x00001601
		public virtual object DigitColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00003404 File Offset: 0x00001604
		public virtual object UseFontPercentExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00003407 File Offset: 0x00001607
		public virtual object DecimalDigitsExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000340A File Offset: 0x0000160A
		public virtual object DigitsExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000340D File Offset: 0x0000160D
		public virtual object MultiplierExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00003410 File Offset: 0x00001610
		public virtual object NonNumericStringExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00003413 File Offset: 0x00001613
		public virtual object OutOfRangeStringExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00003416 File Offset: 0x00001616
		public virtual object ResizeModeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00003419 File Offset: 0x00001619
		public virtual object ShowDecimalPointExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000341C File Offset: 0x0000161C
		public virtual object ShowLeadingZerosExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000341F File Offset: 0x0000161F
		public virtual object IndicatorStyleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00003422 File Offset: 0x00001622
		public virtual object ShowSignExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00003425 File Offset: 0x00001625
		public virtual object SnappingEnabledExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00003428 File Offset: 0x00001628
		public virtual object SnappingIntervalExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000342B File Offset: 0x0000162B
		public virtual object LedDimColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000342E File Offset: 0x0000162E
		public virtual object SeparatorWidthExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00003431 File Offset: 0x00001631
		public virtual object SeparatorColorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000E1 RID: 225
		public GaugeInputValueExprHost GaugeInputValueHost;

		// Token: 0x040000E2 RID: 226
		[CLSCompliant(false)]
		protected IList<NumericIndicatorRangeExprHost> m_numericIndicatorRangesHostsRemotable;

		// Token: 0x040000E3 RID: 227
		public GaugeInputValueExprHost MinimumValueHost;

		// Token: 0x040000E4 RID: 228
		public GaugeInputValueExprHost MaximumValueHost;
	}
}
