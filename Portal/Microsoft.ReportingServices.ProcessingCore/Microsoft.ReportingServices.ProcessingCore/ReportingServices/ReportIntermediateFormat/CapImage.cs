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
	// Token: 0x020003D8 RID: 984
	[Serializable]
	internal sealed class CapImage : BaseGaugeImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060027DA RID: 10202 RVA: 0x000BB9F6 File Offset: 0x000B9BF6
		internal CapImage()
		{
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x000BB9FE File Offset: 0x000B9BFE
		internal CapImage(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x000BBA07 File Offset: 0x000B9C07
		// (set) Token: 0x060027DD RID: 10205 RVA: 0x000BBA0F File Offset: 0x000B9C0F
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

		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x000BBA18 File Offset: 0x000B9C18
		// (set) Token: 0x060027DF RID: 10207 RVA: 0x000BBA20 File Offset: 0x000B9C20
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

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x060027E0 RID: 10208 RVA: 0x000BBA29 File Offset: 0x000B9C29
		// (set) Token: 0x060027E1 RID: 10209 RVA: 0x000BBA31 File Offset: 0x000B9C31
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

		// Token: 0x060027E2 RID: 10210 RVA: 0x000BBA3C File Offset: 0x000B9C3C
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.CapImageStart();
			base.Initialize(context);
			if (this.m_hueColor != null)
			{
				this.m_hueColor.Initialize("HueColor", context);
				context.ExprHostBuilder.CapImageHueColor(this.m_hueColor);
			}
			if (this.m_offsetX != null)
			{
				this.m_offsetX.Initialize("OffsetX", context);
				context.ExprHostBuilder.CapImageOffsetX(this.m_offsetX);
			}
			if (this.m_offsetY != null)
			{
				this.m_offsetY.Initialize("OffsetY", context);
				context.ExprHostBuilder.CapImageOffsetY(this.m_offsetY);
			}
			context.ExprHostBuilder.CapImageEnd();
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000BBAEC File Offset: 0x000B9CEC
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			CapImage capImage = (CapImage)base.PublishClone(context);
			if (this.m_hueColor != null)
			{
				capImage.m_hueColor = (ExpressionInfo)this.m_hueColor.PublishClone(context);
			}
			if (this.m_offsetX != null)
			{
				capImage.m_offsetX = (ExpressionInfo)this.m_offsetX.PublishClone(context);
			}
			if (this.m_offsetY != null)
			{
				capImage.m_offsetY = (ExpressionInfo)this.m_offsetY.PublishClone(context);
			}
			return capImage;
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000BBB64 File Offset: 0x000B9D64
		internal void SetExprHost(CapImageExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000BBB8C File Offset: 0x000B9D8C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CapImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BaseGaugeImage, new List<MemberInfo>
			{
				new MemberInfo(MemberName.HueColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.OffsetX, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.OffsetY, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x000BBBF0 File Offset: 0x000B9DF0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(CapImage.m_Declaration);
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
				}
			}
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x000BBC70 File Offset: 0x000B9E70
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(CapImage.m_Declaration);
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
				}
			}
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x000BBCFC File Offset: 0x000B9EFC
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CapImage;
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x000BBD03 File Offset: 0x000B9F03
		internal string EvaluateHueColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateCapImageHueColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000BBD29 File Offset: 0x000B9F29
		internal string EvaluateOffsetX(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateCapImageOffsetXExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000BBD4F File Offset: 0x000B9F4F
		internal string EvaluateOffsetY(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateCapImageOffsetYExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x0400169F RID: 5791
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = CapImage.GetDeclaration();

		// Token: 0x040016A0 RID: 5792
		private ExpressionInfo m_hueColor;

		// Token: 0x040016A1 RID: 5793
		private ExpressionInfo m_offsetX;

		// Token: 0x040016A2 RID: 5794
		private ExpressionInfo m_offsetY;
	}
}
