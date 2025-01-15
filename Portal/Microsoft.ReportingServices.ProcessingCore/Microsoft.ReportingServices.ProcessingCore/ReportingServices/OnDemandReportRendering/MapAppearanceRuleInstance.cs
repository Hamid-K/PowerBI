using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001CD RID: 461
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapAppearanceRuleInstance : BaseInstance
	{
		// Token: 0x060011ED RID: 4589 RVA: 0x0004A013 File Offset: 0x00048213
		internal MapAppearanceRuleInstance(MapAppearanceRule defObject)
			: base(defObject.MapDef.ReportScope)
		{
			this.m_defObject = defObject;
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x0004A030 File Offset: 0x00048230
		public object DataValue
		{
			get
			{
				if (this.m_dataValue == null)
				{
					this.m_dataValue = this.m_defObject.MapAppearanceRuleDef.EvaluateDataValue(this.m_defObject.ReportScope.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext).Value;
				}
				return this.m_dataValue;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x0004A08C File Offset: 0x0004828C
		public MapRuleDistributionType DistributionType
		{
			get
			{
				if (this.m_distributionType == null)
				{
					this.m_distributionType = new MapRuleDistributionType?(this.m_defObject.MapAppearanceRuleDef.EvaluateDistributionType(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_distributionType.Value;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0004A0E8 File Offset: 0x000482E8
		public int BucketCount
		{
			get
			{
				if (this.m_bucketCount == null)
				{
					this.m_bucketCount = new int?(this.m_defObject.MapAppearanceRuleDef.EvaluateBucketCount(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext));
				}
				return this.m_bucketCount.Value;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x0004A144 File Offset: 0x00048344
		public object StartValue
		{
			get
			{
				if (this.m_startValue == null)
				{
					this.m_startValue = this.m_defObject.MapAppearanceRuleDef.EvaluateStartValue(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext).Value;
				}
				return this.m_startValue;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0004A198 File Offset: 0x00048398
		public object EndValue
		{
			get
			{
				if (this.m_endValue == null)
				{
					this.m_endValue = this.m_defObject.MapAppearanceRuleDef.EvaluateEndValue(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext).Value;
				}
				return this.m_endValue;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x0004A1EC File Offset: 0x000483EC
		public string LegendText
		{
			get
			{
				if (!this.m_legendTextEvaluated)
				{
					this.m_legendText = this.m_defObject.MapAppearanceRuleDef.EvaluateLegendText(this.ReportScopeInstance, this.m_defObject.MapDef.RenderingContext.OdpContext);
					this.m_legendTextEvaluated = true;
				}
				return this.m_legendText;
			}
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0004A23F File Offset: 0x0004843F
		protected override void ResetInstanceCache()
		{
			this.m_dataValue = null;
			this.m_distributionType = null;
			this.m_bucketCount = null;
			this.m_startValue = null;
			this.m_endValue = null;
			this.m_legendText = null;
			this.m_legendTextEvaluated = false;
		}

		// Token: 0x0400087D RID: 2173
		private MapAppearanceRule m_defObject;

		// Token: 0x0400087E RID: 2174
		private object m_dataValue;

		// Token: 0x0400087F RID: 2175
		private MapRuleDistributionType? m_distributionType;

		// Token: 0x04000880 RID: 2176
		private int? m_bucketCount;

		// Token: 0x04000881 RID: 2177
		private object m_startValue;

		// Token: 0x04000882 RID: 2178
		private object m_endValue;

		// Token: 0x04000883 RID: 2179
		private string m_legendText;

		// Token: 0x04000884 RID: 2180
		private bool m_legendTextEvaluated;
	}
}
