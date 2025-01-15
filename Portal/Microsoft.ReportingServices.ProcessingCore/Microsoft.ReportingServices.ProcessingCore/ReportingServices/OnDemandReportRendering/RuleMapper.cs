using System;
using System.Drawing;
using System.Globalization;
using Microsoft.Reporting.Map.WebForms;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000169 RID: 361
	internal class RuleMapper
	{
		// Token: 0x06000F01 RID: 3841 RVA: 0x00040D89 File Offset: 0x0003EF89
		internal RuleMapper(MapAppearanceRule mapRule, VectorLayerMapper vectorLayerMapper, CoreSpatialElementManager coreSpatialElementManager)
		{
			this.m_mapRule = mapRule;
			this.m_mapVectorLayer = vectorLayerMapper.m_mapVectorLayer;
			this.m_coreMap = vectorLayerMapper.m_coreMap;
			this.m_coreSpatialElementManager = coreSpatialElementManager;
			this.m_mapMapper = vectorLayerMapper.m_mapMapper;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00040DC3 File Offset: 0x0003EFC3
		internal bool HasDataValue(ISpatialElement element)
		{
			return this.m_mapRule.DataValue == null || (element[this.m_coreRule.Field] != null && this.IsValueInRange(this.m_coreRule.Field, element));
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00040DFC File Offset: 0x0003EFFC
		private bool IsValueInRange(string fieldName, ISpatialElement element)
		{
			Type type = ((Field)this.m_coreSpatialElementManager.FieldDefinitions.GetByName(fieldName)).Type;
			object startValue = this.GetStartValue(type);
			object endValue = this.GetEndValue(type);
			if (type == typeof(int))
			{
				if (startValue != null && (int)startValue > (int)element[fieldName])
				{
					return false;
				}
				if (endValue != null && (int)endValue < (int)element[fieldName])
				{
					return false;
				}
			}
			else if (type == typeof(double))
			{
				if (startValue != null && (double)startValue > (double)element[fieldName])
				{
					return false;
				}
				if (endValue != null && (double)endValue < (double)element[fieldName])
				{
					return false;
				}
			}
			else if (type == typeof(decimal))
			{
				if (startValue != null && (decimal)startValue > (decimal)element[fieldName])
				{
					return false;
				}
				if (endValue != null && (decimal)endValue < (decimal)element[fieldName])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00040F14 File Offset: 0x0003F114
		private object GetStartValue(Type fieldType)
		{
			if (!this.m_startValueEvaluated)
			{
				if (this.GetDistributionType() == MapRuleDistributionType.Custom)
				{
					MapBucketCollection mapBuckets = this.m_mapRule.MapBuckets;
					if (mapBuckets != null && mapBuckets.Count > 0)
					{
						ReportVariantProperty startValue = mapBuckets[0].StartValue;
						if (startValue != null)
						{
							if (!startValue.IsExpression)
							{
								this.m_startValue = startValue.Value;
							}
							this.m_startValue = mapBuckets[0].Instance.StartValue;
						}
					}
				}
				if (this.m_startValue == null)
				{
					ReportVariantProperty startValue2 = this.m_mapRule.StartValue;
					if (startValue2 != null)
					{
						if (!startValue2.IsExpression)
						{
							this.m_startValue = startValue2.Value;
						}
						this.m_startValue = this.m_mapRule.Instance.StartValue;
					}
				}
				if (this.m_startValue != null)
				{
					try
					{
						this.m_startValue = Convert.ChangeType(this.m_startValue, fieldType, CultureInfo.InvariantCulture);
					}
					catch (Exception ex)
					{
						if (AsynchronousExceptionDetection.IsStoppingException(ex))
						{
							throw;
						}
						this.m_startValue = null;
					}
				}
				this.m_startValueEvaluated = true;
			}
			return this.m_startValue;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0004101C File Offset: 0x0003F21C
		private object GetEndValue(Type fieldType)
		{
			if (!this.m_endValueEvaluated)
			{
				if (this.GetDistributionType() == MapRuleDistributionType.Custom)
				{
					MapBucketCollection mapBuckets = this.m_mapRule.MapBuckets;
					if (mapBuckets != null && mapBuckets.Count > 0)
					{
						ReportVariantProperty endValue = mapBuckets[mapBuckets.Count - 1].EndValue;
						if (endValue != null)
						{
							if (!endValue.IsExpression)
							{
								this.m_endValue = endValue.Value;
							}
							this.m_endValue = mapBuckets[mapBuckets.Count - 1].Instance.EndValue;
						}
					}
				}
				if (this.m_endValue == null)
				{
					ReportVariantProperty endValue2 = this.m_mapRule.EndValue;
					if (endValue2 != null)
					{
						if (!endValue2.IsExpression)
						{
							this.m_endValue = endValue2.Value;
						}
						this.m_endValue = this.m_mapRule.Instance.EndValue;
					}
				}
				if (this.m_endValue != null)
				{
					try
					{
						this.m_endValue = Convert.ChangeType(this.m_endValue, fieldType, CultureInfo.InvariantCulture);
					}
					catch (Exception ex)
					{
						if (AsynchronousExceptionDetection.IsStoppingException(ex))
						{
							throw;
						}
						this.m_endValue = null;
					}
				}
				this.m_endValueEvaluated = true;
			}
			return this.m_endValue;
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00041130 File Offset: 0x0003F330
		internal void SetRuleFieldValue(ISpatialElement spatialElement)
		{
			if (this.m_fieldNameBased != null && this.m_fieldNameBased.Value)
			{
				return;
			}
			object obj = this.EvaluateRuleDataValue();
			if (obj != null)
			{
				if (this.m_fieldNameBased == null && Type.GetTypeCode(obj.GetType()) == TypeCode.String && ((string)obj).StartsWith("#", StringComparison.Ordinal))
				{
					this.m_ruleFieldName = SpatialDataMapper.GetUniqueFieldName(this.m_mapVectorLayer.Name, ((string)obj).Remove(0, 1));
					this.m_coreRule.Field = this.m_ruleFieldName;
					this.m_fieldNameBased = new bool?(true);
					return;
				}
				if (this.m_ruleFieldName == null)
				{
					this.m_ruleFieldName = this.m_coreSpatialElementManager.AddRuleField(obj);
					this.m_fieldNameBased = new bool?(false);
					this.m_coreRule.Field = this.m_ruleFieldName;
				}
				this.m_coreSpatialElementManager.AddFieldValue(spatialElement, this.m_ruleFieldName, obj);
			}
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00041220 File Offset: 0x0003F420
		protected void SetRuleFieldName()
		{
			if (this.m_mapRule.DataValue == null)
			{
				this.m_ruleFieldName = RuleMapper.m_distinctBucketFieldName;
				this.m_coreRule.Field = this.m_ruleFieldName;
				this.m_fieldNameBased = new bool?(true);
				return;
			}
			if (this.m_mapVectorLayer.MapDataRegion == null)
			{
				object obj = this.EvaluateRuleDataValue();
				if (obj is string)
				{
					string text = (string)obj;
					if (text.StartsWith("#", StringComparison.Ordinal))
					{
						this.m_ruleFieldName = SpatialDataMapper.GetUniqueFieldName(this.m_mapVectorLayer.Name, text.Remove(0, 1));
						this.m_coreRule.Field = this.m_ruleFieldName;
						this.m_fieldNameBased = new bool?(true);
					}
				}
			}
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x000412CF File Offset: 0x0003F4CF
		protected void SetRuleLegendProperties(RuleBase coreRule)
		{
			this.SetLegendText(coreRule);
			if (this.m_mapRule.LegendName != null)
			{
				coreRule.ShowInLegend = this.m_mapRule.LegendName;
			}
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x000412F8 File Offset: 0x0003F4F8
		protected void SetLegendText(RuleBase coreRule)
		{
			ReportStringProperty legendText = this.m_mapRule.LegendText;
			if (legendText == null)
			{
				coreRule.LegendText = "";
				return;
			}
			if (!legendText.IsExpression)
			{
				coreRule.LegendText = legendText.Value;
				return;
			}
			coreRule.LegendText = this.m_mapRule.Instance.LegendText;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0004134C File Offset: 0x0003F54C
		protected void SetRuleDistribution(RuleBase coreRule)
		{
			MapRuleDistributionType distributionType = this.GetDistributionType();
			if (distributionType != MapRuleDistributionType.Custom)
			{
				coreRule.DataGrouping = this.GetDataGrouping(distributionType);
				coreRule.FromValue = this.GetFromValue();
				coreRule.ToValue = this.GetToValue();
				return;
			}
			coreRule.DataGrouping = 0;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00041394 File Offset: 0x0003F594
		protected MapRuleDistributionType GetDistributionType()
		{
			ReportEnumProperty<MapRuleDistributionType> distributionType = this.m_mapRule.DistributionType;
			if (distributionType == null)
			{
				return MapRuleDistributionType.Optimal;
			}
			if (!distributionType.IsExpression)
			{
				return distributionType.Value;
			}
			return this.m_mapRule.Instance.DistributionType;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x000413D1 File Offset: 0x0003F5D1
		protected DataGrouping GetDataGrouping(MapRuleDistributionType distributionType)
		{
			if (distributionType == MapRuleDistributionType.EqualInterval)
			{
				return 0;
			}
			if (distributionType == MapRuleDistributionType.EqualDistribution)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x000413E0 File Offset: 0x0003F5E0
		protected string GetFromValue()
		{
			ReportVariantProperty startValue = this.m_mapRule.StartValue;
			if (startValue == null)
			{
				return "";
			}
			if (!startValue.IsExpression)
			{
				return this.ConvertBucketValueToString(startValue.Value);
			}
			return this.ConvertBucketValueToString(this.m_mapRule.Instance.StartValue);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00041430 File Offset: 0x0003F630
		protected string GetToValue()
		{
			ReportVariantProperty endValue = this.m_mapRule.EndValue;
			if (endValue == null)
			{
				return "";
			}
			if (!endValue.IsExpression)
			{
				return this.ConvertBucketValueToString(endValue.Value);
			}
			return this.ConvertBucketValueToString(this.m_mapRule.Instance.EndValue);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00041480 File Offset: 0x0003F680
		protected string GetFromValue(MapBucket bucket)
		{
			ReportVariantProperty startValue = bucket.StartValue;
			if (startValue == null)
			{
				return "";
			}
			if (!startValue.IsExpression)
			{
				return this.ConvertBucketValueToString(startValue.Value);
			}
			return this.ConvertBucketValueToString(bucket.Instance.StartValue);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x000414C4 File Offset: 0x0003F6C4
		protected string GetToValue(MapBucket bucket)
		{
			ReportVariantProperty endValue = bucket.EndValue;
			if (endValue == null)
			{
				return "";
			}
			if (!endValue.IsExpression)
			{
				return this.ConvertBucketValueToString(endValue.Value);
			}
			return this.ConvertBucketValueToString(bucket.Instance.EndValue);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00041508 File Offset: 0x0003F708
		internal ShapeRule CreatePolygonRule()
		{
			ShapeRule shapeRule = new ShapeRule();
			this.m_coreRule = shapeRule;
			shapeRule.BorderColor = Color.Empty;
			shapeRule.Text = "";
			shapeRule.Category = this.m_mapVectorLayer.Name;
			shapeRule.Field = "";
			this.m_coreMap.ShapeRules.Add(shapeRule);
			this.SetRuleFieldName();
			return shapeRule;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00041570 File Offset: 0x0003F770
		internal virtual SymbolRule CreateSymbolRule()
		{
			SymbolRule symbolRule = new SymbolRule();
			this.m_coreRule = symbolRule;
			symbolRule.Category = this.m_mapVectorLayer.Name;
			symbolRule.Field = "";
			this.m_coreMap.SymbolRules.Add(symbolRule);
			this.SetRuleFieldName();
			return symbolRule;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x000415C0 File Offset: 0x0003F7C0
		protected void InitializePredefinedSymbols(PredefinedSymbol predefinedSymbol, PointTemplateMapper symbolTemplateMapper)
		{
			predefinedSymbol.BorderColor = symbolTemplateMapper.GetBorderColor(false);
			predefinedSymbol.BorderStyle = symbolTemplateMapper.GetBorderStyle(false);
			predefinedSymbol.BorderWidth = symbolTemplateMapper.GetBorderWidth(false);
			predefinedSymbol.Font = symbolTemplateMapper.GetFont(false);
			predefinedSymbol.GradientType = symbolTemplateMapper.GetGradientType(false);
			predefinedSymbol.HatchStyle = symbolTemplateMapper.GetHatchStyle(false);
			predefinedSymbol.SecondaryColor = symbolTemplateMapper.GetBackGradientEndColor(false);
			predefinedSymbol.ShadowOffset = symbolTemplateMapper.GetShadowOffset(false);
			predefinedSymbol.TextColor = symbolTemplateMapper.GetTextColor(false);
			predefinedSymbol.LegendText = "";
			predefinedSymbol.Text = "";
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00041658 File Offset: 0x0003F858
		protected int GetBucketCount()
		{
			if (!this.IsRuleFieldDefined)
			{
				return this.m_coreSpatialElementManager.GetSpatialElementCount();
			}
			MapRuleDistributionType distributionType = this.GetDistributionType();
			ReportIntProperty bucketCount = this.m_mapRule.BucketCount;
			int num = RuleMapper.m_defaultBucketCount;
			if (bucketCount != null)
			{
				if (!bucketCount.IsExpression)
				{
					num = bucketCount.Value;
				}
				else
				{
					num = this.m_mapRule.Instance.BucketCount;
				}
			}
			if (!this.IsRuleFieldScalar)
			{
				return this.m_coreSpatialElementManager.GetDistinctValuesCount(this.m_coreRule.Field);
			}
			if (distributionType == MapRuleDistributionType.Optimal || distributionType == MapRuleDistributionType.EqualDistribution)
			{
				return Math.Min(num, this.m_coreSpatialElementManager.GetDistinctValuesCount(this.m_coreRule.Field));
			}
			if (distributionType != MapRuleDistributionType.Custom)
			{
				return num;
			}
			MapBucketCollection mapBuckets = this.m_mapRule.MapBuckets;
			if (mapBuckets == null)
			{
				throw new RenderingObjectModelException(RPResWrapper.rsMapLayerMissingProperty(RPRes.rsObjectTypeMap, this.m_mapRule.MapDef.Name, this.m_mapVectorLayer.Name, "MapBuckets"));
			}
			return mapBuckets.Count;
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x00041743 File Offset: 0x0003F943
		private bool IsRuleFieldDefined
		{
			get
			{
				return this.m_mapRule.DataValue != null;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00041754 File Offset: 0x0003F954
		private bool IsRuleFieldScalar
		{
			get
			{
				return !(this.m_coreRule.Field != "") || this.m_coreSpatialElementManager.FieldDefinitions[this.m_coreRule.Field].Type != typeof(string);
			}
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x000417A9 File Offset: 0x0003F9A9
		private string ConvertBucketValueToString(object value)
		{
			if (value == null)
			{
				return "";
			}
			if (value is IFormattable)
			{
				return ((IFormattable)value).ToString("", CultureInfo.CurrentCulture);
			}
			return value.ToString();
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x000417D8 File Offset: 0x0003F9D8
		private object EvaluateRuleDataValue()
		{
			ReportVariantProperty dataValue = this.m_mapRule.DataValue;
			object obj = null;
			if (dataValue != null)
			{
				if (!dataValue.IsExpression)
				{
					obj = dataValue.Value;
				}
				else
				{
					obj = this.m_mapRule.Instance.DataValue;
				}
			}
			return obj;
		}

		// Token: 0x0400071A RID: 1818
		protected RuleBase m_coreRule;

		// Token: 0x0400071B RID: 1819
		protected MapAppearanceRule m_mapRule;

		// Token: 0x0400071C RID: 1820
		protected MapControl m_coreMap;

		// Token: 0x0400071D RID: 1821
		protected MapVectorLayer m_mapVectorLayer;

		// Token: 0x0400071E RID: 1822
		protected MapMapper m_mapMapper;

		// Token: 0x0400071F RID: 1823
		private string m_ruleFieldName;

		// Token: 0x04000720 RID: 1824
		private bool? m_fieldNameBased;

		// Token: 0x04000721 RID: 1825
		private CoreSpatialElementManager m_coreSpatialElementManager;

		// Token: 0x04000722 RID: 1826
		private object m_startValue;

		// Token: 0x04000723 RID: 1827
		private bool m_startValueEvaluated;

		// Token: 0x04000724 RID: 1828
		private object m_endValue;

		// Token: 0x04000725 RID: 1829
		private bool m_endValueEvaluated;

		// Token: 0x04000726 RID: 1830
		private static string m_distinctBucketFieldName = "(Name)";

		// Token: 0x04000727 RID: 1831
		private static int m_defaultBucketCount = 5;
	}
}
