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
	// Token: 0x020003E3 RID: 995
	[Serializable]
	internal sealed class PinLabel : GaugePanelStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060028C2 RID: 10434 RVA: 0x000BEA70 File Offset: 0x000BCC70
		internal PinLabel()
		{
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x000BEA78 File Offset: 0x000BCC78
		internal PinLabel(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x060028C4 RID: 10436 RVA: 0x000BEA81 File Offset: 0x000BCC81
		// (set) Token: 0x060028C5 RID: 10437 RVA: 0x000BEA89 File Offset: 0x000BCC89
		internal ExpressionInfo Text
		{
			get
			{
				return this.m_text;
			}
			set
			{
				this.m_text = value;
			}
		}

		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x060028C6 RID: 10438 RVA: 0x000BEA92 File Offset: 0x000BCC92
		// (set) Token: 0x060028C7 RID: 10439 RVA: 0x000BEA9A File Offset: 0x000BCC9A
		internal ExpressionInfo AllowUpsideDown
		{
			get
			{
				return this.m_allowUpsideDown;
			}
			set
			{
				this.m_allowUpsideDown = value;
			}
		}

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x060028C8 RID: 10440 RVA: 0x000BEAA3 File Offset: 0x000BCCA3
		// (set) Token: 0x060028C9 RID: 10441 RVA: 0x000BEAAB File Offset: 0x000BCCAB
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

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x060028CA RID: 10442 RVA: 0x000BEAB4 File Offset: 0x000BCCB4
		// (set) Token: 0x060028CB RID: 10443 RVA: 0x000BEABC File Offset: 0x000BCCBC
		internal ExpressionInfo FontAngle
		{
			get
			{
				return this.m_fontAngle;
			}
			set
			{
				this.m_fontAngle = value;
			}
		}

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x060028CC RID: 10444 RVA: 0x000BEAC5 File Offset: 0x000BCCC5
		// (set) Token: 0x060028CD RID: 10445 RVA: 0x000BEACD File Offset: 0x000BCCCD
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

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x060028CE RID: 10446 RVA: 0x000BEAD6 File Offset: 0x000BCCD6
		// (set) Token: 0x060028CF RID: 10447 RVA: 0x000BEADE File Offset: 0x000BCCDE
		internal ExpressionInfo RotateLabel
		{
			get
			{
				return this.m_rotateLabel;
			}
			set
			{
				this.m_rotateLabel = value;
			}
		}

		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x060028D0 RID: 10448 RVA: 0x000BEAE7 File Offset: 0x000BCCE7
		// (set) Token: 0x060028D1 RID: 10449 RVA: 0x000BEAEF File Offset: 0x000BCCEF
		internal ExpressionInfo UseFontPercent
		{
			get
			{
				return this.m_useFontPercent;
			}
			set
			{
				this.m_useFontPercent = value;
			}
		}

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x060028D2 RID: 10450 RVA: 0x000BEAF8 File Offset: 0x000BCCF8
		internal string OwnerName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x060028D3 RID: 10451 RVA: 0x000BEB05 File Offset: 0x000BCD05
		internal PinLabelExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000BEB10 File Offset: 0x000BCD10
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.PinLabelStart();
			base.Initialize(context);
			if (this.m_text != null)
			{
				this.m_text.Initialize("Text", context);
				context.ExprHostBuilder.PinLabelText(this.m_text);
			}
			if (this.m_allowUpsideDown != null)
			{
				this.m_allowUpsideDown.Initialize("AllowUpsideDown", context);
				context.ExprHostBuilder.PinLabelAllowUpsideDown(this.m_allowUpsideDown);
			}
			if (this.m_distanceFromScale != null)
			{
				this.m_distanceFromScale.Initialize("DistanceFromScale", context);
				context.ExprHostBuilder.PinLabelDistanceFromScale(this.m_distanceFromScale);
			}
			if (this.m_fontAngle != null)
			{
				this.m_fontAngle.Initialize("FontAngle", context);
				context.ExprHostBuilder.PinLabelFontAngle(this.m_fontAngle);
			}
			if (this.m_placement != null)
			{
				this.m_placement.Initialize("Placement", context);
				context.ExprHostBuilder.PinLabelPlacement(this.m_placement);
			}
			if (this.m_rotateLabel != null)
			{
				this.m_rotateLabel.Initialize("RotateLabel", context);
				context.ExprHostBuilder.PinLabelRotateLabel(this.m_rotateLabel);
			}
			if (this.m_useFontPercent != null)
			{
				this.m_useFontPercent.Initialize("UseFontPercent", context);
				context.ExprHostBuilder.PinLabelUseFontPercent(this.m_useFontPercent);
			}
			context.ExprHostBuilder.PinLabelEnd();
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000BEC6C File Offset: 0x000BCE6C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			PinLabel pinLabel = (PinLabel)base.PublishClone(context);
			if (this.m_text != null)
			{
				pinLabel.m_text = (ExpressionInfo)this.m_text.PublishClone(context);
			}
			if (this.m_allowUpsideDown != null)
			{
				pinLabel.m_allowUpsideDown = (ExpressionInfo)this.m_allowUpsideDown.PublishClone(context);
			}
			if (this.m_distanceFromScale != null)
			{
				pinLabel.m_distanceFromScale = (ExpressionInfo)this.m_distanceFromScale.PublishClone(context);
			}
			if (this.m_fontAngle != null)
			{
				pinLabel.m_fontAngle = (ExpressionInfo)this.m_fontAngle.PublishClone(context);
			}
			if (this.m_placement != null)
			{
				pinLabel.m_placement = (ExpressionInfo)this.m_placement.PublishClone(context);
			}
			if (this.m_rotateLabel != null)
			{
				pinLabel.m_rotateLabel = (ExpressionInfo)this.m_rotateLabel.PublishClone(context);
			}
			if (this.m_useFontPercent != null)
			{
				pinLabel.m_useFontPercent = (ExpressionInfo)this.m_useFontPercent.PublishClone(context);
			}
			return pinLabel;
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000BED60 File Offset: 0x000BCF60
		internal void SetExprHost(PinLabelExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000BED88 File Offset: 0x000BCF88
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PinLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Text, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.AllowUpsideDown, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DistanceFromScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.FontAngle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Placement, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.RotateLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.UseFontPercent, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000BEE40 File Offset: 0x000BD040
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(PinLabel.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.AllowUpsideDown)
				{
					if (memberName == MemberName.Text)
					{
						writer.Write(this.m_text);
						continue;
					}
					if (memberName == MemberName.AllowUpsideDown)
					{
						writer.Write(this.m_allowUpsideDown);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DistanceFromScale)
					{
						writer.Write(this.m_distanceFromScale);
						continue;
					}
					switch (memberName)
					{
					case MemberName.Placement:
						writer.Write(this.m_placement);
						continue;
					case MemberName.RotateLabel:
						writer.Write(this.m_rotateLabel);
						continue;
					case MemberName.UseFontPercent:
						writer.Write(this.m_useFontPercent);
						continue;
					default:
						if (memberName == MemberName.FontAngle)
						{
							writer.Write(this.m_fontAngle);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000BEF3C File Offset: 0x000BD13C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(PinLabel.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.AllowUpsideDown)
				{
					if (memberName == MemberName.Text)
					{
						this.m_text = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.AllowUpsideDown)
					{
						this.m_allowUpsideDown = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DistanceFromScale)
					{
						this.m_distanceFromScale = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					switch (memberName)
					{
					case MemberName.Placement:
						this.m_placement = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.RotateLabel:
						this.m_rotateLabel = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.UseFontPercent:
						this.m_useFontPercent = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.FontAngle)
						{
							this.m_fontAngle = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000BF05E File Offset: 0x000BD25E
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PinLabel;
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000BF065 File Offset: 0x000BD265
		internal string EvaluateText(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluatePinLabelTextExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000BF08B File Offset: 0x000BD28B
		internal bool EvaluateAllowUpsideDown(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluatePinLabelAllowUpsideDownExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x000BF0B1 File Offset: 0x000BD2B1
		internal double EvaluateDistanceFromScale(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluatePinLabelDistanceFromScaleExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000BF0D7 File Offset: 0x000BD2D7
		internal double EvaluateFontAngle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluatePinLabelFontAngleExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000BF0FD File Offset: 0x000BD2FD
		internal GaugeLabelPlacements EvaluatePlacement(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeLabelPlacements(context.ReportRuntime.EvaluatePinLabelPlacementExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x000BF12E File Offset: 0x000BD32E
		internal bool EvaluateRotateLabel(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluatePinLabelRotateLabelExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000BF154 File Offset: 0x000BD354
		internal bool EvaluateUseFontPercent(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluatePinLabelUseFontPercentExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x040016D7 RID: 5847
		[NonSerialized]
		private PinLabelExprHost m_exprHost;

		// Token: 0x040016D8 RID: 5848
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = PinLabel.GetDeclaration();

		// Token: 0x040016D9 RID: 5849
		private ExpressionInfo m_text;

		// Token: 0x040016DA RID: 5850
		private ExpressionInfo m_allowUpsideDown;

		// Token: 0x040016DB RID: 5851
		private ExpressionInfo m_distanceFromScale;

		// Token: 0x040016DC RID: 5852
		private ExpressionInfo m_fontAngle;

		// Token: 0x040016DD RID: 5853
		private ExpressionInfo m_placement;

		// Token: 0x040016DE RID: 5854
		private ExpressionInfo m_rotateLabel;

		// Token: 0x040016DF RID: 5855
		private ExpressionInfo m_useFontPercent;
	}
}
