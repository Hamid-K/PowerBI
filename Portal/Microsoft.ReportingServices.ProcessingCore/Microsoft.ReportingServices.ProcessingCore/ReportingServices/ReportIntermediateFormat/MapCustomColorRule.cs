using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000445 RID: 1093
	[Serializable]
	internal sealed class MapCustomColorRule : MapColorRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600315E RID: 12638 RVA: 0x000DDA31 File Offset: 0x000DBC31
		internal MapCustomColorRule()
		{
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000DDA39 File Offset: 0x000DBC39
		internal MapCustomColorRule(MapVectorLayer mapVectorLayer, Map map)
			: base(mapVectorLayer, map)
		{
		}

		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x000DDA43 File Offset: 0x000DBC43
		// (set) Token: 0x06003161 RID: 12641 RVA: 0x000DDA4B File Offset: 0x000DBC4B
		internal List<MapCustomColor> MapCustomColors
		{
			get
			{
				return this.m_mapCustomColors;
			}
			set
			{
				this.m_mapCustomColors = value;
			}
		}

		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x000DDA54 File Offset: 0x000DBC54
		internal new MapCustomColorRuleExprHost ExprHost
		{
			get
			{
				return (MapCustomColorRuleExprHost)this.m_exprHost;
			}
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000DDA64 File Offset: 0x000DBC64
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapCustomColorRuleStart();
			base.Initialize(context);
			if (this.m_mapCustomColors != null)
			{
				for (int i = 0; i < this.m_mapCustomColors.Count; i++)
				{
					this.m_mapCustomColors[i].Initialize(context, i);
				}
			}
			context.ExprHostBuilder.MapCustomColorRuleEnd();
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000DDAC1 File Offset: 0x000DBCC1
		internal override void InitializeMapMember(InitializationContext context)
		{
			context.ExprHostBuilder.MapCustomColorRuleStart();
			base.InitializeMapMember(context);
			context.ExprHostBuilder.MapCustomColorRuleEnd();
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000DDAE4 File Offset: 0x000DBCE4
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapCustomColorRule mapCustomColorRule = (MapCustomColorRule)base.PublishClone(context);
			if (this.m_mapCustomColors != null)
			{
				mapCustomColorRule.m_mapCustomColors = new List<MapCustomColor>(this.m_mapCustomColors.Count);
				foreach (MapCustomColor mapCustomColor in this.m_mapCustomColors)
				{
					mapCustomColorRule.m_mapCustomColors.Add((MapCustomColor)mapCustomColor.PublishClone(context));
				}
			}
			return mapCustomColorRule;
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000DDB74 File Offset: 0x000DBD74
		internal override void SetExprHost(MapAppearanceRuleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			IList<MapCustomColorExprHost> mapCustomColorsHostsRemotable = this.ExprHost.MapCustomColorsHostsRemotable;
			if (this.m_mapCustomColors != null && mapCustomColorsHostsRemotable != null)
			{
				for (int i = 0; i < this.m_mapCustomColors.Count; i++)
				{
					MapCustomColor mapCustomColor = this.m_mapCustomColors[i];
					if (mapCustomColor != null && mapCustomColor.ExpressionHostID > -1)
					{
						mapCustomColor.SetExprHost(mapCustomColorsHostsRemotable[mapCustomColor.ExpressionHostID], reportObjectModel);
					}
				}
			}
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x000DDBFC File Offset: 0x000DBDFC
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapCustomColorRule, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapColorRule, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapCustomColors, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapCustomColor)
			});
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x000DDC38 File Offset: 0x000DBE38
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapCustomColorRule.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.MapCustomColors)
				{
					writer.Write<MapCustomColor>(this.m_mapCustomColors);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x000DDC90 File Offset: 0x000DBE90
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapCustomColorRule.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.MapCustomColors)
				{
					this.m_mapCustomColors = reader.ReadGenericListOfRIFObjects<MapCustomColor>();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x000DDCE8 File Offset: 0x000DBEE8
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapCustomColorRule;
		}

		// Token: 0x04001937 RID: 6455
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapCustomColorRule.GetDeclaration();

		// Token: 0x04001938 RID: 6456
		private List<MapCustomColor> m_mapCustomColors;
	}
}
