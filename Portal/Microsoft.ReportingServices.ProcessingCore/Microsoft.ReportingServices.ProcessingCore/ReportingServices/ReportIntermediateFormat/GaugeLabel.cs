using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003E2 RID: 994
	[Serializable]
	internal sealed class GaugeLabel : GaugePanelItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060028A8 RID: 10408 RVA: 0x000BE4AF File Offset: 0x000BC6AF
		internal GaugeLabel()
		{
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x000BE4B7 File Offset: 0x000BC6B7
		internal GaugeLabel(GaugePanel gaugePanel, int id)
			: base(gaugePanel, id)
		{
		}

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x000BE4C1 File Offset: 0x000BC6C1
		// (set) Token: 0x060028AB RID: 10411 RVA: 0x000BE4C9 File Offset: 0x000BC6C9
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

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x060028AC RID: 10412 RVA: 0x000BE4D2 File Offset: 0x000BC6D2
		// (set) Token: 0x060028AD RID: 10413 RVA: 0x000BE4DA File Offset: 0x000BC6DA
		internal ExpressionInfo Angle
		{
			get
			{
				return this.m_angle;
			}
			set
			{
				this.m_angle = value;
			}
		}

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x000BE4E3 File Offset: 0x000BC6E3
		// (set) Token: 0x060028AF RID: 10415 RVA: 0x000BE4EB File Offset: 0x000BC6EB
		internal ExpressionInfo ResizeMode
		{
			get
			{
				return this.m_resizeMode;
			}
			set
			{
				this.m_resizeMode = value;
			}
		}

		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x000BE4F4 File Offset: 0x000BC6F4
		// (set) Token: 0x060028B1 RID: 10417 RVA: 0x000BE4FC File Offset: 0x000BC6FC
		internal ExpressionInfo TextShadowOffset
		{
			get
			{
				return this.m_textShadowOffset;
			}
			set
			{
				this.m_textShadowOffset = value;
			}
		}

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x000BE505 File Offset: 0x000BC705
		// (set) Token: 0x060028B3 RID: 10419 RVA: 0x000BE50D File Offset: 0x000BC70D
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

		// Token: 0x060028B4 RID: 10420 RVA: 0x000BE518 File Offset: 0x000BC718
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.GaugeLabelStart(this.m_name);
			base.Initialize(context);
			if (this.m_text != null)
			{
				this.m_text.Initialize("Text", context);
				context.ExprHostBuilder.GaugeLabelText(this.m_text);
			}
			if (this.m_angle != null)
			{
				this.m_angle.Initialize("Angle", context);
				context.ExprHostBuilder.GaugeLabelAngle(this.m_angle);
			}
			if (this.m_resizeMode != null)
			{
				this.m_resizeMode.Initialize("ResizeMode", context);
				context.ExprHostBuilder.GaugeLabelResizeMode(this.m_resizeMode);
			}
			if (this.m_textShadowOffset != null)
			{
				this.m_textShadowOffset.Initialize("TextShadowOffset", context);
				context.ExprHostBuilder.GaugeLabelTextShadowOffset(this.m_textShadowOffset);
			}
			if (this.m_useFontPercent != null)
			{
				this.m_useFontPercent.Initialize("UseFontPercent", context);
				context.ExprHostBuilder.GaugeLabelUseFontPercent(this.m_useFontPercent);
			}
			this.m_exprHostID = context.ExprHostBuilder.GaugeLabelEnd();
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000BE628 File Offset: 0x000BC828
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			GaugeLabel gaugeLabel = (GaugeLabel)base.PublishClone(context);
			if (this.m_text != null)
			{
				gaugeLabel.m_text = (ExpressionInfo)this.m_text.PublishClone(context);
			}
			if (this.m_angle != null)
			{
				gaugeLabel.m_angle = (ExpressionInfo)this.m_angle.PublishClone(context);
			}
			if (this.m_resizeMode != null)
			{
				gaugeLabel.m_resizeMode = (ExpressionInfo)this.m_resizeMode.PublishClone(context);
			}
			if (this.m_textShadowOffset != null)
			{
				gaugeLabel.m_textShadowOffset = (ExpressionInfo)this.m_textShadowOffset.PublishClone(context);
			}
			if (this.m_useFontPercent != null)
			{
				gaugeLabel.m_useFontPercent = (ExpressionInfo)this.m_useFontPercent.PublishClone(context);
			}
			return gaugeLabel;
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000BE6DE File Offset: 0x000BC8DE
		internal void SetExprHost(GaugeLabelExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x000BE704 File Offset: 0x000BC904
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeLabel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Text, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Angle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ResizeMode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TextShadowOffset, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.UseFontPercent, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x000BE790 File Offset: 0x000BC990
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(GaugeLabel.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Angle)
				{
					if (memberName == MemberName.Text)
					{
						writer.Write(this.m_text);
						continue;
					}
					if (memberName == MemberName.Angle)
					{
						writer.Write(this.m_angle);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.UseFontPercent)
					{
						writer.Write(this.m_useFontPercent);
						continue;
					}
					if (memberName == MemberName.ResizeMode)
					{
						writer.Write(this.m_resizeMode);
						continue;
					}
					if (memberName == MemberName.TextShadowOffset)
					{
						writer.Write(this.m_textShadowOffset);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x000BE858 File Offset: 0x000BCA58
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(GaugeLabel.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Angle)
				{
					if (memberName == MemberName.Text)
					{
						this.m_text = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Angle)
					{
						this.m_angle = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.UseFontPercent)
					{
						this.m_useFontPercent = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.ResizeMode)
					{
						this.m_resizeMode = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.TextShadowOffset)
					{
						this.m_textShadowOffset = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000BE939 File Offset: 0x000BCB39
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeLabel;
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x000BE940 File Offset: 0x000BCB40
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateText(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeLabelTextExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x000BE968 File Offset: 0x000BCB68
		internal string FormatText(Microsoft.ReportingServices.RdlExpressions.VariantResult result, OnDemandProcessingContext context)
		{
			string text = null;
			if (result.ErrorOccurred)
			{
				text = RPRes.rsExpressionErrorValue;
			}
			else if (result.Value != null)
			{
				text = Formatter.Format(result.Value, ref this.m_formatter, this.m_gaugePanel.StyleClass, this.m_styleClass, context, Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel, this.m_gaugePanel.Name);
			}
			return text;
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x000BE9C1 File Offset: 0x000BCBC1
		internal double EvaluateAngle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeLabelAngleExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x000BE9E7 File Offset: 0x000BCBE7
		internal GaugeResizeModes EvaluateResizeMode(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeResizeModes(context.ReportRuntime.EvaluateGaugeLabelResizeModeExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x000BEA18 File Offset: 0x000BCC18
		internal string EvaluateTextShadowOffset(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeLabelTextShadowOffsetExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000BEA3E File Offset: 0x000BCC3E
		internal bool EvaluateUseFontPercent(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeLabelUseFontPercentExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x040016D0 RID: 5840
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = GaugeLabel.GetDeclaration();

		// Token: 0x040016D1 RID: 5841
		[NonSerialized]
		private Formatter m_formatter;

		// Token: 0x040016D2 RID: 5842
		private ExpressionInfo m_text;

		// Token: 0x040016D3 RID: 5843
		private ExpressionInfo m_angle;

		// Token: 0x040016D4 RID: 5844
		private ExpressionInfo m_resizeMode;

		// Token: 0x040016D5 RID: 5845
		private ExpressionInfo m_textShadowOffset;

		// Token: 0x040016D6 RID: 5846
		private ExpressionInfo m_useFontPercent;
	}
}
