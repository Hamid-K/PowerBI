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
	// Token: 0x0200044F RID: 1103
	[Serializable]
	internal sealed class MapPoint : MapSpatialElement, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003225 RID: 12837 RVA: 0x000E0233 File Offset: 0x000DE433
		internal MapPoint()
		{
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x000E023B File Offset: 0x000DE43B
		internal MapPoint(MapPointLayer mapPointLayer, Map map)
			: base(mapPointLayer, map)
		{
		}

		// Token: 0x170016F2 RID: 5874
		// (get) Token: 0x06003227 RID: 12839 RVA: 0x000E0245 File Offset: 0x000DE445
		// (set) Token: 0x06003228 RID: 12840 RVA: 0x000E024D File Offset: 0x000DE44D
		internal ExpressionInfo UseCustomPointTemplate
		{
			get
			{
				return this.m_useCustomPointTemplate;
			}
			set
			{
				this.m_useCustomPointTemplate = value;
			}
		}

		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x000E0256 File Offset: 0x000DE456
		// (set) Token: 0x0600322A RID: 12842 RVA: 0x000E025E File Offset: 0x000DE45E
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

		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x0600322B RID: 12843 RVA: 0x000E0267 File Offset: 0x000DE467
		internal new MapPointExprHost ExprHost
		{
			get
			{
				return (MapPointExprHost)this.m_exprHost;
			}
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000E0274 File Offset: 0x000DE474
		internal override void Initialize(InitializationContext context, int index)
		{
			context.ExprHostBuilder.MapPointStart(index.ToString(CultureInfo.InvariantCulture.NumberFormat));
			base.Initialize(context, index);
			if (this.m_useCustomPointTemplate != null)
			{
				this.m_useCustomPointTemplate.Initialize("UseCustomPointTemplate", context);
				context.ExprHostBuilder.MapPointUseCustomPointTemplate(this.m_useCustomPointTemplate);
			}
			if (this.m_mapPointTemplate != null)
			{
				this.m_mapPointTemplate.Initialize(context);
			}
			this.m_exprHostID = context.ExprHostBuilder.MapPointEnd();
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x000E02F8 File Offset: 0x000DE4F8
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapPoint mapPoint = (MapPoint)base.PublishClone(context);
			if (this.m_useCustomPointTemplate != null)
			{
				mapPoint.m_useCustomPointTemplate = (ExpressionInfo)this.m_useCustomPointTemplate.PublishClone(context);
			}
			if (this.m_mapPointTemplate != null)
			{
				mapPoint.m_mapPointTemplate = (MapPointTemplate)this.m_mapPointTemplate.PublishClone(context);
			}
			return mapPoint;
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x000E0354 File Offset: 0x000DE554
		internal void SetExprHost(MapPointExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_mapPointTemplate != null && this.ExprHost.MapPointTemplateHost != null)
			{
				this.m_mapPointTemplate.SetExprHost(this.ExprHost.MapPointTemplateHost, reportObjectModel);
			}
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x000E03B0 File Offset: 0x000DE5B0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPoint, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSpatialElement, new List<MemberInfo>
			{
				new MemberInfo(MemberName.UseCustomPointTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MapPointTemplate, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPointTemplate)
			});
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x000E0400 File Offset: 0x000DE600
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapPoint.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.UseCustomPointTemplate)
				{
					if (memberName != MemberName.MapPointTemplate)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_mapPointTemplate);
					}
				}
				else
				{
					writer.Write(this.m_useCustomPointTemplate);
				}
			}
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x000E0474 File Offset: 0x000DE674
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapPoint.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.UseCustomPointTemplate)
				{
					if (memberName != MemberName.MapPointTemplate)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_mapPointTemplate = (MapPointTemplate)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_useCustomPointTemplate = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x000E04F1 File Offset: 0x000DE6F1
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapPoint;
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x000E04F8 File Offset: 0x000DE6F8
		internal bool EvaluateUseCustomPointTemplate(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(base.InstancePath, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapPointUseCustomPointTemplateExpression(this, this.m_map.Name);
		}

		// Token: 0x04001968 RID: 6504
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapPoint.GetDeclaration();

		// Token: 0x04001969 RID: 6505
		private ExpressionInfo m_useCustomPointTemplate;

		// Token: 0x0400196A RID: 6506
		private MapPointTemplate m_mapPointTemplate;
	}
}
