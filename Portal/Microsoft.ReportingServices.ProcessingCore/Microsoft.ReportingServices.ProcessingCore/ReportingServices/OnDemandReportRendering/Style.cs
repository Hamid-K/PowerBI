using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000346 RID: 838
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class Style : StyleBase
	{
		// Token: 0x06002020 RID: 8224 RVA: 0x0007B018 File Offset: 0x00079218
		internal Style(ReportElement reportElement, IReportScope reportScope, IStyleContainer styleContainer, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(renderingContext)
		{
			this.m_reportElement = reportElement;
			this.m_lineStyleDefaults = new Microsoft.ReportingServices.OnDemandReportRendering.Style.StyleDefaults(true, Microsoft.ReportingServices.OnDemandReportRendering.Style.GetDefaultFontFamily(renderingContext));
			this.m_normalStyleDefaults = new Microsoft.ReportingServices.OnDemandReportRendering.Style.StyleDefaults(false, Microsoft.ReportingServices.OnDemandReportRendering.Style.GetDefaultFontFamily(renderingContext));
			this.m_reportScope = reportScope;
			this.m_iStyleContainer = styleContainer;
			this.m_isOldSnapshot = false;
			ObjectType objectType = styleContainer.ObjectType;
			if (objectType <= ObjectType.GaugePanel)
			{
				if (objectType == ObjectType.Line)
				{
					this.m_isLineBorderStyle = true;
					this.m_styleDefaults = this.LineStyleDefaults;
					return;
				}
				if (objectType == ObjectType.GaugePanel)
				{
					GaugePanel gaugePanel = reportElement as GaugePanel;
					this.m_disallowBorderTransparencyOnDynamicImage = gaugePanel != null;
					this.m_isDynamicImageStyle = true;
					this.m_styleDefaults = this.NormalStyleDefaults;
					return;
				}
			}
			else
			{
				if (objectType == ObjectType.Chart)
				{
					Microsoft.ReportingServices.OnDemandReportRendering.Chart chart = reportElement as Microsoft.ReportingServices.OnDemandReportRendering.Chart;
					this.m_disallowBorderTransparencyOnDynamicImage = chart != null;
					this.m_isDynamicImageStyle = true;
					this.m_styleDefaults = this.NormalStyleDefaults;
					return;
				}
				if (objectType == ObjectType.Map)
				{
					Map map = reportElement as Map;
					this.m_disallowBorderTransparencyOnDynamicImage = map != null;
					this.m_isDynamicImageStyle = true;
					this.m_styleDefaults = this.NormalStyleDefaults;
					return;
				}
			}
			this.m_styleDefaults = this.NormalStyleDefaults;
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0007B134 File Offset: 0x00079334
		internal Style(Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, bool useRenderStyle)
			: base(renderingContext)
		{
			this.m_isOldSnapshot = true;
			this.m_renderReportItem = renderReportItem;
			this.m_lineStyleDefaults = new Microsoft.ReportingServices.OnDemandReportRendering.Style.StyleDefaults(true, null);
			this.m_normalStyleDefaults = new Microsoft.ReportingServices.OnDemandReportRendering.Style.StyleDefaults(false, null);
			if (useRenderStyle)
			{
				this.m_cachedRenderStyle = renderReportItem.Style;
			}
			if (renderReportItem is Microsoft.ReportingServices.ReportRendering.Line)
			{
				this.m_isLineBorderStyle = true;
				this.m_styleDefaults = this.LineStyleDefaults;
				return;
			}
			this.m_styleDefaults = this.NormalStyleDefaults;
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x0007B1B4 File Offset: 0x000793B4
		internal Style(Microsoft.ReportingServices.ReportProcessing.Style styleDefinition, object[] styleValues, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(renderingContext)
		{
			this.m_isOldSnapshot = true;
			this.m_isDynamicImageStyle = true;
			this.m_lineStyleDefaults = new Microsoft.ReportingServices.OnDemandReportRendering.Style.StyleDefaults(true, Microsoft.ReportingServices.OnDemandReportRendering.Style.GetDefaultFontFamily(renderingContext));
			this.m_normalStyleDefaults = new Microsoft.ReportingServices.OnDemandReportRendering.Style.StyleDefaults(false, Microsoft.ReportingServices.OnDemandReportRendering.Style.GetDefaultFontFamily(renderingContext));
			this.m_styleDef = styleDefinition;
			this.m_styleValues = styleValues;
			this.m_styleDefaults = this.NormalStyleDefaults;
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x0007B224 File Offset: 0x00079424
		internal Style(ReportElement reportElement, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(renderingContext)
		{
			this.m_isOldSnapshot = true;
			this.m_reportElement = reportElement;
			this.m_lineStyleDefaults = new Microsoft.ReportingServices.OnDemandReportRendering.Style.StyleDefaults(true, Microsoft.ReportingServices.OnDemandReportRendering.Style.GetDefaultFontFamily(renderingContext));
			this.m_normalStyleDefaults = new Microsoft.ReportingServices.OnDemandReportRendering.Style.StyleDefaults(false, Microsoft.ReportingServices.OnDemandReportRendering.Style.GetDefaultFontFamily(renderingContext));
			this.m_styleDefaults = this.NormalStyleDefaults;
			this.m_reportScope = reportElement.ReportScope;
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x0007B28F File Offset: 0x0007948F
		internal IReportScope ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x0007B297 File Offset: 0x00079497
		internal ReportElement ReportElement
		{
			get
			{
				return this.m_reportElement;
			}
		}

		// Token: 0x17001223 RID: 4643
		public override ReportProperty this[StyleAttributeNames style]
		{
			get
			{
				return this.GetReportProperty(style);
			}
		}

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x0007B2A8 File Offset: 0x000794A8
		public override List<StyleAttributeNames> SharedStyleAttributes
		{
			get
			{
				if (this.m_sharedStyles == null)
				{
					this.PopulateCollections();
				}
				return this.m_sharedStyles;
			}
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x0007B2BE File Offset: 0x000794BE
		public override List<StyleAttributeNames> NonSharedStyleAttributes
		{
			get
			{
				if (this.m_nonSharedStyles == null)
				{
					this.PopulateCollections();
				}
				return this.m_nonSharedStyles;
			}
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x0007B2D4 File Offset: 0x000794D4
		public override BackgroundImage BackgroundImage
		{
			get
			{
				if (this.m_backgroundImage == null)
				{
					this.m_backgroundImage = this.GetReportProperty(StyleAttributeNames.BackgroundImage) as BackgroundImage;
				}
				return this.m_backgroundImage;
			}
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x0007B2F7 File Offset: 0x000794F7
		public override Border Border
		{
			get
			{
				if (this.m_border == null)
				{
					this.m_border = new Border(this, Border.Position.Default, this.m_isLineBorderStyle);
				}
				return this.m_border;
			}
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x0007B31A File Offset: 0x0007951A
		public override Border TopBorder
		{
			get
			{
				if (this.m_topBorder == null && this.HasBorderProperties(Border.Position.Top))
				{
					this.m_topBorder = new Border(this, Border.Position.Top, false);
				}
				return this.m_topBorder;
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x0007B341 File Offset: 0x00079541
		public override Border RightBorder
		{
			get
			{
				if (this.m_rightBorder == null && this.HasBorderProperties(Border.Position.Right))
				{
					this.m_rightBorder = new Border(this, Border.Position.Right, false);
				}
				return this.m_rightBorder;
			}
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x0600202D RID: 8237 RVA: 0x0007B368 File Offset: 0x00079568
		public override Border BottomBorder
		{
			get
			{
				if (this.m_bottomBorder == null && this.HasBorderProperties(Border.Position.Bottom))
				{
					this.m_bottomBorder = new Border(this, Border.Position.Bottom, false);
				}
				return this.m_bottomBorder;
			}
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x0600202E RID: 8238 RVA: 0x0007B38F File Offset: 0x0007958F
		public override Border LeftBorder
		{
			get
			{
				if (this.m_leftBorder == null && this.HasBorderProperties(Border.Position.Left))
				{
					this.m_leftBorder = new Border(this, Border.Position.Left, false);
				}
				return this.m_leftBorder;
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x0600202F RID: 8239 RVA: 0x0007B3B6 File Offset: 0x000795B6
		public override ReportColorProperty BackgroundGradientEndColor
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.BackgroundGradientEndColor) as ReportColorProperty;
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x0007B3C5 File Offset: 0x000795C5
		public override ReportColorProperty BackgroundColor
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.BackgroundColor) as ReportColorProperty;
			}
		}

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x0007B3D4 File Offset: 0x000795D4
		public override ReportColorProperty Color
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.Color) as ReportColorProperty;
			}
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x0007B3E3 File Offset: 0x000795E3
		public override ReportEnumProperty<FontStyles> FontStyle
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.FontStyle) as ReportEnumProperty<FontStyles>;
			}
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06002033 RID: 8243 RVA: 0x0007B3F2 File Offset: 0x000795F2
		public override ReportStringProperty FontFamily
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.FontFamily) as ReportStringProperty;
			}
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x0007B401 File Offset: 0x00079601
		public override ReportEnumProperty<FontWeights> FontWeight
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.FontWeight) as ReportEnumProperty<FontWeights>;
			}
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x0007B410 File Offset: 0x00079610
		public override ReportStringProperty Format
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.Format) as ReportStringProperty;
			}
		}

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06002036 RID: 8246 RVA: 0x0007B41F File Offset: 0x0007961F
		public override ReportEnumProperty<TextDecorations> TextDecoration
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.TextDecoration) as ReportEnumProperty<TextDecorations>;
			}
		}

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x0007B42E File Offset: 0x0007962E
		public override ReportEnumProperty<TextAlignments> TextAlign
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.TextAlign) as ReportEnumProperty<TextAlignments>;
			}
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x0007B43D File Offset: 0x0007963D
		public override ReportEnumProperty<VerticalAlignments> VerticalAlign
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.VerticalAlign) as ReportEnumProperty<VerticalAlignments>;
			}
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x0007B44C File Offset: 0x0007964C
		public override ReportEnumProperty<Directions> Direction
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.Direction) as ReportEnumProperty<Directions>;
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x0007B45B File Offset: 0x0007965B
		public override ReportEnumProperty<WritingModes> WritingMode
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.WritingMode) as ReportEnumProperty<WritingModes>;
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x0007B46A File Offset: 0x0007966A
		public override ReportStringProperty Language
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.Language) as ReportStringProperty;
			}
		}

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x0600203C RID: 8252 RVA: 0x0007B479 File Offset: 0x00079679
		public override ReportEnumProperty<UnicodeBiDiTypes> UnicodeBiDi
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.UnicodeBiDi) as ReportEnumProperty<UnicodeBiDiTypes>;
			}
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x0007B488 File Offset: 0x00079688
		public override ReportEnumProperty<Calendars> Calendar
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.Calendar) as ReportEnumProperty<Calendars>;
			}
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x0600203E RID: 8254 RVA: 0x0007B497 File Offset: 0x00079697
		public override ReportStringProperty CurrencyLanguage
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.CurrencyLanguage) as ReportStringProperty;
			}
		}

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x0007B4A6 File Offset: 0x000796A6
		public override ReportStringProperty NumeralLanguage
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.NumeralLanguage) as ReportStringProperty;
			}
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x0007B4B5 File Offset: 0x000796B5
		public override ReportEnumProperty<BackgroundGradients> BackgroundGradientType
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.BackgroundGradientType) as ReportEnumProperty<BackgroundGradients>;
			}
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x0007B4C4 File Offset: 0x000796C4
		public override ReportSizeProperty FontSize
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.FontSize) as ReportSizeProperty;
			}
		}

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x0007B4D3 File Offset: 0x000796D3
		public override ReportSizeProperty PaddingLeft
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.PaddingLeft) as ReportSizeProperty;
			}
		}

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x0007B4E2 File Offset: 0x000796E2
		public override ReportSizeProperty PaddingRight
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.PaddingRight) as ReportSizeProperty;
			}
		}

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x0007B4F1 File Offset: 0x000796F1
		public override ReportSizeProperty PaddingTop
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.PaddingTop) as ReportSizeProperty;
			}
		}

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x0007B500 File Offset: 0x00079700
		public override ReportSizeProperty PaddingBottom
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.PaddingBottom) as ReportSizeProperty;
			}
		}

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x0007B50F File Offset: 0x0007970F
		public override ReportSizeProperty LineHeight
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.LineHeight) as ReportSizeProperty;
			}
		}

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x0007B51E File Offset: 0x0007971E
		public override ReportIntProperty NumeralVariant
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.NumeralVariant) as ReportIntProperty;
			}
		}

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06002048 RID: 8264 RVA: 0x0007B52D File Offset: 0x0007972D
		public override ReportEnumProperty<TextEffects> TextEffect
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.TextEffect) as ReportEnumProperty<TextEffects>;
			}
		}

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06002049 RID: 8265 RVA: 0x0007B53C File Offset: 0x0007973C
		public override ReportEnumProperty<BackgroundHatchTypes> BackgroundHatchType
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.BackgroundHatchType) as ReportEnumProperty<BackgroundHatchTypes>;
			}
		}

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x0007B54B File Offset: 0x0007974B
		public override ReportColorProperty ShadowColor
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.ShadowColor) as ReportColorProperty;
			}
		}

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x0007B55A File Offset: 0x0007975A
		public override ReportSizeProperty ShadowOffset
		{
			get
			{
				return this.GetReportProperty(StyleAttributeNames.ShadowOffset) as ReportSizeProperty;
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x0007B569 File Offset: 0x00079769
		internal bool IsOldSnapshot
		{
			get
			{
				return this.m_isOldSnapshot;
			}
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x0600204D RID: 8269 RVA: 0x0007B571 File Offset: 0x00079771
		internal bool IsDynamicImageStyle
		{
			get
			{
				return this.m_isDynamicImageStyle;
			}
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x0007B579 File Offset: 0x00079779
		internal Microsoft.ReportingServices.ReportRendering.Style CachedRenderStyle
		{
			get
			{
				return this.m_cachedRenderStyle;
			}
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x0600204F RID: 8271 RVA: 0x0007B581 File Offset: 0x00079781
		internal IStyleContainer StyleContainer
		{
			get
			{
				return this.m_iStyleContainer;
			}
		}

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06002050 RID: 8272 RVA: 0x0007B589 File Offset: 0x00079789
		internal Microsoft.ReportingServices.OnDemandReportRendering.Style.StyleDefaults NormalStyleDefaults
		{
			get
			{
				return this.m_normalStyleDefaults;
			}
		}

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x0007B591 File Offset: 0x00079791
		internal Microsoft.ReportingServices.OnDemandReportRendering.Style.StyleDefaults LineStyleDefaults
		{
			get
			{
				return this.m_lineStyleDefaults;
			}
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x0007B599 File Offset: 0x00079799
		internal void UpdateStyleCache(Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem)
		{
			if (renderReportItem != null)
			{
				this.m_renderReportItem = renderReportItem;
				this.m_cachedRenderStyle = renderReportItem.Style;
			}
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x0007B5B1 File Offset: 0x000797B1
		internal void UpdateStyleCache(object[] styleValues)
		{
			this.m_styleValues = styleValues;
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x0007B5BC File Offset: 0x000797BC
		internal void SetNewContext()
		{
			if (this.m_backgroundImage != null && this.m_backgroundImage.Instance != null)
			{
				this.m_backgroundImage.Instance.SetNewContext();
			}
			if (this.m_border != null && this.m_border.GetInstance() != null)
			{
				this.m_border.GetInstance().SetNewContext();
			}
			if (this.m_topBorder != null && this.m_topBorder.GetInstance() != null)
			{
				this.m_topBorder.GetInstance().SetNewContext();
			}
			if (this.m_rightBorder != null && this.m_rightBorder.GetInstance() != null)
			{
				this.m_rightBorder.GetInstance().SetNewContext();
			}
			if (this.m_bottomBorder != null && this.m_bottomBorder.GetInstance() != null)
			{
				this.m_bottomBorder.GetInstance().SetNewContext();
			}
			if (this.m_leftBorder != null && this.m_leftBorder.GetInstance() != null)
			{
				this.m_leftBorder.GetInstance().SetNewContext();
			}
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x0007B6A8 File Offset: 0x000798A8
		internal ReportColor EvaluateInstanceReportColor(StyleAttributeNames style)
		{
			ReportColor reportColor = null;
			if (this.m_isOldSnapshot)
			{
				if (this.m_isDynamicImageStyle)
				{
					if (this.m_styleDef != null)
					{
						string styleStringFromEnum = this.GetStyleStringFromEnum(style);
						Microsoft.ReportingServices.ReportProcessing.AttributeInfo attributeInfo = this.m_styleDef.StyleAttributes[styleStringFromEnum];
						if (attributeInfo != null)
						{
							string text;
							if (attributeInfo.IsExpression)
							{
								text = this.m_styleValues[attributeInfo.IntValue] as string;
							}
							else
							{
								text = attributeInfo.Value;
							}
							if (text != null)
							{
								reportColor = new ReportColor(text, false);
							}
						}
					}
				}
				else if (this.IsAvailableStyle(style))
				{
					ReportColor reportColor2 = null;
					if (this.m_cachedRenderStyle != null)
					{
						reportColor2 = this.m_cachedRenderStyle[this.GetStyleStringFromEnum(style)] as ReportColor;
					}
					if (reportColor2 != null)
					{
						reportColor = new ReportColor(reportColor2);
					}
				}
			}
			else if (this.m_iStyleContainer.StyleClass != null)
			{
				this.m_renderingContext.OdpContext.SetupContext(this.m_iStyleContainer.InstancePath, this.m_reportScope.ReportScopeInstance);
				string text2 = this.m_iStyleContainer.StyleClass.EvaluateStyle(this.m_iStyleContainer.ObjectType, this.m_iStyleContainer.Name, (Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId)style, this.m_renderingContext.OdpContext) as string;
				if (text2 != null)
				{
					if (this.m_disallowBorderTransparencyOnDynamicImage)
					{
						if (style <= StyleAttributeNames.BorderColorBottom)
						{
							if (!ReportColor.TryParse(text2, out reportColor))
							{
								this.m_renderingContext.OdpContext.ErrorContext.Register(ProcessingErrorCode.rsInvalidColor, Severity.Warning, this.m_iStyleContainer.ObjectType, this.m_iStyleContainer.Name, this.GetStyleStringFromEnum(style), new string[] { text2 });
							}
						}
						else
						{
							reportColor = new ReportColor(text2, this.m_isDynamicImageStyle);
						}
					}
					else
					{
						reportColor = new ReportColor(text2, this.m_isDynamicImageStyle);
					}
				}
			}
			return reportColor;
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x0007B864 File Offset: 0x00079A64
		internal ReportSize EvaluateInstanceReportSize(StyleAttributeNames style)
		{
			ReportSize reportSize = null;
			if (this.m_isOldSnapshot)
			{
				if (this.m_isDynamicImageStyle)
				{
					if (this.m_styleDef != null)
					{
						string styleStringFromEnum = this.GetStyleStringFromEnum(style);
						Microsoft.ReportingServices.ReportProcessing.AttributeInfo attributeInfo = this.m_styleDef.StyleAttributes[styleStringFromEnum];
						if (attributeInfo != null)
						{
							object obj;
							if (attributeInfo.IsExpression)
							{
								obj = this.m_styleValues[attributeInfo.IntValue];
							}
							else
							{
								obj = attributeInfo.Value;
							}
							if (obj != null)
							{
								reportSize = new ReportSize(obj as string);
							}
						}
					}
				}
				else if (this.IsAvailableStyle(style))
				{
					ReportSize reportSize2 = null;
					if (this.m_cachedRenderStyle != null)
					{
						reportSize2 = this.m_cachedRenderStyle[this.GetStyleStringFromEnum(style)] as ReportSize;
					}
					if (reportSize2 != null)
					{
						reportSize = new ReportSize(reportSize2);
					}
				}
			}
			else if (this.m_iStyleContainer.StyleClass != null)
			{
				this.m_renderingContext.OdpContext.SetupContext(this.m_iStyleContainer.InstancePath, this.m_reportScope.ReportScopeInstance);
				string text = this.m_iStyleContainer.StyleClass.EvaluateStyle(this.m_iStyleContainer.ObjectType, this.m_iStyleContainer.Name, (Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId)style, this.m_renderingContext.OdpContext) as string;
				if (text != null)
				{
					reportSize = new ReportSize(text);
				}
			}
			return reportSize;
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x0007B9A4 File Offset: 0x00079BA4
		internal string EvaluateInstanceStyleString(StyleAttributeNames style)
		{
			string text = null;
			if (this.m_isOldSnapshot)
			{
				if (this.m_isDynamicImageStyle)
				{
					if (this.m_styleDef != null)
					{
						string styleStringFromEnum = this.GetStyleStringFromEnum(style);
						Microsoft.ReportingServices.ReportProcessing.AttributeInfo attributeInfo = this.m_styleDef.StyleAttributes[styleStringFromEnum];
						if (attributeInfo != null)
						{
							if (attributeInfo.IsExpression)
							{
								text = this.m_styleValues[attributeInfo.IntValue] as string;
							}
							else
							{
								text = attributeInfo.Value;
							}
						}
					}
				}
				else if (this.IsAvailableStyle(style) && this.m_cachedRenderStyle != null)
				{
					text = this.m_cachedRenderStyle[this.GetStyleStringFromEnum(style)] as string;
				}
			}
			else if (this.m_iStyleContainer.StyleClass != null)
			{
				text = this.EvaluateInstanceStyleString((Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId)style);
			}
			return text;
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x0007BA58 File Offset: 0x00079C58
		internal string EvaluateInstanceStyleString(Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId style)
		{
			this.m_renderingContext.OdpContext.SetupContext(this.m_iStyleContainer.InstancePath, this.m_reportScope.ReportScopeInstance);
			return this.m_iStyleContainer.StyleClass.EvaluateStyle(this.m_iStyleContainer.ObjectType, this.m_iStyleContainer.Name, style, this.m_renderingContext.OdpContext) as string;
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x0007BAC4 File Offset: 0x00079CC4
		internal int EvaluateInstanceStyleInt(StyleAttributeNames style, int defaultValue)
		{
			object obj = null;
			if (this.m_isOldSnapshot)
			{
				if (this.m_isDynamicImageStyle)
				{
					if (this.m_styleDef != null)
					{
						string styleStringFromEnum = this.GetStyleStringFromEnum(style);
						Microsoft.ReportingServices.ReportProcessing.AttributeInfo attributeInfo = this.m_styleDef.StyleAttributes[styleStringFromEnum];
						if (attributeInfo != null)
						{
							if (attributeInfo.IsExpression)
							{
								obj = this.m_styleValues[attributeInfo.IntValue];
							}
							else
							{
								obj = attributeInfo.IntValue;
							}
						}
					}
				}
				else if (this.IsAvailableStyle(style) && this.m_cachedRenderStyle != null)
				{
					obj = this.m_cachedRenderStyle[this.GetStyleStringFromEnum(style)];
				}
			}
			else if (this.m_iStyleContainer.StyleClass != null)
			{
				obj = this.EvaluateInstanceStyleInt((Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId)style);
			}
			if (obj != null && obj is int)
			{
				return (int)obj;
			}
			return defaultValue;
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x0007BB7C File Offset: 0x00079D7C
		private object EvaluateInstanceStyleInt(Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId style)
		{
			this.m_renderingContext.OdpContext.SetupContext(this.m_iStyleContainer.InstancePath, this.m_reportScope.ReportScopeInstance);
			return this.m_iStyleContainer.StyleClass.EvaluateStyle(this.m_iStyleContainer.ObjectType, this.m_iStyleContainer.Name, style, this.m_renderingContext.OdpContext);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x0007BBE1 File Offset: 0x00079DE1
		internal int EvaluateInstanceStyleEnum(StyleAttributeNames style)
		{
			return this.EvaluateInstanceStyleEnum(style, 1);
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x0007BBEC File Offset: 0x00079DEC
		internal int EvaluateInstanceStyleEnum(StyleAttributeNames style, int styleDefaultValueIfNull)
		{
			int? num = null;
			if (this.m_isOldSnapshot)
			{
				if (this.m_isDynamicImageStyle)
				{
					if (this.m_styleDef != null)
					{
						string styleStringFromEnum = this.GetStyleStringFromEnum(style);
						Microsoft.ReportingServices.ReportProcessing.AttributeInfo attributeInfo = this.m_styleDef.StyleAttributes[styleStringFromEnum];
						if (attributeInfo != null)
						{
							string text;
							if (attributeInfo.IsExpression)
							{
								text = this.m_styleValues[attributeInfo.IntValue] as string;
							}
							else
							{
								text = attributeInfo.Value;
							}
							if (text != null)
							{
								num = new int?(StyleTranslator.TranslateStyle(style, text, null, this.m_isDynamicImageStyle));
							}
						}
					}
				}
				else if (this.IsAvailableStyle(style) && this.m_cachedRenderStyle != null)
				{
					string text2 = this.m_cachedRenderStyle[this.GetStyleStringFromEnum(style)] as string;
					if (text2 != null)
					{
						num = new int?(StyleTranslator.TranslateStyle(style, text2, null, this.m_isDynamicImageStyle));
					}
				}
			}
			else if (this.m_iStyleContainer.StyleClass != null)
			{
				num = this.EvaluateInstanceStyleEnum((Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId)style);
			}
			if (num == null)
			{
				return styleDefaultValueIfNull;
			}
			return num.Value;
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x0007BCEC File Offset: 0x00079EEC
		internal int? EvaluateInstanceStyleEnum(Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId style)
		{
			int? num = null;
			this.m_renderingContext.OdpContext.SetupContext(this.m_iStyleContainer.InstancePath, this.m_reportScope.ReportScopeInstance);
			object obj = this.m_iStyleContainer.StyleClass.EvaluateStyle(this.m_iStyleContainer.ObjectType, this.m_iStyleContainer.Name, style, this.m_renderingContext.OdpContext);
			if (obj != null)
			{
				string text = obj as string;
				if (text != null)
				{
					num = new int?(StyleTranslator.TranslateStyle((StyleAttributeNames)style, text, null, this.m_isDynamicImageStyle));
				}
				else
				{
					num = new int?((int)obj);
				}
				if (Microsoft.ReportingServices.ReportPublishing.Validator.IsDynamicImageReportItem(this.m_iStyleContainer.ObjectType) && !Microsoft.ReportingServices.ReportPublishing.Validator.IsDynamicImageSubElement(this.m_iStyleContainer) && (num.Value == 6 || num.Value == 7))
				{
					num = new int?(3);
				}
			}
			return num;
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x0007BDC8 File Offset: 0x00079FC8
		internal object EvaluateInstanceStyleVariant(Microsoft.ReportingServices.ReportIntermediateFormat.Style.StyleId style)
		{
			this.m_renderingContext.OdpContext.SetupContext(this.m_iStyleContainer.InstancePath, this.m_reportScope.ReportScopeInstance);
			return this.m_iStyleContainer.StyleClass.EvaluateStyle(this.m_iStyleContainer.ObjectType, this.m_iStyleContainer.Name, style, this.m_renderingContext.OdpContext);
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x0007BE30 File Offset: 0x0007A030
		internal void ConstructStyleDefinition()
		{
			Global.Tracer.Assert(this.ReportElement != null, "(ReportElement != null)");
			Global.Tracer.Assert(this.ReportElement is Microsoft.ReportingServices.OnDemandReportRendering.ReportItem, "(ReportElement is ReportItem)");
			Global.Tracer.Assert(this.ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Definition, "(ReportElement.CriGenerationPhase == ReportElement.CriGenerationPhases.Definition)");
			Global.Tracer.Assert(this.ReportElement.ReportItemDef.StyleClass == null, "(ReportElement.ReportItemDef.StyleClass == null)");
			this.ReportElement.ReportItemDef.StyleClass = new Microsoft.ReportingServices.ReportIntermediateFormat.Style(Microsoft.ReportingServices.ReportIntermediateFormat.ConstructionPhase.Publishing);
			this.ReportElement.ReportItemDef.StyleClass.InitializeForCRIGeneratedReportItem();
			this.Border.ConstructBorderDefinition();
			this.TopBorder.ConstructBorderDefinition();
			this.BottomBorder.ConstructBorderDefinition();
			this.LeftBorder.ConstructBorderDefinition();
			this.RightBorder.ConstructBorderDefinition();
			StyleInstance style = ((Microsoft.ReportingServices.OnDemandReportRendering.ReportItem)this.ReportElement).Instance.Style;
			Global.Tracer.Assert(!this.BackgroundColor.IsExpression, "(!this.BackgroundColor.IsExpression)");
			if (style.IsBackgroundColorAssigned)
			{
				string text = ((style.BackgroundColor != null) ? style.BackgroundColor.ToString() : null);
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.BackgroundColor), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(text));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.BackgroundColor), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.BackgroundGradientEndColor.IsExpression, "(!this.BackgroundGradientEndColor.IsExpression)");
			if (style.IsBackgroundGradientEndColorAssigned)
			{
				string text2 = ((style.BackgroundGradientEndColor != null) ? style.BackgroundGradientEndColor.ToString() : null);
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.BackgroundGradientEndColor), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(text2));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.BackgroundGradientEndColor), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.Color.IsExpression, "(!this.Color.IsExpression)");
			if (style.IsColorAssigned)
			{
				string text3 = ((style.Color != null) ? style.Color.ToString() : null);
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.Color), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(text3));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.Color), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.FontStyle.IsExpression, "(!this.FontStyle.IsExpression)");
			if (style.IsFontStyleAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.FontStyle), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.FontStyle.ToString()));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.FontStyle), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.FontFamily.IsExpression, "(!this.FontFamily.IsExpression)");
			if (style.IsFontFamilyAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.FontFamily), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.FontFamily));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.FontFamily), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.FontWeight.IsExpression, "(!this.FontWeight.IsExpression)");
			if (style.IsFontWeightAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.FontWeight), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.FontWeight.ToString()));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.FontWeight), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.Format.IsExpression, "(!this.Format.IsExpression)");
			if (style.IsFormatAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.Format), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.Format));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.Format), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.TextDecoration.IsExpression, "(!this.TextDecoration.IsExpression)");
			if (style.IsTextDecorationAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.TextDecoration), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.TextDecoration.ToString()));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.TextDecoration), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.TextAlign.IsExpression, "(!this.TextAlign.IsExpression)");
			if (style.IsTextAlignAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.TextAlign), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.TextAlign.ToString()));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.TextAlign), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.VerticalAlign.IsExpression, "(!this.VerticalAlign.IsExpression)");
			if (style.IsVerticalAlignAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.VerticalAlign), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.VerticalAlign.ToString()));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.VerticalAlign), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.Direction.IsExpression, "(!this.Direction.IsExpression)");
			if (style.IsDirectionAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.Direction), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.Direction.ToString()));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.Direction), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.WritingMode.IsExpression, "(!this.WritingMode.IsExpression)");
			if (style.IsWritingModeAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.WritingMode), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.WritingMode.ToString()));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.WritingMode), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.Language.IsExpression, "(!this.Language.IsExpression)");
			if (style.IsLanguageAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.Language), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.Language));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.Language), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.UnicodeBiDi.IsExpression, "(!this.UnicodeBiDi.IsExpression)");
			if (style.IsUnicodeBiDiAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.UnicodeBiDi), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.UnicodeBiDi.ToString()));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.UnicodeBiDi), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.Calendar.IsExpression, "(!this.Calendar.IsExpression)");
			if (style.IsCalendarAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.Calendar), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.Calendar.ToString()));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.Calendar), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.CurrencyLanguage.IsExpression, "(!this.CurrencyLanguage.IsExpression)");
			if (style.IsCurrencyLanguageAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.CurrencyLanguage), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.CurrencyLanguage));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.CurrencyLanguage), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.NumeralLanguage.IsExpression, "(!this.NumeralLanguage.IsExpression)");
			if (style.IsNumeralLanguageAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.NumeralLanguage), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.NumeralLanguage));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.NumeralLanguage), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.BackgroundGradientType.IsExpression, "(!this.BackgroundGradientType.IsExpression)");
			if (style.IsBackgroundGradientTypeAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.BackgroundGradientType), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.BackgroundGradientType.ToString()));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.BackgroundGradientType), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.FontSize.IsExpression, "(!this.FontSize.IsExpression)");
			if (style.IsFontSizeAssigned)
			{
				string text4 = ((style.FontSize != null) ? style.FontSize.ToString() : null);
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.FontSize), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(text4));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.FontSize), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.PaddingLeft.IsExpression, "(!this.PaddingLeft.IsExpression)");
			if (style.IsPaddingLeftAssigned)
			{
				string text5 = ((style.PaddingLeft != null) ? style.PaddingLeft.ToString() : null);
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.PaddingLeft), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(text5));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.PaddingLeft), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.PaddingRight.IsExpression, "(!this.PaddingRight.IsExpression)");
			if (style.IsPaddingRightAssigned)
			{
				string text6 = ((style.PaddingRight != null) ? style.PaddingRight.ToString() : null);
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.PaddingRight), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(text6));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.PaddingRight), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.PaddingTop.IsExpression, "(!this.PaddingTop.IsExpression)");
			if (style.IsPaddingTopAssigned)
			{
				string text7 = ((style.PaddingTop != null) ? style.PaddingTop.ToString() : null);
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.PaddingTop), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(text7));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.PaddingTop), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.PaddingBottom.IsExpression, "(!this.PaddingBottom.IsExpression)");
			if (style.IsPaddingBottomAssigned)
			{
				string text8 = ((style.PaddingBottom != null) ? style.PaddingBottom.ToString() : null);
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.PaddingBottom), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(text8));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.PaddingBottom), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.LineHeight.IsExpression, "(!this.LineHeight.IsExpression)");
			if (style.IsLineHeightAssigned)
			{
				string text9 = ((style.LineHeight != null) ? style.LineHeight.ToString() : null);
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.LineHeight), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(text9));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.LineHeight), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.NumeralVariant.IsExpression, "(!this.NumeralVariant.IsExpression)");
			if (style.IsNumeralVariantAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.NumeralVariant), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.NumeralVariant));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.NumeralVariant), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.TextEffect.IsExpression, "(!this.TextEffect.IsExpression)");
			if (style.IsTextEffectAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.TextEffect), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.TextEffect.ToString()));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.TextEffect), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.BackgroundHatchType.IsExpression, "(!this.BackgroundHatchType.IsExpression)");
			if (style.IsBackgroundHatchTypeAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.BackgroundHatchType), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.BackgroundHatchType.ToString()));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.BackgroundHatchType), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.ShadowColor.IsExpression, "(!this.ShadowColor.IsExpression)");
			if (style.IsShadowColorAssigned)
			{
				string text10 = ((style.Color != null) ? style.ShadowColor.ToString() : null);
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.ShadowColor), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(text10));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.ShadowColor), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			Global.Tracer.Assert(!this.ShadowOffset.IsExpression, "(!this.ShadowOffset.IsExpression)");
			if (style.IsShadowOffsetAssigned)
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.ShadowOffset), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateConstExpression(style.ShadowOffset.ToString()));
			}
			else
			{
				this.m_iStyleContainer.StyleClass.AddAttribute(this.GetStyleStringFromEnum(StyleAttributeNames.ShadowOffset), Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
			}
			foreach (StyleAttributeNames styleAttributeNames in StyleBase.StyleNames)
			{
				string styleStringFromEnum = this.GetStyleStringFromEnum(styleAttributeNames);
				Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo attributeInfo;
				if (!this.m_iStyleContainer.StyleClass.GetAttributeInfo(styleStringFromEnum, out attributeInfo))
				{
					this.m_iStyleContainer.StyleClass.AddAttribute(styleStringFromEnum, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.CreateEmptyExpression());
				}
				else if (!attributeInfo.IsExpression && attributeInfo.Value == null)
				{
					this.m_iStyleContainer.StyleClass.StyleAttributes.Remove(styleStringFromEnum);
				}
			}
			this.m_sharedStyles = null;
			this.m_nonSharedStyles = null;
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x0007CD00 File Offset: 0x0007AF00
		private bool HasBorderProperties(Border.Position position)
		{
			if (position == Border.Position.Default)
			{
				return true;
			}
			if (!this.m_isOldSnapshot)
			{
				return true;
			}
			if (!this.IsAvailableStyle(StyleAttributeNames.BorderColor))
			{
				return false;
			}
			if (this.m_cachedRenderStyle == null && this.m_styleDef == null)
			{
				return false;
			}
			string text = null;
			switch (position)
			{
			case Border.Position.Top:
				text = "BorderStyleTop";
				break;
			case Border.Position.Left:
				text = "BorderStyleLeft";
				break;
			case Border.Position.Right:
				text = "BorderStyleRight";
				break;
			case Border.Position.Bottom:
				text = "BorderStyleBottom";
				break;
			}
			if (this.m_isDynamicImageStyle)
			{
				if (this.m_styleDef.StyleAttributes[text] != null)
				{
					return true;
				}
			}
			else if (this.m_cachedRenderStyle.GetStyleDefinition(text) != null)
			{
				return true;
			}
			switch (position)
			{
			case Border.Position.Top:
				text = "BorderColorTop";
				break;
			case Border.Position.Left:
				text = "BorderColorLeft";
				break;
			case Border.Position.Right:
				text = "BorderColorRight";
				break;
			case Border.Position.Bottom:
				text = "BorderColorBottom";
				break;
			}
			if (this.m_isDynamicImageStyle)
			{
				if (this.m_styleDef.StyleAttributes[text] != null)
				{
					return true;
				}
			}
			else if (this.m_cachedRenderStyle.GetStyleDefinition(text) != null)
			{
				return true;
			}
			switch (position)
			{
			case Border.Position.Top:
				text = "BorderWidthTop";
				break;
			case Border.Position.Left:
				text = "BorderWidthLeft";
				break;
			case Border.Position.Right:
				text = "BorderWidthRight";
				break;
			case Border.Position.Bottom:
				text = "BorderWidthBottom";
				break;
			}
			if (this.m_isDynamicImageStyle)
			{
				if (this.m_styleDef.StyleAttributes[text] != null)
				{
					return true;
				}
			}
			else if (this.m_cachedRenderStyle.GetStyleDefinition(text) != null)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x0007CE6E File Offset: 0x0007B06E
		private static string GetDefaultFontFamily(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			if (renderingContext.OdpContext != null && renderingContext.OdpContext.ReportDefinition != null)
			{
				return renderingContext.OdpContext.ReportDefinition.DefaultFontFamily;
			}
			return null;
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x0007CE98 File Offset: 0x0007B098
		private void PopulateCollections()
		{
			if (this.m_isOldSnapshot)
			{
				if (this.m_cachedRenderStyle == null && this.m_styleDef == null)
				{
					return;
				}
				this.m_sharedStyles = new List<StyleAttributeNames>();
				this.m_nonSharedStyles = new List<StyleAttributeNames>();
				using (IEnumerator<StyleAttributeNames> enumerator = StyleBase.StyleNames.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						StyleAttributeNames styleAttributeNames = enumerator.Current;
						bool flag;
						if (StyleAttributeNames.BackgroundImage != styleAttributeNames)
						{
							Microsoft.ReportingServices.ReportProcessing.AttributeInfo attributeInfo = null;
							if (this.IsAvailableStyle(styleAttributeNames))
							{
								if (this.m_isDynamicImageStyle)
								{
									attributeInfo = this.m_styleDef.StyleAttributes[this.GetStyleStringFromEnum(styleAttributeNames)];
								}
								else
								{
									attributeInfo = this.m_cachedRenderStyle.GetStyleDefinition(this.GetStyleStringFromEnum(styleAttributeNames));
								}
							}
							if (attributeInfo != null)
							{
								if (attributeInfo.IsExpression)
								{
									this.m_nonSharedStyles.Add(styleAttributeNames);
								}
								else
								{
									this.m_sharedStyles.Add(styleAttributeNames);
								}
							}
						}
						else if (!this.m_isDynamicImageStyle && this.m_cachedRenderStyle.HasBackgroundImage(out flag))
						{
							if (flag)
							{
								this.m_nonSharedStyles.Add(styleAttributeNames);
							}
							else
							{
								this.m_sharedStyles.Add(styleAttributeNames);
							}
						}
					}
					return;
				}
			}
			this.m_sharedStyles = new List<StyleAttributeNames>();
			this.m_nonSharedStyles = new List<StyleAttributeNames>();
			if (this.m_iStyleContainer != null && this.m_iStyleContainer.StyleClass != null && this.m_iStyleContainer.StyleClass.StyleAttributes != null)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo attributeInfo2 = null;
				foreach (StyleAttributeNames styleAttributeNames2 in StyleBase.StyleNames)
				{
					string text;
					if (StyleAttributeNames.BackgroundImage != styleAttributeNames2)
					{
						text = this.GetStyleStringFromEnum(styleAttributeNames2);
					}
					else
					{
						text = "BackgroundImageValue";
					}
					if (this.m_iStyleContainer.StyleClass.GetAttributeInfo(text, out attributeInfo2))
					{
						if (attributeInfo2.IsExpression)
						{
							this.m_nonSharedStyles.Add(styleAttributeNames2);
						}
						else
						{
							this.m_sharedStyles.Add(styleAttributeNames2);
						}
					}
				}
			}
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x0007D08C File Offset: 0x0007B28C
		internal string GetStyleStringFromEnum(StyleAttributeNames style)
		{
			switch (style)
			{
			case StyleAttributeNames.BorderColor:
				return "BorderColor";
			case StyleAttributeNames.BorderColorTop:
				return "BorderColorTop";
			case StyleAttributeNames.BorderColorLeft:
				return "BorderColorLeft";
			case StyleAttributeNames.BorderColorRight:
				return "BorderColorRight";
			case StyleAttributeNames.BorderColorBottom:
				return "BorderColorBottom";
			case StyleAttributeNames.BorderStyle:
				return "BorderStyle";
			case StyleAttributeNames.BorderStyleTop:
				return "BorderStyleTop";
			case StyleAttributeNames.BorderStyleLeft:
				return "BorderStyleLeft";
			case StyleAttributeNames.BorderStyleRight:
				return "BorderStyleRight";
			case StyleAttributeNames.BorderStyleBottom:
				return "BorderStyleBottom";
			case StyleAttributeNames.BorderWidth:
				return "BorderWidth";
			case StyleAttributeNames.BorderWidthTop:
				return "BorderWidthTop";
			case StyleAttributeNames.BorderWidthLeft:
				return "BorderWidthLeft";
			case StyleAttributeNames.BorderWidthRight:
				return "BorderWidthRight";
			case StyleAttributeNames.BorderWidthBottom:
				return "BorderWidthBottom";
			case StyleAttributeNames.BackgroundColor:
				return "BackgroundColor";
			case StyleAttributeNames.FontStyle:
				return "FontStyle";
			case StyleAttributeNames.FontFamily:
				return "FontFamily";
			case StyleAttributeNames.FontSize:
				return "FontSize";
			case StyleAttributeNames.FontWeight:
				return "FontWeight";
			case StyleAttributeNames.Format:
				return "Format";
			case StyleAttributeNames.TextDecoration:
				return "TextDecoration";
			case StyleAttributeNames.TextAlign:
				return "TextAlign";
			case StyleAttributeNames.VerticalAlign:
				return "VerticalAlign";
			case StyleAttributeNames.Color:
				return "Color";
			case StyleAttributeNames.PaddingLeft:
				return "PaddingLeft";
			case StyleAttributeNames.PaddingRight:
				return "PaddingRight";
			case StyleAttributeNames.PaddingTop:
				return "PaddingTop";
			case StyleAttributeNames.PaddingBottom:
				return "PaddingBottom";
			case StyleAttributeNames.LineHeight:
				return "LineHeight";
			case StyleAttributeNames.Direction:
				return "Direction";
			case StyleAttributeNames.WritingMode:
				return "WritingMode";
			case StyleAttributeNames.Language:
				return "Language";
			case StyleAttributeNames.UnicodeBiDi:
				return "UnicodeBiDi";
			case StyleAttributeNames.Calendar:
				return "Calendar";
			case StyleAttributeNames.NumeralLanguage:
				return "NumeralLanguage";
			case StyleAttributeNames.NumeralVariant:
				return "NumeralVariant";
			case StyleAttributeNames.BackgroundGradientType:
				return "BackgroundGradientType";
			case StyleAttributeNames.BackgroundGradientEndColor:
				return "BackgroundGradientEndColor";
			case StyleAttributeNames.BackgroundHatchType:
				return "BackgroundHatchType";
			case StyleAttributeNames.TransparentColor:
				return "TransparentColor";
			case StyleAttributeNames.ShadowColor:
				return "ShadowColor";
			case StyleAttributeNames.ShadowOffset:
				return "ShadowOffset";
			case StyleAttributeNames.Position:
				return "Position";
			case StyleAttributeNames.TextEffect:
				return "TextEffect";
			case StyleAttributeNames.BackgroundImage:
				return "BackgroundImage";
			case StyleAttributeNames.BackgroundImageRepeat:
				return "BackgroundRepeat";
			case StyleAttributeNames.BackgroundImageSource:
				return "BackgroundImageSource";
			case StyleAttributeNames.BackgroundImageValue:
				return "BackgroundImageValue";
			case StyleAttributeNames.BackgroundImageMimeType:
				return "BackgroundImageMIMEType";
			case StyleAttributeNames.CurrencyLanguage:
				return "CurrencyLanguage";
			default:
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x0007D2AC File Offset: 0x0007B4AC
		private ReportProperty GetReportProperty(StyleAttributeNames styleName)
		{
			if (styleName >= StyleAttributeNames.Count)
			{
				return null;
			}
			ReportProperty reportProperty;
			if (this.m_cachedReportProperties[(int)styleName] != null)
			{
				reportProperty = this.m_cachedReportProperties[(int)styleName];
			}
			else
			{
				if (this.m_isOldSnapshot)
				{
					if (this.m_isDynamicImageStyle)
					{
						reportProperty = this.GetOldSnapshotReportProperty(styleName, this.m_styleDef);
					}
					else
					{
						reportProperty = this.GetOldSnapshotReportProperty(styleName, this.m_cachedRenderStyle);
					}
				}
				else
				{
					reportProperty = this.GetOdpReportProperty(styleName);
				}
				if (this.ReportElement == null || this.ReportElement.CriGenerationPhase != ReportElement.CriGenerationPhases.Definition)
				{
					this.m_cachedReportProperties[(int)styleName] = reportProperty;
				}
			}
			return reportProperty;
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x0007D33C File Offset: 0x0007B53C
		internal Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo GetAttributeInfo(string styleNameString, out string expressionString)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo attributeInfo = null;
			expressionString = null;
			if (this.m_iStyleContainer.StyleClass != null && this.m_iStyleContainer.StyleClass.GetAttributeInfo(styleNameString, out attributeInfo))
			{
				if (attributeInfo.IsExpression)
				{
					expressionString = this.m_iStyleContainer.StyleClass.ExpressionList[attributeInfo.IntValue].OriginalText;
				}
				else
				{
					expressionString = attributeInfo.Value;
				}
			}
			return attributeInfo;
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x0007D3A8 File Offset: 0x0007B5A8
		private ReportProperty GetOdpReportProperty(StyleAttributeNames styleName)
		{
			string text;
			if (styleName == StyleAttributeNames.BackgroundImage)
			{
				text = "BackgroundImageValue";
			}
			else
			{
				text = this.GetStyleStringFromEnum(styleName);
			}
			string text2 = null;
			string text3 = null;
			Microsoft.ReportingServices.ReportIntermediateFormat.AttributeInfo attributeInfo = this.GetAttributeInfo(text, out text3);
			switch (styleName)
			{
			case StyleAttributeNames.BorderColor:
			case StyleAttributeNames.BorderColorTop:
			case StyleAttributeNames.BorderColorLeft:
			case StyleAttributeNames.BorderColorRight:
			case StyleAttributeNames.BorderColorBottom:
			case StyleAttributeNames.BackgroundColor:
			case StyleAttributeNames.Color:
			case StyleAttributeNames.BackgroundGradientEndColor:
			case StyleAttributeNames.ShadowColor:
			{
				ReportColor reportColor = null;
				if (!this.m_isDynamicImageStyle || styleName != StyleAttributeNames.Color)
				{
					reportColor = this.m_styleDefaults[text] as ReportColor;
				}
				if (attributeInfo == null)
				{
					return new ReportColorProperty(false, null, reportColor, reportColor);
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportColorProperty(attributeInfo.IsExpression, text3, attributeInfo.IsExpression ? null : new ReportColor(text2, this.m_isDynamicImageStyle), reportColor);
			}
			case StyleAttributeNames.BorderStyle:
			{
				BorderStyles borderStyles = (this.m_isLineBorderStyle ? BorderStyles.Solid : BorderStyles.None);
				if (attributeInfo == null)
				{
					return new ReportEnumProperty<BorderStyles>(false, null, borderStyles, borderStyles);
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportEnumProperty<BorderStyles>(attributeInfo.IsExpression, text3, StyleTranslator.TranslateBorderStyle(text2, null), borderStyles);
			}
			case StyleAttributeNames.BorderStyleTop:
			case StyleAttributeNames.BorderStyleLeft:
			case StyleAttributeNames.BorderStyleRight:
			case StyleAttributeNames.BorderStyleBottom:
				if (attributeInfo == null)
				{
					return null;
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportEnumProperty<BorderStyles>(attributeInfo.IsExpression, text3, StyleTranslator.TranslateBorderStyle(text2, null), BorderStyles.None);
			case StyleAttributeNames.BorderWidth:
			case StyleAttributeNames.FontSize:
			case StyleAttributeNames.PaddingLeft:
			case StyleAttributeNames.PaddingRight:
			case StyleAttributeNames.PaddingTop:
			case StyleAttributeNames.PaddingBottom:
			case StyleAttributeNames.LineHeight:
			case StyleAttributeNames.ShadowOffset:
				if (attributeInfo == null)
				{
					return new ReportSizeProperty(false, null, this.m_styleDefaults[text] as ReportSize);
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportSizeProperty(attributeInfo.IsExpression, text3, attributeInfo.IsExpression ? null : new ReportSize(text2, false), attributeInfo.IsExpression ? (this.m_styleDefaults[text] as ReportSize) : null);
			case StyleAttributeNames.BorderWidthTop:
			case StyleAttributeNames.BorderWidthLeft:
			case StyleAttributeNames.BorderWidthRight:
			case StyleAttributeNames.BorderWidthBottom:
				if (attributeInfo == null)
				{
					return null;
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportSizeProperty(attributeInfo.IsExpression, text3, attributeInfo.IsExpression ? null : new ReportSize(text2, false), null);
			case StyleAttributeNames.FontStyle:
				if (attributeInfo == null)
				{
					return new ReportEnumProperty<FontStyles>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumFontStyle);
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportEnumProperty<FontStyles>(attributeInfo.IsExpression, text3, StyleTranslator.TranslateFontStyle(text2, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumFontStyle);
			case StyleAttributeNames.FontFamily:
			case StyleAttributeNames.Format:
			case StyleAttributeNames.Language:
			case StyleAttributeNames.NumeralLanguage:
			case StyleAttributeNames.CurrencyLanguage:
				if (attributeInfo == null)
				{
					return new ReportStringProperty(false, null, this.m_styleDefaults[text] as string);
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportStringProperty(attributeInfo.IsExpression, text3, text2, attributeInfo.IsExpression ? (this.m_styleDefaults[text] as string) : null);
			case StyleAttributeNames.FontWeight:
				if (attributeInfo == null)
				{
					return new ReportEnumProperty<FontWeights>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumFontWeight);
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportEnumProperty<FontWeights>(attributeInfo.IsExpression, text3, StyleTranslator.TranslateFontWeight(text2, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumFontWeight);
			case StyleAttributeNames.TextDecoration:
				if (attributeInfo == null)
				{
					return new ReportEnumProperty<TextDecorations>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumTextDecoration);
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportEnumProperty<TextDecorations>(attributeInfo.IsExpression, text3, StyleTranslator.TranslateTextDecoration(text2, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumTextDecoration);
			case StyleAttributeNames.TextAlign:
				if (attributeInfo == null)
				{
					return new ReportEnumProperty<TextAlignments>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumTextAlignment);
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportEnumProperty<TextAlignments>(attributeInfo.IsExpression, text3, StyleTranslator.TranslateTextAlign(text2, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumTextAlignment);
			case StyleAttributeNames.VerticalAlign:
				if (attributeInfo == null)
				{
					return new ReportEnumProperty<VerticalAlignments>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumVerticalAlignment);
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportEnumProperty<VerticalAlignments>(attributeInfo.IsExpression, text3, StyleTranslator.TranslateVerticalAlign(text2, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumVerticalAlignment);
			case StyleAttributeNames.Direction:
				if (attributeInfo == null)
				{
					return new ReportEnumProperty<Directions>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumDirection);
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportEnumProperty<Directions>(attributeInfo.IsExpression, text3, StyleTranslator.TranslateDirection(text2, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumDirection);
			case StyleAttributeNames.WritingMode:
				if (attributeInfo == null)
				{
					return new ReportEnumProperty<WritingModes>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumWritingMode);
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportEnumProperty<WritingModes>(attributeInfo.IsExpression, text3, StyleTranslator.TranslateWritingMode(text2, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumWritingMode);
			case StyleAttributeNames.UnicodeBiDi:
				if (attributeInfo == null)
				{
					return new ReportEnumProperty<UnicodeBiDiTypes>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumUnicodeBiDiType);
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportEnumProperty<UnicodeBiDiTypes>(attributeInfo.IsExpression, text3, StyleTranslator.TranslateUnicodeBiDi(text2, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumUnicodeBiDiType);
			case StyleAttributeNames.Calendar:
				if (attributeInfo == null)
				{
					return new ReportEnumProperty<Calendars>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumCalendar);
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportEnumProperty<Calendars>(attributeInfo.IsExpression, text3, StyleTranslator.TranslateCalendar(text2, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumCalendar);
			case StyleAttributeNames.NumeralVariant:
			{
				int num = (int)this.m_styleDefaults[text];
				if (attributeInfo == null)
				{
					return new ReportIntProperty(false, null, num, num);
				}
				if (!attributeInfo.IsExpression)
				{
					num = attributeInfo.IntValue;
				}
				return new ReportIntProperty(attributeInfo.IsExpression, text3, num, num);
			}
			case StyleAttributeNames.BackgroundGradientType:
				if (attributeInfo == null)
				{
					return new ReportEnumProperty<BackgroundGradients>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumBackgroundGradient);
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportEnumProperty<BackgroundGradients>(attributeInfo.IsExpression, text3, StyleTranslator.TranslateBackgroundGradientType(text2, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumBackgroundGradient);
			case StyleAttributeNames.BackgroundHatchType:
			{
				BackgroundHatchTypes backgroundHatchTypes = StyleTranslator.TranslateBackgroundHatchType(null, null, this.m_isDynamicImageStyle);
				if (attributeInfo == null)
				{
					return new ReportEnumProperty<BackgroundHatchTypes>(false, null, BackgroundHatchTypes.Default, backgroundHatchTypes);
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportEnumProperty<BackgroundHatchTypes>(attributeInfo.IsExpression, text3, StyleTranslator.TranslateBackgroundHatchType(text2, null, this.m_isDynamicImageStyle), backgroundHatchTypes);
			}
			case StyleAttributeNames.TextEffect:
			{
				TextEffects textEffects = StyleTranslator.TranslateTextEffect(null, null, this.m_isDynamicImageStyle);
				if (attributeInfo == null)
				{
					return new ReportEnumProperty<TextEffects>(false, null, TextEffects.Default, textEffects);
				}
				if (!attributeInfo.IsExpression)
				{
					text2 = attributeInfo.Value;
				}
				return new ReportEnumProperty<TextEffects>(attributeInfo.IsExpression, text3, StyleTranslator.TranslateTextEffect(text2, null, this.m_isDynamicImageStyle), textEffects);
			}
			case StyleAttributeNames.BackgroundImage:
				if (attributeInfo != null)
				{
					return new BackgroundImage(attributeInfo.IsExpression, text3, this);
				}
				break;
			}
			return null;
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x0007D978 File Offset: 0x0007BB78
		private ReportProperty GetOldSnapshotReportProperty(StyleAttributeNames styleName, Microsoft.ReportingServices.ReportRendering.Style style)
		{
			Microsoft.ReportingServices.ReportProcessing.AttributeInfo attributeInfo = null;
			string styleStringFromEnum = this.GetStyleStringFromEnum(styleName);
			string text = null;
			if (style != null && styleName != StyleAttributeNames.BackgroundImage)
			{
				attributeInfo = style.GetStyleDefinition(styleStringFromEnum, out text);
			}
			return this.GetOldSnapshotReportProperty(attributeInfo, text, styleName, styleStringFromEnum, style);
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x0007D9B0 File Offset: 0x0007BBB0
		private ReportProperty GetOldSnapshotReportProperty(StyleAttributeNames styleName, Microsoft.ReportingServices.ReportProcessing.Style style)
		{
			Microsoft.ReportingServices.ReportProcessing.AttributeInfo attributeInfo = null;
			string text = null;
			string styleStringFromEnum = this.GetStyleStringFromEnum(styleName);
			if (style != null)
			{
				attributeInfo = this.m_styleDef.StyleAttributes[styleStringFromEnum];
				if (attributeInfo.IsExpression)
				{
					text = this.m_styleDef.ExpressionList[attributeInfo.IntValue].OriginalText;
				}
			}
			return this.GetOldSnapshotReportProperty(attributeInfo, text, styleName, styleStringFromEnum, null);
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x0007DA10 File Offset: 0x0007BC10
		private ReportProperty GetOldSnapshotReportProperty(Microsoft.ReportingServices.ReportProcessing.AttributeInfo styleDefinition, string expressionString, StyleAttributeNames styleName, string styleNameString, Microsoft.ReportingServices.ReportRendering.Style style)
		{
			if (!this.IsAvailableStyle(styleName))
			{
				styleDefinition = null;
			}
			switch (styleName)
			{
			case StyleAttributeNames.BorderColor:
			case StyleAttributeNames.BorderColorTop:
			case StyleAttributeNames.BorderColorLeft:
			case StyleAttributeNames.BorderColorRight:
			case StyleAttributeNames.BorderColorBottom:
			case StyleAttributeNames.BackgroundColor:
			case StyleAttributeNames.Color:
			case StyleAttributeNames.BackgroundGradientEndColor:
			case StyleAttributeNames.ShadowColor:
			{
				ReportColor reportColor = null;
				if (!this.m_isDynamicImageStyle || styleName != StyleAttributeNames.Color)
				{
					reportColor = this.m_styleDefaults[styleNameString] as ReportColor;
				}
				if (styleDefinition == null)
				{
					return new ReportColorProperty(false, null, reportColor, reportColor);
				}
				return new ReportColorProperty(styleDefinition.IsExpression, expressionString, styleDefinition.IsExpression ? null : new ReportColor(styleDefinition.Value, this.m_isDynamicImageStyle), reportColor);
			}
			case StyleAttributeNames.BorderStyle:
			{
				BorderStyles borderStyles = (this.m_isLineBorderStyle ? BorderStyles.Solid : BorderStyles.None);
				if (styleDefinition == null)
				{
					return new ReportEnumProperty<BorderStyles>(false, null, borderStyles, borderStyles);
				}
				return new ReportEnumProperty<BorderStyles>(styleDefinition.IsExpression, expressionString, StyleTranslator.TranslateBorderStyle(styleDefinition.Value, null), borderStyles);
			}
			case StyleAttributeNames.BorderStyleTop:
			case StyleAttributeNames.BorderStyleLeft:
			case StyleAttributeNames.BorderStyleRight:
			case StyleAttributeNames.BorderStyleBottom:
				if (styleDefinition == null)
				{
					return null;
				}
				return new ReportEnumProperty<BorderStyles>(styleDefinition.IsExpression, expressionString, StyleTranslator.TranslateBorderStyle(styleDefinition.Value, null), BorderStyles.None);
			case StyleAttributeNames.BorderWidth:
			case StyleAttributeNames.FontSize:
			case StyleAttributeNames.PaddingLeft:
			case StyleAttributeNames.PaddingRight:
			case StyleAttributeNames.PaddingTop:
			case StyleAttributeNames.PaddingBottom:
			case StyleAttributeNames.LineHeight:
			case StyleAttributeNames.ShadowOffset:
				if (styleDefinition == null)
				{
					return new ReportSizeProperty(false, null, this.m_styleDefaults[styleNameString] as ReportSize);
				}
				return new ReportSizeProperty(styleDefinition.IsExpression, expressionString, styleDefinition.IsExpression ? null : new ReportSize(styleDefinition.Value, false), styleDefinition.IsExpression ? (this.m_styleDefaults[styleNameString] as ReportSize) : null);
			case StyleAttributeNames.BorderWidthTop:
			case StyleAttributeNames.BorderWidthLeft:
			case StyleAttributeNames.BorderWidthRight:
			case StyleAttributeNames.BorderWidthBottom:
				if (styleDefinition == null)
				{
					return null;
				}
				return new ReportSizeProperty(styleDefinition.IsExpression, expressionString, styleDefinition.IsExpression ? null : new ReportSize(styleDefinition.Value, false), null);
			case StyleAttributeNames.FontStyle:
				if (styleDefinition == null)
				{
					return new ReportEnumProperty<FontStyles>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumFontStyle);
				}
				return new ReportEnumProperty<FontStyles>(styleDefinition.IsExpression, expressionString, StyleTranslator.TranslateFontStyle(styleDefinition.Value, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumFontStyle);
			case StyleAttributeNames.FontFamily:
			case StyleAttributeNames.Format:
			case StyleAttributeNames.Language:
			case StyleAttributeNames.NumeralLanguage:
			case StyleAttributeNames.CurrencyLanguage:
				if (styleDefinition == null)
				{
					return new ReportStringProperty(false, null, this.m_styleDefaults[styleNameString] as string);
				}
				return new ReportStringProperty(styleDefinition.IsExpression, expressionString, styleDefinition.Value, styleDefinition.IsExpression ? (this.m_styleDefaults[styleNameString] as string) : null);
			case StyleAttributeNames.FontWeight:
				if (styleDefinition == null)
				{
					return new ReportEnumProperty<FontWeights>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumFontWeight);
				}
				return new ReportEnumProperty<FontWeights>(styleDefinition.IsExpression, expressionString, StyleTranslator.TranslateFontWeight(styleDefinition.Value, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumFontWeight);
			case StyleAttributeNames.TextDecoration:
				if (styleDefinition == null)
				{
					return new ReportEnumProperty<TextDecorations>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumTextDecoration);
				}
				return new ReportEnumProperty<TextDecorations>(styleDefinition.IsExpression, expressionString, StyleTranslator.TranslateTextDecoration(styleDefinition.Value, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumTextDecoration);
			case StyleAttributeNames.TextAlign:
				if (styleDefinition == null)
				{
					return new ReportEnumProperty<TextAlignments>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumTextAlignment);
				}
				return new ReportEnumProperty<TextAlignments>(styleDefinition.IsExpression, expressionString, StyleTranslator.TranslateTextAlign(styleDefinition.Value, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumTextAlignment);
			case StyleAttributeNames.VerticalAlign:
				if (styleDefinition == null)
				{
					return new ReportEnumProperty<VerticalAlignments>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumVerticalAlignment);
				}
				return new ReportEnumProperty<VerticalAlignments>(styleDefinition.IsExpression, expressionString, StyleTranslator.TranslateVerticalAlign(styleDefinition.Value, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumVerticalAlignment);
			case StyleAttributeNames.Direction:
				if (styleDefinition == null)
				{
					return new ReportEnumProperty<Directions>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumDirection);
				}
				return new ReportEnumProperty<Directions>(styleDefinition.IsExpression, expressionString, StyleTranslator.TranslateDirection(styleDefinition.Value, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumDirection);
			case StyleAttributeNames.WritingMode:
				if (styleDefinition == null)
				{
					return new ReportEnumProperty<WritingModes>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumWritingMode);
				}
				return new ReportEnumProperty<WritingModes>(styleDefinition.IsExpression, expressionString, StyleTranslator.TranslateWritingMode(styleDefinition.Value, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumWritingMode);
			case StyleAttributeNames.UnicodeBiDi:
				if (styleDefinition == null)
				{
					return new ReportEnumProperty<UnicodeBiDiTypes>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumUnicodeBiDiType);
				}
				return new ReportEnumProperty<UnicodeBiDiTypes>(styleDefinition.IsExpression, expressionString, StyleTranslator.TranslateUnicodeBiDi(styleDefinition.Value, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumUnicodeBiDiType);
			case StyleAttributeNames.Calendar:
				if (styleDefinition == null)
				{
					return new ReportEnumProperty<Calendars>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumCalendar);
				}
				return new ReportEnumProperty<Calendars>(styleDefinition.IsExpression, expressionString, StyleTranslator.TranslateCalendar(styleDefinition.Value, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumCalendar);
			case StyleAttributeNames.NumeralVariant:
			{
				int num = (int)this.m_styleDefaults[styleNameString];
				if (styleDefinition == null)
				{
					return new ReportIntProperty(false, null, num, num);
				}
				return new ReportIntProperty(styleDefinition.IsExpression, expressionString, styleDefinition.IntValue, num);
			}
			case StyleAttributeNames.BackgroundGradientType:
				if (styleDefinition == null)
				{
					return new ReportEnumProperty<BackgroundGradients>(Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumBackgroundGradient);
				}
				return new ReportEnumProperty<BackgroundGradients>(styleDefinition.IsExpression, expressionString, StyleTranslator.TranslateBackgroundGradientType(styleDefinition.Value, null), Microsoft.ReportingServices.OnDemandReportRendering.Style.DefaultEnumBackgroundGradient);
			case StyleAttributeNames.BackgroundHatchType:
			{
				BackgroundHatchTypes backgroundHatchTypes = StyleTranslator.TranslateBackgroundHatchType(null, null, this.m_isDynamicImageStyle);
				if (styleDefinition == null)
				{
					return new ReportEnumProperty<BackgroundHatchTypes>(false, null, BackgroundHatchTypes.Default, backgroundHatchTypes);
				}
				return new ReportEnumProperty<BackgroundHatchTypes>(styleDefinition.IsExpression, expressionString, StyleTranslator.TranslateBackgroundHatchType(styleDefinition.Value, null, this.m_isDynamicImageStyle), backgroundHatchTypes);
			}
			case StyleAttributeNames.TextEffect:
			{
				TextEffects textEffects = StyleTranslator.TranslateTextEffect(null, null, this.m_isDynamicImageStyle);
				if (styleDefinition == null)
				{
					return new ReportEnumProperty<TextEffects>(false, null, TextEffects.Default, textEffects);
				}
				return new ReportEnumProperty<TextEffects>(styleDefinition.IsExpression, expressionString, StyleTranslator.TranslateTextEffect(styleDefinition.Value, null, this.m_isDynamicImageStyle), textEffects);
			}
			case StyleAttributeNames.BackgroundImage:
				if (this.IsAvailableStyle(styleName))
				{
					BackgroundImage backgroundImage = null;
					if (style != null)
					{
						backgroundImage = style[styleNameString] as BackgroundImage;
					}
					if (backgroundImage != null)
					{
						return new BackgroundImage(true, expressionString, style, this);
					}
				}
				break;
			}
			return null;
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x0007DF1C File Offset: 0x0007C11C
		protected virtual bool IsAvailableStyle(StyleAttributeNames styleName)
		{
			return true;
		}

		// Token: 0x04001033 RID: 4147
		private bool m_isOldSnapshot;

		// Token: 0x04001034 RID: 4148
		private IStyleContainer m_iStyleContainer;

		// Token: 0x04001035 RID: 4149
		private Microsoft.ReportingServices.ReportRendering.ReportItem m_renderReportItem;

		// Token: 0x04001036 RID: 4150
		private Microsoft.ReportingServices.ReportRendering.Style m_cachedRenderStyle;

		// Token: 0x04001037 RID: 4151
		private bool m_isLineBorderStyle;

		// Token: 0x04001038 RID: 4152
		private Microsoft.ReportingServices.OnDemandReportRendering.Style.StyleDefaults m_styleDefaults;

		// Token: 0x04001039 RID: 4153
		private BackgroundImage m_backgroundImage;

		// Token: 0x0400103A RID: 4154
		private Border m_border;

		// Token: 0x0400103B RID: 4155
		private Border m_topBorder;

		// Token: 0x0400103C RID: 4156
		private Border m_rightBorder;

		// Token: 0x0400103D RID: 4157
		private Border m_bottomBorder;

		// Token: 0x0400103E RID: 4158
		private Border m_leftBorder;

		// Token: 0x0400103F RID: 4159
		private Microsoft.ReportingServices.OnDemandReportRendering.Style.StyleDefaults m_normalStyleDefaults;

		// Token: 0x04001040 RID: 4160
		private Microsoft.ReportingServices.OnDemandReportRendering.Style.StyleDefaults m_lineStyleDefaults;

		// Token: 0x04001041 RID: 4161
		private IReportScope m_reportScope;

		// Token: 0x04001042 RID: 4162
		private ReportElement m_reportElement;

		// Token: 0x04001043 RID: 4163
		private Microsoft.ReportingServices.ReportProcessing.Style m_styleDef;

		// Token: 0x04001044 RID: 4164
		private bool m_isDynamicImageStyle;

		// Token: 0x04001045 RID: 4165
		private object[] m_styleValues;

		// Token: 0x04001046 RID: 4166
		private ReportProperty[] m_cachedReportProperties = new ReportProperty[51];

		// Token: 0x04001047 RID: 4167
		private bool m_disallowBorderTransparencyOnDynamicImage;

		// Token: 0x04001048 RID: 4168
		internal static FontStyles DefaultEnumFontStyle = FontStyles.Normal;

		// Token: 0x04001049 RID: 4169
		internal static FontWeights DefaultEnumFontWeight = FontWeights.Normal;

		// Token: 0x0400104A RID: 4170
		internal static TextDecorations DefaultEnumTextDecoration = TextDecorations.None;

		// Token: 0x0400104B RID: 4171
		internal static TextAlignments DefaultEnumTextAlignment = TextAlignments.General;

		// Token: 0x0400104C RID: 4172
		internal static VerticalAlignments DefaultEnumVerticalAlignment = VerticalAlignments.Top;

		// Token: 0x0400104D RID: 4173
		internal static Directions DefaultEnumDirection = Directions.LTR;

		// Token: 0x0400104E RID: 4174
		internal static WritingModes DefaultEnumWritingMode = WritingModes.Horizontal;

		// Token: 0x0400104F RID: 4175
		internal static UnicodeBiDiTypes DefaultEnumUnicodeBiDiType = UnicodeBiDiTypes.Normal;

		// Token: 0x04001050 RID: 4176
		internal static Calendars DefaultEnumCalendar = Calendars.Default;

		// Token: 0x04001051 RID: 4177
		internal static BackgroundGradients DefaultEnumBackgroundGradient = BackgroundGradients.None;

		// Token: 0x04001052 RID: 4178
		internal static BackgroundRepeatTypes DefaultEnumBackgroundRepeatType = BackgroundRepeatTypes.Repeat;

		// Token: 0x02000951 RID: 2385
		internal sealed class StyleDefaults
		{
			// Token: 0x06007FBF RID: 32703 RVA: 0x0020E2A4 File Offset: 0x0020C4A4
			internal StyleDefaults(bool isLine, string defaultFontFamily)
			{
				this.m_nameMap = new Hashtable(51);
				this.m_keyCollection = new string[51];
				this.m_valueCollection = new object[51];
				int num = 0;
				this.m_nameMap["BorderColor"] = num;
				this.m_keyCollection[num] = "BorderColor";
				this.m_valueCollection[num++] = new ReportColor("Black", global::System.Drawing.Color.Empty, false);
				this.m_nameMap["BorderColorTop"] = num;
				this.m_keyCollection[num] = "BorderColorTop";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderColorLeft"] = num;
				this.m_keyCollection[num] = "BorderColorLeft";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderColorRight"] = num;
				this.m_keyCollection[num] = "BorderColorRight";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderColorBottom"] = num;
				this.m_keyCollection[num] = "BorderColorBottom";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderStyle"] = num;
				this.m_keyCollection[num] = "BorderStyle";
				if (!isLine)
				{
					this.m_valueCollection[num++] = "None";
				}
				else
				{
					this.m_valueCollection[num++] = "Solid";
				}
				this.m_nameMap["BorderStyleTop"] = num;
				this.m_keyCollection[num] = "BorderStyleTop";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderStyleLeft"] = num;
				this.m_keyCollection[num] = "BorderStyleLeft";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderStyleRight"] = num;
				this.m_keyCollection[num] = "BorderStyleRight";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderStyleBottom"] = num;
				this.m_keyCollection[num] = "BorderStyleBottom";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderWidth"] = num;
				this.m_keyCollection[num] = "BorderWidth";
				this.m_valueCollection[num++] = new ReportSize("1pt");
				this.m_nameMap["BorderWidthTop"] = num;
				this.m_keyCollection[num] = "BorderWidthTop";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderWidthLeft"] = num;
				this.m_keyCollection[num] = "BorderWidthLeft";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderWidthRight"] = num;
				this.m_keyCollection[num] = "BorderWidthRight";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BorderWidthBottom"] = num;
				this.m_keyCollection[num] = "BorderWidthBottom";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BackgroundColor"] = num;
				this.m_keyCollection[num] = "BackgroundColor";
				this.m_valueCollection[num++] = new ReportColor("Transparent", global::System.Drawing.Color.Empty, true);
				this.m_nameMap["BackgroundGradientType"] = num;
				this.m_keyCollection[num] = "BackgroundGradientType";
				this.m_valueCollection[num++] = BackgroundGradients.None;
				this.m_nameMap["BackgroundGradientEndColor"] = num;
				this.m_keyCollection[num] = "BackgroundGradientEndColor";
				this.m_valueCollection[num++] = new ReportColor("Transparent", global::System.Drawing.Color.Empty, true);
				this.m_nameMap["BackgroundImage"] = num;
				this.m_keyCollection[num] = "BackgroundImage";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BackgroundRepeat"] = num;
				this.m_keyCollection[num] = "BackgroundRepeat";
				this.m_valueCollection[num++] = "Repeat";
				this.m_nameMap["FontStyle"] = num;
				this.m_keyCollection[num] = "FontStyle";
				this.m_valueCollection[num++] = "Normal";
				this.m_nameMap["FontFamily"] = num;
				this.m_keyCollection[num] = "FontFamily";
				this.m_valueCollection[num++] = defaultFontFamily ?? "Arial";
				this.m_nameMap["FontSize"] = num;
				this.m_keyCollection[num] = "FontSize";
				this.m_valueCollection[num++] = new ReportSize("10pt");
				this.m_nameMap["FontWeight"] = num;
				this.m_keyCollection[num] = "FontWeight";
				this.m_valueCollection[num++] = "Normal";
				this.m_nameMap["Format"] = num;
				this.m_keyCollection[num] = "Format";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["TextDecoration"] = num;
				this.m_keyCollection[num] = "TextDecoration";
				this.m_valueCollection[num++] = "None";
				this.m_nameMap["TextAlign"] = num;
				this.m_keyCollection[num] = "TextAlign";
				this.m_valueCollection[num++] = "General";
				this.m_nameMap["VerticalAlign"] = num;
				this.m_keyCollection[num] = "VerticalAlign";
				this.m_valueCollection[num++] = "Top";
				this.m_nameMap["Color"] = num;
				this.m_keyCollection[num] = "Color";
				this.m_valueCollection[num++] = new ReportColor("Black", global::System.Drawing.Color.Empty, false);
				this.m_nameMap["PaddingLeft"] = num;
				this.m_keyCollection[num] = "PaddingLeft";
				this.m_valueCollection[num++] = new ReportSize("0pt", 0.0);
				this.m_nameMap["PaddingRight"] = num;
				this.m_keyCollection[num] = "PaddingRight";
				this.m_valueCollection[num++] = new ReportSize("0pt", 0.0);
				this.m_nameMap["PaddingTop"] = num;
				this.m_keyCollection[num] = "PaddingTop";
				this.m_valueCollection[num++] = new ReportSize("0pt", 0.0);
				this.m_nameMap["PaddingBottom"] = num;
				this.m_keyCollection[num] = "PaddingBottom";
				this.m_valueCollection[num++] = new ReportSize("0pt", 0.0);
				this.m_nameMap["LineHeight"] = num;
				this.m_keyCollection[num] = "LineHeight";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["Direction"] = num;
				this.m_keyCollection[num] = "Direction";
				this.m_valueCollection[num++] = "LTR";
				this.m_nameMap["WritingMode"] = num;
				this.m_keyCollection[num] = "WritingMode";
				this.m_valueCollection[num++] = "lr-tb";
				this.m_nameMap["Language"] = num;
				this.m_keyCollection[num] = "Language";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["UnicodeBiDi"] = num;
				this.m_keyCollection[num] = "UnicodeBiDi";
				this.m_valueCollection[num++] = "Normal";
				this.m_nameMap["Calendar"] = num;
				this.m_keyCollection[num] = "Calendar";
				this.m_valueCollection[num++] = "Gregorian";
				this.m_nameMap["NumeralLanguage"] = num;
				this.m_keyCollection[num] = "NumeralLanguage";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["NumeralVariant"] = num;
				this.m_keyCollection[num] = "NumeralVariant";
				this.m_valueCollection[num++] = 1;
				this.m_nameMap["TextEffect"] = num;
				this.m_keyCollection[num] = "TextEffect";
				this.m_valueCollection[num++] = "None";
				this.m_nameMap["BackgroundHatchType"] = num;
				this.m_keyCollection[num] = "BackgroundHatchType";
				this.m_valueCollection[num++] = "None";
				this.m_nameMap["ShadowColor"] = num;
				this.m_keyCollection[num] = "ShadowColor";
				this.m_valueCollection[num++] = new ReportColor("Transparent", global::System.Drawing.Color.Empty, true);
				this.m_nameMap["ShadowOffset"] = num;
				this.m_keyCollection[num] = "ShadowOffset";
				this.m_valueCollection[num++] = new ReportSize("0pt", 0.0);
				this.m_nameMap["Position"] = num;
				this.m_keyCollection[num] = "Position";
				this.m_valueCollection[num++] = "Center";
				this.m_nameMap["TransparentColor"] = num;
				this.m_keyCollection[num] = "TransparentColor";
				this.m_valueCollection[num++] = new ReportColor("Transparent", global::System.Drawing.Color.Empty, true);
				this.m_nameMap["BackgroundImageSource"] = num;
				this.m_keyCollection[num] = "BackgroundImageSource";
				this.m_valueCollection[num++] = Microsoft.ReportingServices.ReportRendering.Image.SourceType.External;
				this.m_nameMap["BackgroundImageValue"] = num;
				this.m_keyCollection[num] = "BackgroundImageValue";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["BackgroundImageMIMEType"] = num;
				this.m_keyCollection[num] = "BackgroundImageMIMEType";
				this.m_valueCollection[num++] = null;
				this.m_nameMap["CurrencyLanguage"] = num;
				this.m_keyCollection[num] = "CurrencyLanguage";
				this.m_valueCollection[num++] = null;
				Global.Tracer.Assert(51 == num, "(Style.StyleAttributeCount == index)");
			}

			// Token: 0x17002977 RID: 10615
			internal object this[int index]
			{
				get
				{
					return this.m_valueCollection[index];
				}
			}

			// Token: 0x17002978 RID: 10616
			internal object this[string styleName]
			{
				get
				{
					return this.m_valueCollection[(int)this.m_nameMap[styleName]];
				}
			}

			// Token: 0x06007FC2 RID: 32706 RVA: 0x0020EDD4 File Offset: 0x0020CFD4
			internal string GetName(int index)
			{
				return this.m_keyCollection[index];
			}

			// Token: 0x0400406A RID: 16490
			private Hashtable m_nameMap;

			// Token: 0x0400406B RID: 16491
			private string[] m_keyCollection;

			// Token: 0x0400406C RID: 16492
			private object[] m_valueCollection;
		}
	}
}
