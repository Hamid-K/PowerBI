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
	// Token: 0x020003D7 RID: 983
	[Serializable]
	internal class BaseGaugeImage : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060027C0 RID: 10176 RVA: 0x000BB46E File Offset: 0x000B966E
		internal BaseGaugeImage()
		{
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x000BB476 File Offset: 0x000B9676
		internal BaseGaugeImage(GaugePanel gaugePanel)
		{
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x060027C2 RID: 10178 RVA: 0x000BB485 File Offset: 0x000B9685
		// (set) Token: 0x060027C3 RID: 10179 RVA: 0x000BB48D File Offset: 0x000B968D
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

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x060027C4 RID: 10180 RVA: 0x000BB496 File Offset: 0x000B9696
		// (set) Token: 0x060027C5 RID: 10181 RVA: 0x000BB49E File Offset: 0x000B969E
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

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x060027C6 RID: 10182 RVA: 0x000BB4A7 File Offset: 0x000B96A7
		// (set) Token: 0x060027C7 RID: 10183 RVA: 0x000BB4AF File Offset: 0x000B96AF
		internal ExpressionInfo MIMEType
		{
			get
			{
				return this.m_MIMEType;
			}
			set
			{
				this.m_MIMEType = value;
			}
		}

		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x060027C8 RID: 10184 RVA: 0x000BB4B8 File Offset: 0x000B96B8
		// (set) Token: 0x060027C9 RID: 10185 RVA: 0x000BB4C0 File Offset: 0x000B96C0
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

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x060027CA RID: 10186 RVA: 0x000BB4C9 File Offset: 0x000B96C9
		internal string OwnerName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x000BB4D6 File Offset: 0x000B96D6
		internal BaseGaugeImageExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x000BB4E0 File Offset: 0x000B96E0
		internal virtual void Initialize(InitializationContext context)
		{
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
			if (this.m_MIMEType != null)
			{
				this.m_MIMEType.Initialize("MIMEType", context);
				context.ExprHostBuilder.BaseGaugeImageMIMEType(this.m_MIMEType);
			}
			if (this.m_transparentColor != null)
			{
				this.m_transparentColor.Initialize("TransparentColor", context);
				context.ExprHostBuilder.BaseGaugeImageTransparentColor(this.m_transparentColor);
			}
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x000BB59C File Offset: 0x000B979C
		internal virtual object PublishClone(AutomaticSubtotalContext context)
		{
			BaseGaugeImage baseGaugeImage = (BaseGaugeImage)base.MemberwiseClone();
			baseGaugeImage.m_gaugePanel = (GaugePanel)context.CurrentDataRegionClone;
			if (this.m_source != null)
			{
				baseGaugeImage.m_source = (ExpressionInfo)this.m_source.PublishClone(context);
			}
			if (this.m_value != null)
			{
				baseGaugeImage.m_value = (ExpressionInfo)this.m_value.PublishClone(context);
			}
			if (this.m_MIMEType != null)
			{
				baseGaugeImage.m_MIMEType = (ExpressionInfo)this.m_MIMEType.PublishClone(context);
			}
			if (this.m_transparentColor != null)
			{
				baseGaugeImage.m_transparentColor = (ExpressionInfo)this.m_transparentColor.PublishClone(context);
			}
			return baseGaugeImage;
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x000BB644 File Offset: 0x000B9844
		internal void SetExprHost(BaseGaugeImageExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x000BB670 File Offset: 0x000B9870
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BaseGaugeImage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Source, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.TransparentColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MIMEType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.GaugePanel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GaugePanel, Token.Reference)
			});
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x000BB6F8 File Offset: 0x000B98F8
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(BaseGaugeImage.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Source)
				{
					if (memberName != MemberName.Value)
					{
						if (memberName == MemberName.Source)
						{
							writer.Write(this.m_source);
						}
					}
					else
					{
						writer.Write(this.m_value);
					}
				}
				else if (memberName != MemberName.MIMEType)
				{
					if (memberName != MemberName.GaugePanel)
					{
						if (memberName == MemberName.TransparentColor)
						{
							writer.Write(this.m_transparentColor);
						}
					}
					else
					{
						writer.WriteReference(this.m_gaugePanel);
					}
				}
				else
				{
					writer.Write(this.m_MIMEType);
				}
			}
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x000BB7AC File Offset: 0x000B99AC
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(BaseGaugeImage.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Source)
				{
					if (memberName != MemberName.Value)
					{
						if (memberName == MemberName.Source)
						{
							this.m_source = (ExpressionInfo)reader.ReadRIFObject();
						}
					}
					else
					{
						this.m_value = (ExpressionInfo)reader.ReadRIFObject();
					}
				}
				else if (memberName != MemberName.MIMEType)
				{
					if (memberName != MemberName.GaugePanel)
					{
						if (memberName == MemberName.TransparentColor)
						{
							this.m_transparentColor = (ExpressionInfo)reader.ReadRIFObject();
						}
					}
					else
					{
						this.m_gaugePanel = reader.ReadReference<GaugePanel>(this);
					}
				}
				else
				{
					this.m_MIMEType = (ExpressionInfo)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x000BB874 File Offset: 0x000B9A74
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(BaseGaugeImage.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.GaugePanel)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_gaugePanel = (GaugePanel)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000BB918 File Offset: 0x000B9B18
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BaseGaugeImage;
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000BB91F File Offset: 0x000B9B1F
		internal Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType EvaluateSource(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return EnumTranslator.TranslateImageSourceType(context.ReportRuntime.EvaluateBaseGaugeImageSourceExpression(this, this.m_gaugePanel.Name), context.ReportRuntime);
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x000BB950 File Offset: 0x000B9B50
		internal string EvaluateStringValue(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context, out bool errorOccurred)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateBaseGaugeImageStringValueExpression(this, this.m_gaugePanel.Name, out errorOccurred);
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x000BB977 File Offset: 0x000B9B77
		internal byte[] EvaluateBinaryValue(IReportScopeInstance romInstance, OnDemandProcessingContext context, out bool errOccurred)
		{
			context.SetupContext(this.m_gaugePanel, romInstance);
			return context.ReportRuntime.EvaluateBaseGaugeImageBinaryValueExpression(this, this.m_gaugePanel.Name, out errOccurred);
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x000BB99E File Offset: 0x000B9B9E
		internal string EvaluateMIMEType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateBaseGaugeImageMIMETypeExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x000BB9C4 File Offset: 0x000B9BC4
		internal string EvaluateTransparentColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_gaugePanel, reportScopeInstance);
			return context.ReportRuntime.EvaluateBaseGaugeImageTransparentColorExpression(this, this.m_gaugePanel.Name);
		}

		// Token: 0x04001698 RID: 5784
		[NonSerialized]
		protected BaseGaugeImageExprHost m_exprHost;

		// Token: 0x04001699 RID: 5785
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = BaseGaugeImage.GetDeclaration();

		// Token: 0x0400169A RID: 5786
		[Reference]
		protected GaugePanel m_gaugePanel;

		// Token: 0x0400169B RID: 5787
		private ExpressionInfo m_source;

		// Token: 0x0400169C RID: 5788
		private ExpressionInfo m_value;

		// Token: 0x0400169D RID: 5789
		private ExpressionInfo m_MIMEType;

		// Token: 0x0400169E RID: 5790
		private ExpressionInfo m_transparentColor;
	}
}
