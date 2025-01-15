using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000434 RID: 1076
	[Serializable]
	internal sealed class MapPointLayer : MapVectorLayer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002FF1 RID: 12273 RVA: 0x000D884F File Offset: 0x000D6A4F
		internal MapPointLayer()
		{
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000D8857 File Offset: 0x000D6A57
		internal MapPointLayer(int ID, Map map)
			: base(ID, map)
		{
		}

		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x06002FF3 RID: 12275 RVA: 0x000D8861 File Offset: 0x000D6A61
		// (set) Token: 0x06002FF4 RID: 12276 RVA: 0x000D8869 File Offset: 0x000D6A69
		internal MapPointTemplate MapPointTemplate
		{
			get
			{
				return this.m_mapPointTemplate;
			}
			set
			{
				this.m_mapPointTemplate = value;
			}
		}

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x06002FF5 RID: 12277 RVA: 0x000D8872 File Offset: 0x000D6A72
		// (set) Token: 0x06002FF6 RID: 12278 RVA: 0x000D887A File Offset: 0x000D6A7A
		internal MapPointRules MapPointRules
		{
			get
			{
				return this.m_mapPointRules;
			}
			set
			{
				this.m_mapPointRules = value;
			}
		}

		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x06002FF7 RID: 12279 RVA: 0x000D8883 File Offset: 0x000D6A83
		// (set) Token: 0x06002FF8 RID: 12280 RVA: 0x000D888B File Offset: 0x000D6A8B
		internal List<MapPoint> MapPoints
		{
			get
			{
				return this.m_mapPoints;
			}
			set
			{
				this.m_mapPoints = value;
			}
		}

		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x06002FF9 RID: 12281 RVA: 0x000D8894 File Offset: 0x000D6A94
		protected override bool Embedded
		{
			get
			{
				return this.MapPoints != null;
			}
		}

		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x06002FFA RID: 12282 RVA: 0x000D889F File Offset: 0x000D6A9F
		internal new MapPointLayerExprHost ExprHost
		{
			get
			{
				return (MapPointLayerExprHost)this.m_exprHost;
			}
		}

		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x06002FFB RID: 12283 RVA: 0x000D88AC File Offset: 0x000D6AAC
		internal new MapPointLayerExprHost ExprHostMapMember
		{
			get
			{
				return (MapPointLayerExprHost)this.m_exprHostMapMember;
			}
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000D88BC File Offset: 0x000D6ABC
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapPointLayerStart(base.Name);
			base.Initialize(context);
			if (this.m_mapPointRules != null)
			{
				this.m_mapPointRules.Initialize(context);
			}
			if (base.MapDataRegionName == null)
			{
				if (this.m_mapPointTemplate != null)
				{
					this.m_mapPointTemplate.Initialize(context);
				}
				if (this.m_mapPoints != null)
				{
					for (int i = 0; i < this.m_mapPoints.Count; i++)
					{
						this.m_mapPoints[i].Initialize(context, i);
					}
				}
			}
			this.m_exprHostID = context.ExprHostBuilder.MapPointLayerEnd();
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000D8958 File Offset: 0x000D6B58
		internal override void InitializeMapMember(InitializationContext context)
		{
			context.ExprHostBuilder.MapPointLayerStart(base.Name);
			base.InitializeMapMember(context);
			if (this.m_mapPointRules != null)
			{
				this.m_mapPointRules.InitializeMapMember(context);
			}
			if (base.MapDataRegionName != null)
			{
				if (this.m_mapPointTemplate != null)
				{
					this.m_mapPointTemplate.Initialize(context);
				}
				if (this.m_mapPoints != null)
				{
					for (int i = 0; i < this.m_mapPoints.Count; i++)
					{
						this.m_mapPoints[i].Initialize(context, i);
					}
				}
			}
			this.m_exprHostMapMemberID = context.ExprHostBuilder.MapPointLayerEnd();
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x000D89F4 File Offset: 0x000D6BF4
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapPointLayer mapPointLayer = (MapPointLayer)base.PublishClone(context);
			context.CurrentMapVectorLayerClone = mapPointLayer;
			if (this.m_mapPointTemplate != null)
			{
				mapPointLayer.m_mapPointTemplate = (MapPointTemplate)this.m_mapPointTemplate.PublishClone(context);
			}
			if (this.m_mapPointRules != null)
			{
				mapPointLayer.m_mapPointRules = (MapPointRules)this.m_mapPointRules.PublishClone(context);
			}
			if (this.m_mapPoints != null)
			{
				mapPointLayer.m_mapPoints = new List<MapPoint>(this.m_mapPoints.Count);
				foreach (MapPoint mapPoint in this.m_mapPoints)
				{
					mapPointLayer.m_mapPoints.Add((MapPoint)mapPoint.PublishClone(context));
				}
			}
			return mapPointLayer;
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000D8ACC File Offset: 0x000D6CCC
		internal override void SetExprHost(MapLayerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_mapPointRules != null && this.ExprHost.MapPointRulesHost != null)
			{
				this.m_mapPointRules.SetExprHost(this.ExprHost.MapPointRulesHost, reportObjectModel);
			}
			if (base.MapDataRegionName == null)
			{
				if (this.m_mapPointTemplate != null && this.ExprHost.MapPointTemplateHost != null)
				{
					this.m_mapPointTemplate.SetExprHost(this.ExprHost.MapPointTemplateHost, reportObjectModel);
				}
				IList<MapPointExprHost> mapPointsHostsRemotable = this.ExprHost.MapPointsHostsRemotable;
				if (this.m_mapPoints != null && mapPointsHostsRemotable != null)
				{
					for (int i = 0; i < this.m_mapPoints.Count; i++)
					{
						MapPoint mapPoint = this.m_mapPoints[i];
						if (mapPoint != null && mapPoint.ExpressionHostID > -1)
						{
							mapPoint.SetExprHost(mapPointsHostsRemotable[mapPoint.ExpressionHostID], reportObjectModel);
						}
					}
				}
			}
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x000D8BB8 File Offset: 0x000D6DB8
		internal override void SetExprHostMapMember(MapVectorLayerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHostMapMember(exprHost, reportObjectModel);
			if (this.m_mapPointRules != null && this.ExprHostMapMember.MapPointRulesHost != null)
			{
				this.m_mapPointRules.SetExprHostMapMember(this.ExprHostMapMember.MapPointRulesHost, reportObjectModel);
			}
			if (base.MapDataRegionName != null)
			{
				if (this.m_mapPointTemplate != null && this.ExprHostMapMember.MapPointTemplateHost != null)
				{
					this.m_mapPointTemplate.SetExprHost(this.ExprHostMapMember.MapPointTemplateHost, reportObjectModel);
				}
				IList<MapPointExprHost> mapPointsHostsRemotable = this.ExprHostMapMember.MapPointsHostsRemotable;
				if (this.m_mapPoints != null && mapPointsHostsRemotable != null)
				{
					for (int i = 0; i < this.m_mapPoints.Count; i++)
					{
						MapPoint mapPoint = this.m_mapPoints[i];
						if (mapPoint != null && mapPoint.ExpressionHostID > -1)
						{
							mapPoint.SetExprHost(mapPointsHostsRemotable[mapPoint.ExpressionHostID], reportObjectModel);
						}
					}
				}
			}
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x000D8CA4 File Offset: 0x000D6EA4
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPointLayer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapVectorLayer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapPointTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPointTemplate),
				new MemberInfo(MemberName.MapPointRules, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPointRules),
				new MemberInfo(MemberName.MapPoints, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPoint)
			});
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000D8D08 File Offset: 0x000D6F08
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapPointLayer.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.MapPointRules)
				{
					if (memberName != MemberName.MapPointTemplate)
					{
						if (memberName != MemberName.MapPoints)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write<MapPoint>(this.m_mapPoints);
						}
					}
					else
					{
						writer.Write(this.m_mapPointTemplate);
					}
				}
				else
				{
					writer.Write(this.m_mapPointRules);
				}
			}
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x000D8D94 File Offset: 0x000D6F94
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapPointLayer.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.MapPointRules)
				{
					if (memberName != MemberName.MapPointTemplate)
					{
						if (memberName != MemberName.MapPoints)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_mapPoints = reader.ReadGenericListOfRIFObjects<MapPoint>();
						}
					}
					else
					{
						this.m_mapPointTemplate = (MapPointTemplate)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_mapPointRules = (MapPointRules)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000D8E28 File Offset: 0x000D7028
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPointLayer;
		}

		// Token: 0x040018D5 RID: 6357
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapPointLayer.GetDeclaration();

		// Token: 0x040018D6 RID: 6358
		private MapPointTemplate m_mapPointTemplate;

		// Token: 0x040018D7 RID: 6359
		private MapPointRules m_mapPointRules;

		// Token: 0x040018D8 RID: 6360
		private List<MapPoint> m_mapPoints;
	}
}
