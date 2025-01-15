using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000433 RID: 1075
	[Serializable]
	internal sealed class MapSpatialDataSet : MapSpatialData, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002FDE RID: 12254 RVA: 0x000D840E File Offset: 0x000D660E
		internal MapSpatialDataSet()
		{
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000D8416 File Offset: 0x000D6616
		internal MapSpatialDataSet(MapVectorLayer mapVectorLayer, Map map)
			: base(mapVectorLayer, map)
		{
		}

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x06002FE0 RID: 12256 RVA: 0x000D8420 File Offset: 0x000D6620
		// (set) Token: 0x06002FE1 RID: 12257 RVA: 0x000D8428 File Offset: 0x000D6628
		internal ExpressionInfo DataSetName
		{
			get
			{
				return this.m_dataSetName;
			}
			set
			{
				this.m_dataSetName = value;
			}
		}

		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x06002FE2 RID: 12258 RVA: 0x000D8431 File Offset: 0x000D6631
		// (set) Token: 0x06002FE3 RID: 12259 RVA: 0x000D8439 File Offset: 0x000D6639
		internal ExpressionInfo SpatialField
		{
			get
			{
				return this.m_spatialField;
			}
			set
			{
				this.m_spatialField = value;
			}
		}

		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x06002FE4 RID: 12260 RVA: 0x000D8442 File Offset: 0x000D6642
		// (set) Token: 0x06002FE5 RID: 12261 RVA: 0x000D844A File Offset: 0x000D664A
		internal List<MapFieldName> MapFieldNames
		{
			get
			{
				return this.m_mapFieldNames;
			}
			set
			{
				this.m_mapFieldNames = value;
			}
		}

		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x000D8453 File Offset: 0x000D6653
		internal new MapSpatialDataSetExprHost ExprHost
		{
			get
			{
				return (MapSpatialDataSetExprHost)this.m_exprHost;
			}
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000D8460 File Offset: 0x000D6660
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapSpatialDataSetStart();
			base.Initialize(context);
			if (this.m_dataSetName != null)
			{
				this.m_dataSetName.Initialize("DataSetName", context);
				context.ExprHostBuilder.MapSpatialDataSetDataSetName(this.m_dataSetName);
			}
			if (this.m_spatialField != null)
			{
				this.m_spatialField.Initialize("SpatialField", context);
				context.ExprHostBuilder.MapSpatialDataSetSpatialField(this.m_spatialField);
			}
			if (this.m_mapFieldNames != null)
			{
				for (int i = 0; i < this.m_mapFieldNames.Count; i++)
				{
					this.m_mapFieldNames[i].Initialize(context, i);
				}
			}
			context.ExprHostBuilder.MapSpatialDataSetEnd();
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000D8514 File Offset: 0x000D6714
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapSpatialDataSet mapSpatialDataSet = (MapSpatialDataSet)base.PublishClone(context);
			if (this.m_dataSetName != null)
			{
				mapSpatialDataSet.m_dataSetName = (ExpressionInfo)this.m_dataSetName.PublishClone(context);
			}
			if (this.m_spatialField != null)
			{
				mapSpatialDataSet.m_spatialField = (ExpressionInfo)this.m_spatialField.PublishClone(context);
			}
			if (this.m_mapFieldNames != null)
			{
				mapSpatialDataSet.m_mapFieldNames = new List<MapFieldName>(this.m_mapFieldNames.Count);
				foreach (MapFieldName mapFieldName in this.m_mapFieldNames)
				{
					mapSpatialDataSet.m_mapFieldNames.Add((MapFieldName)mapFieldName.PublishClone(context));
				}
			}
			return mapSpatialDataSet;
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000D85E4 File Offset: 0x000D67E4
		internal override void SetExprHost(MapSpatialDataExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHostInternal(exprHost, reportObjectModel);
			IList<MapFieldNameExprHost> mapFieldNamesHostsRemotable = this.ExprHost.MapFieldNamesHostsRemotable;
			if (this.m_mapFieldNames != null && mapFieldNamesHostsRemotable != null)
			{
				for (int i = 0; i < this.m_mapFieldNames.Count; i++)
				{
					MapFieldName mapFieldName = this.m_mapFieldNames[i];
					if (mapFieldName != null && mapFieldName.ExpressionHostID > -1)
					{
						mapFieldName.SetExprHost(mapFieldNamesHostsRemotable[mapFieldName.ExpressionHostID], reportObjectModel);
					}
				}
			}
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000D866C File Offset: 0x000D686C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialDataSet, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialData, new List<MemberInfo>
			{
				new MemberInfo(MemberName.DataSetName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.SpatialField, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MapFieldNames, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapFieldName)
			});
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000D86D0 File Offset: 0x000D68D0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapSpatialDataSet.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.DataSetName)
				{
					if (memberName != MemberName.MapFieldNames)
					{
						if (memberName != MemberName.SpatialField)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_spatialField);
						}
					}
					else
					{
						writer.Write<MapFieldName>(this.m_mapFieldNames);
					}
				}
				else
				{
					writer.Write(this.m_dataSetName);
				}
			}
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000D875C File Offset: 0x000D695C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapSpatialDataSet.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.DataSetName)
				{
					if (memberName != MemberName.MapFieldNames)
					{
						if (memberName != MemberName.SpatialField)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_spatialField = (ExpressionInfo)reader.ReadRIFObject();
						}
					}
					else
					{
						this.m_mapFieldNames = reader.ReadGenericListOfRIFObjects<MapFieldName>();
					}
				}
				else
				{
					this.m_dataSetName = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000D87F0 File Offset: 0x000D69F0
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialDataSet;
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000D87F7 File Offset: 0x000D69F7
		internal string EvaluateDataSetName(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapSpatialDataSetDataSetNameExpression(this, this.m_map.Name);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000D881D File Offset: 0x000D6A1D
		internal string EvaluateSpatialField(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapSpatialDataSetSpatialFieldExpression(this, this.m_map.Name);
		}

		// Token: 0x040018D1 RID: 6353
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapSpatialDataSet.GetDeclaration();

		// Token: 0x040018D2 RID: 6354
		private ExpressionInfo m_dataSetName;

		// Token: 0x040018D3 RID: 6355
		private ExpressionInfo m_spatialField;

		// Token: 0x040018D4 RID: 6356
		private List<MapFieldName> m_mapFieldNames;
	}
}
