using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200033C RID: 828
	public sealed class Image : Microsoft.ReportingServices.OnDemandReportRendering.ReportItem, IImage, IROMActionOwner, IBaseImage
	{
		// Token: 0x06001EF7 RID: 7927 RVA: 0x000773D2 File Offset: 0x000755D2
		internal Image(IReportScope reportScope, IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, Microsoft.ReportingServices.ReportIntermediateFormat.Image reportItemDef, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(reportScope, parentDefinitionPath, indexIntoParentCollectionDef, reportItemDef, renderingContext)
		{
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x000773E1 File Offset: 0x000755E1
		internal Image(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, bool inSubtotal, Microsoft.ReportingServices.ReportRendering.Image renderImage, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, renderImage, renderingContext)
		{
			this.m_renderImage = renderImage;
		}

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x000773F8 File Offset: 0x000755F8
		public Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType Source
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return (Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType)((Microsoft.ReportingServices.ReportProcessing.Image)this.m_renderReportItem.ReportItemDef).Source;
				}
				return this.ImageDef.Source;
			}
		}

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x00077424 File Offset: 0x00075624
		public ReportStringProperty Value
		{
			get
			{
				if (this.m_value == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_value = new ReportStringProperty(((Microsoft.ReportingServices.ReportProcessing.Image)this.m_renderReportItem.ReportItemDef).Value);
					}
					else
					{
						this.m_value = new ReportStringProperty(this.ImageDef.Value);
					}
				}
				return this.m_value;
			}
		}

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x06001EFB RID: 7931 RVA: 0x00077480 File Offset: 0x00075680
		public ReportStringProperty MIMEType
		{
			get
			{
				if (this.m_mimeType == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_mimeType = new ReportStringProperty(((Microsoft.ReportingServices.ReportProcessing.Image)this.m_renderReportItem.ReportItemDef).MIMEType);
					}
					else
					{
						this.m_mimeType = new ReportStringProperty(this.ImageDef.MIMEType);
					}
				}
				return this.m_mimeType;
			}
		}

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x000774DC File Offset: 0x000756DC
		public ReportVariantProperty Tag
		{
			get
			{
				TagCollection tags = this.Tags;
				if (tags == null)
				{
					return new ReportVariantProperty(false);
				}
				return tags[0].Value;
			}
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x00077506 File Offset: 0x00075706
		internal TagCollection Tags
		{
			get
			{
				if (this.m_tags == null && !this.m_isOldSnapshot && this.ImageDef.Tags != null)
				{
					this.m_tags = new TagCollection(this);
				}
				return this.m_tags;
			}
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x00077537 File Offset: 0x00075737
		internal bool IsNullImage
		{
			get
			{
				return !this.Value.IsExpression && string.IsNullOrEmpty(this.Value.Value);
			}
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x00077558 File Offset: 0x00075758
		// (set) Token: 0x06001F00 RID: 7936 RVA: 0x0007757E File Offset: 0x0007577E
		public Microsoft.ReportingServices.OnDemandReportRendering.Image.Sizings Sizing
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return (Microsoft.ReportingServices.OnDemandReportRendering.Image.Sizings)((Microsoft.ReportingServices.ReportRendering.Image)this.m_renderReportItem).Sizing;
				}
				return this.ImageDef.Sizing;
			}
			set
			{
				if (base.CriGenerationPhase != ReportElement.CriGenerationPhases.Definition)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.ImageDef.Sizing = value;
			}
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x000775A0 File Offset: 0x000757A0
		string IROMActionOwner.UniqueName
		{
			get
			{
				return this.m_reportItemDef.UniqueName;
			}
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x000775B0 File Offset: 0x000757B0
		public ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null)
				{
					if (this.m_isOldSnapshot)
					{
						if (((Microsoft.ReportingServices.ReportRendering.Image)this.m_renderReportItem).ActionInfo != null)
						{
							this.m_actionInfo = new ActionInfo(base.RenderingContext, ((Microsoft.ReportingServices.ReportRendering.Image)this.m_renderReportItem).ActionInfo);
						}
					}
					else
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Action action = this.ImageDef.Action;
						if (action != null)
						{
							this.m_actionInfo = new ActionInfo(base.RenderingContext, this.ReportScope, action, this.m_reportItemDef, this, this.m_reportItemDef.ObjectType, this.m_reportItemDef.Name, this);
						}
					}
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06001F03 RID: 7939 RVA: 0x00077650 File Offset: 0x00075850
		public ImageInstance ImageInstance
		{
			get
			{
				return (ImageInstance)base.Instance;
			}
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x00077660 File Offset: 0x00075860
		internal override ReportItemInstance GetOrCreateInstance()
		{
			if (this.m_instance == null)
			{
				if (base.CriOwner != null)
				{
					this.m_instance = new CriImageInstance(this);
				}
				else if (base.IsOldSnapshot)
				{
					this.m_instance = new ShimImageInstance(this);
				}
				else
				{
					this.m_instance = new InternalImageInstance(this);
				}
			}
			return this.m_instance;
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x000776B3 File Offset: 0x000758B3
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Image ImageDef
		{
			get
			{
				return (Microsoft.ReportingServices.ReportIntermediateFormat.Image)this.m_reportItemDef;
			}
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x000776C0 File Offset: 0x000758C0
		List<string> IROMActionOwner.FieldsUsedInValueExpression
		{
			get
			{
				return ((ImageInstance)this.GetOrCreateInstance()).GetFieldsUsedInValueExpression();
			}
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x000776D2 File Offset: 0x000758D2
		ObjectType IBaseImage.ObjectType
		{
			get
			{
				return ObjectType.Image;
			}
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x000776D5 File Offset: 0x000758D5
		string IBaseImage.ObjectName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x000776DD File Offset: 0x000758DD
		ReportProperty IBaseImage.Value
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x000776E5 File Offset: 0x000758E5
		string IBaseImage.ImageDataPropertyName
		{
			get
			{
				return "ImageData";
			}
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x000776EC File Offset: 0x000758EC
		string IBaseImage.ImageValuePropertyName
		{
			get
			{
				return "Value";
			}
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x000776F3 File Offset: 0x000758F3
		string IBaseImage.MIMETypePropertyName
		{
			get
			{
				return "MIMEType";
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x000776FA File Offset: 0x000758FA
		Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes IBaseImage.EmbeddingMode
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes.Inline;
				}
				return this.ImageDef.EmbeddingMode;
			}
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x00077714 File Offset: 0x00075914
		byte[] IBaseImage.GetImageData(out List<string> fieldsUsedInValue, out bool errorOccurred)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Image image = (Microsoft.ReportingServices.ReportIntermediateFormat.Image)base.ReportItemDef;
			bool flag = image.ShouldTrackFieldsUsedInValue();
			fieldsUsedInValue = null;
			if (flag)
			{
				base.RenderingContext.OdpContext.ReportObjectModel.ResetFieldsUsedInExpression();
			}
			byte[] array = image.EvaluateBinaryValueExpression(base.Instance.ReportScopeInstance, base.RenderingContext.OdpContext, out errorOccurred);
			if (errorOccurred)
			{
				return null;
			}
			if (flag)
			{
				fieldsUsedInValue = new List<string>();
				base.RenderingContext.OdpContext.ReportObjectModel.AddFieldsUsedInExpression(fieldsUsedInValue);
			}
			return array;
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x00077793 File Offset: 0x00075993
		string IBaseImage.GetMIMETypeValue()
		{
			return ((Microsoft.ReportingServices.ReportIntermediateFormat.Image)base.ReportItemDef).EvaluateMimeTypeExpression(base.Instance.ReportScopeInstance, base.RenderingContext.OdpContext);
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x000777BC File Offset: 0x000759BC
		string IBaseImage.GetValueAsString(out List<string> fieldsUsedInValue, out bool errOccurred)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Image image = (Microsoft.ReportingServices.ReportIntermediateFormat.Image)base.ReportItemDef;
			bool flag = image.ShouldTrackFieldsUsedInValue();
			fieldsUsedInValue = null;
			if (flag)
			{
				base.RenderingContext.OdpContext.ReportObjectModel.ResetFieldsUsedInExpression();
			}
			string text = image.EvaluateStringValueExpression(base.Instance.ReportScopeInstance, base.RenderingContext.OdpContext, out errOccurred);
			if (errOccurred)
			{
				return null;
			}
			if (flag)
			{
				fieldsUsedInValue = new List<string>();
				base.RenderingContext.OdpContext.ReportObjectModel.AddFieldsUsedInExpression(fieldsUsedInValue);
			}
			return text;
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x0007783C File Offset: 0x00075A3C
		string IBaseImage.GetTransparentImageProperties(out string mimeType, out byte[] imageData)
		{
			InternalImageInstance internalImageInstance = this.ImageInstance as InternalImageInstance;
			Global.Tracer.Assert(internalImageInstance != null, "GetTransparentImageProperties may only be called from the ODP engine.");
			return internalImageInstance.LoadAndCacheTransparentImage(out mimeType, out imageData);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x00077870 File Offset: 0x00075A70
		internal override void UpdateRenderReportItem(Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem)
		{
			base.UpdateRenderReportItem(renderReportItem);
			if (this.m_actionInfo != null)
			{
				this.m_actionInfo.Update(((Microsoft.ReportingServices.ReportRendering.Image)this.m_renderReportItem).ActionInfo);
			}
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x0007789C File Offset: 0x00075A9C
		internal override void SetNewContextChildren()
		{
			base.SetNewContextChildren();
			if (this.m_actionInfo != null)
			{
				this.m_actionInfo.SetNewContext();
			}
			if (this.m_tags != null)
			{
				this.m_tags.SetNewContext();
			}
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x000778CC File Offset: 0x00075ACC
		internal override void ConstructReportItemDefinition()
		{
			base.ConstructReportItemDefinitionImpl();
			ImageInstance imageInstance = this.ImageInstance;
			Global.Tracer.Assert(imageInstance != null, "(instance != null)");
			if (imageInstance.MIMEType != null)
			{
				this.ImageDef.MIMEType = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(imageInstance.MIMEType);
			}
			else
			{
				this.ImageDef.MIMEType = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			}
			this.m_mimeType = null;
			if (imageInstance.ImageData != null || imageInstance.StreamName != null)
			{
				Global.Tracer.Assert(false, "Runtime construction of images with constant Image.Value is not supported.");
			}
			else
			{
				this.ImageDef.Value = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression();
			}
			this.m_value = null;
			if (!this.ActionInfo.ConstructActionDefinition())
			{
				this.ImageDef.Action = null;
				this.m_actionInfo = null;
			}
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x0007798C File Offset: 0x00075B8C
		internal override void CompleteCriGeneratedInstanceEvaluation()
		{
			Global.Tracer.Assert(base.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance, "(CriGenerationPhase == CriGenerationPhases.Instance)");
			ImageInstance imageInstance = this.ImageInstance;
			Global.Tracer.Assert(imageInstance != null, "(instance != null)");
			if (imageInstance.ActionInfoWithDynamicImageMapAreas != null)
			{
				base.CriGenerationPhase = ReportElement.CriGenerationPhases.Definition;
				imageInstance.ActionInfoWithDynamicImageMapAreas.ConstructDefinitions();
				base.CriGenerationPhase = ReportElement.CriGenerationPhases.Instance;
			}
		}

		// Token: 0x04000FBB RID: 4027
		private Microsoft.ReportingServices.ReportRendering.Image m_renderImage;

		// Token: 0x04000FBC RID: 4028
		private ReportStringProperty m_value;

		// Token: 0x04000FBD RID: 4029
		private ReportStringProperty m_mimeType;

		// Token: 0x04000FBE RID: 4030
		private TagCollection m_tags;

		// Token: 0x04000FBF RID: 4031
		private ActionInfo m_actionInfo;

		// Token: 0x0200094B RID: 2379
		public enum Sizings
		{
			// Token: 0x04004051 RID: 16465
			AutoSize,
			// Token: 0x04004052 RID: 16466
			Fit,
			// Token: 0x04004053 RID: 16467
			FitProportional,
			// Token: 0x04004054 RID: 16468
			Clip
		}

		// Token: 0x0200094C RID: 2380
		public enum SourceType
		{
			// Token: 0x04004056 RID: 16470
			External,
			// Token: 0x04004057 RID: 16471
			Embedded,
			// Token: 0x04004058 RID: 16472
			Database
		}

		// Token: 0x0200094D RID: 2381
		internal enum EmbeddingModes
		{
			// Token: 0x0400405A RID: 16474
			Inline,
			// Token: 0x0400405B RID: 16475
			Package
		}
	}
}
