using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000451 RID: 1105
	[Serializable]
	internal sealed class MapViewport : MapSubItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x0600324D RID: 12877 RVA: 0x000E09DB File Offset: 0x000DEBDB
		internal MapViewport()
		{
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000E09E3 File Offset: 0x000DEBE3
		internal MapViewport(Map map)
			: base(map)
		{
		}

		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x0600324F RID: 12879 RVA: 0x000E09EC File Offset: 0x000DEBEC
		// (set) Token: 0x06003250 RID: 12880 RVA: 0x000E09F4 File Offset: 0x000DEBF4
		internal ExpressionInfo MapCoordinateSystem
		{
			get
			{
				return this.m_mapCoordinateSystem;
			}
			set
			{
				this.m_mapCoordinateSystem = value;
			}
		}

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x06003251 RID: 12881 RVA: 0x000E09FD File Offset: 0x000DEBFD
		// (set) Token: 0x06003252 RID: 12882 RVA: 0x000E0A05 File Offset: 0x000DEC05
		internal ExpressionInfo MapProjection
		{
			get
			{
				return this.m_mapProjection;
			}
			set
			{
				this.m_mapProjection = value;
			}
		}

		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x06003253 RID: 12883 RVA: 0x000E0A0E File Offset: 0x000DEC0E
		// (set) Token: 0x06003254 RID: 12884 RVA: 0x000E0A16 File Offset: 0x000DEC16
		internal ExpressionInfo ProjectionCenterX
		{
			get
			{
				return this.m_projectionCenterX;
			}
			set
			{
				this.m_projectionCenterX = value;
			}
		}

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x06003255 RID: 12885 RVA: 0x000E0A1F File Offset: 0x000DEC1F
		// (set) Token: 0x06003256 RID: 12886 RVA: 0x000E0A27 File Offset: 0x000DEC27
		internal ExpressionInfo ProjectionCenterY
		{
			get
			{
				return this.m_projectionCenterY;
			}
			set
			{
				this.m_projectionCenterY = value;
			}
		}

		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x000E0A30 File Offset: 0x000DEC30
		// (set) Token: 0x06003258 RID: 12888 RVA: 0x000E0A38 File Offset: 0x000DEC38
		internal MapLimits MapLimits
		{
			get
			{
				return this.m_mapLimits;
			}
			set
			{
				this.m_mapLimits = value;
			}
		}

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x000E0A41 File Offset: 0x000DEC41
		// (set) Token: 0x0600325A RID: 12890 RVA: 0x000E0A49 File Offset: 0x000DEC49
		internal MapView MapView
		{
			get
			{
				return this.m_mapView;
			}
			set
			{
				this.m_mapView = value;
			}
		}

		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x000E0A52 File Offset: 0x000DEC52
		// (set) Token: 0x0600325C RID: 12892 RVA: 0x000E0A5A File Offset: 0x000DEC5A
		internal ExpressionInfo MaximumZoom
		{
			get
			{
				return this.m_maximumZoom;
			}
			set
			{
				this.m_maximumZoom = value;
			}
		}

		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x000E0A63 File Offset: 0x000DEC63
		// (set) Token: 0x0600325E RID: 12894 RVA: 0x000E0A6B File Offset: 0x000DEC6B
		internal ExpressionInfo MinimumZoom
		{
			get
			{
				return this.m_minimumZoom;
			}
			set
			{
				this.m_minimumZoom = value;
			}
		}

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x0600325F RID: 12895 RVA: 0x000E0A74 File Offset: 0x000DEC74
		// (set) Token: 0x06003260 RID: 12896 RVA: 0x000E0A7C File Offset: 0x000DEC7C
		internal ExpressionInfo SimplificationResolution
		{
			get
			{
				return this.m_simplificationResolution;
			}
			set
			{
				this.m_simplificationResolution = value;
			}
		}

		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x000E0A85 File Offset: 0x000DEC85
		// (set) Token: 0x06003262 RID: 12898 RVA: 0x000E0A8D File Offset: 0x000DEC8D
		internal ExpressionInfo ContentMargin
		{
			get
			{
				return this.m_contentMargin;
			}
			set
			{
				this.m_contentMargin = value;
			}
		}

		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x06003263 RID: 12899 RVA: 0x000E0A96 File Offset: 0x000DEC96
		// (set) Token: 0x06003264 RID: 12900 RVA: 0x000E0A9E File Offset: 0x000DEC9E
		internal MapGridLines MapMeridians
		{
			get
			{
				return this.m_mapMeridians;
			}
			set
			{
				this.m_mapMeridians = value;
			}
		}

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x000E0AA7 File Offset: 0x000DECA7
		// (set) Token: 0x06003266 RID: 12902 RVA: 0x000E0AAF File Offset: 0x000DECAF
		internal MapGridLines MapParallels
		{
			get
			{
				return this.m_mapParallels;
			}
			set
			{
				this.m_mapParallels = value;
			}
		}

		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x06003267 RID: 12903 RVA: 0x000E0AB8 File Offset: 0x000DECB8
		// (set) Token: 0x06003268 RID: 12904 RVA: 0x000E0AC0 File Offset: 0x000DECC0
		internal ExpressionInfo GridUnderContent
		{
			get
			{
				return this.m_gridUnderContent;
			}
			set
			{
				this.m_gridUnderContent = value;
			}
		}

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x000E0AC9 File Offset: 0x000DECC9
		internal new MapViewportExprHost ExprHost
		{
			get
			{
				return (MapViewportExprHost)this.m_exprHost;
			}
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000E0AD8 File Offset: 0x000DECD8
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.MapViewportStart();
			base.Initialize(context);
			if (this.m_mapCoordinateSystem != null)
			{
				this.m_mapCoordinateSystem.Initialize("MapCoordinateSystem", context);
				context.ExprHostBuilder.MapViewportMapCoordinateSystem(this.m_mapCoordinateSystem);
			}
			if (this.m_mapProjection != null)
			{
				this.m_mapProjection.Initialize("MapProjection", context);
				context.ExprHostBuilder.MapViewportMapProjection(this.m_mapProjection);
			}
			if (this.m_projectionCenterX != null)
			{
				this.m_projectionCenterX.Initialize("ProjectionCenterX", context);
				context.ExprHostBuilder.MapViewportProjectionCenterX(this.m_projectionCenterX);
			}
			if (this.m_projectionCenterY != null)
			{
				this.m_projectionCenterY.Initialize("ProjectionCenterY", context);
				context.ExprHostBuilder.MapViewportProjectionCenterY(this.m_projectionCenterY);
			}
			if (this.m_mapLimits != null)
			{
				this.m_mapLimits.Initialize(context);
			}
			if (this.m_mapView != null)
			{
				this.m_mapView.Initialize(context);
			}
			if (this.m_maximumZoom != null)
			{
				this.m_maximumZoom.Initialize("MaximumZoom", context);
				context.ExprHostBuilder.MapViewportMaximumZoom(this.m_maximumZoom);
			}
			if (this.m_minimumZoom != null)
			{
				this.m_minimumZoom.Initialize("MinimumZoom", context);
				context.ExprHostBuilder.MapViewportMinimumZoom(this.m_minimumZoom);
			}
			if (this.m_contentMargin != null)
			{
				this.m_contentMargin.Initialize("ContentMargin", context);
				context.ExprHostBuilder.MapViewportContentMargin(this.m_contentMargin);
			}
			if (this.m_mapMeridians != null)
			{
				this.m_mapMeridians.Initialize(context, true);
			}
			if (this.m_mapParallels != null)
			{
				this.m_mapParallels.Initialize(context, false);
			}
			if (this.m_gridUnderContent != null)
			{
				this.m_gridUnderContent.Initialize("GridUnderContent", context);
				context.ExprHostBuilder.MapViewportGridUnderContent(this.m_gridUnderContent);
			}
			if (this.m_simplificationResolution != null)
			{
				this.m_simplificationResolution.Initialize("SimplificationResolution", context);
				context.ExprHostBuilder.MapViewportSimplificationResolution(this.m_simplificationResolution);
			}
			context.ExprHostBuilder.MapViewportEnd();
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000E0CDC File Offset: 0x000DEEDC
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			MapViewport mapViewport = (MapViewport)base.PublishClone(context);
			if (this.m_mapCoordinateSystem != null)
			{
				mapViewport.m_mapCoordinateSystem = (ExpressionInfo)this.m_mapCoordinateSystem.PublishClone(context);
			}
			if (this.m_mapProjection != null)
			{
				mapViewport.m_mapProjection = (ExpressionInfo)this.m_mapProjection.PublishClone(context);
			}
			if (this.m_projectionCenterX != null)
			{
				mapViewport.m_projectionCenterX = (ExpressionInfo)this.m_projectionCenterX.PublishClone(context);
			}
			if (this.m_projectionCenterY != null)
			{
				mapViewport.m_projectionCenterY = (ExpressionInfo)this.m_projectionCenterY.PublishClone(context);
			}
			if (this.m_mapLimits != null)
			{
				mapViewport.m_mapLimits = (MapLimits)this.m_mapLimits.PublishClone(context);
			}
			if (this.m_mapView != null)
			{
				mapViewport.m_mapView = (MapView)this.m_mapView.PublishClone(context);
			}
			if (this.m_maximumZoom != null)
			{
				mapViewport.m_maximumZoom = (ExpressionInfo)this.m_maximumZoom.PublishClone(context);
			}
			if (this.m_minimumZoom != null)
			{
				mapViewport.m_minimumZoom = (ExpressionInfo)this.m_minimumZoom.PublishClone(context);
			}
			if (this.m_contentMargin != null)
			{
				mapViewport.m_contentMargin = (ExpressionInfo)this.m_contentMargin.PublishClone(context);
			}
			if (this.m_mapMeridians != null)
			{
				mapViewport.m_mapMeridians = (MapGridLines)this.m_mapMeridians.PublishClone(context);
			}
			if (this.m_mapParallels != null)
			{
				mapViewport.m_mapParallels = (MapGridLines)this.m_mapParallels.PublishClone(context);
			}
			if (this.m_gridUnderContent != null)
			{
				mapViewport.m_gridUnderContent = (ExpressionInfo)this.m_gridUnderContent.PublishClone(context);
			}
			if (this.m_simplificationResolution != null)
			{
				mapViewport.m_simplificationResolution = (ExpressionInfo)this.m_simplificationResolution.PublishClone(context);
			}
			return mapViewport;
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x000E0E8C File Offset: 0x000DF08C
		internal void SetExprHost(MapViewportExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
			if (this.m_mapLimits != null && this.ExprHost.MapLimitsHost != null)
			{
				this.m_mapLimits.SetExprHost(this.ExprHost.MapLimitsHost, reportObjectModel);
			}
			if (this.m_mapMeridians != null && this.ExprHost.MapMeridiansHost != null)
			{
				this.m_mapMeridians.SetExprHost(this.ExprHost.MapMeridiansHost, reportObjectModel);
			}
			if (this.m_mapParallels != null && this.ExprHost.MapParallelsHost != null)
			{
				this.m_mapParallels.SetExprHost(this.ExprHost.MapParallelsHost, reportObjectModel);
			}
			if (this.m_mapView != null && this.ExprHost.MapViewHost != null)
			{
				this.m_mapView.SetExprHost(this.ExprHost.MapViewHost, reportObjectModel);
			}
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000E0F6C File Offset: 0x000DF16C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapViewport, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapSubItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.MapCoordinateSystem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MapProjection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ProjectionCenterX, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ProjectionCenterY, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MapLimits, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapLimits),
				new MemberInfo(MemberName.MapView, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapView),
				new MemberInfo(MemberName.MaximumZoom, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MinimumZoom, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ContentMargin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MapMeridians, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapGridLines),
				new MemberInfo(MemberName.MapParallels, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapGridLines),
				new MemberInfo(MemberName.GridUnderContent, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.SimplificationResolution, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000E10A0 File Offset: 0x000DF2A0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(MapViewport.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.MapCoordinateSystem:
					writer.Write(this.m_mapCoordinateSystem);
					break;
				case MemberName.MapProjection:
					writer.Write(this.m_mapProjection);
					break;
				case MemberName.ProjectionCenterX:
					writer.Write(this.m_projectionCenterX);
					break;
				case MemberName.ProjectionCenterY:
					writer.Write(this.m_projectionCenterY);
					break;
				case MemberName.MaximumZoom:
					writer.Write(this.m_maximumZoom);
					break;
				case MemberName.MinimumZoom:
					writer.Write(this.m_minimumZoom);
					break;
				case MemberName.MapLimits:
					writer.Write(this.m_mapLimits);
					break;
				case MemberName.ContentMargin:
					writer.Write(this.m_contentMargin);
					break;
				case MemberName.MapMeridians:
					writer.Write(this.m_mapMeridians);
					break;
				case MemberName.MapParallels:
					writer.Write(this.m_mapParallels);
					break;
				case MemberName.GridUnderContent:
					writer.Write(this.m_gridUnderContent);
					break;
				default:
					if (memberName != MemberName.SimplificationResolution)
					{
						if (memberName != MemberName.MapView)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_mapView);
						}
					}
					else
					{
						writer.Write(this.m_simplificationResolution);
					}
					break;
				}
			}
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000E120C File Offset: 0x000DF40C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(MapViewport.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.MapCoordinateSystem:
					this.m_mapCoordinateSystem = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.MapProjection:
					this.m_mapProjection = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.ProjectionCenterX:
					this.m_projectionCenterX = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.ProjectionCenterY:
					this.m_projectionCenterY = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.MaximumZoom:
					this.m_maximumZoom = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.MinimumZoom:
					this.m_minimumZoom = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.MapLimits:
					this.m_mapLimits = (MapLimits)reader.ReadRIFObject();
					break;
				case MemberName.ContentMargin:
					this.m_contentMargin = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.MapMeridians:
					this.m_mapMeridians = (MapGridLines)reader.ReadRIFObject();
					break;
				case MemberName.MapParallels:
					this.m_mapParallels = (MapGridLines)reader.ReadRIFObject();
					break;
				case MemberName.GridUnderContent:
					this.m_gridUnderContent = (ExpressionInfo)reader.ReadRIFObject();
					break;
				default:
					if (memberName != MemberName.SimplificationResolution)
					{
						if (memberName != MemberName.MapView)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_mapView = (MapView)reader.ReadRIFObject();
						}
					}
					else
					{
						this.m_simplificationResolution = (ExpressionInfo)reader.ReadRIFObject();
					}
					break;
				}
			}
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x000E13BE File Offset: 0x000DF5BE
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapViewport;
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x000E13C5 File Offset: 0x000DF5C5
		internal MapCoordinateSystem EvaluateMapCoordinateSystem(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateMapCoordinateSystem(context.ReportRuntime.EvaluateMapViewportMapCoordinateSystemExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x000E13F6 File Offset: 0x000DF5F6
		internal MapProjection EvaluateMapProjection(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return EnumTranslator.TranslateMapProjection(context.ReportRuntime.EvaluateMapViewportMapProjectionExpression(this, this.m_map.Name), context.ReportRuntime);
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x000E1427 File Offset: 0x000DF627
		internal double EvaluateProjectionCenterX(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapViewportProjectionCenterXExpression(this, this.m_map.Name);
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x000E144D File Offset: 0x000DF64D
		internal double EvaluateProjectionCenterY(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapViewportProjectionCenterYExpression(this, this.m_map.Name);
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x000E1473 File Offset: 0x000DF673
		internal double EvaluateMaximumZoom(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapViewportMaximumZoomExpression(this, this.m_map.Name);
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x000E1499 File Offset: 0x000DF699
		internal double EvaluateMinimumZoom(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapViewportMinimumZoomExpression(this, this.m_map.Name);
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x000E14BF File Offset: 0x000DF6BF
		internal string EvaluateContentMargin(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapViewportContentMarginExpression(this, this.m_map.Name);
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x000E14E5 File Offset: 0x000DF6E5
		internal bool EvaluateGridUnderContent(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapViewportGridUnderContentExpression(this, this.m_map.Name);
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x000E150B File Offset: 0x000DF70B
		internal double EvaluateSimplificationResolution(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_map, reportScopeInstance);
			return context.ReportRuntime.EvaluateMapViewportSimplificationResolutionExpression(this, this.m_map.Name);
		}

		// Token: 0x04001971 RID: 6513
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapViewport.GetDeclaration();

		// Token: 0x04001972 RID: 6514
		private ExpressionInfo m_mapCoordinateSystem;

		// Token: 0x04001973 RID: 6515
		private ExpressionInfo m_mapProjection;

		// Token: 0x04001974 RID: 6516
		private ExpressionInfo m_projectionCenterX;

		// Token: 0x04001975 RID: 6517
		private ExpressionInfo m_projectionCenterY;

		// Token: 0x04001976 RID: 6518
		private MapLimits m_mapLimits;

		// Token: 0x04001977 RID: 6519
		private MapView m_mapView;

		// Token: 0x04001978 RID: 6520
		private ExpressionInfo m_maximumZoom;

		// Token: 0x04001979 RID: 6521
		private ExpressionInfo m_minimumZoom;

		// Token: 0x0400197A RID: 6522
		private ExpressionInfo m_contentMargin;

		// Token: 0x0400197B RID: 6523
		private MapGridLines m_mapMeridians;

		// Token: 0x0400197C RID: 6524
		private MapGridLines m_mapParallels;

		// Token: 0x0400197D RID: 6525
		private ExpressionInfo m_gridUnderContent;

		// Token: 0x0400197E RID: 6526
		private ExpressionInfo m_simplificationResolution;
	}
}
