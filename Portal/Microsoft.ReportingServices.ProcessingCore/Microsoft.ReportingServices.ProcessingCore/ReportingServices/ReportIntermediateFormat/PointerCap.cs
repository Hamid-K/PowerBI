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
	// Token: 0x020003F1 RID: 1009
	[Serializable]
	internal sealed class PointerCap : GaugePanelStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060029FC RID: 10748 RVA: 0x000C3A46 File Offset: 0x000C1C46
		internal PointerCap()
		{
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x000C3A4E File Offset: 0x000C1C4E
		internal PointerCap(GaugePanel gaugePanel)
			: base(gaugePanel)
		{
		}

		// Token: 0x170014BD RID: 5309
		// (get) Token: 0x060029FE RID: 10750 RVA: 0x000C3A57 File Offset: 0x000C1C57
		// (set) Token: 0x060029FF RID: 10751 RVA: 0x000C3A5F File Offset: 0x000C1C5F
		internal CapImage CapImage
		{
			get
			{
				return this.m_capImage;
			}
			set
			{
				this.m_capImage = value;
			}
		}

		// Token: 0x170014BE RID: 5310
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x000C3A68 File Offset: 0x000C1C68
		// (set) Token: 0x06002A01 RID: 10753 RVA: 0x000C3A70 File Offset: 0x000C1C70
		internal ExpressionInfo OnTop
		{
			get
			{
				return this.m_onTop;
			}
			set
			{
				this.m_onTop = value;
			}
		}

		// Token: 0x170014BF RID: 5311
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x000C3A79 File Offset: 0x000C1C79
		// (set) Token: 0x06002A03 RID: 10755 RVA: 0x000C3A81 File Offset: 0x000C1C81
		internal ExpressionInfo Reflection
		{
			get
			{
				return this.m_reflection;
			}
			set
			{
				this.m_reflection = value;
			}
		}

		// Token: 0x170014C0 RID: 5312
		// (get) Token: 0x06002A04 RID: 10756 RVA: 0x000C3A8A File Offset: 0x000C1C8A
		// (set) Token: 0x06002A05 RID: 10757 RVA: 0x000C3A92 File Offset: 0x000C1C92
		internal ExpressionInfo CapStyle
		{
			get
			{
				return this.m_capStyle;
			}
			set
			{
				this.m_capStyle = value;
			}
		}

		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x06002A06 RID: 10758 RVA: 0x000C3A9B File Offset: 0x000C1C9B
		// (set) Token: 0x06002A07 RID: 10759 RVA: 0x000C3AA3 File Offset: 0x000C1CA3
		internal ExpressionInfo Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x06002A08 RID: 10760 RVA: 0x000C3AAC File Offset: 0x000C1CAC
		// (set) Token: 0x06002A09 RID: 10761 RVA: 0x000C3AB4 File Offset: 0x000C1CB4
		internal ExpressionInfo Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x000C3ABD File Offset: 0x000C1CBD
		internal string OwnerName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x06002A0B RID: 10763 RVA: 0x000C3ACA File Offset: 0x000C1CCA
		internal PointerCapExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x000C3AD4 File Offset: 0x000C1CD4
		internal override void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.PointerCapStart();
			base.Initialize(context);
			if (this.m_capImage != null)
			{
				this.m_capImage.Initialize(context);
			}
			if (this.m_onTop != null)
			{
				this.m_onTop.Initialize("OnTop", context);
				context.ExprHostBuilder.PointerCapOnTop(this.m_onTop);
			}
			if (this.m_reflection != null)
			{
				this.m_reflection.Initialize("Reflection", context);
				context.ExprHostBuilder.PointerCapReflection(this.m_reflection);
			}
			if (this.m_capStyle != null)
			{
				this.m_capStyle.Initialize("CapStyle", context);
				context.ExprHostBuilder.PointerCapCapStyle(this.m_capStyle);
			}
			if (this.m_hidden != null)
			{
				this.m_hidden.Initialize("Hidden", context);
				context.ExprHostBuilder.PointerCapHidden(this.m_hidden);
			}
			if (this.m_width != null)
			{
				this.m_width.Initialize("Width", context);
				context.ExprHostBuilder.PointerCapWidth(this.m_width);
			}
			context.ExprHostBuilder.PointerCapEnd();
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000C3BEC File Offset: 0x000C1DEC
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			PointerCap pointerCap = (PointerCap)base.PublishClone(context);
			if (this.m_capImage != null)
			{
				pointerCap.m_capImage = (CapImage)this.m_capImage.PublishClone(context);
			}
			if (this.m_onTop != null)
			{
				pointerCap.m_onTop = (ExpressionInfo)this.m_onTop.PublishClone(context);
			}
			if (this.m_reflection != null)
			{
				pointerCap.m_reflection = (ExpressionInfo)this.m_reflection.PublishClone(context);
			}
			if (this.m_capStyle != null)
			{
				pointerCap.m_capStyle = (ExpressionInfo)this.m_capStyle.PublishClone(context);
			}
			if (this.m_hidden != null)
			{
				pointerCap.m_hidden = (ExpressionInfo)this.m_hidden.PublishClone(context);
			}
			if (this.m_width != null)
			{
				pointerCap.m_width = (ExpressionInfo)this.m_width.PublishClone(context);
			}
			return pointerCap;
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x000C3CC4 File Offset: 0x000C1EC4
		internal void SetExprHost(PointerCapExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_capImage != null && this.m_exprHost.CapImageHost != null)
			{
				this.m_capImage.SetExprHost(this.m_exprHost.CapImageHost, reportObjectModel);
			}
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000C3D20 File Offset: 0x000C1F20
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PointerCap, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanelStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.CapImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CapImage),
				new MemberInfo(MemberName.OnTop, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Reflection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.CapStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Hidden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Width, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x000C3DC4 File Offset: 0x000C1FC4
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(PointerCap.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Width)
				{
					if (memberName != MemberName.Hidden)
					{
						switch (memberName)
						{
						case MemberName.CapImage:
							writer.Write(this.m_capImage);
							break;
						case MemberName.OnTop:
							writer.Write(this.m_onTop);
							break;
						case MemberName.Reflection:
							writer.Write(this.m_reflection);
							break;
						case MemberName.CapStyle:
							writer.Write(this.m_capStyle);
							break;
						default:
							Global.Tracer.Assert(false);
							break;
						}
					}
					else
					{
						writer.Write(this.m_hidden);
					}
				}
				else
				{
					writer.Write(this.m_width);
				}
			}
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x000C3E98 File Offset: 0x000C2098
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(PointerCap.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Width)
				{
					if (memberName != MemberName.Hidden)
					{
						switch (memberName)
						{
						case MemberName.CapImage:
							this.m_capImage = (CapImage)reader.ReadRIFObject();
							break;
						case MemberName.OnTop:
							this.m_onTop = (ExpressionInfo)reader.ReadRIFObject();
							break;
						case MemberName.Reflection:
							this.m_reflection = (ExpressionInfo)reader.ReadRIFObject();
							break;
						case MemberName.CapStyle:
							this.m_capStyle = (ExpressionInfo)reader.ReadRIFObject();
							break;
						default:
							Global.Tracer.Assert(false);
							break;
						}
					}
					else
					{
						this.m_hidden = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_width = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x000C3F8A File Offset: 0x000C218A
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PointerCap;
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x000C3F91 File Offset: 0x000C2191
		internal bool EvaluateOnTop(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluatePointerCapOnTopExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x000C3FB7 File Offset: 0x000C21B7
		internal bool EvaluateReflection(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluatePointerCapReflectionExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000C3FDD File Offset: 0x000C21DD
		internal GaugeCapStyles EvaluateCapStyle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateGaugeCapStyles(context.ReportRuntime.EvaluatePointerCapCapStyleExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x000C400E File Offset: 0x000C220E
		internal bool EvaluateHidden(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluatePointerCapHiddenExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x000C4034 File Offset: 0x000C2234
		internal double EvaluateWidth(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluatePointerCapWidthExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x04001730 RID: 5936
		[NonSerialized]
		private PointerCapExprHost m_exprHost;

		// Token: 0x04001731 RID: 5937
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = PointerCap.GetDeclaration();

		// Token: 0x04001732 RID: 5938
		private CapImage m_capImage;

		// Token: 0x04001733 RID: 5939
		private ExpressionInfo m_onTop;

		// Token: 0x04001734 RID: 5940
		private ExpressionInfo m_reflection;

		// Token: 0x04001735 RID: 5941
		private ExpressionInfo m_capStyle;

		// Token: 0x04001736 RID: 5942
		private ExpressionInfo m_hidden;

		// Token: 0x04001737 RID: 5943
		private ExpressionInfo m_width;
	}
}
