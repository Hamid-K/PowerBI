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
	// Token: 0x020003F8 RID: 1016
	[Serializable]
	internal class TickMarkStyle : GaugePanelStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002AD8 RID: 10968 RVA: 0x000C6DA5 File Offset: 0x000C4FA5
		internal TickMarkStyle()
		{
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x000C6DAD File Offset: 0x000C4FAD
		internal TickMarkStyle(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x06002ADA RID: 10970 RVA: 0x000C6DB6 File Offset: 0x000C4FB6
		// (set) Token: 0x06002ADB RID: 10971 RVA: 0x000C6DBE File Offset: 0x000C4FBE
		internal ExpressionInfo DistanceFromScale
		{
			get
			{
				return this.m_distanceFromScale;
			}
			set
			{
				this.m_distanceFromScale = value;
			}
		}

		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x06002ADC RID: 10972 RVA: 0x000C6DC7 File Offset: 0x000C4FC7
		// (set) Token: 0x06002ADD RID: 10973 RVA: 0x000C6DCF File Offset: 0x000C4FCF
		internal ExpressionInfo Placement
		{
			get
			{
				return this.m_placement;
			}
			set
			{
				this.m_placement = value;
			}
		}

		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x06002ADE RID: 10974 RVA: 0x000C6DD8 File Offset: 0x000C4FD8
		// (set) Token: 0x06002ADF RID: 10975 RVA: 0x000C6DE0 File Offset: 0x000C4FE0
		internal ExpressionInfo EnableGradient
		{
			get
			{
				return this.m_enableGradient;
			}
			set
			{
				this.m_enableGradient = value;
			}
		}

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x06002AE0 RID: 10976 RVA: 0x000C6DE9 File Offset: 0x000C4FE9
		// (set) Token: 0x06002AE1 RID: 10977 RVA: 0x000C6DF1 File Offset: 0x000C4FF1
		internal ExpressionInfo GradientDensity
		{
			get
			{
				return this.m_gradientDensity;
			}
			set
			{
				this.m_gradientDensity = value;
			}
		}

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x06002AE2 RID: 10978 RVA: 0x000C6DFA File Offset: 0x000C4FFA
		// (set) Token: 0x06002AE3 RID: 10979 RVA: 0x000C6E02 File Offset: 0x000C5002
		internal TopImage TickMarkImage
		{
			get
			{
				return this.m_tickMarkImage;
			}
			set
			{
				this.m_tickMarkImage = value;
			}
		}

		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x000C6E0B File Offset: 0x000C500B
		// (set) Token: 0x06002AE5 RID: 10981 RVA: 0x000C6E13 File Offset: 0x000C5013
		internal ExpressionInfo Length
		{
			get
			{
				return this.m_length;
			}
			set
			{
				this.m_length = value;
			}
		}

		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x06002AE6 RID: 10982 RVA: 0x000C6E1C File Offset: 0x000C501C
		// (set) Token: 0x06002AE7 RID: 10983 RVA: 0x000C6E24 File Offset: 0x000C5024
		internal ExpressionInfo Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x06002AE8 RID: 10984 RVA: 0x000C6E2D File Offset: 0x000C502D
		// (set) Token: 0x06002AE9 RID: 10985 RVA: 0x000C6E35 File Offset: 0x000C5035
		internal ExpressionInfo Shape
		{
			get
			{
				return this.m_shape;
			}
			set
			{
				this.m_shape = value;
			}
		}

		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x06002AEA RID: 10986 RVA: 0x000C6E3E File Offset: 0x000C503E
		// (set) Token: 0x06002AEB RID: 10987 RVA: 0x000C6E46 File Offset: 0x000C5046
		internal ExpressionInfo Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x000C6E4F File Offset: 0x000C504F
		internal string OwnerName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x000C6E5C File Offset: 0x000C505C
		internal TickMarkStyleExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x000C6E64 File Offset: 0x000C5064
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.TickMarkStyleStart();
			this.InitializeInternal(context);
			context.ExprHostBuilder.TickMarkStyleEnd();
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000C6E88 File Offset: 0x000C5088
		internal void InitializeInternal(InitializationContext context)
		{
			base.Initialize(context);
			if (this.m_distanceFromScale != null)
			{
				this.m_distanceFromScale.Initialize("DistanceFromScale", context);
				context.ExprHostBuilder.TickMarkStyleDistanceFromScale(this.m_distanceFromScale);
			}
			if (this.m_placement != null)
			{
				this.m_placement.Initialize("Placement", context);
				context.ExprHostBuilder.TickMarkStylePlacement(this.m_placement);
			}
			if (this.m_enableGradient != null)
			{
				this.m_enableGradient.Initialize("EnableGradient", context);
				context.ExprHostBuilder.TickMarkStyleEnableGradient(this.m_enableGradient);
			}
			if (this.m_gradientDensity != null)
			{
				this.m_gradientDensity.Initialize("GradientDensity", context);
				context.ExprHostBuilder.TickMarkStyleGradientDensity(this.m_gradientDensity);
			}
			if (this.m_tickMarkImage != null)
			{
				this.m_tickMarkImage.Initialize(context);
			}
			if (this.m_length != null)
			{
				this.m_length.Initialize("Length", context);
				context.ExprHostBuilder.TickMarkStyleLength(this.m_length);
			}
			if (this.m_width != null)
			{
				this.m_width.Initialize("Width", context);
				context.ExprHostBuilder.TickMarkStyleWidth(this.m_width);
			}
			if (this.m_shape != null)
			{
				this.m_shape.Initialize("Shape", context);
				context.ExprHostBuilder.TickMarkStyleShape(this.m_shape);
			}
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.TickMarkStyleHidden(this.m_hidden);
			}
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x000C7008 File Offset: 0x000C5208
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			TickMarkStyle tickMarkStyle = (TickMarkStyle)base.PublishClone(context);
			if (this.m_distanceFromScale != null)
			{
				tickMarkStyle.m_distanceFromScale = (ExpressionInfo)this.m_distanceFromScale.PublishClone(context);
			}
			if (this.m_placement != null)
			{
				tickMarkStyle.m_placement = (ExpressionInfo)this.m_placement.PublishClone(context);
			}
			if (this.m_enableGradient != null)
			{
				tickMarkStyle.m_enableGradient = (ExpressionInfo)this.m_enableGradient.PublishClone(context);
			}
			if (this.m_gradientDensity != null)
			{
				tickMarkStyle.m_gradientDensity = (ExpressionInfo)this.m_gradientDensity.PublishClone(context);
			}
			if (this.m_tickMarkImage != null)
			{
				tickMarkStyle.m_tickMarkImage = (TopImage)this.m_tickMarkImage.PublishClone(context);
			}
			if (this.m_length != null)
			{
				tickMarkStyle.m_length = (ExpressionInfo)this.m_length.PublishClone(context);
			}
			if (this.m_width != null)
			{
				tickMarkStyle.m_width = (ExpressionInfo)this.m_width.PublishClone(context);
			}
			if (this.m_shape != null)
			{
				tickMarkStyle.m_shape = (ExpressionInfo)this.m_shape.PublishClone(context);
			}
			if (this.m_hidden != null)
			{
				tickMarkStyle.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
			}
			return tickMarkStyle;
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x000C713C File Offset: 0x000C533C
		internal void SetExprHost(TickMarkStyleExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_tickMarkImage != null && this.m_exprHost.TickMarkImageHost != null)
			{
				this.m_tickMarkImage.SetExprHost(this.m_exprHost.TickMarkImageHost, reportObjectModel);
			}
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000C7198 File Offset: 0x000C5398
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TickMarkStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.DistanceFromScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Placement, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.EnableGradient, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.GradientDensity, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TickMarkImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TopImage),
				new MemberInfo(MemberName.Length, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Width, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Shape, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000C7278 File Offset: 0x000C5478
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(TickMarkStyle.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Hidden)
				{
					if (memberName == MemberName.Width)
					{
						writer.Write(this.m_width);
						continue;
					}
					if (memberName == MemberName.Length)
					{
						writer.Write(this.m_length);
						continue;
					}
					if (memberName == MemberName.Hidden)
					{
						writer.Write(this.m_hidden);
						continue;
					}
				}
				else if (memberName <= MemberName.DistanceFromScale)
				{
					if (memberName == MemberName.Shape)
					{
						writer.Write(this.m_shape);
						continue;
					}
					if (memberName == MemberName.DistanceFromScale)
					{
						writer.Write(this.m_distanceFromScale);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Placement)
					{
						writer.Write(this.m_placement);
						continue;
					}
					switch (memberName)
					{
					case MemberName.EnableGradient:
						writer.Write(this.m_enableGradient);
						continue;
					case MemberName.GradientDensity:
						writer.Write(this.m_gradientDensity);
						continue;
					case MemberName.TickMarkImage:
						writer.Write(this.m_tickMarkImage);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000C73C0 File Offset: 0x000C55C0
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(TickMarkStyle.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Hidden)
				{
					if (memberName == MemberName.Width)
					{
						this.m_width = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Length)
					{
						this.m_length = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Hidden)
					{
						this.m_hidden = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else if (memberName <= MemberName.DistanceFromScale)
				{
					if (memberName == MemberName.Shape)
					{
						this.m_shape = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.DistanceFromScale)
					{
						this.m_distanceFromScale = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Placement)
					{
						this.m_placement = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					switch (memberName)
					{
					case MemberName.EnableGradient:
						this.m_enableGradient = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.GradientDensity:
						this.m_gradientDensity = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.TickMarkImage:
						this.m_tickMarkImage = (TopImage)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x000C7539 File Offset: 0x000C5739
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TickMarkStyle;
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x000C7540 File Offset: 0x000C5740
		internal double EvaluateDistanceFromScale(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateTickMarkStyleDistanceFromScaleExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x000C7566 File Offset: 0x000C5766
		internal GaugeLabelPlacements EvaluatePlacement(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeLabelPlacements(context.ReportRuntime.EvaluateTickMarkStylePlacementExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x000C7597 File Offset: 0x000C5797
		internal bool EvaluateEnableGradient(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateTickMarkStyleEnableGradientExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x000C75BD File Offset: 0x000C57BD
		internal double EvaluateGradientDensity(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateTickMarkStyleGradientDensityExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x000C75E3 File Offset: 0x000C57E3
		internal double EvaluateLength(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateTickMarkStyleLengthExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x000C7609 File Offset: 0x000C5809
		internal double EvaluateWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateTickMarkStyleWidthExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x000C762F File Offset: 0x000C582F
		internal GaugeTickMarkShapes EvaluateShape(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeTickMarkShapes(context.ReportRuntime.EvaluateTickMarkStyleShapeExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000C7660 File Offset: 0x000C5860
		internal bool EvaluateHidden(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateTickMarkStyleHiddenExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x04001770 RID: 6000
		[NonSerialized]
		protected TickMarkStyleExprHost m_exprHost;

		// Token: 0x04001771 RID: 6001
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = TickMarkStyle.GetDeclaration();

		// Token: 0x04001772 RID: 6002
		private ExpressionInfo m_distanceFromScale;

		// Token: 0x04001773 RID: 6003
		private ExpressionInfo m_placement;

		// Token: 0x04001774 RID: 6004
		private ExpressionInfo m_enableGradient;

		// Token: 0x04001775 RID: 6005
		private ExpressionInfo m_gradientDensity;

		// Token: 0x04001776 RID: 6006
		private TopImage m_tickMarkImage;

		// Token: 0x04001777 RID: 6007
		private ExpressionInfo m_length;

		// Token: 0x04001778 RID: 6008
		private ExpressionInfo m_width;

		// Token: 0x04001779 RID: 6009
		private ExpressionInfo m_shape;

		// Token: 0x0400177A RID: 6010
		private ExpressionInfo m_hidden;
	}
}
