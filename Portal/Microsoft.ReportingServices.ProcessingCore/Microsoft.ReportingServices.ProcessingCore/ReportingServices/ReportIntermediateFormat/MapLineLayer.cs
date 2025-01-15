using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200042D RID: 1069
	[Serializable]
	internal sealed class MapLineLayer : MapVectorLayer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002F72 RID: 12146 RVA: 0x000D6AC2 File Offset: 0x000D4CC2
		internal MapLineLayer()
		{
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x000D6ACA File Offset: 0x000D4CCA
		internal MapLineLayer(int ID, Map map)
			: base(ID, map)
		{
		}

		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x06002F74 RID: 12148 RVA: 0x000D6AD4 File Offset: 0x000D4CD4
		// (set) Token: 0x06002F75 RID: 12149 RVA: 0x000D6ADC File Offset: 0x000D4CDC
		internal MapLineTemplate MapLineTemplate
		{
			get
			{
				return this.m_mapLineTemplate;
			}
			set
			{
				this.m_mapLineTemplate = value;
			}
		}

		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x06002F76 RID: 12150 RVA: 0x000D6AE5 File Offset: 0x000D4CE5
		// (set) Token: 0x06002F77 RID: 12151 RVA: 0x000D6AED File Offset: 0x000D4CED
		internal MapLineRules MapLineRules
		{
			get
			{
				return this.m_mapLineRules;
			}
			set
			{
				this.m_mapLineRules = value;
			}
		}

		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x06002F78 RID: 12152 RVA: 0x000D6AF6 File Offset: 0x000D4CF6
		// (set) Token: 0x06002F79 RID: 12153 RVA: 0x000D6AFE File Offset: 0x000D4CFE
		internal List<MapLine> MapLines
		{
			get
			{
				return this.m_mapLines;
			}
			set
			{
				this.m_mapLines = value;
			}
		}

		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06002F7A RID: 12154 RVA: 0x000D6B07 File Offset: 0x000D4D07
		protected override bool Embedded
		{
			get
			{
				return this.MapLines != null;
			}
		}

		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06002F7B RID: 12155 RVA: 0x000D6B12 File Offset: 0x000D4D12
		internal new MapLineLayerExprHost ExprHost
		{
			get
			{
				return (MapLineLayerExprHost)this.m_exprHost;
			}
		}

		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x06002F7C RID: 12156 RVA: 0x000D6B1F File Offset: 0x000D4D1F
		internal new MapLineLayerExprHost ExprHostMapMember
		{
			get
			{
				return (MapLineLayerExprHost)this.m_exprHostMapMember;
			}
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x000D6B2C File Offset: 0x000D4D2C
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapLineLayerStart(base.Name);
			base.Initialize(context);
			if (this.m_mapLineRules != null)
			{
				this.m_mapLineRules.Initialize(context);
			}
			if (base.MapDataRegionName == null)
			{
				if (this.m_mapLineTemplate != null)
				{
					this.m_mapLineTemplate.Initialize(context);
				}
				if (this.m_mapLines != null)
				{
					for (int i = 0; i < this.m_mapLines.Count; i++)
					{
						this.m_mapLines[i].Initialize(context, i);
					}
				}
			}
			this.m_exprHostID = context.ExprHostBuilder.MapLineLayerEnd();
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x000D6BC8 File Offset: 0x000D4DC8
		internal override void InitializeMapMember(InitializationContext context)
		{
			context.ExprHostBuilder.MapLineLayerStart(base.Name);
			base.InitializeMapMember(context);
			if (this.m_mapLineRules != null)
			{
				this.m_mapLineRules.InitializeMapMember(context);
			}
			if (base.MapDataRegionName != null)
			{
				if (this.m_mapLineTemplate != null)
				{
					this.m_mapLineTemplate.Initialize(context);
				}
				if (this.m_mapLines != null)
				{
					for (int i = 0; i < this.m_mapLines.Count; i++)
					{
						this.m_mapLines[i].Initialize(context, i);
					}
				}
			}
			this.m_exprHostMapMemberID = context.ExprHostBuilder.MapLineLayerEnd();
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x000D6C64 File Offset: 0x000D4E64
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapLineLayer mapLineLayer = (MapLineLayer)base.PublishClone(context);
			context.CurrentMapVectorLayerClone = mapLineLayer;
			if (this.m_mapLineTemplate != null)
			{
				mapLineLayer.m_mapLineTemplate = (MapLineTemplate)this.m_mapLineTemplate.PublishClone(context);
			}
			if (this.m_mapLineRules != null)
			{
				mapLineLayer.m_mapLineRules = (MapLineRules)this.m_mapLineRules.PublishClone(context);
			}
			if (this.m_mapLines != null)
			{
				mapLineLayer.m_mapLines = new List<MapLine>(this.m_mapLines.Count);
				foreach (MapLine mapLine in this.m_mapLines)
				{
					mapLineLayer.m_mapLines.Add((MapLine)mapLine.PublishClone(context));
				}
			}
			return mapLineLayer;
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x000D6D3C File Offset: 0x000D4F3C
		internal override void SetExprHost(MapLayerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_mapLineRules != null && this.ExprHost.MapLineRulesHost != null)
			{
				this.m_mapLineRules.SetExprHost(this.ExprHost.MapLineRulesHost, reportObjectModel);
			}
			if (base.MapDataRegionName == null)
			{
				if (this.m_mapLineTemplate != null && this.ExprHost.MapLineTemplateHost != null)
				{
					this.m_mapLineTemplate.SetExprHost(this.ExprHost.MapLineTemplateHost, reportObjectModel);
				}
				IList<MapLineExprHost> mapLinesHostsRemotable = this.ExprHost.MapLinesHostsRemotable;
				if (this.m_mapLines != null && mapLinesHostsRemotable != null)
				{
					for (int i = 0; i < this.m_mapLines.Count; i++)
					{
						MapLine mapLine = this.m_mapLines[i];
						if (mapLine != null && mapLine.ExpressionHostID > -1)
						{
							mapLine.SetExprHost(mapLinesHostsRemotable[mapLine.ExpressionHostID], reportObjectModel);
						}
					}
				}
			}
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x000D6E28 File Offset: 0x000D5028
		internal override void SetExprHostMapMember(MapVectorLayerExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHostMapMember(exprHost, reportObjectModel);
			if (this.m_mapLineRules != null && this.ExprHostMapMember.MapLineRulesHost != null)
			{
				this.m_mapLineRules.SetExprHostMapMember(this.ExprHostMapMember.MapLineRulesHost, reportObjectModel);
			}
			if (base.MapDataRegionName != null)
			{
				if (this.m_mapLineTemplate != null && this.ExprHostMapMember.MapLineTemplateHost != null)
				{
					this.m_mapLineTemplate.SetExprHost(this.ExprHostMapMember.MapLineTemplateHost, reportObjectModel);
				}
				IList<MapLineExprHost> mapLinesHostsRemotable = this.ExprHostMapMember.MapLinesHostsRemotable;
				if (this.m_mapLines != null && mapLinesHostsRemotable != null)
				{
					for (int i = 0; i < this.m_mapLines.Count; i++)
					{
						MapLine mapLine = this.m_mapLines[i];
						if (mapLine != null && mapLine.ExpressionHostID > -1)
						{
							mapLine.SetExprHost(mapLinesHostsRemotable[mapLine.ExpressionHostID], reportObjectModel);
						}
					}
				}
			}
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x000D6F14 File Offset: 0x000D5114
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLineLayer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapVectorLayer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapLineTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLineTemplate),
				new MemberInfo(MemberName.MapLineRules, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLineRules),
				new MemberInfo(MemberName.MapLines, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLine)
			});
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000D6F78 File Offset: 0x000D5178
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapLineLayer.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.MapLineTemplate:
					writer.Write(this.m_mapLineTemplate);
					continue;
				case MemberName.MapLineRules:
					writer.Write(this.m_mapLineRules);
					continue;
				case MemberName.MapLines:
					writer.Write<MapLine>(this.m_mapLines);
					continue;
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x000D7008 File Offset: 0x000D5208
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapLineLayer.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.MapLineTemplate:
					this.m_mapLineTemplate = (MapLineTemplate)reader.ReadRIFObject();
					continue;
				case MemberName.MapLineRules:
					this.m_mapLineRules = (MapLineRules)reader.ReadRIFObject();
					continue;
				case MemberName.MapLines:
					this.m_mapLines = reader.ReadGenericListOfRIFObjects<MapLine>();
					continue;
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000D70A0 File Offset: 0x000D52A0
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLineLayer;
		}

		// Token: 0x040018BB RID: 6331
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapLineLayer.GetDeclaration();

		// Token: 0x040018BC RID: 6332
		private MapLineTemplate m_mapLineTemplate;

		// Token: 0x040018BD RID: 6333
		private MapLineRules m_mapLineRules;

		// Token: 0x040018BE RID: 6334
		private List<MapLine> m_mapLines;
	}
}
