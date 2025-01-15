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
	// Token: 0x020003DF RID: 991
	[Serializable]
	internal class Gauge : GaugePanelItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002873 RID: 10355 RVA: 0x000BD889 File Offset: 0x000BBA89
		internal Gauge()
		{
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000BD891 File Offset: 0x000BBA91
		internal Gauge(GaugePanel gaugePanel, int id)
			: base(gaugePanel, id)
		{
		}

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x000BD89B File Offset: 0x000BBA9B
		// (set) Token: 0x06002876 RID: 10358 RVA: 0x000BD8A3 File Offset: 0x000BBAA3
		internal BackFrame BackFrame
		{
			get
			{
				return this.m_backFrame;
			}
			set
			{
				this.m_backFrame = value;
			}
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x06002877 RID: 10359 RVA: 0x000BD8AC File Offset: 0x000BBAAC
		// (set) Token: 0x06002878 RID: 10360 RVA: 0x000BD8B4 File Offset: 0x000BBAB4
		internal ExpressionInfo ClipContent
		{
			get
			{
				return this.m_clipContent;
			}
			set
			{
				this.m_clipContent = value;
			}
		}

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x06002879 RID: 10361 RVA: 0x000BD8BD File Offset: 0x000BBABD
		// (set) Token: 0x0600287A RID: 10362 RVA: 0x000BD8C5 File Offset: 0x000BBAC5
		internal TopImage TopImage
		{
			get
			{
				return this.m_topImage;
			}
			set
			{
				this.m_topImage = value;
			}
		}

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x0600287B RID: 10363 RVA: 0x000BD8CE File Offset: 0x000BBACE
		// (set) Token: 0x0600287C RID: 10364 RVA: 0x000BD8D6 File Offset: 0x000BBAD6
		internal ExpressionInfo AspectRatio
		{
			get
			{
				return this.m_aspectRatio;
			}
			set
			{
				this.m_aspectRatio = value;
			}
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000BD8E0 File Offset: 0x000BBAE0
		internal override void Initialize(InitializationContext context)
		{
			base.Initialize(context);
			if (this.m_backFrame != null)
			{
				this.m_backFrame.Initialize(context);
			}
			if (this.m_clipContent != null)
			{
				this.m_clipContent.Initialize("ClipContent", context);
				context.ExprHostBuilder.GaugeClipContent(this.m_clipContent);
			}
			if (this.m_topImage != null)
			{
				this.m_topImage.Initialize(context);
			}
			if (this.m_aspectRatio != null)
			{
				this.m_aspectRatio.Initialize("AspectRatio", context);
				context.ExprHostBuilder.GaugeAspectRatio(this.m_aspectRatio);
			}
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000BD974 File Offset: 0x000BBB74
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Gauge gauge = (Gauge)base.PublishClone(context);
			if (this.m_backFrame != null)
			{
				gauge.m_backFrame = (BackFrame)this.m_backFrame.PublishClone(context);
			}
			if (this.m_clipContent != null)
			{
				gauge.m_clipContent = (ExpressionInfo)this.m_clipContent.PublishClone(context);
			}
			if (this.m_topImage != null)
			{
				gauge.m_topImage = (TopImage)this.m_topImage.PublishClone(context);
			}
			if (this.m_aspectRatio != null)
			{
				gauge.m_aspectRatio = (ExpressionInfo)this.m_aspectRatio.PublishClone(context);
			}
			return gauge;
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x000BDA0C File Offset: 0x000BBC0C
		internal void SetExprHost(GaugeExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_backFrame != null && ((GaugeExprHost)this.m_exprHost).BackFrameHost != null)
			{
				this.m_backFrame.SetExprHost(((GaugeExprHost)this.m_exprHost).BackFrameHost, reportObjectModel);
			}
			if (this.m_topImage != null && ((GaugeExprHost)this.m_exprHost).TopImageHost != null)
			{
				this.m_topImage.SetExprHost(((GaugeExprHost)this.m_exprHost).TopImageHost, reportObjectModel);
			}
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x000BDAA8 File Offset: 0x000BBCA8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Gauge, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.BackFrame, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BackFrame),
				new MemberInfo(MemberName.ClipContent, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TopImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TopImage),
				new MemberInfo(MemberName.AspectRatio, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000BDB20 File Offset: 0x000BBD20
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Gauge.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.BackFrame:
					writer.Write(this.m_backFrame);
					break;
				case MemberName.ClipContent:
					writer.Write(this.m_clipContent);
					break;
				case MemberName.TopImage:
					writer.Write(this.m_topImage);
					break;
				default:
					if (memberName != MemberName.AspectRatio)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_aspectRatio);
					}
					break;
				}
			}
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000BDBC4 File Offset: 0x000BBDC4
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Gauge.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.BackFrame:
					this.m_backFrame = (BackFrame)reader.ReadRIFObject();
					break;
				case MemberName.ClipContent:
					this.m_clipContent = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.TopImage:
					this.m_topImage = (TopImage)reader.ReadRIFObject();
					break;
				default:
					if (memberName != MemberName.AspectRatio)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_aspectRatio = (ExpressionInfo)reader.ReadRIFObject();
					}
					break;
				}
			}
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000BDC7F File Offset: 0x000BBE7F
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Gauge;
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000BDC86 File Offset: 0x000BBE86
		internal bool EvaluateClipContent(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeClipContentExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x000BDCAC File Offset: 0x000BBEAC
		internal double EvaluateAspectRatio(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateGaugeAspectRatioExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x040016C4 RID: 5828
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Gauge.GetDeclaration();

		// Token: 0x040016C5 RID: 5829
		private BackFrame m_backFrame;

		// Token: 0x040016C6 RID: 5830
		private ExpressionInfo m_clipContent;

		// Token: 0x040016C7 RID: 5831
		private TopImage m_topImage;

		// Token: 0x040016C8 RID: 5832
		private ExpressionInfo m_aspectRatio;
	}
}
