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
	// Token: 0x0200049D RID: 1181
	[Serializable]
	internal sealed class ChartThreeDProperties : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003923 RID: 14627 RVA: 0x000F8AED File Offset: 0x000F6CED
		internal ChartThreeDProperties()
		{
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x000F8AF5 File Offset: 0x000F6CF5
		internal ChartThreeDProperties(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
		{
			this.m_chart = chart;
		}

		// Token: 0x170018DD RID: 6365
		// (get) Token: 0x06003925 RID: 14629 RVA: 0x000F8B04 File Offset: 0x000F6D04
		// (set) Token: 0x06003926 RID: 14630 RVA: 0x000F8B0C File Offset: 0x000F6D0C
		internal ExpressionInfo Enabled
		{
			get
			{
				return this.m_enabled;
			}
			set
			{
				this.m_enabled = value;
			}
		}

		// Token: 0x170018DE RID: 6366
		// (get) Token: 0x06003927 RID: 14631 RVA: 0x000F8B15 File Offset: 0x000F6D15
		// (set) Token: 0x06003928 RID: 14632 RVA: 0x000F8B1D File Offset: 0x000F6D1D
		internal ExpressionInfo ProjectionMode
		{
			get
			{
				return this.m_projectionMode;
			}
			set
			{
				this.m_projectionMode = value;
			}
		}

		// Token: 0x170018DF RID: 6367
		// (get) Token: 0x06003929 RID: 14633 RVA: 0x000F8B26 File Offset: 0x000F6D26
		// (set) Token: 0x0600392A RID: 14634 RVA: 0x000F8B2E File Offset: 0x000F6D2E
		internal ExpressionInfo Rotation
		{
			get
			{
				return this.m_rotation;
			}
			set
			{
				this.m_rotation = value;
			}
		}

		// Token: 0x170018E0 RID: 6368
		// (get) Token: 0x0600392B RID: 14635 RVA: 0x000F8B37 File Offset: 0x000F6D37
		// (set) Token: 0x0600392C RID: 14636 RVA: 0x000F8B3F File Offset: 0x000F6D3F
		internal ExpressionInfo Inclination
		{
			get
			{
				return this.m_inclination;
			}
			set
			{
				this.m_inclination = value;
			}
		}

		// Token: 0x170018E1 RID: 6369
		// (get) Token: 0x0600392D RID: 14637 RVA: 0x000F8B48 File Offset: 0x000F6D48
		// (set) Token: 0x0600392E RID: 14638 RVA: 0x000F8B50 File Offset: 0x000F6D50
		internal ExpressionInfo Perspective
		{
			get
			{
				return this.m_perspective;
			}
			set
			{
				this.m_perspective = value;
			}
		}

		// Token: 0x170018E2 RID: 6370
		// (get) Token: 0x0600392F RID: 14639 RVA: 0x000F8B59 File Offset: 0x000F6D59
		// (set) Token: 0x06003930 RID: 14640 RVA: 0x000F8B61 File Offset: 0x000F6D61
		internal ExpressionInfo DepthRatio
		{
			get
			{
				return this.m_depthRatio;
			}
			set
			{
				this.m_depthRatio = value;
			}
		}

		// Token: 0x170018E3 RID: 6371
		// (get) Token: 0x06003931 RID: 14641 RVA: 0x000F8B6A File Offset: 0x000F6D6A
		// (set) Token: 0x06003932 RID: 14642 RVA: 0x000F8B72 File Offset: 0x000F6D72
		internal ExpressionInfo Shading
		{
			get
			{
				return this.m_shading;
			}
			set
			{
				this.m_shading = value;
			}
		}

		// Token: 0x170018E4 RID: 6372
		// (get) Token: 0x06003933 RID: 14643 RVA: 0x000F8B7B File Offset: 0x000F6D7B
		// (set) Token: 0x06003934 RID: 14644 RVA: 0x000F8B83 File Offset: 0x000F6D83
		internal ExpressionInfo GapDepth
		{
			get
			{
				return this.m_gapDepth;
			}
			set
			{
				this.m_gapDepth = value;
			}
		}

		// Token: 0x170018E5 RID: 6373
		// (get) Token: 0x06003935 RID: 14645 RVA: 0x000F8B8C File Offset: 0x000F6D8C
		// (set) Token: 0x06003936 RID: 14646 RVA: 0x000F8B94 File Offset: 0x000F6D94
		internal ExpressionInfo WallThickness
		{
			get
			{
				return this.m_wallThickness;
			}
			set
			{
				this.m_wallThickness = value;
			}
		}

		// Token: 0x170018E6 RID: 6374
		// (get) Token: 0x06003937 RID: 14647 RVA: 0x000F8B9D File Offset: 0x000F6D9D
		// (set) Token: 0x06003938 RID: 14648 RVA: 0x000F8BA5 File Offset: 0x000F6DA5
		internal ExpressionInfo Clustered
		{
			get
			{
				return this.m_clustered;
			}
			set
			{
				this.m_clustered = value;
			}
		}

		// Token: 0x170018E7 RID: 6375
		// (get) Token: 0x06003939 RID: 14649 RVA: 0x000F8BAE File Offset: 0x000F6DAE
		internal Chart3DPropertiesExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x000F8BB8 File Offset: 0x000F6DB8
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.Chart3DPropertiesStart();
			if (this.m_enabled != null)
			{
				this.m_enabled.Initialize("Rotation", context);
				context.ExprHostBuilder.Chart3DPropertiesEnabled(this.m_enabled);
			}
			if (this.m_projectionMode != null)
			{
				this.m_projectionMode.Initialize("ProjectionMode", context);
				context.ExprHostBuilder.Chart3DPropertiesProjectionMode(this.m_projectionMode);
			}
			if (this.m_rotation != null)
			{
				this.m_rotation.Initialize("Rotation", context);
				context.ExprHostBuilder.Chart3DPropertiesRotation(this.m_rotation);
			}
			if (this.m_inclination != null)
			{
				this.m_inclination.Initialize("Inclination", context);
				context.ExprHostBuilder.Chart3DPropertiesInclination(this.m_inclination);
			}
			if (this.m_perspective != null)
			{
				this.m_perspective.Initialize("Perspective", context);
				context.ExprHostBuilder.Chart3DPropertiesPerspective(this.m_perspective);
			}
			if (this.m_depthRatio != null)
			{
				this.m_depthRatio.Initialize("DepthRatio", context);
				context.ExprHostBuilder.Chart3DPropertiesDepthRatio(this.m_depthRatio);
			}
			if (this.m_shading != null)
			{
				this.m_shading.Initialize("Shading", context);
				context.ExprHostBuilder.Chart3DPropertiesShading(this.m_shading);
			}
			if (this.m_gapDepth != null)
			{
				this.m_gapDepth.Initialize("GapDepth", context);
				context.ExprHostBuilder.Chart3DPropertiesGapDepth(this.m_gapDepth);
			}
			if (this.m_wallThickness != null)
			{
				this.m_wallThickness.Initialize("WallThickness", context);
				context.ExprHostBuilder.Chart3DPropertiesWallThickness(this.m_wallThickness);
			}
			if (this.m_clustered != null)
			{
				this.m_clustered.Initialize("Clustered", context);
				context.ExprHostBuilder.Chart3DPropertiesClustered(this.m_clustered);
			}
			context.ExprHostBuilder.Chart3DPropertiesEnd();
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x000F8D8C File Offset: 0x000F6F8C
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			ChartThreeDProperties chartThreeDProperties = (ChartThreeDProperties)base.MemberwiseClone();
			chartThreeDProperties.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)context.CurrentDataRegionClone;
			if (this.m_enabled != null)
			{
				chartThreeDProperties.m_enabled = (ExpressionInfo)this.m_enabled.PublishClone(context);
			}
			if (this.m_projectionMode != null)
			{
				chartThreeDProperties.m_projectionMode = (ExpressionInfo)this.m_projectionMode.PublishClone(context);
			}
			if (this.m_rotation != null)
			{
				chartThreeDProperties.m_rotation = (ExpressionInfo)this.m_rotation.PublishClone(context);
			}
			if (this.m_inclination != null)
			{
				chartThreeDProperties.m_inclination = (ExpressionInfo)this.m_inclination.PublishClone(context);
			}
			if (this.m_perspective != null)
			{
				chartThreeDProperties.m_perspective = (ExpressionInfo)this.m_perspective.PublishClone(context);
			}
			if (this.m_depthRatio != null)
			{
				chartThreeDProperties.m_depthRatio = (ExpressionInfo)this.m_depthRatio.PublishClone(context);
			}
			if (this.m_shading != null)
			{
				chartThreeDProperties.m_shading = (ExpressionInfo)this.m_shading.PublishClone(context);
			}
			if (this.m_gapDepth != null)
			{
				chartThreeDProperties.m_gapDepth = (ExpressionInfo)this.m_gapDepth.PublishClone(context);
			}
			if (this.m_wallThickness != null)
			{
				chartThreeDProperties.m_wallThickness = (ExpressionInfo)this.m_wallThickness.PublishClone(context);
			}
			if (this.m_clustered != null)
			{
				chartThreeDProperties.m_clustered = (ExpressionInfo)this.m_clustered.PublishClone(context);
			}
			return chartThreeDProperties;
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x000F8EF0 File Offset: 0x000F70F0
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ThreeDProperties, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Enabled, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ProjectionMode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Rotation, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Inclination, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Perspective, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DepthRatio, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Shading, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.GapDepth, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.WallThickness, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Clustered, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Chart, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Chart, Token.Reference)
			});
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000F8FF8 File Offset: 0x000F71F8
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ChartThreeDProperties.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.Rotation:
					writer.Write(this.m_rotation);
					continue;
				case MemberName.Inclination:
					writer.Write(this.m_inclination);
					continue;
				case MemberName.Perspective:
					writer.Write(this.m_perspective);
					continue;
				case MemberName.HeightRatio:
				case MemberName.Origin:
				case MemberName.InsidePlotArea:
				case MemberName.DrawingStyleCube:
					break;
				case MemberName.DepthRatio:
					writer.Write(this.m_depthRatio);
					continue;
				case MemberName.Shading:
					writer.Write(this.m_shading);
					continue;
				case MemberName.GapDepth:
					writer.Write(this.m_gapDepth);
					continue;
				case MemberName.WallThickness:
					writer.Write(this.m_wallThickness);
					continue;
				case MemberName.Enabled:
					writer.Write(this.m_enabled);
					continue;
				case MemberName.Clustered:
					writer.Write(this.m_clustered);
					continue;
				default:
					if (memberName == MemberName.Chart)
					{
						writer.WriteReference(this.m_chart);
						continue;
					}
					if (memberName == MemberName.ProjectionMode)
					{
						writer.Write(this.m_projectionMode);
						continue;
					}
					break;
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000F9140 File Offset: 0x000F7340
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ChartThreeDProperties.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.Rotation:
					this.m_rotation = (ExpressionInfo)reader.ReadRIFObject();
					continue;
				case MemberName.Inclination:
					this.m_inclination = (ExpressionInfo)reader.ReadRIFObject();
					continue;
				case MemberName.Perspective:
					this.m_perspective = (ExpressionInfo)reader.ReadRIFObject();
					continue;
				case MemberName.HeightRatio:
				case MemberName.Origin:
				case MemberName.InsidePlotArea:
				case MemberName.DrawingStyleCube:
					break;
				case MemberName.DepthRatio:
					this.m_depthRatio = (ExpressionInfo)reader.ReadRIFObject();
					continue;
				case MemberName.Shading:
					this.m_shading = (ExpressionInfo)reader.ReadRIFObject();
					continue;
				case MemberName.GapDepth:
					this.m_gapDepth = (ExpressionInfo)reader.ReadRIFObject();
					continue;
				case MemberName.WallThickness:
					this.m_wallThickness = (ExpressionInfo)reader.ReadRIFObject();
					continue;
				case MemberName.Enabled:
					this.m_enabled = (ExpressionInfo)reader.ReadRIFObject();
					continue;
				case MemberName.Clustered:
					this.m_clustered = (ExpressionInfo)reader.ReadRIFObject();
					continue;
				default:
					if (memberName == MemberName.Chart)
					{
						this.m_chart = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.Chart>(this);
						continue;
					}
					if (memberName == MemberName.ProjectionMode)
					{
						this.m_projectionMode = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					break;
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000F92C0 File Offset: 0x000F74C0
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ChartThreeDProperties.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.Chart)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000F9364 File Offset: 0x000F7564
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ThreeDProperties;
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x000F936B File Offset: 0x000F756B
		internal void SetExprHost(Chart3DPropertiesExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x000F9380 File Offset: 0x000F7580
		internal bool EvaluateEnabled(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartThreeDPropertiesEnabledExpression(this, this.m_chart.Name, "Enabled");
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x000F93AB File Offset: 0x000F75AB
		internal ChartThreeDProjectionModes EvaluateProjectionMode(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return EnumTranslator.TranslateChartThreeDProjectionMode(context.ReportRuntime.EvaluateChartThreeDPropertiesProjectionModeExpression(this, this.m_chart.Name, "ProjectionMode"), context.ReportRuntime);
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x000F93E1 File Offset: 0x000F75E1
		internal int EvaluateRotation(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartThreeDPropertiesRotationExpression(this, this.m_chart.Name, "Rotation");
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x000F940C File Offset: 0x000F760C
		internal int EvaluateInclination(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartThreeDPropertiesInclinationExpression(this, this.m_chart.Name, "Inclination");
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x000F9437 File Offset: 0x000F7637
		internal int EvaluatePerspective(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartThreeDPropertiesPerspectiveExpression(this, this.m_chart.Name, "Perspective");
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x000F9462 File Offset: 0x000F7662
		internal int EvaluateDepthRatio(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartThreeDPropertiesDepthRatioExpression(this, this.m_chart.Name, "DepthRatio");
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x000F948D File Offset: 0x000F768D
		internal ChartThreeDShadingTypes EvaluateShading(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return EnumTranslator.TranslateChartThreeDShading(context.ReportRuntime.EvaluateChartThreeDPropertiesShadingExpression(this, this.m_chart.Name, "Shading"), context.ReportRuntime);
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x000F94C3 File Offset: 0x000F76C3
		internal int EvaluateGapDepth(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartThreeDPropertiesGapDepthExpression(this, this.m_chart.Name, "GapDepth");
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x000F94EE File Offset: 0x000F76EE
		internal int EvaluateWallThickness(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartThreeDPropertiesWallThicknessExpression(this, this.m_chart.Name, "WallThickness");
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x000F9519 File Offset: 0x000F7719
		internal bool EvaluateClustered(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartThreeDPropertiesClusteredExpression(this, this.m_chart.Name, "Clustered");
		}

		// Token: 0x04001B7E RID: 7038
		private ExpressionInfo m_enabled;

		// Token: 0x04001B7F RID: 7039
		private ExpressionInfo m_projectionMode;

		// Token: 0x04001B80 RID: 7040
		private ExpressionInfo m_rotation;

		// Token: 0x04001B81 RID: 7041
		private ExpressionInfo m_inclination;

		// Token: 0x04001B82 RID: 7042
		private ExpressionInfo m_perspective;

		// Token: 0x04001B83 RID: 7043
		private ExpressionInfo m_depthRatio;

		// Token: 0x04001B84 RID: 7044
		private ExpressionInfo m_shading;

		// Token: 0x04001B85 RID: 7045
		private ExpressionInfo m_gapDepth;

		// Token: 0x04001B86 RID: 7046
		private ExpressionInfo m_wallThickness;

		// Token: 0x04001B87 RID: 7047
		private ExpressionInfo m_clustered;

		// Token: 0x04001B88 RID: 7048
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Chart m_chart;

		// Token: 0x04001B89 RID: 7049
		[NonSerialized]
		private Chart3DPropertiesExprHost m_exprHost;

		// Token: 0x04001B8A RID: 7050
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartThreeDProperties.GetDeclaration();
	}
}
