using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001C7 RID: 455
	public sealed class MapMarkerImage : IBaseImage
	{
		// Token: 0x060011B5 RID: 4533 RVA: 0x000496FF File Offset: 0x000478FF
		internal MapMarkerImage(MapMarkerImage defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x00049715 File Offset: 0x00047915
		ObjectType IBaseImage.ObjectType
		{
			get
			{
				return this.m_map.ReportItemDef.ObjectType;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00049727 File Offset: 0x00047927
		string IBaseImage.ObjectName
		{
			get
			{
				return this.m_map.Name;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00049734 File Offset: 0x00047934
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

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x00049762 File Offset: 0x00047962
		ReportProperty IBaseImage.Value
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x0004976A File Offset: 0x0004796A
		ReportStringProperty IBaseImage.MIMEType
		{
			get
			{
				return this.MIMEType;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x00049772 File Offset: 0x00047972
		string IBaseImage.ImageDataPropertyName
		{
			get
			{
				return "ImageData";
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x00049779 File Offset: 0x00047979
		string IBaseImage.ImageValuePropertyName
		{
			get
			{
				return "Value";
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00049780 File Offset: 0x00047980
		string IBaseImage.MIMETypePropertyName
		{
			get
			{
				return "MIMEType";
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x00049787 File Offset: 0x00047987
		Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes IBaseImage.EmbeddingMode
		{
			get
			{
				return Microsoft.ReportingServices.OnDemandReportRendering.Image.EmbeddingModes.Inline;
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0004978A File Offset: 0x0004798A
		byte[] IBaseImage.GetImageData(out List<string> fieldsUsedInValue, out bool errorOccurred)
		{
			fieldsUsedInValue = null;
			return this.m_defObject.EvaluateBinaryValue(this.Instance.ReportScopeInstance, this.m_map.RenderingContext.OdpContext, out errorOccurred);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x000497B8 File Offset: 0x000479B8
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
			return this.MapMarkerImageDef.EvaluateMIMEType(this.Instance.ReportScopeInstance, this.MapDef.RenderingContext.OdpContext);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00049808 File Offset: 0x00047A08
		string IBaseImage.GetValueAsString(out List<string> fieldsUsedInValue, out bool errOccurred)
		{
			fieldsUsedInValue = null;
			ReportVariantProperty value = this.Value;
			errOccurred = false;
			if (value.IsExpression)
			{
				return this.m_defObject.EvaluateStringValue(this.Instance.ReportScopeInstance, this.m_map.RenderingContext.OdpContext, out errOccurred);
			}
			object value2 = value.Value;
			if (value2 is string)
			{
				return (string)value2;
			}
			return null;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00049869 File Offset: 0x00047A69
		string IBaseImage.GetTransparentImageProperties(out string mimeType, out byte[] imageData)
		{
			mimeType = null;
			imageData = null;
			return null;
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00049874 File Offset: 0x00047A74
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

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x000498DD File Offset: 0x00047ADD
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

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00049910 File Offset: 0x00047B10
		public ReportStringProperty MIMEType
		{
			get
			{
				if (this.m_mIMEType == null && this.m_defObject.MIMEType != null)
				{
					this.m_mIMEType = new ReportStringProperty(this.m_defObject.MIMEType);
				}
				return this.m_mIMEType;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x00049944 File Offset: 0x00047B44
		public ReportColorProperty TransparentColor
		{
			get
			{
				if (this.m_transparentColor == null && this.m_defObject.TransparentColor != null)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo transparentColor = this.m_defObject.TransparentColor;
					if (transparentColor != null)
					{
						this.m_transparentColor = new ReportColorProperty(transparentColor.IsExpression, this.m_defObject.TransparentColor.OriginalText, transparentColor.IsExpression ? null : new ReportColor(transparentColor.StringValue.Trim(), true), transparentColor.IsExpression ? new ReportColor("", Color.Empty, true) : null);
					}
				}
				return this.m_transparentColor;
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x000499D4 File Offset: 0x00047BD4
		public ReportEnumProperty<MapResizeMode> ResizeMode
		{
			get
			{
				if (this.m_resizeMode == null && this.m_defObject.ResizeMode != null)
				{
					this.m_resizeMode = new ReportEnumProperty<MapResizeMode>(this.m_defObject.ResizeMode.IsExpression, this.m_defObject.ResizeMode.OriginalText, EnumTranslator.TranslateMapResizeMode(this.m_defObject.ResizeMode.StringValue, null));
				}
				return this.m_resizeMode;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x00049A3D File Offset: 0x00047C3D
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00049A45 File Offset: 0x00047C45
		internal MapMarkerImage MapMarkerImageDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x00049A4D File Offset: 0x00047C4D
		public MapMarkerImageInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapMarkerImageInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00049A7D File Offset: 0x00047C7D
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000865 RID: 2149
		private Map m_map;

		// Token: 0x04000866 RID: 2150
		private MapMarkerImage m_defObject;

		// Token: 0x04000867 RID: 2151
		private MapMarkerImageInstance m_instance;

		// Token: 0x04000868 RID: 2152
		private ReportEnumProperty<Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType> m_source;

		// Token: 0x04000869 RID: 2153
		private ReportVariantProperty m_value;

		// Token: 0x0400086A RID: 2154
		private ReportStringProperty m_mIMEType;

		// Token: 0x0400086B RID: 2155
		private ReportColorProperty m_transparentColor;

		// Token: 0x0400086C RID: 2156
		private ReportEnumProperty<MapResizeMode> m_resizeMode;
	}
}
