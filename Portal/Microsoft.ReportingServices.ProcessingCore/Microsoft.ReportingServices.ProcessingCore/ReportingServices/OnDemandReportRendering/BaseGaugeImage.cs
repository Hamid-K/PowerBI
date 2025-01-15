using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000E0 RID: 224
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class BaseGaugeImage : IBaseImage
	{
		// Token: 0x06000AA6 RID: 2726 RVA: 0x00030766 File Offset: 0x0002E966
		internal BaseGaugeImage(BaseGaugeImage defObject, GaugePanel gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0003077C File Offset: 0x0002E97C
		ObjectType IBaseImage.ObjectType
		{
			get
			{
				return this.m_gaugePanel.ReportItemDef.ObjectType;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0003078E File Offset: 0x0002E98E
		string IBaseImage.ObjectName
		{
			get
			{
				return this.m_gaugePanel.Name;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0003079C File Offset: 0x0002E99C
		Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType IBaseImage.Source
		{
			get
			{
				ReportEnumProperty<Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType> source = this.Source;
				if (!source.IsExpression)
				{
					return source.Value;
				}
				return this.Instance.Source;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x000307CA File Offset: 0x0002E9CA
		ReportProperty IBaseImage.Value
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x000307D2 File Offset: 0x0002E9D2
		ReportStringProperty IBaseImage.MIMEType
		{
			get
			{
				return this.MIMEType;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x000307DA File Offset: 0x0002E9DA
		string IBaseImage.ImageDataPropertyName
		{
			get
			{
				return "ImageData";
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x000307E1 File Offset: 0x0002E9E1
		string IBaseImage.ImageValuePropertyName
		{
			get
			{
				return "Value";
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x000307E8 File Offset: 0x0002E9E8
		string IBaseImage.MIMETypePropertyName
		{
			get
			{
				return "MIMEType";
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x000307EF File Offset: 0x0002E9EF
		Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes IBaseImage.EmbeddingMode
		{
			get
			{
				return Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes.Inline;
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000307F2 File Offset: 0x0002E9F2
		byte[] IBaseImage.GetImageData(out List<string> fieldsUsedInValue, out bool errorOccurred)
		{
			fieldsUsedInValue = null;
			return this.m_defObject.EvaluateBinaryValue(this.Instance.ReportScopeInstance, this.m_gaugePanel.RenderingContext.OdpContext, out errorOccurred);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00030820 File Offset: 0x0002EA20
		string IBaseImage.GetMIMETypeValue()
		{
			ReportStringProperty mimetype = this.MIMEType;
			if (mimetype == null)
			{
				return null;
			}
			if (!mimetype.IsExpression)
			{
				return mimetype.Value;
			}
			return this.BaseGaugeImageDef.EvaluateMIMEType(this.Instance.ReportScopeInstance, this.GaugePanelDef.RenderingContext.OdpContext);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00030870 File Offset: 0x0002EA70
		string IBaseImage.GetValueAsString(out List<string> fieldsUsedInValue, out bool errOccurred)
		{
			fieldsUsedInValue = null;
			ReportVariantProperty value = this.Value;
			errOccurred = false;
			if (value.IsExpression)
			{
				return this.m_defObject.EvaluateStringValue(this.Instance.ReportScopeInstance, this.m_gaugePanel.RenderingContext.OdpContext, out errOccurred);
			}
			object value2 = value.Value;
			if (value2 is string)
			{
				return (string)value2;
			}
			return null;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x000308D1 File Offset: 0x0002EAD1
		string IBaseImage.GetTransparentImageProperties(out string mimeType, out byte[] imageData)
		{
			mimeType = null;
			imageData = null;
			return null;
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x000308DC File Offset: 0x0002EADC
		public ReportEnumProperty<Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType> Source
		{
			get
			{
				if (this.m_source == null && this.m_defObject.Source != null)
				{
					this.m_source = new ReportEnumProperty<Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType>(this.m_defObject.Source.IsExpression, this.m_defObject.Source.OriginalText, EnumTranslator.TranslateImageSourceType(this.m_defObject.Source.StringValue, null));
				}
				return this.m_source;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x00030945 File Offset: 0x0002EB45
		public ReportVariantProperty Value
		{
			get
			{
				if (this.m_value == null && this.m_defObject.Value != null)
				{
					this.m_value = new ReportVariantProperty(this.m_defObject.Value);
				}
				return this.m_value;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00030978 File Offset: 0x0002EB78
		public ReportStringProperty MIMEType
		{
			get
			{
				if (this.m_MIMEType == null && this.m_defObject.MIMEType != null)
				{
					this.m_MIMEType = new ReportStringProperty(this.m_defObject.MIMEType);
				}
				return this.m_MIMEType;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x000309AC File Offset: 0x0002EBAC
		public ReportColorProperty TransparentColor
		{
			get
			{
				if (this.m_transparentColor == null && this.m_defObject.TransparentColor != null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo transparentColor = this.m_defObject.TransparentColor;
					if (transparentColor != null)
					{
						this.m_transparentColor = new ReportColorProperty(transparentColor.IsExpression, transparentColor.OriginalText, transparentColor.IsExpression ? null : new ReportColor(transparentColor.StringValue.Trim(), true), transparentColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_transparentColor;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00030A31 File Offset: 0x0002EC31
		internal GaugePanel GaugePanelDef
		{
			get
			{
				return this.m_gaugePanel;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x00030A39 File Offset: 0x0002EC39
		internal BaseGaugeImage BaseGaugeImageDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00030A41 File Offset: 0x0002EC41
		internal BaseGaugeImageInstance Instance
		{
			get
			{
				return this.GetInstance();
			}
		}

		// Token: 0x06000ABB RID: 2747
		internal abstract BaseGaugeImageInstance GetInstance();

		// Token: 0x06000ABC RID: 2748 RVA: 0x00030A49 File Offset: 0x0002EC49
		internal virtual void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000485 RID: 1157
		internal GaugePanel m_gaugePanel;

		// Token: 0x04000486 RID: 1158
		internal BaseGaugeImage m_defObject;

		// Token: 0x04000487 RID: 1159
		internal BaseGaugeImageInstance m_instance;

		// Token: 0x04000488 RID: 1160
		private ReportEnumProperty<Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType> m_source;

		// Token: 0x04000489 RID: 1161
		private ReportVariantProperty m_value;

		// Token: 0x0400048A RID: 1162
		private ReportStringProperty m_MIMEType;

		// Token: 0x0400048B RID: 1163
		private ReportColorProperty m_transparentColor;
	}
}
