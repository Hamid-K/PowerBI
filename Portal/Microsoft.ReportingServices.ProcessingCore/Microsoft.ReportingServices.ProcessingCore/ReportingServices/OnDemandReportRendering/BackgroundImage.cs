using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200033B RID: 827
	public sealed class BackgroundImage : ReportProperty, IImage, IBaseImage
	{
		// Token: 0x06001EE2 RID: 7906 RVA: 0x00076ECA File Offset: 0x000750CA
		internal BackgroundImage(bool isExpression, string expressionString, Microsoft.ReportingServices.OnDemandReportRendering.Style styleDef)
			: base(isExpression, expressionString)
		{
			this.m_styleDef = styleDef;
			this.m_isOldSnapshot = false;
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x00076EE2 File Offset: 0x000750E2
		internal BackgroundImage(bool isExpression, string expressionString, Microsoft.ReportingServices.ReportRendering.Style renderStyle, Microsoft.ReportingServices.OnDemandReportRendering.Style styleDef)
			: base(isExpression, expressionString)
		{
			this.m_styleDef = styleDef;
			this.m_renderStyle = renderStyle;
			this.m_isOldSnapshot = true;
		}

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x00076F04 File Offset: 0x00075104
		public Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType Source
		{
			get
			{
				if (this.m_imageSource == null)
				{
					if (this.m_isOldSnapshot)
					{
						Microsoft.ReportingServices.ReportRendering.Image.SourceType sourceType;
						if (this.m_renderStyle.GetBackgroundImageSource(this.m_renderStyle.GetStyleDefinition("BackgroundImageSource"), out sourceType))
						{
							this.m_imageSource = new Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType?((Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType)sourceType);
						}
					}
					else
					{
						int? num = this.m_styleDef.EvaluateInstanceStyleEnum(Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageSource);
						this.m_imageSource = new Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType?((Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType)((num != null) ? num.Value : 0));
					}
				}
				return this.m_imageSource.Value;
			}
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x00076F8C File Offset: 0x0007518C
		public ReportStringProperty Value
		{
			get
			{
				if (this.m_value == null)
				{
					if (this.m_isOldSnapshot)
					{
						object obj;
						bool flag;
						if (this.m_renderStyle.GetBackgroundImageValue(this.m_renderStyle.GetStyleDefinition("BackgroundImageValue"), out obj, out flag))
						{
							this.m_value = new ReportStringProperty(flag, null, (obj is string) ? ((string)obj) : null);
						}
						else
						{
							this.m_value = new ReportStringProperty();
						}
					}
					else
					{
						string text;
						Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo attributeInfo = this.m_styleDef.GetAttributeInfo("BackgroundImageValue", out text);
						if (attributeInfo == null)
						{
							this.m_value = new ReportStringProperty();
						}
						else
						{
							this.m_value = new ReportStringProperty(attributeInfo.IsExpression, text, attributeInfo.Value);
						}
					}
				}
				return this.m_value;
			}
		}

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x0007703C File Offset: 0x0007523C
		public ReportStringProperty MIMEType
		{
			get
			{
				if (this.m_mimeType == null)
				{
					if (this.m_isOldSnapshot)
					{
						Microsoft.ReportingServices.ReportProcessing.AttributeInfo styleDefinition = this.m_renderStyle.GetStyleDefinition("BackgroundImageMIMEType");
						if (styleDefinition == null)
						{
							this.m_mimeType = new ReportStringProperty();
						}
						else
						{
							this.m_mimeType = new ReportStringProperty(styleDefinition.IsExpression, null, styleDefinition.Value);
						}
					}
					else
					{
						string text;
						Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo attributeInfo = this.m_styleDef.GetAttributeInfo("BackgroundImageMIMEType", out text);
						if (attributeInfo == null)
						{
							this.m_mimeType = new ReportStringProperty();
						}
						else
						{
							this.m_mimeType = new ReportStringProperty(attributeInfo.IsExpression, text, attributeInfo.Value);
						}
					}
				}
				return this.m_mimeType;
			}
		}

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x000770D8 File Offset: 0x000752D8
		public ReportEnumProperty<BackgroundRepeatTypes> BackgroundRepeat
		{
			get
			{
				if (this.m_repeat == null)
				{
					if (this.m_isOldSnapshot)
					{
						Microsoft.ReportingServices.ReportProcessing.AttributeInfo styleDefinition = this.m_renderStyle.GetStyleDefinition("BackgroundRepeat");
						if (styleDefinition == null)
						{
							this.m_repeat = new ReportEnumProperty<BackgroundRepeatTypes>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumBackgroundRepeatType);
						}
						else
						{
							this.m_repeat = new ReportEnumProperty<BackgroundRepeatTypes>(styleDefinition.IsExpression, null, StyleTranslator.TranslateBackgroundRepeat(styleDefinition.Value, null, this.m_styleDef.IsDynamicImageStyle), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumBackgroundRepeatType);
						}
					}
					else
					{
						string text;
						Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo attributeInfo = this.m_styleDef.GetAttributeInfo("BackgroundRepeat", out text);
						if (attributeInfo == null)
						{
							this.m_repeat = new ReportEnumProperty<BackgroundRepeatTypes>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumBackgroundRepeatType);
						}
						else
						{
							this.m_repeat = new ReportEnumProperty<BackgroundRepeatTypes>(attributeInfo.IsExpression, text, (BackgroundRepeatTypes)StyleTranslator.TranslateStyle(StyleAttributeNames.BackgroundImageRepeat, attributeInfo.Value, null, this.m_styleDef.IsDynamicImageStyle), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumBackgroundRepeatType);
						}
					}
				}
				return this.m_repeat;
			}
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x000771B0 File Offset: 0x000753B0
		public ReportEnumProperty<Positions> Position
		{
			get
			{
				if (this.m_position == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_position = new ReportEnumProperty<Positions>();
					}
					else
					{
						string text;
						Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo attributeInfo = this.m_styleDef.GetAttributeInfo("Position", out text);
						bool flag = false;
						string text2 = null;
						if (attributeInfo != null)
						{
							text2 = attributeInfo.Value;
							flag = attributeInfo.IsExpression;
						}
						this.m_position = new ReportEnumProperty<Positions>(flag, text, StyleTranslator.TranslatePosition(text2, null, this.m_styleDef.IsDynamicImageStyle));
					}
				}
				return this.m_position;
			}
		}

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x00077228 File Offset: 0x00075428
		public ReportColorProperty TransparentColor
		{
			get
			{
				if (this.m_transparentColor == null)
				{
					ReportColor reportColor = new ReportColor("Transparent", Color.Transparent, true);
					if (this.m_isOldSnapshot)
					{
						this.m_transparentColor = new ReportColorProperty(false, null, null, reportColor);
					}
					else
					{
						string text;
						Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo attributeInfo = this.m_styleDef.GetAttributeInfo("TransparentColor", out text);
						bool flag = false;
						ReportColor reportColor2 = null;
						if (attributeInfo != null)
						{
							flag = attributeInfo.IsExpression;
							if (!flag)
							{
								reportColor2 = new ReportColor(attributeInfo.Value, this.m_styleDef.IsDynamicImageStyle);
							}
						}
						this.m_transparentColor = new ReportColorProperty(flag, text, reportColor2, reportColor);
					}
				}
				return this.m_transparentColor;
			}
		}

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x000772BC File Offset: 0x000754BC
		public BackgroundImageInstance Instance
		{
			get
			{
				if (!this.m_isOldSnapshot && this.m_styleDef.m_renderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_instance = new ShimBackgroundImageInstance(this, this.m_renderStyle["BackgroundImage"] as BackgroundImage, this.m_renderStyle["BackgroundRepeat"] as string);
					}
					else
					{
						this.m_instance = new InternalBackgroundImageInstance(this);
					}
				}
				return this.m_instance;
			}
		}

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x0007733F File Offset: 0x0007553F
		internal Microsoft.ReportingServices.OnDemandReportRendering.Style StyleDef
		{
			get
			{
				return this.m_styleDef;
			}
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x00077347 File Offset: 0x00075547
		ObjectType IBaseImage.ObjectType
		{
			get
			{
				return this.m_styleDef.StyleContainer.ObjectType;
			}
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x00077359 File Offset: 0x00075559
		string IBaseImage.ObjectName
		{
			get
			{
				return this.m_styleDef.StyleContainer.Name;
			}
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x0007736B File Offset: 0x0007556B
		ReportProperty IBaseImage.Value
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x00077373 File Offset: 0x00075573
		string IBaseImage.ImageDataPropertyName
		{
			get
			{
				return "BackgroundImageValue";
			}
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x0007737A File Offset: 0x0007557A
		string IBaseImage.ImageValuePropertyName
		{
			get
			{
				return "BackgroundImageValue";
			}
		}

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x00077381 File Offset: 0x00075581
		string IBaseImage.MIMETypePropertyName
		{
			get
			{
				return "BackgroundImageMIMEType";
			}
		}

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x00077388 File Offset: 0x00075588
		Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes IBaseImage.EmbeddingMode
		{
			get
			{
				return Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes.Inline;
			}
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x0007738B File Offset: 0x0007558B
		byte[] IBaseImage.GetImageData(out List<string> fieldsUsedInValue, out bool errorOccurred)
		{
			fieldsUsedInValue = null;
			errorOccurred = false;
			return this.m_styleDef.EvaluateInstanceStyleVariant(Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageValue) as byte[];
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x000773A5 File Offset: 0x000755A5
		string IBaseImage.GetMIMETypeValue()
		{
			return this.m_styleDef.EvaluateInstanceStyleString(Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageMimeType);
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x000773B4 File Offset: 0x000755B4
		string IBaseImage.GetValueAsString(out List<string> fieldsUsedInValue, out bool errOccurred)
		{
			errOccurred = false;
			fieldsUsedInValue = null;
			return this.m_styleDef.EvaluateInstanceStyleString(Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId.BackgroundImageValue);
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x000773C9 File Offset: 0x000755C9
		string IBaseImage.GetTransparentImageProperties(out string mimeType, out byte[] imageData)
		{
			mimeType = null;
			imageData = null;
			return null;
		}

		// Token: 0x04000FB1 RID: 4017
		private bool m_isOldSnapshot;

		// Token: 0x04000FB2 RID: 4018
		private Microsoft.ReportingServices.ReportRendering.Style m_renderStyle;

		// Token: 0x04000FB3 RID: 4019
		private Microsoft.ReportingServices.OnDemandReportRendering.Style m_styleDef;

		// Token: 0x04000FB4 RID: 4020
		private ReportStringProperty m_value;

		// Token: 0x04000FB5 RID: 4021
		private ReportStringProperty m_mimeType;

		// Token: 0x04000FB6 RID: 4022
		private ReportEnumProperty<BackgroundRepeatTypes> m_repeat;

		// Token: 0x04000FB7 RID: 4023
		private BackgroundImageInstance m_instance;

		// Token: 0x04000FB8 RID: 4024
		private Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType? m_imageSource;

		// Token: 0x04000FB9 RID: 4025
		private ReportEnumProperty<Positions> m_position;

		// Token: 0x04000FBA RID: 4026
		private ReportColorProperty m_transparentColor;
	}
}
