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
	// Token: 0x020003DD RID: 989
	[Serializable]
	internal sealed class GaugeImage : GaugePanelItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002834 RID: 10292 RVA: 0x000BCA20 File Offset: 0x000BAC20
		internal GaugeImage()
		{
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x000BCA28 File Offset: 0x000BAC28
		internal GaugeImage(GaugePanel gaugePanel, int id)
			: base(gaugePanel, id)
		{
		}

		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x06002836 RID: 10294 RVA: 0x000BCA32 File Offset: 0x000BAC32
		// (set) Token: 0x06002837 RID: 10295 RVA: 0x000BCA3A File Offset: 0x000BAC3A
		internal ExpressionInfo Source
		{
			get
			{
				return this.m_source;
			}
			set
			{
				this.m_source = value;
			}
		}

		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x06002838 RID: 10296 RVA: 0x000BCA43 File Offset: 0x000BAC43
		// (set) Token: 0x06002839 RID: 10297 RVA: 0x000BCA4B File Offset: 0x000BAC4B
		internal ExpressionInfo Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x000BCA54 File Offset: 0x000BAC54
		// (set) Token: 0x0600283B RID: 10299 RVA: 0x000BCA5C File Offset: 0x000BAC5C
		internal ExpressionInfo TransparentColor
		{
			get
			{
				return this.m_transparentColor;
			}
			set
			{
				this.m_transparentColor = value;
			}
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x000BCA68 File Offset: 0x000BAC68
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.GaugeImageStart(this.m_name);
			base.Initialize(context);
			if (this.m_source != null)
			{
				this.m_source.Initialize("Source", context);
				context.ExprHostBuilder.BaseGaugeImageSource(this.m_source);
			}
			if (this.m_value != null)
			{
				this.m_value.Initialize("Value", context);
				context.ExprHostBuilder.BaseGaugeImageValue(this.m_value);
			}
			if (this.m_transparentColor != null)
			{
				this.m_transparentColor.Initialize("TransparentColor", context);
				context.ExprHostBuilder.BaseGaugeImageTransparentColor(this.m_transparentColor);
			}
			this.m_exprHostID = context.ExprHostBuilder.GaugeImageEnd();
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x000BCB24 File Offset: 0x000BAD24
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			GaugeImage gaugeImage = (GaugeImage)base.PublishClone(context);
			if (this.m_source != null)
			{
				gaugeImage.m_source = (ExpressionInfo)this.m_source.PublishClone(context);
			}
			if (this.m_value != null)
			{
				gaugeImage.m_value = (ExpressionInfo)this.m_value.PublishClone(context);
			}
			if (this.m_transparentColor != null)
			{
				gaugeImage.m_transparentColor = (ExpressionInfo)this.m_transparentColor.PublishClone(context);
			}
			return gaugeImage;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000BCB9C File Offset: 0x000BAD9C
		internal void SetExprHost(GaugeImageExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x000BCBC4 File Offset: 0x000BADC4
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Source, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TransparentColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x000BCC24 File Offset: 0x000BAE24
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(GaugeImage.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Value)
				{
					if (memberName != MemberName.Source)
					{
						if (memberName != MemberName.TransparentColor)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_transparentColor);
						}
					}
					else
					{
						writer.Write(this.m_source);
					}
				}
				else
				{
					writer.Write(this.m_value);
				}
			}
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x000BCCAC File Offset: 0x000BAEAC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(GaugeImage.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Value)
				{
					if (memberName != MemberName.Source)
					{
						if (memberName != MemberName.TransparentColor)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_transparentColor = (ExpressionInfo)reader.ReadRIFObject();
						}
					}
					else
					{
						this.m_source = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_value = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000BCD42 File Offset: 0x000BAF42
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugeImage;
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x000BCD49 File Offset: 0x000BAF49
		internal Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType EvaluateSource(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateImageSourceType(context.ReportRuntime.EvaluateGaugeImageSourceExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000BCD7A File Offset: 0x000BAF7A
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateValue(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeImageValueExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000BCDA0 File Offset: 0x000BAFA0
		internal string EvaluateTransparentColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeImageTransparentColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x040016B1 RID: 5809
		private ExpressionInfo m_source;

		// Token: 0x040016B2 RID: 5810
		private ExpressionInfo m_value;

		// Token: 0x040016B3 RID: 5811
		private ExpressionInfo m_transparentColor;

		// Token: 0x040016B4 RID: 5812
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = GaugeImage.GetDeclaration();
	}
}
