using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000CB RID: 203
	public abstract class MapAppearanceRuleExprHost : ReportObjectModelProxy
	{
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00003B37 File Offset: 0x00001D37
		public virtual object DataValueExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00003B3A File Offset: 0x00001D3A
		public virtual object DistributionTypeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00003B3D File Offset: 0x00001D3D
		public virtual object BucketCountExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00003B40 File Offset: 0x00001D40
		public virtual object StartValueExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00003B43 File Offset: 0x00001D43
		public virtual object EndValueExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00003B46 File Offset: 0x00001D46
		internal IList<MapBucketExprHost> MapBucketsHostsRemotable
		{
			get
			{
				return this.m_mapBucketsHostsRemotable;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00003B4E File Offset: 0x00001D4E
		public virtual object LegendTextExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000147 RID: 327
		[CLSCompliant(false)]
		protected IList<MapBucketExprHost> m_mapBucketsHostsRemotable;
	}
}
