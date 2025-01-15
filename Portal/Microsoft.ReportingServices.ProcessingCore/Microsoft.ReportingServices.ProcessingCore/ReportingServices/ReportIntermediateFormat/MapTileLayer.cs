using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000436 RID: 1078
	[Serializable]
	internal sealed class MapTileLayer : MapLayer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600301A RID: 12314 RVA: 0x000D91EB File Offset: 0x000D73EB
		internal MapTileLayer()
		{
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000D91F3 File Offset: 0x000D73F3
		internal MapTileLayer(Map map)
			: base(map)
		{
		}

		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x0600301C RID: 12316 RVA: 0x000D91FC File Offset: 0x000D73FC
		// (set) Token: 0x0600301D RID: 12317 RVA: 0x000D9204 File Offset: 0x000D7404
		internal ExpressionInfo ServiceUrl
		{
			get
			{
				return this.m_serviceUrl;
			}
			set
			{
				this.m_serviceUrl = value;
			}
		}

		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x0600301E RID: 12318 RVA: 0x000D920D File Offset: 0x000D740D
		// (set) Token: 0x0600301F RID: 12319 RVA: 0x000D9215 File Offset: 0x000D7415
		internal ExpressionInfo TileStyle
		{
			get
			{
				return this.m_tileStyle;
			}
			set
			{
				this.m_tileStyle = value;
			}
		}

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x06003020 RID: 12320 RVA: 0x000D921E File Offset: 0x000D741E
		// (set) Token: 0x06003021 RID: 12321 RVA: 0x000D9226 File Offset: 0x000D7426
		internal ExpressionInfo UseSecureConnection
		{
			get
			{
				return this.m_useSecureConnection;
			}
			set
			{
				this.m_useSecureConnection = value;
			}
		}

		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x06003022 RID: 12322 RVA: 0x000D922F File Offset: 0x000D742F
		// (set) Token: 0x06003023 RID: 12323 RVA: 0x000D9237 File Offset: 0x000D7437
		internal List<MapTile> MapTiles
		{
			get
			{
				return this.m_mapTiles;
			}
			set
			{
				this.m_mapTiles = value;
			}
		}

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x06003024 RID: 12324 RVA: 0x000D9240 File Offset: 0x000D7440
		internal new MapTileLayerExprHost ExprHost
		{
			get
			{
				return (MapTileLayerExprHost)this.m_exprHost;
			}
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000D9250 File Offset: 0x000D7450
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapTileLayerStart(base.Name);
			base.Initialize(context);
			if (this.m_serviceUrl != null)
			{
				this.m_serviceUrl.Initialize("ServiceUrl", context);
				context.ExprHostBuilder.MapTileLayerServiceUrl(this.m_serviceUrl);
			}
			if (this.m_tileStyle != null)
			{
				this.m_tileStyle.Initialize("TileStyle", context);
				context.ExprHostBuilder.MapTileLayerTileStyle(this.m_tileStyle);
			}
			if (this.m_useSecureConnection != null)
			{
				this.m_useSecureConnection.Initialize("UseSecureConnection", context);
				context.ExprHostBuilder.MapTileLayerUseSecureConnection(this.m_useSecureConnection);
			}
			if (this.m_mapTiles != null)
			{
				for (int i = 0; i < this.m_mapTiles.Count; i++)
				{
					this.m_mapTiles[i].Initialize(context, i);
				}
			}
			this.m_exprHostID = context.ExprHostBuilder.MapTileLayerEnd();
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000D933C File Offset: 0x000D753C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapTileLayer mapTileLayer = (MapTileLayer)base.PublishClone(context);
			if (this.m_serviceUrl != null)
			{
				mapTileLayer.m_serviceUrl = (ExpressionInfo)this.m_serviceUrl.PublishClone(context);
			}
			if (this.m_tileStyle != null)
			{
				mapTileLayer.m_tileStyle = (ExpressionInfo)this.m_tileStyle.PublishClone(context);
			}
			if (this.m_mapTiles != null)
			{
				mapTileLayer.m_mapTiles = new List<MapTile>(this.m_mapTiles.Count);
				foreach (MapTile mapTile in this.m_mapTiles)
				{
					mapTileLayer.m_mapTiles.Add((MapTile)mapTile.PublishClone(context));
				}
			}
			if (this.m_useSecureConnection != null)
			{
				mapTileLayer.m_useSecureConnection = (ExpressionInfo)this.m_useSecureConnection.PublishClone(context);
			}
			return mapTileLayer;
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000D9428 File Offset: 0x000D7628
		internal void SetExprHost(MapTileLayerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			IList<MapTileExprHost> mapTilesHostsRemotable = this.ExprHost.MapTilesHostsRemotable;
			if (this.m_mapTiles != null && mapTilesHostsRemotable != null)
			{
				for (int i = 0; i < this.m_mapTiles.Count; i++)
				{
					MapTile mapTile = this.m_mapTiles[i];
					if (mapTile != null && mapTile.ExpressionHostID > -1)
					{
						mapTile.SetExprHost(mapTilesHostsRemotable[mapTile.ExpressionHostID], reportObjectModel);
					}
				}
			}
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000D94B0 File Offset: 0x000D76B0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapTileLayer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLayer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ServiceUrl, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TileStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MapTiles, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapTile),
				new MemberInfo(MemberName.UseSecureConnection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000D9528 File Offset: 0x000D7728
		internal Stream GetTileData(string url, out string mimeType, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			ImageInfo imageInfo;
			if (renderingContext.OdpContext.OdpMetadata.TryGetExternalImage(url, out imageInfo))
			{
				return renderingContext.OdpContext.ChunkFactory.GetChunk(imageInfo.StreamName, ReportProcessing.ReportChunkTypes.Image, ChunkMode.Open, out mimeType);
			}
			mimeType = null;
			return null;
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000D9568 File Offset: 0x000D7768
		internal void SetTileData(string url, byte[] data, string mimeType, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			string text = Guid.NewGuid().ToString("N");
			ImageInfo imageInfo = new ImageInfo(text, "");
			renderingContext.OdpContext.OdpMetadata.AddExternalImage(url, imageInfo);
			using (Stream stream = renderingContext.OdpContext.ChunkFactory.CreateChunk(text, ReportProcessing.ReportChunkTypes.Image, mimeType))
			{
				stream.Write(data, 0, data.Length);
			}
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000D95E4 File Offset: 0x000D77E4
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapTileLayer.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.ServiceUrl:
					writer.Write(this.m_serviceUrl);
					break;
				case MemberName.TileStyle:
					writer.Write(this.m_tileStyle);
					break;
				case MemberName.MapTiles:
					writer.Write<MapTile>(this.m_mapTiles);
					break;
				default:
					if (memberName != MemberName.UseSecureConnection)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_useSecureConnection);
					}
					break;
				}
			}
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000D9688 File Offset: 0x000D7888
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapTileLayer.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.ServiceUrl:
					this.m_serviceUrl = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.TileStyle:
					this.m_tileStyle = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.MapTiles:
					this.m_mapTiles = reader.ReadGenericListOfRIFObjects<MapTile>();
					break;
				default:
					if (memberName != MemberName.UseSecureConnection)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_useSecureConnection = (ExpressionInfo)reader.ReadRIFObject();
					}
					break;
				}
			}
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x000D973E File Offset: 0x000D793E
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapTileLayer;
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x000D9745 File Offset: 0x000D7945
		internal string EvaluateServiceUrl(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapTileLayerServiceUrlExpression(this, this.m_map.Name);
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x000D976B File Offset: 0x000D796B
		internal MapTileStyle EvaluateTileStyle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateMapTileStyle(context.ReportRuntime.EvaluateMapTileLayerTileStyleExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x000D979C File Offset: 0x000D799C
		internal bool EvaluateUseSecureConnection(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapTileLayerUseSecureConnectionExpression(this, this.m_map.Name);
		}

		// Token: 0x040018E0 RID: 6368
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapTileLayer.GetDeclaration();

		// Token: 0x040018E1 RID: 6369
		private ExpressionInfo m_serviceUrl;

		// Token: 0x040018E2 RID: 6370
		private ExpressionInfo m_tileStyle;

		// Token: 0x040018E3 RID: 6371
		private ExpressionInfo m_useSecureConnection;

		// Token: 0x040018E4 RID: 6372
		private List<MapTile> m_mapTiles;
	}
}
