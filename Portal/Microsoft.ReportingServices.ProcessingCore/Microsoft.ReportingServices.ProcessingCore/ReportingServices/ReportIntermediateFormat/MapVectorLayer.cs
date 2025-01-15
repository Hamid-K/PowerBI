using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000437 RID: 1079
	[Serializable]
	internal abstract class MapVectorLayer : MapLayer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable
	{
		// Token: 0x06003032 RID: 12338 RVA: 0x000D97CE File Offset: 0x000D79CE
		internal MapVectorLayer()
		{
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x000D97DD File Offset: 0x000D79DD
		internal MapVectorLayer(int ID, Map map)
			: base(map)
		{
			this.m_ID = ID;
		}

		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x06003034 RID: 12340 RVA: 0x000D97F4 File Offset: 0x000D79F4
		// (set) Token: 0x06003035 RID: 12341 RVA: 0x000D9810 File Offset: 0x000D7A10
		internal string DataElementName
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_dataElementName))
				{
					return base.Name;
				}
				return this.m_dataElementName;
			}
			set
			{
				this.m_dataElementName = value;
			}
		}

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x06003036 RID: 12342 RVA: 0x000D9819 File Offset: 0x000D7A19
		// (set) Token: 0x06003037 RID: 12343 RVA: 0x000D9821 File Offset: 0x000D7A21
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

		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x06003038 RID: 12344 RVA: 0x000D982A File Offset: 0x000D7A2A
		public int ID
		{
			get
			{
				return this.m_ID;
			}
		}

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x06003039 RID: 12345 RVA: 0x000D9832 File Offset: 0x000D7A32
		// (set) Token: 0x0600303A RID: 12346 RVA: 0x000D983A File Offset: 0x000D7A3A
		internal string MapDataRegionName
		{
			get
			{
				return this.m_mapDataRegionName;
			}
			set
			{
				this.m_mapDataRegionName = value;
			}
		}

		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x0600303B RID: 12347 RVA: 0x000D9843 File Offset: 0x000D7A43
		// (set) Token: 0x0600303C RID: 12348 RVA: 0x000D984B File Offset: 0x000D7A4B
		internal List<MapBindingFieldPair> MapBindingFieldPairs
		{
			get
			{
				return this.m_mapBindingFieldPairs;
			}
			set
			{
				this.m_mapBindingFieldPairs = value;
			}
		}

		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x0600303D RID: 12349 RVA: 0x000D9854 File Offset: 0x000D7A54
		// (set) Token: 0x0600303E RID: 12350 RVA: 0x000D985C File Offset: 0x000D7A5C
		internal List<MapFieldDefinition> MapFieldDefinitions
		{
			get
			{
				return this.m_mapFieldDefinitions;
			}
			set
			{
				this.m_mapFieldDefinitions = value;
			}
		}

		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x000D9865 File Offset: 0x000D7A65
		// (set) Token: 0x06003040 RID: 12352 RVA: 0x000D986D File Offset: 0x000D7A6D
		internal MapSpatialData MapSpatialData
		{
			get
			{
				return this.m_mapSpatialData;
			}
			set
			{
				this.m_mapSpatialData = value;
			}
		}

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x000D9876 File Offset: 0x000D7A76
		internal new MapVectorLayerExprHost ExprHost
		{
			get
			{
				return (MapVectorLayerExprHost)this.m_exprHost;
			}
		}

		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x000D9883 File Offset: 0x000D7A83
		internal MapVectorLayerExprHost ExprHostMapMember
		{
			get
			{
				return this.m_exprHostMapMember;
			}
		}

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x06003043 RID: 12355 RVA: 0x000D988B File Offset: 0x000D7A8B
		// (set) Token: 0x06003044 RID: 12356 RVA: 0x000D9893 File Offset: 0x000D7A93
		internal int ExpressionHostMapMemberID
		{
			get
			{
				return this.m_exprHostMapMemberID;
			}
			set
			{
				this.m_exprHostMapMemberID = value;
			}
		}

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x06003045 RID: 12357 RVA: 0x000D989C File Offset: 0x000D7A9C
		internal IInstancePath InstancePath
		{
			get
			{
				if (this.m_instancePath == null)
				{
					if (this.m_mapDataRegionName != null)
					{
						foreach (MapDataRegion mapDataRegion in this.m_map.MapDataRegions)
						{
							if (string.CompareOrdinal(this.m_mapDataRegionName, mapDataRegion.Name) == 0)
							{
								this.m_instancePath = mapDataRegion.InnerMostMapMember;
							}
						}
					}
					if (this.m_instancePath == null)
					{
						this.m_instancePath = this.m_map;
					}
				}
				return this.m_instancePath;
			}
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000D9938 File Offset: 0x000D7B38
		internal void Validate(PublishingErrorContext errorContext)
		{
			if (this.MapSpatialData is MapSpatialDataRegion && this.MapDataRegionName == null)
			{
				errorContext.Register(ProcessingErrorCode.rsMapLayerMissingProperty, Severity.Error, this.m_map.ObjectType, this.m_map.Name, this.m_name, new string[] { "MapDataRegionName" });
			}
			if (!(this.MapSpatialData is MapSpatialDataRegion) && this.MapDataRegionName != null && this.MapBindingFieldPairs == null)
			{
				errorContext.Register(ProcessingErrorCode.rsMapLayerMissingProperty, Severity.Error, this.m_map.ObjectType, this.m_map.Name, this.m_name, new string[] { "MapBindingFieldPairs" });
			}
		}

		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x06003047 RID: 12359
		protected abstract bool Embedded { get; }

		// Token: 0x06003048 RID: 12360 RVA: 0x000D99E8 File Offset: 0x000D7BE8
		internal override void Initialize(InitializationContext context)
		{
			base.Initialize(context);
			if (this.m_mapSpatialData != null)
			{
				this.m_mapSpatialData.Initialize(context);
			}
			if (this.m_mapBindingFieldPairs != null)
			{
				for (int i = 0; i < this.m_mapBindingFieldPairs.Count; i++)
				{
					this.m_mapBindingFieldPairs[i].Initialize(context, i);
				}
			}
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000D9A44 File Offset: 0x000D7C44
		internal virtual void InitializeMapMember(InitializationContext context)
		{
			if (this.m_mapSpatialData != null)
			{
				this.m_mapSpatialData.InitializeMapMember(context);
			}
			if (this.m_mapBindingFieldPairs != null)
			{
				for (int i = 0; i < this.m_mapBindingFieldPairs.Count; i++)
				{
					this.m_mapBindingFieldPairs[i].InitializeMapMember(context, i);
				}
			}
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x000D9A98 File Offset: 0x000D7C98
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapVectorLayer mapVectorLayer = (MapVectorLayer)base.PublishClone(context);
			context.CurrentMapVectorLayerClone = mapVectorLayer;
			mapVectorLayer.m_ID = context.GenerateID();
			if (this.MapDataRegionName != null)
			{
				mapVectorLayer.MapDataRegionName = context.GetNewScopeName(this.MapDataRegionName);
			}
			if (this.m_mapBindingFieldPairs != null)
			{
				mapVectorLayer.m_mapBindingFieldPairs = new List<MapBindingFieldPair>(this.m_mapBindingFieldPairs.Count);
				foreach (MapBindingFieldPair mapBindingFieldPair in this.m_mapBindingFieldPairs)
				{
					mapVectorLayer.m_mapBindingFieldPairs.Add((MapBindingFieldPair)mapBindingFieldPair.PublishClone(context));
				}
			}
			if (this.m_mapFieldDefinitions != null)
			{
				mapVectorLayer.m_mapFieldDefinitions = new List<MapFieldDefinition>(this.m_mapFieldDefinitions.Count);
				foreach (MapFieldDefinition mapFieldDefinition in this.m_mapFieldDefinitions)
				{
					mapVectorLayer.m_mapFieldDefinitions.Add((MapFieldDefinition)mapFieldDefinition.PublishClone(context));
				}
			}
			if (this.m_mapSpatialData != null)
			{
				mapVectorLayer.m_mapSpatialData = (MapSpatialData)this.m_mapSpatialData.PublishClone(context);
			}
			return mapVectorLayer;
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000D9BE8 File Offset: 0x000D7DE8
		internal override void SetExprHost(MapLayerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			IList<MapBindingFieldPairExprHost> mapBindingFieldPairsHostsRemotable = this.ExprHost.MapBindingFieldPairsHostsRemotable;
			if (this.m_mapBindingFieldPairs != null && mapBindingFieldPairsHostsRemotable != null)
			{
				for (int i = 0; i < this.m_mapBindingFieldPairs.Count; i++)
				{
					MapBindingFieldPair mapBindingFieldPair = this.m_mapBindingFieldPairs[i];
					if (mapBindingFieldPair != null && mapBindingFieldPair.ExpressionHostID > -1)
					{
						mapBindingFieldPair.SetExprHost(mapBindingFieldPairsHostsRemotable[mapBindingFieldPair.ExpressionHostID], reportObjectModel);
					}
				}
			}
			if (this.m_mapSpatialData != null && this.ExprHost.MapSpatialDataHost != null)
			{
				this.m_mapSpatialData.SetExprHost(this.ExprHost.MapSpatialDataHost, reportObjectModel);
			}
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000D9C9C File Offset: 0x000D7E9C
		internal virtual void SetExprHostMapMember(MapVectorLayerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHostMapMember = exprHost;
			this.m_exprHostMapMember.SetReportObjectModel(reportObjectModel);
			IList<MapBindingFieldPairExprHost> mapBindingFieldPairsHostsRemotable = this.ExprHostMapMember.MapBindingFieldPairsHostsRemotable;
			if (this.m_mapBindingFieldPairs != null && mapBindingFieldPairsHostsRemotable != null)
			{
				for (int i = 0; i < this.m_mapBindingFieldPairs.Count; i++)
				{
					MapBindingFieldPair mapBindingFieldPair = this.m_mapBindingFieldPairs[i];
					if (mapBindingFieldPair != null && mapBindingFieldPair.ExpressionHostMapMemberID > -1)
					{
						mapBindingFieldPair.SetExprHostMapMember(mapBindingFieldPairsHostsRemotable[mapBindingFieldPair.ExpressionHostMapMemberID], reportObjectModel);
					}
				}
			}
			if (this.m_mapSpatialData != null && this.ExprHostMapMember.MapSpatialDataHost != null)
			{
				this.m_mapSpatialData.SetExprHostMapMember(this.ExprHostMapMember.MapSpatialDataHost, reportObjectModel);
			}
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000D9D5C File Offset: 0x000D7F5C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapVectorLayer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLayer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapDataRegionName, Token.String),
				new MemberInfo(MemberName.MapBindingFieldPairs, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapBindingFieldPair),
				new MemberInfo(MemberName.MapFieldDefinitions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapFieldDefinition),
				new MemberInfo(MemberName.MapSpatialData, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialData),
				new MemberInfo(MemberName.ExprHostMapMemberID, Token.Int32),
				new MemberInfo(MemberName.ID, Token.Int32),
				new MemberInfo(MemberName.DataElementName, Token.String),
				new MemberInfo(MemberName.DataElementOutput, Token.Enum)
			});
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000D9E24 File Offset: 0x000D8024
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapVectorLayer.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.DataElementOutput)
				{
					if (memberName == MemberName.ID)
					{
						writer.Write(this.m_ID);
						continue;
					}
					if (memberName == MemberName.DataElementName)
					{
						writer.Write(this.m_dataElementName);
						continue;
					}
					if (memberName == MemberName.DataElementOutput)
					{
						writer.WriteEnum((int)this.m_dataElementOutput);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.MapBindingFieldPairs)
					{
						writer.Write<MapBindingFieldPair>(this.m_mapBindingFieldPairs);
						continue;
					}
					switch (memberName)
					{
					case MemberName.MapDataRegionName:
						writer.Write(this.m_mapDataRegionName);
						continue;
					case MemberName.MapFieldDefinitions:
						writer.Write<MapFieldDefinition>(this.m_mapFieldDefinitions);
						continue;
					case MemberName.MapSpatialData:
						writer.Write(this.m_mapSpatialData);
						continue;
					default:
						if (memberName == MemberName.ExprHostMapMemberID)
						{
							writer.Write(this.m_exprHostMapMemberID);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x000D9F38 File Offset: 0x000D8138
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapVectorLayer.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.DataElementOutput)
				{
					if (memberName == MemberName.ID)
					{
						this.m_ID = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.DataElementName)
					{
						this.m_dataElementName = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.DataElementOutput)
					{
						this.m_dataElementOutput = (DataElementOutputTypes)reader.ReadEnum();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.MapBindingFieldPairs)
					{
						this.m_mapBindingFieldPairs = reader.ReadGenericListOfRIFObjects<MapBindingFieldPair>();
						continue;
					}
					switch (memberName)
					{
					case MemberName.MapDataRegionName:
						this.m_mapDataRegionName = reader.ReadString();
						continue;
					case MemberName.MapFieldDefinitions:
						this.m_mapFieldDefinitions = reader.ReadGenericListOfRIFObjects<MapFieldDefinition>();
						continue;
					case MemberName.MapSpatialData:
						this.m_mapSpatialData = (MapSpatialData)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.ExprHostMapMemberID)
						{
							this.m_exprHostMapMemberID = reader.ReadInt32();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x000DA051 File Offset: 0x000D8251
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapVectorLayer;
		}

		// Token: 0x040018E5 RID: 6373
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapVectorLayer.GetDeclaration();

		// Token: 0x040018E6 RID: 6374
		private string m_mapDataRegionName;

		// Token: 0x040018E7 RID: 6375
		private int m_ID;

		// Token: 0x040018E8 RID: 6376
		private List<MapBindingFieldPair> m_mapBindingFieldPairs;

		// Token: 0x040018E9 RID: 6377
		private List<MapFieldDefinition> m_mapFieldDefinitions;

		// Token: 0x040018EA RID: 6378
		private MapSpatialData m_mapSpatialData;

		// Token: 0x040018EB RID: 6379
		private string m_dataElementName;

		// Token: 0x040018EC RID: 6380
		private DataElementOutputTypes m_dataElementOutput;

		// Token: 0x040018ED RID: 6381
		protected int m_exprHostMapMemberID = -1;

		// Token: 0x040018EE RID: 6382
		[NonSerialized]
		protected MapVectorLayerExprHost m_exprHostMapMember;

		// Token: 0x040018EF RID: 6383
		[NonSerialized]
		private IInstancePath m_instancePath;
	}
}
