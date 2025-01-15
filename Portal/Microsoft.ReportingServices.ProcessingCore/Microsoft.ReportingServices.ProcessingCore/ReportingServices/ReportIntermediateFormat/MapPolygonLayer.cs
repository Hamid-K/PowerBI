using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000430 RID: 1072
	[Serializable]
	internal sealed class MapPolygonLayer : MapVectorLayer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002FA6 RID: 12198 RVA: 0x000D7764 File Offset: 0x000D5964
		internal MapPolygonLayer()
		{
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000D776C File Offset: 0x000D596C
		internal MapPolygonLayer(int ID, Map map)
			: base(ID, map)
		{
		}

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x06002FA8 RID: 12200 RVA: 0x000D7776 File Offset: 0x000D5976
		// (set) Token: 0x06002FA9 RID: 12201 RVA: 0x000D777E File Offset: 0x000D597E
		internal MapPolygonTemplate MapPolygonTemplate
		{
			get
			{
				return this.m_mapPolygonTemplate;
			}
			set
			{
				this.m_mapPolygonTemplate = value;
			}
		}

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x06002FAA RID: 12202 RVA: 0x000D7787 File Offset: 0x000D5987
		// (set) Token: 0x06002FAB RID: 12203 RVA: 0x000D778F File Offset: 0x000D598F
		internal MapPolygonRules MapPolygonRules
		{
			get
			{
				return this.m_mapPolygonRules;
			}
			set
			{
				this.m_mapPolygonRules = value;
			}
		}

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x06002FAC RID: 12204 RVA: 0x000D7798 File Offset: 0x000D5998
		// (set) Token: 0x06002FAD RID: 12205 RVA: 0x000D77A0 File Offset: 0x000D59A0
		internal MapPointTemplate MapCenterPointTemplate
		{
			get
			{
				return this.m_mapCenterPointTemplate;
			}
			set
			{
				this.m_mapCenterPointTemplate = value;
			}
		}

		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x000D77A9 File Offset: 0x000D59A9
		// (set) Token: 0x06002FAF RID: 12207 RVA: 0x000D77B1 File Offset: 0x000D59B1
		internal MapPointRules MapCenterPointRules
		{
			get
			{
				return this.m_mapCenterPointRules;
			}
			set
			{
				this.m_mapCenterPointRules = value;
			}
		}

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x000D77BA File Offset: 0x000D59BA
		// (set) Token: 0x06002FB1 RID: 12209 RVA: 0x000D77C2 File Offset: 0x000D59C2
		internal List<MapPolygon> MapPolygons
		{
			get
			{
				return this.m_mapPolygons;
			}
			set
			{
				this.m_mapPolygons = value;
			}
		}

		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x000D77CB File Offset: 0x000D59CB
		protected override bool Embedded
		{
			get
			{
				return this.MapPolygons != null;
			}
		}

		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x000D77D6 File Offset: 0x000D59D6
		internal new MapPolygonLayerExprHost ExprHost
		{
			get
			{
				return (MapPolygonLayerExprHost)this.m_exprHost;
			}
		}

		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x000D77E3 File Offset: 0x000D59E3
		internal new MapPolygonLayerExprHost ExprHostMapMember
		{
			get
			{
				return (MapPolygonLayerExprHost)this.m_exprHostMapMember;
			}
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000D77F0 File Offset: 0x000D59F0
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapPolygonLayerStart(base.Name);
			base.Initialize(context);
			if (this.m_mapPolygonRules != null)
			{
				this.m_mapPolygonRules.Initialize(context);
			}
			if (this.m_mapCenterPointRules != null)
			{
				this.m_mapCenterPointRules.Initialize(context);
			}
			if (base.MapDataRegionName == null)
			{
				if (this.m_mapPolygonTemplate != null)
				{
					this.m_mapPolygonTemplate.Initialize(context);
				}
				if (this.m_mapCenterPointTemplate != null)
				{
					this.m_mapCenterPointTemplate.Initialize(context);
				}
				if (this.m_mapPolygons != null)
				{
					for (int i = 0; i < this.m_mapPolygons.Count; i++)
					{
						this.m_mapPolygons[i].Initialize(context, i);
					}
				}
			}
			this.m_exprHostID = context.ExprHostBuilder.MapPolygonLayerEnd();
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000D78B4 File Offset: 0x000D5AB4
		internal override void InitializeMapMember(InitializationContext context)
		{
			context.ExprHostBuilder.MapPolygonLayerStart(base.Name);
			base.InitializeMapMember(context);
			if (this.m_mapPolygonRules != null)
			{
				this.m_mapPolygonRules.InitializeMapMember(context);
			}
			if (this.m_mapCenterPointRules != null)
			{
				this.m_mapCenterPointRules.InitializeMapMember(context);
			}
			if (base.MapDataRegionName != null)
			{
				if (this.m_mapPolygonTemplate != null)
				{
					this.m_mapPolygonTemplate.Initialize(context);
				}
				if (this.m_mapCenterPointTemplate != null)
				{
					this.m_mapCenterPointTemplate.Initialize(context);
				}
				if (this.m_mapPolygons != null)
				{
					for (int i = 0; i < this.m_mapPolygons.Count; i++)
					{
						this.m_mapPolygons[i].Initialize(context, i);
					}
				}
			}
			this.m_exprHostMapMemberID = context.ExprHostBuilder.MapPolygonLayerEnd();
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000D7978 File Offset: 0x000D5B78
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapPolygonLayer mapPolygonLayer = (MapPolygonLayer)base.PublishClone(context);
			context.CurrentMapVectorLayerClone = mapPolygonLayer;
			if (this.m_mapPolygonTemplate != null)
			{
				mapPolygonLayer.m_mapPolygonTemplate = (MapPolygonTemplate)this.m_mapPolygonTemplate.PublishClone(context);
			}
			if (this.m_mapPolygonRules != null)
			{
				mapPolygonLayer.m_mapPolygonRules = (MapPolygonRules)this.m_mapPolygonRules.PublishClone(context);
			}
			if (this.m_mapCenterPointTemplate != null)
			{
				mapPolygonLayer.m_mapCenterPointTemplate = (MapPointTemplate)this.m_mapCenterPointTemplate.PublishClone(context);
			}
			if (this.m_mapCenterPointRules != null)
			{
				mapPolygonLayer.m_mapCenterPointRules = (MapPointRules)this.m_mapCenterPointRules.PublishClone(context);
			}
			if (this.m_mapPolygons != null)
			{
				mapPolygonLayer.m_mapPolygons = new List<MapPolygon>(this.m_mapPolygons.Count);
				foreach (MapPolygon mapPolygon in this.m_mapPolygons)
				{
					mapPolygonLayer.m_mapPolygons.Add((MapPolygon)mapPolygon.PublishClone(context));
				}
			}
			return mapPolygonLayer;
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000D7A8C File Offset: 0x000D5C8C
		internal override void SetExprHost(MapLayerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_mapPolygonRules != null && this.ExprHost.MapPolygonRulesHost != null)
			{
				this.m_mapPolygonRules.SetExprHost(this.ExprHost.MapPolygonRulesHost, reportObjectModel);
			}
			if (this.m_mapCenterPointRules != null && this.ExprHost.MapPointRulesHost != null)
			{
				this.m_mapCenterPointRules.SetExprHost(this.ExprHost.MapPointRulesHost, reportObjectModel);
			}
			if (base.MapDataRegionName == null)
			{
				if (this.m_mapPolygonTemplate != null && this.ExprHost.MapPolygonTemplateHost != null)
				{
					this.m_mapPolygonTemplate.SetExprHost(this.ExprHost.MapPolygonTemplateHost, reportObjectModel);
				}
				if (this.m_mapCenterPointTemplate != null && this.ExprHost.MapPointTemplateHost != null)
				{
					this.m_mapCenterPointTemplate.SetExprHost(this.ExprHost.MapPointTemplateHost, reportObjectModel);
				}
				IList<MapPolygonExprHost> mapPolygonsHostsRemotable = this.ExprHost.MapPolygonsHostsRemotable;
				if (this.m_mapPolygons != null && mapPolygonsHostsRemotable != null)
				{
					for (int i = 0; i < this.m_mapPolygons.Count; i++)
					{
						MapPolygon mapPolygon = this.m_mapPolygons[i];
						if (mapPolygon != null && mapPolygon.ExpressionHostID > -1)
						{
							mapPolygon.SetExprHost(mapPolygonsHostsRemotable[mapPolygon.ExpressionHostID], reportObjectModel);
						}
					}
				}
			}
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000D7BD0 File Offset: 0x000D5DD0
		internal override void SetExprHostMapMember(MapVectorLayerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHostMapMember(exprHost, reportObjectModel);
			if (this.m_mapPolygonRules != null && this.ExprHostMapMember.MapPolygonRulesHost != null)
			{
				this.m_mapPolygonRules.SetExprHostMapMember(this.ExprHostMapMember.MapPolygonRulesHost, reportObjectModel);
			}
			if (this.m_mapCenterPointRules != null && this.ExprHostMapMember.MapPointRulesHost != null)
			{
				this.m_mapCenterPointRules.SetExprHostMapMember(this.ExprHostMapMember.MapPointRulesHost, reportObjectModel);
			}
			if (base.MapDataRegionName != null)
			{
				if (this.m_mapPolygonTemplate != null && this.ExprHostMapMember.MapPolygonTemplateHost != null)
				{
					this.m_mapPolygonTemplate.SetExprHost(this.ExprHostMapMember.MapPolygonTemplateHost, reportObjectModel);
				}
				if (this.m_mapCenterPointTemplate != null && this.ExprHostMapMember.MapPointTemplateHost != null)
				{
					this.m_mapCenterPointTemplate.SetExprHost(this.ExprHostMapMember.MapPointTemplateHost, reportObjectModel);
				}
				IList<MapPolygonExprHost> mapPolygonsHostsRemotable = this.ExprHostMapMember.MapPolygonsHostsRemotable;
				if (this.m_mapPolygons != null && mapPolygonsHostsRemotable != null)
				{
					for (int i = 0; i < this.m_mapPolygons.Count; i++)
					{
						MapPolygon mapPolygon = this.m_mapPolygons[i];
						if (mapPolygon != null && mapPolygon.ExpressionHostID > -1)
						{
							mapPolygon.SetExprHost(mapPolygonsHostsRemotable[mapPolygon.ExpressionHostID], reportObjectModel);
						}
					}
				}
			}
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000D7D14 File Offset: 0x000D5F14
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPolygonLayer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapVectorLayer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapPolygonTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPolygonTemplate),
				new MemberInfo(MemberName.MapPolygonRules, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPolygonRules),
				new MemberInfo(MemberName.MapPointTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPointTemplate),
				new MemberInfo(MemberName.MapPointRules, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPointRules),
				new MemberInfo(MemberName.MapPolygons, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPolygon)
			});
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x000D7DA4 File Offset: 0x000D5FA4
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapPolygonLayer.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.MapPolygonTemplate)
				{
					if (memberName == MemberName.MapPointRules)
					{
						writer.Write(this.m_mapCenterPointRules);
						continue;
					}
					if (memberName == MemberName.MapPolygonTemplate)
					{
						writer.Write(this.m_mapPolygonTemplate);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.MapPointTemplate)
					{
						writer.Write(this.m_mapCenterPointTemplate);
						continue;
					}
					if (memberName == MemberName.MapPolygons)
					{
						writer.Write<MapPolygon>(this.m_mapPolygons);
						continue;
					}
					if (memberName == MemberName.MapPolygonRules)
					{
						writer.Write(this.m_mapPolygonRules);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000D7E6C File Offset: 0x000D606C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapPolygonLayer.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.MapPolygonTemplate)
				{
					if (memberName == MemberName.MapPointRules)
					{
						this.m_mapCenterPointRules = (MapPointRules)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.MapPolygonTemplate)
					{
						this.m_mapPolygonTemplate = (MapPolygonTemplate)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.MapPointTemplate)
					{
						this.m_mapCenterPointTemplate = (MapPointTemplate)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.MapPolygons)
					{
						this.m_mapPolygons = reader.ReadGenericListOfRIFObjects<MapPolygon>();
						continue;
					}
					if (memberName == MemberName.MapPolygonRules)
					{
						this.m_mapPolygonRules = (MapPolygonRules)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000D7F48 File Offset: 0x000D6148
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPolygonLayer;
		}

		// Token: 0x040018C5 RID: 6341
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapPolygonLayer.GetDeclaration();

		// Token: 0x040018C6 RID: 6342
		private MapPolygonTemplate m_mapPolygonTemplate;

		// Token: 0x040018C7 RID: 6343
		private MapPolygonRules m_mapPolygonRules;

		// Token: 0x040018C8 RID: 6344
		private MapPointTemplate m_mapCenterPointTemplate;

		// Token: 0x040018C9 RID: 6345
		private MapPointRules m_mapCenterPointRules;

		// Token: 0x040018CA RID: 6346
		private List<MapPolygon> m_mapPolygons;
	}
}
