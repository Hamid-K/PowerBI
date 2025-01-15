using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000438 RID: 1080
	[Serializable]
	internal class MapAppearanceRule : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003052 RID: 12370 RVA: 0x000DA064 File Offset: 0x000D8264
		internal MapAppearanceRule()
		{
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000DA06C File Offset: 0x000D826C
		internal MapAppearanceRule(MapVectorLayer mapVectorLayer, Map map)
		{
			this.m_map = map;
			this.m_mapVectorLayer = mapVectorLayer;
		}

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x000DA082 File Offset: 0x000D8282
		// (set) Token: 0x06003055 RID: 12373 RVA: 0x000DA08A File Offset: 0x000D828A
		internal string DataElementName
		{
			get
			{
				return this.m_dataElementName;
			}
			set
			{
				this.m_dataElementName = value;
			}
		}

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x06003056 RID: 12374 RVA: 0x000DA093 File Offset: 0x000D8293
		// (set) Token: 0x06003057 RID: 12375 RVA: 0x000DA09B File Offset: 0x000D829B
		internal DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_dataElementOutput;
			}
			set
			{
				this.m_dataElementOutput = value;
			}
		}

		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x06003058 RID: 12376 RVA: 0x000DA0A4 File Offset: 0x000D82A4
		// (set) Token: 0x06003059 RID: 12377 RVA: 0x000DA0AC File Offset: 0x000D82AC
		internal ExpressionInfo DataValue
		{
			get
			{
				return this.m_dataValue;
			}
			set
			{
				this.m_dataValue = value;
			}
		}

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x0600305A RID: 12378 RVA: 0x000DA0B5 File Offset: 0x000D82B5
		// (set) Token: 0x0600305B RID: 12379 RVA: 0x000DA0BD File Offset: 0x000D82BD
		internal ExpressionInfo DistributionType
		{
			get
			{
				return this.m_distributionType;
			}
			set
			{
				this.m_distributionType = value;
			}
		}

		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x0600305C RID: 12380 RVA: 0x000DA0C6 File Offset: 0x000D82C6
		// (set) Token: 0x0600305D RID: 12381 RVA: 0x000DA0CE File Offset: 0x000D82CE
		internal ExpressionInfo BucketCount
		{
			get
			{
				return this.m_bucketCount;
			}
			set
			{
				this.m_bucketCount = value;
			}
		}

		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x0600305E RID: 12382 RVA: 0x000DA0D7 File Offset: 0x000D82D7
		// (set) Token: 0x0600305F RID: 12383 RVA: 0x000DA0DF File Offset: 0x000D82DF
		internal ExpressionInfo StartValue
		{
			get
			{
				return this.m_startValue;
			}
			set
			{
				this.m_startValue = value;
			}
		}

		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x06003060 RID: 12384 RVA: 0x000DA0E8 File Offset: 0x000D82E8
		// (set) Token: 0x06003061 RID: 12385 RVA: 0x000DA0F0 File Offset: 0x000D82F0
		internal ExpressionInfo EndValue
		{
			get
			{
				return this.m_endValue;
			}
			set
			{
				this.m_endValue = value;
			}
		}

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x06003062 RID: 12386 RVA: 0x000DA0F9 File Offset: 0x000D82F9
		// (set) Token: 0x06003063 RID: 12387 RVA: 0x000DA101 File Offset: 0x000D8301
		internal List<MapBucket> MapBuckets
		{
			get
			{
				return this.m_mapBuckets;
			}
			set
			{
				this.m_mapBuckets = value;
			}
		}

		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x06003064 RID: 12388 RVA: 0x000DA10A File Offset: 0x000D830A
		// (set) Token: 0x06003065 RID: 12389 RVA: 0x000DA112 File Offset: 0x000D8312
		internal string LegendName
		{
			get
			{
				return this.m_legendName;
			}
			set
			{
				this.m_legendName = value;
			}
		}

		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x000DA11B File Offset: 0x000D831B
		// (set) Token: 0x06003067 RID: 12391 RVA: 0x000DA123 File Offset: 0x000D8323
		internal ExpressionInfo LegendText
		{
			get
			{
				return this.m_legendText;
			}
			set
			{
				this.m_legendText = value;
			}
		}

		// Token: 0x17001691 RID: 5777
		// (get) Token: 0x06003068 RID: 12392 RVA: 0x000DA12C File Offset: 0x000D832C
		internal string OwnerName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x17001692 RID: 5778
		// (get) Token: 0x06003069 RID: 12393 RVA: 0x000DA139 File Offset: 0x000D8339
		internal MapAppearanceRuleExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x0600306A RID: 12394 RVA: 0x000DA141 File Offset: 0x000D8341
		internal MapAppearanceRuleExprHost ExprHostMapMember
		{
			get
			{
				return this.m_exprHostMapMember;
			}
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000DA14C File Offset: 0x000D834C
		internal virtual void Initialize(InitializationContext context)
		{
			if (this.m_distributionType != null)
			{
				this.m_distributionType.Initialize("DistributionType", context);
				context.ExprHostBuilder.MapAppearanceRuleDistributionType(this.m_distributionType);
			}
			if (this.m_bucketCount != null)
			{
				this.m_bucketCount.Initialize("BucketCount", context);
				context.ExprHostBuilder.MapAppearanceRuleBucketCount(this.m_bucketCount);
			}
			if (this.m_startValue != null)
			{
				this.m_startValue.Initialize("StartValue", context);
				context.ExprHostBuilder.MapAppearanceRuleStartValue(this.m_startValue);
			}
			if (this.m_endValue != null)
			{
				this.m_endValue.Initialize("EndValue", context);
				context.ExprHostBuilder.MapAppearanceRuleEndValue(this.m_endValue);
			}
			if (this.m_mapBuckets != null)
			{
				for (int i = 0; i < this.m_mapBuckets.Count; i++)
				{
					this.m_mapBuckets[i].Initialize(context, i);
				}
			}
			if (this.m_legendText != null)
			{
				this.m_legendText.Initialize("LegendText", context);
				context.ExprHostBuilder.MapAppearanceRuleLegendText(this.m_legendText);
			}
			if (this.m_mapVectorLayer.MapDataRegionName == null && this.m_dataValue != null)
			{
				this.m_dataValue.Initialize("DataValue", context);
				context.ExprHostBuilder.MapAppearanceRuleDataValue(this.m_dataValue);
			}
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000DA299 File Offset: 0x000D8499
		internal virtual void InitializeMapMember(InitializationContext context)
		{
			if (this.m_mapVectorLayer.MapDataRegionName != null && this.m_dataValue != null)
			{
				this.m_dataValue.Initialize("DataValue", context);
				context.ExprHostBuilder.MapAppearanceRuleDataValue(this.m_dataValue);
			}
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000DA2D4 File Offset: 0x000D84D4
		internal virtual object PublishClone(AutomaticSubtotalContext context)
		{
			MapAppearanceRule mapAppearanceRule = (MapAppearanceRule)base.MemberwiseClone();
			mapAppearanceRule.m_map = context.CurrentMapClone;
			mapAppearanceRule.m_mapVectorLayer = context.CurrentMapVectorLayerClone;
			if (this.m_dataValue != null)
			{
				mapAppearanceRule.m_dataValue = (ExpressionInfo)this.m_dataValue.PublishClone(context);
			}
			if (this.m_distributionType != null)
			{
				mapAppearanceRule.m_distributionType = (ExpressionInfo)this.m_distributionType.PublishClone(context);
			}
			if (this.m_bucketCount != null)
			{
				mapAppearanceRule.m_bucketCount = (ExpressionInfo)this.m_bucketCount.PublishClone(context);
			}
			if (this.m_startValue != null)
			{
				mapAppearanceRule.m_startValue = (ExpressionInfo)this.m_startValue.PublishClone(context);
			}
			if (this.m_endValue != null)
			{
				mapAppearanceRule.m_endValue = (ExpressionInfo)this.m_endValue.PublishClone(context);
			}
			if (this.m_mapBuckets != null)
			{
				mapAppearanceRule.m_mapBuckets = new List<MapBucket>(this.m_mapBuckets.Count);
				foreach (MapBucket mapBucket in this.m_mapBuckets)
				{
					mapAppearanceRule.m_mapBuckets.Add((MapBucket)mapBucket.PublishClone(context));
				}
			}
			if (this.m_legendText != null)
			{
				mapAppearanceRule.m_legendText = (ExpressionInfo)this.m_legendText.PublishClone(context);
			}
			return mapAppearanceRule;
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000DA438 File Offset: 0x000D8638
		internal virtual void SetExprHost(MapAppearanceRuleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			IList<MapBucketExprHost> mapBucketsHostsRemotable = this.ExprHost.MapBucketsHostsRemotable;
			if (this.m_mapBuckets != null && mapBucketsHostsRemotable != null)
			{
				for (int i = 0; i < this.m_mapBuckets.Count; i++)
				{
					MapBucket mapBucket = this.m_mapBuckets[i];
					if (mapBucket != null && mapBucket.ExpressionHostID > -1)
					{
						mapBucket.SetExprHost(mapBucketsHostsRemotable[mapBucket.ExpressionHostID], reportObjectModel);
					}
				}
			}
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000DA4CA File Offset: 0x000D86CA
		internal virtual void SetExprHostMapMember(MapAppearanceRuleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHostMapMember = exprHost;
			this.m_exprHostMapMember.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000DA4F8 File Offset: 0x000D86F8
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapAppearanceRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.DataValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DistributionType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.BucketCount, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.StartValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.EndValue, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MapBuckets, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapBucket),
				new MemberInfo(MemberName.LegendName, Token.String),
				new MemberInfo(MemberName.LegendText, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Map, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Map, Token.Reference),
				new MemberInfo(MemberName.MapVectorLayer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapVectorLayer, Token.Reference),
				new MemberInfo(MemberName.DataElementName, Token.String),
				new MemberInfo(MemberName.DataElementOutput, Token.Enum)
			});
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000DA614 File Offset: 0x000D8814
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(MapAppearanceRule.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.StartValue)
				{
					if (memberName <= MemberName.DataElementName)
					{
						if (memberName == MemberName.LegendText)
						{
							writer.Write(this.m_legendText);
							continue;
						}
						if (memberName == MemberName.DataElementName)
						{
							writer.Write(this.m_dataElementName);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.DataElementOutput)
						{
							writer.WriteEnum((int)this.m_dataElementOutput);
							continue;
						}
						if (memberName == MemberName.LegendName)
						{
							writer.Write(this.m_legendName);
							continue;
						}
						if (memberName == MemberName.StartValue)
						{
							writer.Write(this.m_startValue);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.Map)
				{
					if (memberName == MemberName.EndValue)
					{
						writer.Write(this.m_endValue);
						continue;
					}
					if (memberName == MemberName.Map)
					{
						writer.WriteReference(this.m_map);
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.DistributionType:
						writer.Write(this.m_distributionType);
						continue;
					case MemberName.BucketCount:
						writer.Write(this.m_bucketCount);
						continue;
					case MemberName.MapBuckets:
						writer.Write<MapBucket>(this.m_mapBuckets);
						continue;
					default:
						if (memberName == MemberName.DataValue)
						{
							writer.Write(this.m_dataValue);
							continue;
						}
						if (memberName == MemberName.MapVectorLayer)
						{
							writer.WriteReference(this.m_mapVectorLayer);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000DA7B4 File Offset: 0x000D89B4
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(MapAppearanceRule.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.StartValue)
				{
					if (memberName <= MemberName.DataElementName)
					{
						if (memberName == MemberName.LegendText)
						{
							this.m_legendText = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.DataElementName)
						{
							this.m_dataElementName = reader.ReadString();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.DataElementOutput)
						{
							this.m_dataElementOutput = (DataElementOutputTypes)reader.ReadEnum();
							continue;
						}
						if (memberName == MemberName.LegendName)
						{
							this.m_legendName = reader.ReadString();
							continue;
						}
						if (memberName == MemberName.StartValue)
						{
							this.m_startValue = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.Map)
				{
					if (memberName == MemberName.EndValue)
					{
						this.m_endValue = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Map)
					{
						this.m_map = reader.ReadReference<Map>(this);
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.DistributionType:
						this.m_distributionType = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.BucketCount:
						this.m_bucketCount = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.MapBuckets:
						this.m_mapBuckets = reader.ReadGenericListOfRIFObjects<MapBucket>();
						continue;
					default:
						if (memberName == MemberName.DataValue)
						{
							this.m_dataValue = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.MapVectorLayer)
						{
							this.m_mapVectorLayer = reader.ReadReference<MapVectorLayer>(this);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000DA978 File Offset: 0x000D8B78
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(MapAppearanceRule.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName != MemberName.Map)
					{
						if (memberName != MemberName.MapVectorLayer)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
							this.m_mapVectorLayer = (MapVectorLayer)referenceableItems[memberReference.RefID];
						}
					}
					else
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_map = (Map)referenceableItems[memberReference.RefID];
					}
				}
			}
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000DAA60 File Offset: 0x000D8C60
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapAppearanceRule;
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x000DAA67 File Offset: 0x000D8C67
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateDataValue(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_mapVectorLayer.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapAppearanceRuleDataValueExpression(this, this.m_map.Name);
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000DAA92 File Offset: 0x000D8C92
		internal MapRuleDistributionType EvaluateDistributionType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateMapRuleDistributionType(context.ReportRuntime.EvaluateMapAppearanceRuleDistributionTypeExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000DAAC3 File Offset: 0x000D8CC3
		internal int EvaluateBucketCount(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapAppearanceRuleBucketCountExpression(this, this.m_map.Name);
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000DAAE9 File Offset: 0x000D8CE9
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateStartValue(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapAppearanceRuleStartValueExpression(this, this.m_map.Name);
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000DAB0F File Offset: 0x000D8D0F
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateEndValue(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapAppearanceRuleEndValueExpression(this, this.m_map.Name);
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x000DAB38 File Offset: 0x000D8D38
		internal string EvaluateLegendText(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			Microsoft.ReportingServices.RdlExpressions.VariantResult variantResult = context.ReportRuntime.EvaluateMapAppearanceRuleLegendTextExpression(this, this.m_map.Name);
			return this.m_map.GetFormattedStringFromValue(ref variantResult, context);
		}

		// Token: 0x040018F0 RID: 6384
		[NonSerialized]
		protected MapAppearanceRuleExprHost m_exprHost;

		// Token: 0x040018F1 RID: 6385
		[NonSerialized]
		protected MapAppearanceRuleExprHost m_exprHostMapMember;

		// Token: 0x040018F2 RID: 6386
		[Reference]
		protected Map m_map;

		// Token: 0x040018F3 RID: 6387
		[Reference]
		protected MapVectorLayer m_mapVectorLayer;

		// Token: 0x040018F4 RID: 6388
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapAppearanceRule.GetDeclaration();

		// Token: 0x040018F5 RID: 6389
		private ExpressionInfo m_dataValue;

		// Token: 0x040018F6 RID: 6390
		private ExpressionInfo m_distributionType;

		// Token: 0x040018F7 RID: 6391
		private ExpressionInfo m_bucketCount;

		// Token: 0x040018F8 RID: 6392
		private ExpressionInfo m_startValue;

		// Token: 0x040018F9 RID: 6393
		private ExpressionInfo m_endValue;

		// Token: 0x040018FA RID: 6394
		private List<MapBucket> m_mapBuckets;

		// Token: 0x040018FB RID: 6395
		private string m_legendName;

		// Token: 0x040018FC RID: 6396
		private ExpressionInfo m_legendText;

		// Token: 0x040018FD RID: 6397
		private string m_dataElementName;

		// Token: 0x040018FE RID: 6398
		private DataElementOutputTypes m_dataElementOutput;
	}
}
