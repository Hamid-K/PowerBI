using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000449 RID: 1097
	[Serializable]
	internal sealed class MapMarkerTemplate : MapPointTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060031C1 RID: 12737 RVA: 0x000DEF10 File Offset: 0x000DD110
		internal MapMarkerTemplate()
		{
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000DEF18 File Offset: 0x000DD118
		internal MapMarkerTemplate(MapVectorLayer mapVectorLayer, Map map, int id)
			: base(mapVectorLayer, map, id)
		{
		}

		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x060031C3 RID: 12739 RVA: 0x000DEF23 File Offset: 0x000DD123
		// (set) Token: 0x060031C4 RID: 12740 RVA: 0x000DEF2B File Offset: 0x000DD12B
		internal MapMarker MapMarker
		{
			get
			{
				return this.m_mapMarker;
			}
			set
			{
				this.m_mapMarker = value;
			}
		}

		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x060031C5 RID: 12741 RVA: 0x000DEF34 File Offset: 0x000DD134
		internal new MapMarkerTemplateExprHost ExprHost
		{
			get
			{
				return (MapMarkerTemplateExprHost)this.m_exprHost;
			}
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x000DEF41 File Offset: 0x000DD141
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapMarkerTemplateStart();
			base.Initialize(context);
			if (this.m_mapMarker != null)
			{
				this.m_mapMarker.Initialize(context);
			}
			context.ExprHostBuilder.MapMarkerTemplateEnd();
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x000DEF78 File Offset: 0x000DD178
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapMarkerTemplate mapMarkerTemplate = (MapMarkerTemplate)base.PublishClone(context);
			if (this.m_mapMarker != null)
			{
				mapMarkerTemplate.m_mapMarker = (MapMarker)this.m_mapMarker.PublishClone(context);
			}
			return mapMarkerTemplate;
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000DEFB4 File Offset: 0x000DD1B4
		internal override void SetExprHost(MapPointTemplateExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_mapMarker != null && this.ExprHost.MapMarkerHost != null)
			{
				this.m_mapMarker.SetExprHost(this.ExprHost.MapMarkerHost, reportObjectModel);
			}
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000DF010 File Offset: 0x000DD210
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMarkerTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPointTemplate, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapMarker, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMarker)
			});
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000DF048 File Offset: 0x000DD248
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapMarkerTemplate.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.MapMarker)
				{
					writer.Write(this.m_mapMarker);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000DF0A0 File Offset: 0x000DD2A0
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapMarkerTemplate.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.MapMarker)
				{
					this.m_mapMarker = (MapMarker)reader.ReadRIFObject();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000DF0FD File Offset: 0x000DD2FD
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapMarkerTemplate;
		}

		// Token: 0x04001950 RID: 6480
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapMarkerTemplate.GetDeclaration();

		// Token: 0x04001951 RID: 6481
		private MapMarker m_mapMarker;
	}
}
