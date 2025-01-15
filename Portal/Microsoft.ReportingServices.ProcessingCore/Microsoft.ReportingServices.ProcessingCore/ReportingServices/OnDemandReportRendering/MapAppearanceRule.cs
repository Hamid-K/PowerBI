using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001BF RID: 447
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class MapAppearanceRule
	{
		// Token: 0x06001176 RID: 4470 RVA: 0x00048CA4 File Offset: 0x00046EA4
		internal MapAppearanceRule(MapAppearanceRule defObject, MapVectorLayer mapVectorLayer, Map map)
		{
			this.m_defObject = defObject;
			this.m_mapVectorLayer = mapVectorLayer;
			this.m_map = map;
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x00048CC1 File Offset: 0x00046EC1
		public string DataElementName
		{
			get
			{
				return this.m_defObject.DataElementName;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x00048CCE File Offset: 0x00046ECE
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_defObject.DataElementOutput;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x00048CDB File Offset: 0x00046EDB
		public ReportVariantProperty DataValue
		{
			get
			{
				if (this.m_dataValue == null && this.m_defObject.DataValue != null)
				{
					this.m_dataValue = new ReportVariantProperty(this.m_defObject.DataValue);
				}
				return this.m_dataValue;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x00048D10 File Offset: 0x00046F10
		public ReportEnumProperty<MapRuleDistributionType> DistributionType
		{
			get
			{
				if (this.m_distributionType == null && this.m_defObject.DistributionType != null)
				{
					this.m_distributionType = new ReportEnumProperty<MapRuleDistributionType>(this.m_defObject.DistributionType.IsExpression, this.m_defObject.DistributionType.OriginalText, EnumTranslator.TranslateMapRuleDistributionType(this.m_defObject.DistributionType.StringValue, null));
				}
				return this.m_distributionType;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x00048D7C File Offset: 0x00046F7C
		public ReportIntProperty BucketCount
		{
			get
			{
				if (this.m_bucketCount == null && this.m_defObject.BucketCount != null)
				{
					this.m_bucketCount = new ReportIntProperty(this.m_defObject.BucketCount.IsExpression, this.m_defObject.BucketCount.OriginalText, this.m_defObject.BucketCount.IntValue, 0);
				}
				return this.m_bucketCount;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x00048DE0 File Offset: 0x00046FE0
		public ReportVariantProperty StartValue
		{
			get
			{
				if (this.m_startValue == null && this.m_defObject.StartValue != null)
				{
					this.m_startValue = new ReportVariantProperty(this.m_defObject.StartValue);
				}
				return this.m_startValue;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00048E13 File Offset: 0x00047013
		public ReportVariantProperty EndValue
		{
			get
			{
				if (this.m_endValue == null && this.m_defObject.EndValue != null)
				{
					this.m_endValue = new ReportVariantProperty(this.m_defObject.EndValue);
				}
				return this.m_endValue;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x00048E46 File Offset: 0x00047046
		public MapBucketCollection MapBuckets
		{
			get
			{
				if (this.m_mapBuckets == null && this.m_defObject.MapBuckets != null)
				{
					this.m_mapBuckets = new MapBucketCollection(this, this.m_map);
				}
				return this.m_mapBuckets;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x00048E75 File Offset: 0x00047075
		public string LegendName
		{
			get
			{
				return this.m_defObject.LegendName;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x00048E82 File Offset: 0x00047082
		public ReportStringProperty LegendText
		{
			get
			{
				if (this.m_legendText == null && this.m_defObject.LegendText != null)
				{
					this.m_legendText = new ReportStringProperty(this.m_defObject.LegendText);
				}
				return this.m_legendText;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x00048EB5 File Offset: 0x000470B5
		internal IReportScope ReportScope
		{
			get
			{
				return this.m_mapVectorLayer.ReportScope;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x00048EC2 File Offset: 0x000470C2
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00048ECA File Offset: 0x000470CA
		internal MapAppearanceRule MapAppearanceRuleDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x00048ED2 File Offset: 0x000470D2
		public MapAppearanceRuleInstance Instance
		{
			get
			{
				return this.GetInstance();
			}
		}

		// Token: 0x06001185 RID: 4485
		internal abstract MapAppearanceRuleInstance GetInstance();

		// Token: 0x06001186 RID: 4486 RVA: 0x00048EDA File Offset: 0x000470DA
		internal virtual void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_mapBuckets != null)
			{
				this.m_mapBuckets.SetNewContext();
			}
		}

		// Token: 0x04000844 RID: 2116
		protected Map m_map;

		// Token: 0x04000845 RID: 2117
		protected MapVectorLayer m_mapVectorLayer;

		// Token: 0x04000846 RID: 2118
		private MapAppearanceRule m_defObject;

		// Token: 0x04000847 RID: 2119
		protected MapAppearanceRuleInstance m_instance;

		// Token: 0x04000848 RID: 2120
		private ReportVariantProperty m_dataValue;

		// Token: 0x04000849 RID: 2121
		private ReportEnumProperty<MapRuleDistributionType> m_distributionType;

		// Token: 0x0400084A RID: 2122
		private ReportIntProperty m_bucketCount;

		// Token: 0x0400084B RID: 2123
		private ReportVariantProperty m_startValue;

		// Token: 0x0400084C RID: 2124
		private ReportVariantProperty m_endValue;

		// Token: 0x0400084D RID: 2125
		private MapBucketCollection m_mapBuckets;

		// Token: 0x0400084E RID: 2126
		private ReportStringProperty m_legendText;
	}
}
