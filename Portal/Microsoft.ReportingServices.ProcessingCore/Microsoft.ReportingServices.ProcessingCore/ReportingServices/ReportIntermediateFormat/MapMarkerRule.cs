using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000442 RID: 1090
	[Serializable]
	internal sealed class MapMarkerRule : MapAppearanceRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600312A RID: 12586 RVA: 0x000DCE30 File Offset: 0x000DB030
		internal MapMarkerRule()
		{
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x000DCE38 File Offset: 0x000DB038
		internal MapMarkerRule(MapVectorLayer mapVectorLayer, Map map)
			: base(mapVectorLayer, map)
		{
		}

		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x000DCE42 File Offset: 0x000DB042
		// (set) Token: 0x0600312D RID: 12589 RVA: 0x000DCE4A File Offset: 0x000DB04A
		internal List<MapMarker> MapMarkers
		{
			get
			{
				return this.m_mapMarkers;
			}
			set
			{
				this.m_mapMarkers = value;
			}
		}

		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x000DCE53 File Offset: 0x000DB053
		internal new MapMarkerRuleExprHost ExprHost
		{
			get
			{
				return (MapMarkerRuleExprHost)this.m_exprHost;
			}
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x000DCE60 File Offset: 0x000DB060
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapMarkerRuleStart();
			base.Initialize(context);
			if (this.m_mapMarkers != null)
			{
				for (int i = 0; i < this.m_mapMarkers.Count; i++)
				{
					this.m_mapMarkers[i].Initialize(context, i);
				}
			}
			context.ExprHostBuilder.MapMarkerRuleEnd();
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000DCEBD File Offset: 0x000DB0BD
		internal override void InitializeMapMember(InitializationContext context)
		{
			context.ExprHostBuilder.MapMarkerRuleStart();
			base.InitializeMapMember(context);
			context.ExprHostBuilder.MapMarkerRuleEnd();
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000DCEE0 File Offset: 0x000DB0E0
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapMarkerRule mapMarkerRule = (MapMarkerRule)base.PublishClone(context);
			if (this.m_mapMarkers != null)
			{
				mapMarkerRule.m_mapMarkers = new List<MapMarker>(this.m_mapMarkers.Count);
				foreach (MapMarker mapMarker in this.m_mapMarkers)
				{
					mapMarkerRule.m_mapMarkers.Add((MapMarker)mapMarker.PublishClone(context));
				}
			}
			return mapMarkerRule;
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000DCF70 File Offset: 0x000DB170
		internal override void SetExprHost(MapAppearanceRuleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			IList<MapMarkerExprHost> mapMarkersHostsRemotable = this.ExprHost.MapMarkersHostsRemotable;
			if (this.m_mapMarkers != null && mapMarkersHostsRemotable != null)
			{
				for (int i = 0; i < this.m_mapMarkers.Count; i++)
				{
					MapMarker mapMarker = this.m_mapMarkers[i];
					if (mapMarker != null && mapMarker.ExpressionHostID > -1)
					{
						mapMarker.SetExprHost(mapMarkersHostsRemotable[mapMarker.ExpressionHostID], reportObjectModel);
					}
				}
			}
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000DCFF8 File Offset: 0x000DB1F8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMarkerRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapAppearanceRule, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapMarkers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMarker)
			});
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000DD034 File Offset: 0x000DB234
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapMarkerRule.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.MapMarkers)
				{
					writer.Write<MapMarker>(this.m_mapMarkers);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000DD08C File Offset: 0x000DB28C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapMarkerRule.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.MapMarkers)
				{
					this.m_mapMarkers = reader.ReadGenericListOfRIFObjects<MapMarker>();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000DD0E4 File Offset: 0x000DB2E4
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMarkerRule;
		}

		// Token: 0x04001929 RID: 6441
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapMarkerRule.GetDeclaration();

		// Token: 0x0400192A RID: 6442
		private List<MapMarker> m_mapMarkers;
	}
}
