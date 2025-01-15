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
	// Token: 0x020003D5 RID: 981
	[Serializable]
	internal sealed class BackFrame : GaugePanelStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002798 RID: 10136 RVA: 0x000BAD3B File Offset: 0x000B8F3B
		internal BackFrame()
		{
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x000BAD43 File Offset: 0x000B8F43
		internal BackFrame(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x000BAD4C File Offset: 0x000B8F4C
		// (set) Token: 0x0600279B RID: 10139 RVA: 0x000BAD54 File Offset: 0x000B8F54
		internal ExpressionInfo FrameStyle
		{
			get
			{
				return this.m_frameStyle;
			}
			set
			{
				this.m_frameStyle = value;
			}
		}

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x000BAD5D File Offset: 0x000B8F5D
		// (set) Token: 0x0600279D RID: 10141 RVA: 0x000BAD65 File Offset: 0x000B8F65
		internal ExpressionInfo FrameShape
		{
			get
			{
				return this.m_frameShape;
			}
			set
			{
				this.m_frameShape = value;
			}
		}

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x0600279E RID: 10142 RVA: 0x000BAD6E File Offset: 0x000B8F6E
		// (set) Token: 0x0600279F RID: 10143 RVA: 0x000BAD76 File Offset: 0x000B8F76
		internal ExpressionInfo FrameWidth
		{
			get
			{
				return this.m_frameWidth;
			}
			set
			{
				this.m_frameWidth = value;
			}
		}

		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x060027A0 RID: 10144 RVA: 0x000BAD7F File Offset: 0x000B8F7F
		// (set) Token: 0x060027A1 RID: 10145 RVA: 0x000BAD87 File Offset: 0x000B8F87
		internal ExpressionInfo GlassEffect
		{
			get
			{
				return this.m_glassEffect;
			}
			set
			{
				this.m_glassEffect = value;
			}
		}

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x060027A2 RID: 10146 RVA: 0x000BAD90 File Offset: 0x000B8F90
		// (set) Token: 0x060027A3 RID: 10147 RVA: 0x000BAD98 File Offset: 0x000B8F98
		internal FrameBackground FrameBackground
		{
			get
			{
				return this.m_frameBackground;
			}
			set
			{
				this.m_frameBackground = value;
			}
		}

		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x060027A4 RID: 10148 RVA: 0x000BADA1 File Offset: 0x000B8FA1
		// (set) Token: 0x060027A5 RID: 10149 RVA: 0x000BADA9 File Offset: 0x000B8FA9
		internal FrameImage FrameImage
		{
			get
			{
				return this.m_frameImage;
			}
			set
			{
				this.m_frameImage = value;
			}
		}

		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x060027A6 RID: 10150 RVA: 0x000BADB2 File Offset: 0x000B8FB2
		internal string OwnerName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x000BADBF File Offset: 0x000B8FBF
		internal BackFrameExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000BADC8 File Offset: 0x000B8FC8
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.BackFrameStart();
			base.Initialize(context);
			if (this.m_frameStyle != null)
			{
				this.m_frameStyle.Initialize("FrameStyle", context);
				context.ExprHostBuilder.BackFrameFrameStyle(this.m_frameStyle);
			}
			if (this.m_frameShape != null)
			{
				this.m_frameShape.Initialize("FrameShape", context);
				context.ExprHostBuilder.BackFrameFrameShape(this.m_frameShape);
			}
			if (this.m_frameWidth != null)
			{
				this.m_frameWidth.Initialize("FrameWidth", context);
				context.ExprHostBuilder.BackFrameFrameWidth(this.m_frameWidth);
			}
			if (this.m_glassEffect != null)
			{
				this.m_glassEffect.Initialize("GlassEffect", context);
				context.ExprHostBuilder.BackFrameGlassEffect(this.m_glassEffect);
			}
			if (this.m_frameBackground != null)
			{
				this.m_frameBackground.Initialize(context);
			}
			if (this.m_frameImage != null)
			{
				this.m_frameImage.Initialize(context);
			}
			context.ExprHostBuilder.BackFrameEnd();
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000BAEC8 File Offset: 0x000B90C8
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			BackFrame backFrame = (BackFrame)base.PublishClone(context);
			if (this.m_frameStyle != null)
			{
				backFrame.m_frameStyle = (ExpressionInfo)this.m_frameStyle.PublishClone(context);
			}
			if (this.m_frameShape != null)
			{
				backFrame.m_frameShape = (ExpressionInfo)this.m_frameShape.PublishClone(context);
			}
			if (this.m_frameWidth != null)
			{
				backFrame.m_frameWidth = (ExpressionInfo)this.m_frameWidth.PublishClone(context);
			}
			if (this.m_glassEffect != null)
			{
				backFrame.m_glassEffect = (ExpressionInfo)this.m_glassEffect.PublishClone(context);
			}
			if (this.m_frameBackground != null)
			{
				backFrame.m_frameBackground = (FrameBackground)this.m_frameBackground.PublishClone(context);
			}
			if (this.m_frameImage != null)
			{
				backFrame.m_frameImage = (FrameImage)this.m_frameImage.PublishClone(context);
			}
			return backFrame;
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000BAFA0 File Offset: 0x000B91A0
		internal void SetExprHost(BackFrameExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_frameBackground != null && this.m_exprHost.FrameBackgroundHost != null)
			{
				this.m_frameBackground.SetExprHost(this.m_exprHost.FrameBackgroundHost, reportObjectModel);
			}
			if (this.m_frameImage != null && this.m_exprHost.FrameImageHost != null)
			{
				this.m_frameImage.SetExprHost(this.m_exprHost.FrameImageHost, reportObjectModel);
			}
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000BB028 File Offset: 0x000B9228
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BackFrame, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.FrameStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.FrameShape, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.FrameWidth, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.GlassEffect, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.FrameBackground, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FrameBackground),
				new MemberInfo(MemberName.FrameImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FrameImage)
			});
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000BB0CC File Offset: 0x000B92CC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(BackFrame.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.FrameStyle:
					writer.Write(this.m_frameStyle);
					break;
				case MemberName.FrameShape:
					writer.Write(this.m_frameShape);
					break;
				case MemberName.FrameWidth:
					writer.Write(this.m_frameWidth);
					break;
				case MemberName.GlassEffect:
					writer.Write(this.m_glassEffect);
					break;
				case MemberName.FrameBackground:
					writer.Write(this.m_frameBackground);
					break;
				case MemberName.FrameImage:
					writer.Write(this.m_frameImage);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000BB198 File Offset: 0x000B9398
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(BackFrame.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.FrameStyle:
					this.m_frameStyle = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.FrameShape:
					this.m_frameShape = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.FrameWidth:
					this.m_frameWidth = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.GlassEffect:
					this.m_glassEffect = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.FrameBackground:
					this.m_frameBackground = (FrameBackground)reader.ReadRIFObject();
					break;
				case MemberName.FrameImage:
					this.m_frameImage = (FrameImage)reader.ReadRIFObject();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000BB27F File Offset: 0x000B947F
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BackFrame;
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000BB286 File Offset: 0x000B9486
		internal GaugeFrameStyles EvaluateFrameStyle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeFrameStyles(context.ReportRuntime.EvaluateBackFrameFrameStyleExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000BB2B7 File Offset: 0x000B94B7
		internal GaugeFrameShapes EvaluateFrameShape(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeFrameShapes(context.ReportRuntime.EvaluateBackFrameFrameShapeExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000BB2E8 File Offset: 0x000B94E8
		internal double EvaluateFrameWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateBackFrameFrameWidthExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000BB30E File Offset: 0x000B950E
		internal GaugeGlassEffects EvaluateGlassEffect(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeGlassEffects(context.ReportRuntime.EvaluateBackFrameGlassEffectExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x0400168E RID: 5774
		[NonSerialized]
		private BackFrameExprHost m_exprHost;

		// Token: 0x0400168F RID: 5775
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = BackFrame.GetDeclaration();

		// Token: 0x04001690 RID: 5776
		private ExpressionInfo m_frameStyle;

		// Token: 0x04001691 RID: 5777
		private ExpressionInfo m_frameShape;

		// Token: 0x04001692 RID: 5778
		private ExpressionInfo m_frameWidth;

		// Token: 0x04001693 RID: 5779
		private ExpressionInfo m_glassEffect;

		// Token: 0x04001694 RID: 5780
		private FrameBackground m_frameBackground;

		// Token: 0x04001695 RID: 5781
		private FrameImage m_frameImage;
	}
}
