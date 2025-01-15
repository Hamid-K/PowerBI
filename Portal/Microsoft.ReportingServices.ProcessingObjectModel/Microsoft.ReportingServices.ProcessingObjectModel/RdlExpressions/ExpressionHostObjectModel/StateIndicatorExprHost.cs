using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000092 RID: 146
	public abstract class StateIndicatorExprHost : GaugePanelItemExprHost
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00003512 File Offset: 0x00001712
		public virtual object IndicatorStyleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00003515 File Offset: 0x00001715
		public virtual object ScaleFactorExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00003518 File Offset: 0x00001718
		[CLSCompliant(false)]
		public IList<IndicatorStateExprHost> IndicatorStatesHostsRemotable
		{
			[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
			get
			{
				return this.m_indicatorStatesHostsRemotable;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00003520 File Offset: 0x00001720
		public virtual object ResizeModeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00003523 File Offset: 0x00001723
		public virtual object AngleExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00003526 File Offset: 0x00001726
		public virtual object TransformationTypeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000EF RID: 239
		public GaugeInputValueExprHost GaugeInputValueHost;

		// Token: 0x040000F0 RID: 240
		public IndicatorImageExprHost IndicatorImageHost;

		// Token: 0x040000F1 RID: 241
		[CLSCompliant(false)]
		protected IList<IndicatorStateExprHost> m_indicatorStatesHostsRemotable;

		// Token: 0x040000F2 RID: 242
		public GaugeInputValueExprHost MaximumValueHost;

		// Token: 0x040000F3 RID: 243
		public GaugeInputValueExprHost MinimumValueHost;
	}
}
