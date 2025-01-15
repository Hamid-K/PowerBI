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
	// Token: 0x020003D9 RID: 985
	[Serializable]
	internal sealed class FrameImage : BaseGaugeImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060027ED RID: 10221 RVA: 0x000BBD81 File Offset: 0x000B9F81
		internal FrameImage()
		{
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x000BBD89 File Offset: 0x000B9F89
		internal FrameImage(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x000BBD92 File Offset: 0x000B9F92
		// (set) Token: 0x060027F0 RID: 10224 RVA: 0x000BBD9A File Offset: 0x000B9F9A
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

		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x060027F1 RID: 10225 RVA: 0x000BBDA3 File Offset: 0x000B9FA3
		// (set) Token: 0x060027F2 RID: 10226 RVA: 0x000BBDAB File Offset: 0x000B9FAB
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

		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x060027F3 RID: 10227 RVA: 0x000BBDB4 File Offset: 0x000B9FB4
		// (set) Token: 0x060027F4 RID: 10228 RVA: 0x000BBDBC File Offset: 0x000B9FBC
		internal ExpressionInfo ClipImage
		{
			get
			{
				return this.m_clipImage;
			}
			set
			{
				this.m_clipImage = value;
			}
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000BBDC8 File Offset: 0x000B9FC8
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.FrameImageStart();
			base.Initialize(context);
			if (this.m_hueColor != null)
			{
				this.m_hueColor.Initialize("HueColor", context);
				context.ExprHostBuilder.FrameImageHueColor(this.m_hueColor);
			}
			if (this.m_transparency != null)
			{
				this.m_transparency.Initialize("Transparency", context);
				context.ExprHostBuilder.FrameImageTransparency(this.m_transparency);
			}
			if (this.m_clipImage != null)
			{
				this.m_clipImage.Initialize("ClipImage", context);
				context.ExprHostBuilder.FrameImageClipImage(this.m_clipImage);
			}
			context.ExprHostBuilder.FrameImageEnd();
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000BBE78 File Offset: 0x000BA078
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			FrameImage frameImage = (FrameImage)base.PublishClone(context);
			if (this.m_hueColor != null)
			{
				frameImage.m_hueColor = (ExpressionInfo)this.m_hueColor.PublishClone(context);
			}
			if (this.m_transparency != null)
			{
				frameImage.m_transparency = (ExpressionInfo)this.m_transparency.PublishClone(context);
			}
			if (this.m_clipImage != null)
			{
				frameImage.m_clipImage = (ExpressionInfo)this.m_clipImage.PublishClone(context);
			}
			return frameImage;
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x000BBEF0 File Offset: 0x000BA0F0
		internal void SetExprHost(FrameImageExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000BBF18 File Offset: 0x000BA118
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FrameImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BaseGaugeImage, new List<MemberInfo>
			{
				new MemberInfo(MemberName.HueColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Transparency, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ClipImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000BBF7C File Offset: 0x000BA17C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(FrameImage.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.HueColor:
					writer.Write(this.m_hueColor);
					break;
				case MemberName.Transparency:
					writer.Write(this.m_transparency);
					break;
				case MemberName.ClipImage:
					writer.Write(this.m_clipImage);
					break;
				}
			}
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000BC004 File Offset: 0x000BA204
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(FrameImage.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.HueColor:
					this.m_hueColor = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.Transparency:
					this.m_transparency = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.ClipImage:
					this.m_clipImage = (ExpressionInfo)reader.ReadRIFObject();
					break;
				}
			}
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000BC098 File Offset: 0x000BA298
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.FrameImage;
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x000BC09F File Offset: 0x000BA29F
		internal string EvaluateHueColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateFrameImageHueColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x000BC0C5 File Offset: 0x000BA2C5
		internal double EvaluateTransparency(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateFrameImageTransparencyExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x000BC0EB File Offset: 0x000BA2EB
		internal bool EvaluateClipImage(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateFrameImageClipImageExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x040016A3 RID: 5795
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = FrameImage.GetDeclaration();

		// Token: 0x040016A4 RID: 5796
		private ExpressionInfo m_hueColor;

		// Token: 0x040016A5 RID: 5797
		private ExpressionInfo m_transparency;

		// Token: 0x040016A6 RID: 5798
		private ExpressionInfo m_clipImage;
	}
}
