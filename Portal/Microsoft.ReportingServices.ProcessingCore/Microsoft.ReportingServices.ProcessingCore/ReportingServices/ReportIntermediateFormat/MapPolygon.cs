using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200044D RID: 1101
	[Serializable]
	internal sealed class MapPolygon : MapSpatialElement, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060031FD RID: 12797 RVA: 0x000DF94A File Offset: 0x000DDB4A
		internal MapPolygon()
		{
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x000DF952 File Offset: 0x000DDB52
		internal MapPolygon(MapPolygonLayer mapPolygonLayer, Map map)
			: base(mapPolygonLayer, map)
		{
		}

		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x060031FF RID: 12799 RVA: 0x000DF95C File Offset: 0x000DDB5C
		// (set) Token: 0x06003200 RID: 12800 RVA: 0x000DF964 File Offset: 0x000DDB64
		internal ExpressionInfo UseCustomPolygonTemplate
		{
			get
			{
				return this.m_useCustomPolygonTemplate;
			}
			set
			{
				this.m_useCustomPolygonTemplate = value;
			}
		}

		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x06003201 RID: 12801 RVA: 0x000DF96D File Offset: 0x000DDB6D
		// (set) Token: 0x06003202 RID: 12802 RVA: 0x000DF975 File Offset: 0x000DDB75
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

		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x06003203 RID: 12803 RVA: 0x000DF97E File Offset: 0x000DDB7E
		// (set) Token: 0x06003204 RID: 12804 RVA: 0x000DF986 File Offset: 0x000DDB86
		internal ExpressionInfo UseCustomCenterPointTemplate
		{
			get
			{
				return this.m_useCustomCenterPointTemplate;
			}
			set
			{
				this.m_useCustomCenterPointTemplate = value;
			}
		}

		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x06003205 RID: 12805 RVA: 0x000DF98F File Offset: 0x000DDB8F
		// (set) Token: 0x06003206 RID: 12806 RVA: 0x000DF997 File Offset: 0x000DDB97
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

		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x000DF9A0 File Offset: 0x000DDBA0
		internal new MapPolygonExprHost ExprHost
		{
			get
			{
				return (MapPolygonExprHost)this.m_exprHost;
			}
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x000DF9B0 File Offset: 0x000DDBB0
		internal override void Initialize(InitializationContext context, int index)
		{
			context.ExprHostBuilder.MapPolygonStart(index.ToString(CultureInfo.InvariantCulture.NumberFormat));
			base.Initialize(context, index);
			if (this.m_useCustomPolygonTemplate != null)
			{
				this.m_useCustomPolygonTemplate.Initialize("UseCustomPolygonTemplate", context);
				context.ExprHostBuilder.MapPolygonUseCustomPolygonTemplate(this.m_useCustomPolygonTemplate);
			}
			if (this.m_mapPolygonTemplate != null)
			{
				this.m_mapPolygonTemplate.Initialize(context);
			}
			if (this.m_useCustomCenterPointTemplate != null)
			{
				this.m_useCustomCenterPointTemplate.Initialize("UseCustomPointTemplate", context);
				context.ExprHostBuilder.MapPolygonUseCustomCenterPointTemplate(this.m_useCustomCenterPointTemplate);
			}
			if (this.m_mapCenterPointTemplate != null)
			{
				this.m_mapCenterPointTemplate.Initialize(context);
			}
			this.m_exprHostID = context.ExprHostBuilder.MapPolygonEnd();
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x000DFA74 File Offset: 0x000DDC74
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapPolygon mapPolygon = (MapPolygon)base.PublishClone(context);
			if (this.m_useCustomPolygonTemplate != null)
			{
				mapPolygon.m_useCustomPolygonTemplate = (ExpressionInfo)this.m_useCustomPolygonTemplate.PublishClone(context);
			}
			if (this.m_mapPolygonTemplate != null)
			{
				mapPolygon.m_mapPolygonTemplate = (MapPolygonTemplate)this.m_mapPolygonTemplate.PublishClone(context);
			}
			if (this.m_useCustomCenterPointTemplate != null)
			{
				mapPolygon.m_useCustomCenterPointTemplate = (ExpressionInfo)this.m_useCustomCenterPointTemplate.PublishClone(context);
			}
			if (this.m_mapCenterPointTemplate != null)
			{
				mapPolygon.m_mapCenterPointTemplate = (MapPointTemplate)this.m_mapCenterPointTemplate.PublishClone(context);
			}
			return mapPolygon;
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x000DFB0C File Offset: 0x000DDD0C
		internal void SetExprHost(MapPolygonExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_mapPolygonTemplate != null && this.ExprHost.MapPolygonTemplateHost != null)
			{
				this.m_mapPolygonTemplate.SetExprHost(this.ExprHost.MapPolygonTemplateHost, reportObjectModel);
			}
			if (this.m_mapCenterPointTemplate != null && this.ExprHost.MapPointTemplateHost != null)
			{
				this.m_mapCenterPointTemplate.SetExprHost(this.ExprHost.MapPointTemplateHost, reportObjectModel);
			}
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x000DFB94 File Offset: 0x000DDD94
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPolygon, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialElement, new List<MemberInfo>
			{
				new MemberInfo(MemberName.UseCustomPolygonTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MapPolygonTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPolygonTemplate),
				new MemberInfo(MemberName.UseCustomPointTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MapPointTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPointTemplate)
			});
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x000DFC0C File Offset: 0x000DDE0C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapPolygon.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.UseCustomPolygonTemplate:
					writer.Write(this.m_useCustomPolygonTemplate);
					continue;
				case MemberName.UseCustomPointTemplate:
					writer.Write(this.m_useCustomCenterPointTemplate);
					continue;
				case MemberName.MapPolygonTemplate:
					writer.Write(this.m_mapPolygonTemplate);
					continue;
				case MemberName.MapPointTemplate:
					writer.Write(this.m_mapCenterPointTemplate);
					continue;
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x000DFCB4 File Offset: 0x000DDEB4
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapPolygon.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.UseCustomPolygonTemplate:
					this.m_useCustomPolygonTemplate = (ExpressionInfo)reader.ReadRIFObject();
					continue;
				case MemberName.UseCustomPointTemplate:
					this.m_useCustomCenterPointTemplate = (ExpressionInfo)reader.ReadRIFObject();
					continue;
				case MemberName.MapPolygonTemplate:
					this.m_mapPolygonTemplate = (MapPolygonTemplate)reader.ReadRIFObject();
					continue;
				case MemberName.MapPointTemplate:
					this.m_mapCenterPointTemplate = (MapPointTemplate)reader.ReadRIFObject();
					continue;
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x000DFD73 File Offset: 0x000DDF73
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPolygon;
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x000DFD7A File Offset: 0x000DDF7A
		internal bool EvaluateUseCustomPolygonTemplate(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapPolygonUseCustomPolygonTemplateExpression(this, this.m_map.Name);
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x000DFDA0 File Offset: 0x000DDFA0
		internal bool EvaluateUseCustomCenterPointTemplate(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapPolygonUseCustomPointTemplateExpression(this, this.m_map.Name);
		}

		// Token: 0x0400195C RID: 6492
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapPolygon.GetDeclaration();

		// Token: 0x0400195D RID: 6493
		private ExpressionInfo m_useCustomPolygonTemplate;

		// Token: 0x0400195E RID: 6494
		private MapPolygonTemplate m_mapPolygonTemplate;

		// Token: 0x0400195F RID: 6495
		private ExpressionInfo m_useCustomCenterPointTemplate;

		// Token: 0x04001960 RID: 6496
		private MapPointTemplate m_mapCenterPointTemplate;
	}
}
