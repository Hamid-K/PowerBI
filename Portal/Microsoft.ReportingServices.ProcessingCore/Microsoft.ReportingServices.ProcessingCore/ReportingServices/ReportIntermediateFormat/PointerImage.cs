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
	// Token: 0x020003DA RID: 986
	[Serializable]
	internal sealed class PointerImage : BaseGaugeImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002800 RID: 10240 RVA: 0x000BC11D File Offset: 0x000BA31D
		internal PointerImage()
		{
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x000BC125 File Offset: 0x000BA325
		internal PointerImage(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x06002802 RID: 10242 RVA: 0x000BC12E File Offset: 0x000BA32E
		// (set) Token: 0x06002803 RID: 10243 RVA: 0x000BC136 File Offset: 0x000BA336
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

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x06002804 RID: 10244 RVA: 0x000BC13F File Offset: 0x000BA33F
		// (set) Token: 0x06002805 RID: 10245 RVA: 0x000BC147 File Offset: 0x000BA347
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

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x06002806 RID: 10246 RVA: 0x000BC150 File Offset: 0x000BA350
		// (set) Token: 0x06002807 RID: 10247 RVA: 0x000BC158 File Offset: 0x000BA358
		internal ExpressionInfo OffsetX
		{
			get
			{
				return this.m_offsetX;
			}
			set
			{
				this.m_offsetX = value;
			}
		}

		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x000BC161 File Offset: 0x000BA361
		// (set) Token: 0x06002809 RID: 10249 RVA: 0x000BC169 File Offset: 0x000BA369
		internal ExpressionInfo OffsetY
		{
			get
			{
				return this.m_offsetY;
			}
			set
			{
				this.m_offsetY = value;
			}
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x000BC174 File Offset: 0x000BA374
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.PointerImageStart();
			base.Initialize(context);
			if (this.m_hueColor != null)
			{
				this.m_hueColor.Initialize("HueColor", context);
				context.ExprHostBuilder.PointerImageHueColor(this.m_hueColor);
			}
			if (this.m_transparency != null)
			{
				this.m_transparency.Initialize("Transparency", context);
				context.ExprHostBuilder.PointerImageTransparency(this.m_transparency);
			}
			if (this.m_offsetX != null)
			{
				this.m_offsetX.Initialize("OffsetX", context);
				context.ExprHostBuilder.PointerImageOffsetX(this.m_offsetX);
			}
			if (this.m_offsetY != null)
			{
				this.m_offsetY.Initialize("OffsetY", context);
				context.ExprHostBuilder.PointerImageOffsetY(this.m_offsetY);
			}
			context.ExprHostBuilder.PointerImageEnd();
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000BC24C File Offset: 0x000BA44C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			PointerImage pointerImage = (PointerImage)base.PublishClone(context);
			if (this.m_hueColor != null)
			{
				pointerImage.m_hueColor = (ExpressionInfo)this.m_hueColor.PublishClone(context);
			}
			if (this.m_transparency != null)
			{
				pointerImage.m_transparency = (ExpressionInfo)this.m_transparency.PublishClone(context);
			}
			if (this.m_offsetX != null)
			{
				pointerImage.m_offsetX = (ExpressionInfo)this.m_offsetX.PublishClone(context);
			}
			if (this.m_offsetY != null)
			{
				pointerImage.m_offsetY = (ExpressionInfo)this.m_offsetY.PublishClone(context);
			}
			return pointerImage;
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000BC2E3 File Offset: 0x000BA4E3
		internal void SetExprHost(PointerImageExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x000BC308 File Offset: 0x000BA508
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PointerImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BaseGaugeImage, new List<MemberInfo>
			{
				new MemberInfo(MemberName.HueColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Transparency, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.OffsetX, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.OffsetY, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000BC380 File Offset: 0x000BA580
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(PointerImage.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.HueColor:
					writer.Write(this.m_hueColor);
					break;
				case MemberName.OffsetX:
					writer.Write(this.m_offsetX);
					break;
				case MemberName.OffsetY:
					writer.Write(this.m_offsetY);
					break;
				case MemberName.Transparency:
					writer.Write(this.m_transparency);
					break;
				}
			}
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000BC410 File Offset: 0x000BA610
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(PointerImage.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.HueColor:
					this.m_hueColor = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.OffsetX:
					this.m_offsetX = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.OffsetY:
					this.m_offsetY = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.Transparency:
					this.m_transparency = (ExpressionInfo)reader.ReadRIFObject();
					break;
				}
			}
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000BC4B7 File Offset: 0x000BA6B7
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PointerImage;
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x000BC4BE File Offset: 0x000BA6BE
		internal string EvaluateHueColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluatePointerImageHueColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x000BC4E4 File Offset: 0x000BA6E4
		internal double EvaluateTransparency(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluatePointerImageTransparencyExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x000BC50A File Offset: 0x000BA70A
		internal string EvaluateOffsetX(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluatePointerImageOffsetXExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x000BC530 File Offset: 0x000BA730
		internal string EvaluateOffsetY(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluatePointerImageOffsetYExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x040016A7 RID: 5799
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = PointerImage.GetDeclaration();

		// Token: 0x040016A8 RID: 5800
		private ExpressionInfo m_hueColor;

		// Token: 0x040016A9 RID: 5801
		private ExpressionInfo m_transparency;

		// Token: 0x040016AA RID: 5802
		private ExpressionInfo m_offsetX;

		// Token: 0x040016AB RID: 5803
		private ExpressionInfo m_offsetY;
	}
}
