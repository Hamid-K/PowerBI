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
	// Token: 0x020003DB RID: 987
	[Serializable]
	internal sealed class TopImage : BaseGaugeImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002816 RID: 10262 RVA: 0x000BC562 File Offset: 0x000BA762
		internal TopImage()
		{
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x000BC56A File Offset: 0x000BA76A
		internal TopImage(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x06002818 RID: 10264 RVA: 0x000BC573 File Offset: 0x000BA773
		// (set) Token: 0x06002819 RID: 10265 RVA: 0x000BC57B File Offset: 0x000BA77B
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

		// Token: 0x0600281A RID: 10266 RVA: 0x000BC584 File Offset: 0x000BA784
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.TopImageStart();
			base.Initialize(context);
			if (this.m_hueColor != null)
			{
				this.m_hueColor.Initialize("HueColor", context);
				context.ExprHostBuilder.TopImageHueColor(this.m_hueColor);
			}
			context.ExprHostBuilder.TopImageEnd();
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x000BC5DC File Offset: 0x000BA7DC
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			TopImage topImage = (TopImage)base.PublishClone(context);
			if (this.m_hueColor != null)
			{
				topImage.m_hueColor = (ExpressionInfo)this.m_hueColor.PublishClone(context);
			}
			return topImage;
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x000BC616 File Offset: 0x000BA816
		internal void SetExprHost(TopImageExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000BC63C File Offset: 0x000BA83C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TopImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BaseGaugeImage, new List<MemberInfo>
			{
				new MemberInfo(MemberName.HueColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000BC674 File Offset: 0x000BA874
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(TopImage.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.HueColor)
				{
					writer.Write(this.m_hueColor);
				}
			}
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x000BC6B4 File Offset: 0x000BA8B4
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(TopImage.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.HueColor)
				{
					this.m_hueColor = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000BC704 File Offset: 0x000BA904
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TopImage;
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000BC70B File Offset: 0x000BA90B
		internal string EvaluateHueColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateTopImageHueColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x040016AC RID: 5804
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = TopImage.GetDeclaration();

		// Token: 0x040016AD RID: 5805
		private ExpressionInfo m_hueColor;
	}
}
