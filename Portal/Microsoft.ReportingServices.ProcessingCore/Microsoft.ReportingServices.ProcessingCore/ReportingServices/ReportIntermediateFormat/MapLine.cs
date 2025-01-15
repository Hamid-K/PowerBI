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
	// Token: 0x0200044C RID: 1100
	[Serializable]
	internal sealed class MapLine : MapSpatialElement, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060031ED RID: 12781 RVA: 0x000DF653 File Offset: 0x000DD853
		internal MapLine()
		{
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x000DF65B File Offset: 0x000DD85B
		internal MapLine(MapLineLayer mapLineLayer, Map map)
			: base(mapLineLayer, map)
		{
		}

		// Token: 0x170016E4 RID: 5860
		// (get) Token: 0x060031EF RID: 12783 RVA: 0x000DF665 File Offset: 0x000DD865
		// (set) Token: 0x060031F0 RID: 12784 RVA: 0x000DF66D File Offset: 0x000DD86D
		internal ExpressionInfo UseCustomLineTemplate
		{
			get
			{
				return this.m_useCustomLineTemplate;
			}
			set
			{
				this.m_useCustomLineTemplate = value;
			}
		}

		// Token: 0x170016E5 RID: 5861
		// (get) Token: 0x060031F1 RID: 12785 RVA: 0x000DF676 File Offset: 0x000DD876
		// (set) Token: 0x060031F2 RID: 12786 RVA: 0x000DF67E File Offset: 0x000DD87E
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

		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x000DF687 File Offset: 0x000DD887
		internal new MapLineExprHost ExprHost
		{
			get
			{
				return (MapLineExprHost)this.m_exprHost;
			}
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000DF694 File Offset: 0x000DD894
		internal override void Initialize(InitializationContext context, int index)
		{
			context.ExprHostBuilder.MapLineStart(index.ToString(CultureInfo.InvariantCulture.NumberFormat));
			base.Initialize(context, index);
			if (this.m_useCustomLineTemplate != null)
			{
				this.m_useCustomLineTemplate.Initialize("UseCustomLineTemplate", context);
				context.ExprHostBuilder.MapLineUseCustomLineTemplate(this.m_useCustomLineTemplate);
			}
			if (this.m_mapLineTemplate != null)
			{
				this.m_mapLineTemplate.Initialize(context);
			}
			this.m_exprHostID = context.ExprHostBuilder.MapLineEnd();
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000DF718 File Offset: 0x000DD918
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapLine mapLine = (MapLine)base.PublishClone(context);
			if (this.m_useCustomLineTemplate != null)
			{
				mapLine.m_useCustomLineTemplate = (ExpressionInfo)this.m_useCustomLineTemplate.PublishClone(context);
			}
			if (this.m_mapLineTemplate != null)
			{
				mapLine.m_mapLineTemplate = (MapLineTemplate)this.m_mapLineTemplate.PublishClone(context);
			}
			return mapLine;
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x000DF774 File Offset: 0x000DD974
		internal void SetExprHost(MapLineExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_mapLineTemplate != null && this.ExprHost.MapLineTemplateHost != null)
			{
				this.m_mapLineTemplate.SetExprHost(this.ExprHost.MapLineTemplateHost, reportObjectModel);
			}
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x000DF7D0 File Offset: 0x000DD9D0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLine, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialElement, new List<MemberInfo>
			{
				new MemberInfo(MemberName.UseCustomLineTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MapLineTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLineTemplate)
			});
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x000DF820 File Offset: 0x000DDA20
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapLine.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.UseCustomLineTemplate)
				{
					if (memberName != MemberName.MapLineTemplate)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_mapLineTemplate);
					}
				}
				else
				{
					writer.Write(this.m_useCustomLineTemplate);
				}
			}
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x000DF894 File Offset: 0x000DDA94
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapLine.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.UseCustomLineTemplate)
				{
					if (memberName != MemberName.MapLineTemplate)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_mapLineTemplate = (MapLineTemplate)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_useCustomLineTemplate = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x000DF911 File Offset: 0x000DDB11
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLine;
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x000DF918 File Offset: 0x000DDB18
		internal bool EvaluateUseCustomLineTemplate(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapLineUseCustomLineTemplateExpression(this, this.m_map.Name);
		}

		// Token: 0x04001959 RID: 6489
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapLine.GetDeclaration();

		// Token: 0x0400195A RID: 6490
		private ExpressionInfo m_useCustomLineTemplate;

		// Token: 0x0400195B RID: 6491
		private MapLineTemplate m_mapLineTemplate;
	}
}
