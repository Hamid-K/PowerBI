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
	// Token: 0x020003DC RID: 988
	[Serializable]
	internal sealed class IndicatorImage : BaseGaugeImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002823 RID: 10275 RVA: 0x000BC73D File Offset: 0x000BA93D
		internal IndicatorImage()
		{
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000BC745 File Offset: 0x000BA945
		internal IndicatorImage(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x06002825 RID: 10277 RVA: 0x000BC74E File Offset: 0x000BA94E
		// (set) Token: 0x06002826 RID: 10278 RVA: 0x000BC756 File Offset: 0x000BA956
		internal ExpressionInfo HueColor
		{
			get
			{
				return this.m_hueColor;
			}
			set
			{
				this.m_hueColor = value;
			}
		}

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x06002827 RID: 10279 RVA: 0x000BC75F File Offset: 0x000BA95F
		// (set) Token: 0x06002828 RID: 10280 RVA: 0x000BC767 File Offset: 0x000BA967
		internal ExpressionInfo Transparency
		{
			get
			{
				return this.m_transparency;
			}
			set
			{
				this.m_transparency = value;
			}
		}

		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x000BC770 File Offset: 0x000BA970
		internal new IndicatorImageExprHost ExprHost
		{
			get
			{
				return (IndicatorImageExprHost)this.m_exprHost;
			}
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000BC780 File Offset: 0x000BA980
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.IndicatorImageStart();
			base.Initialize(context);
			if (this.m_hueColor != null)
			{
				this.m_hueColor.Initialize("HueColor", context);
				context.ExprHostBuilder.IndicatorImageHueColor(this.m_hueColor);
			}
			if (this.m_transparency != null)
			{
				this.m_transparency.Initialize("Transparency", context);
				context.ExprHostBuilder.IndicatorImageTransparency(this.m_transparency);
			}
			context.ExprHostBuilder.IndicatorImageEnd();
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000BC804 File Offset: 0x000BAA04
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			IndicatorImage indicatorImage = (IndicatorImage)base.PublishClone(context);
			if (this.m_hueColor != null)
			{
				indicatorImage.m_hueColor = (ExpressionInfo)this.m_hueColor.PublishClone(context);
			}
			if (this.m_transparency != null)
			{
				indicatorImage.m_transparency = (ExpressionInfo)this.m_transparency.PublishClone(context);
			}
			return indicatorImage;
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x000BC85D File Offset: 0x000BAA5D
		internal void SetExprHost(IndicatorImageExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			base.SetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x000BC880 File Offset: 0x000BAA80
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IndicatorImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BaseGaugeImage, new List<MemberInfo>
			{
				new MemberInfo(MemberName.HueColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Transparency, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x000BC8D0 File Offset: 0x000BAAD0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(IndicatorImage.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.HueColor)
				{
					if (memberName != MemberName.Transparency)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_transparency);
					}
				}
				else
				{
					writer.Write(this.m_hueColor);
				}
			}
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x000BC944 File Offset: 0x000BAB44
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(IndicatorImage.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.HueColor)
				{
					if (memberName != MemberName.Transparency)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_transparency = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_hueColor = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x000BC9C1 File Offset: 0x000BABC1
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IndicatorImage;
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x000BC9C8 File Offset: 0x000BABC8
		internal string EvaluateHueColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateIndicatorImageHueColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x000BC9EE File Offset: 0x000BABEE
		internal double EvaluateTransparency(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateIndicatorImageTransparencyExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x040016AE RID: 5806
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = IndicatorImage.GetDeclaration();

		// Token: 0x040016AF RID: 5807
		private ExpressionInfo m_hueColor;

		// Token: 0x040016B0 RID: 5808
		private ExpressionInfo m_transparency;
	}
}
