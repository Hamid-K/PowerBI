using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200281E RID: 10270
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(GroupFill))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(Highlight))]
	[ChildElementInfo(typeof(UnderlineFollowsText))]
	[ChildElementInfo(typeof(Underline))]
	[ChildElementInfo(typeof(UnderlineFillText))]
	[ChildElementInfo(typeof(UnderlineFill))]
	[ChildElementInfo(typeof(LatinFont))]
	[ChildElementInfo(typeof(EastAsianFont))]
	[ChildElementInfo(typeof(ComplexScriptFont))]
	[ChildElementInfo(typeof(SymbolFont))]
	[ChildElementInfo(typeof(HyperlinkOnClick))]
	[ChildElementInfo(typeof(HyperlinkOnMouseOver))]
	[ChildElementInfo(typeof(RightToLeft), FileFormatVersions.Office2010)]
	internal abstract class TextCharacterPropertiesType : OpenXmlCompositeElement
	{
		// Token: 0x17006599 RID: 26009
		// (get) Token: 0x06014184 RID: 82308 RVA: 0x0030F120 File Offset: 0x0030D320
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextCharacterPropertiesType.attributeTagNames;
			}
		}

		// Token: 0x1700659A RID: 26010
		// (get) Token: 0x06014185 RID: 82309 RVA: 0x0030F127 File Offset: 0x0030D327
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextCharacterPropertiesType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700659B RID: 26011
		// (get) Token: 0x06014186 RID: 82310 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06014187 RID: 82311 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "kumimoji")]
		public BooleanValue Kumimoji
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700659C RID: 26012
		// (get) Token: 0x06014188 RID: 82312 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06014189 RID: 82313 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "lang")]
		public StringValue Language
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700659D RID: 26013
		// (get) Token: 0x0601418A RID: 82314 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601418B RID: 82315 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "altLang")]
		public StringValue AlternativeLanguage
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700659E RID: 26014
		// (get) Token: 0x0601418C RID: 82316 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x0601418D RID: 82317 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "sz")]
		public Int32Value FontSize
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700659F RID: 26015
		// (get) Token: 0x0601418E RID: 82318 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0601418F RID: 82319 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "b")]
		public BooleanValue Bold
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170065A0 RID: 26016
		// (get) Token: 0x06014190 RID: 82320 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06014191 RID: 82321 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "i")]
		public BooleanValue Italic
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170065A1 RID: 26017
		// (get) Token: 0x06014192 RID: 82322 RVA: 0x0030F12E File Offset: 0x0030D32E
		// (set) Token: 0x06014193 RID: 82323 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "u")]
		public EnumValue<TextUnderlineValues> Underline
		{
			get
			{
				return (EnumValue<TextUnderlineValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170065A2 RID: 26018
		// (get) Token: 0x06014194 RID: 82324 RVA: 0x0030F13D File Offset: 0x0030D33D
		// (set) Token: 0x06014195 RID: 82325 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "strike")]
		public EnumValue<TextStrikeValues> Strike
		{
			get
			{
				return (EnumValue<TextStrikeValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170065A3 RID: 26019
		// (get) Token: 0x06014196 RID: 82326 RVA: 0x002ED55B File Offset: 0x002EB75B
		// (set) Token: 0x06014197 RID: 82327 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "kern")]
		public Int32Value Kerning
		{
			get
			{
				return (Int32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170065A4 RID: 26020
		// (get) Token: 0x06014198 RID: 82328 RVA: 0x0030F14C File Offset: 0x0030D34C
		// (set) Token: 0x06014199 RID: 82329 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "cap")]
		public EnumValue<TextCapsValues> Capital
		{
			get
			{
				return (EnumValue<TextCapsValues>)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x170065A5 RID: 26021
		// (get) Token: 0x0601419A RID: 82330 RVA: 0x002E7730 File Offset: 0x002E5930
		// (set) Token: 0x0601419B RID: 82331 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "spc")]
		public Int32Value Spacing
		{
			get
			{
				return (Int32Value)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x170065A6 RID: 26022
		// (get) Token: 0x0601419C RID: 82332 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x0601419D RID: 82333 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "normalizeH")]
		public BooleanValue NormalizeHeight
		{
			get
			{
				return (BooleanValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x170065A7 RID: 26023
		// (get) Token: 0x0601419E RID: 82334 RVA: 0x0030F15C File Offset: 0x0030D35C
		// (set) Token: 0x0601419F RID: 82335 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "baseline")]
		public Int32Value Baseline
		{
			get
			{
				return (Int32Value)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x170065A8 RID: 26024
		// (get) Token: 0x060141A0 RID: 82336 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x060141A1 RID: 82337 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "noProof")]
		public BooleanValue NoProof
		{
			get
			{
				return (BooleanValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x170065A9 RID: 26025
		// (get) Token: 0x060141A2 RID: 82338 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x060141A3 RID: 82339 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "dirty")]
		public BooleanValue Dirty
		{
			get
			{
				return (BooleanValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x170065AA RID: 26026
		// (get) Token: 0x060141A4 RID: 82340 RVA: 0x002CA745 File Offset: 0x002C8945
		// (set) Token: 0x060141A5 RID: 82341 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "err")]
		public BooleanValue SpellingError
		{
			get
			{
				return (BooleanValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x170065AB RID: 26027
		// (get) Token: 0x060141A6 RID: 82342 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x060141A7 RID: 82343 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "smtClean")]
		public BooleanValue SmartTagClean
		{
			get
			{
				return (BooleanValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x170065AC RID: 26028
		// (get) Token: 0x060141A8 RID: 82344 RVA: 0x0030F16C File Offset: 0x0030D36C
		// (set) Token: 0x060141A9 RID: 82345 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "smtId")]
		public UInt32Value SmartTagId
		{
			get
			{
				return (UInt32Value)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x170065AD RID: 26029
		// (get) Token: 0x060141AA RID: 82346 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x060141AB RID: 82347 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "bmk")]
		public StringValue Bookmark
		{
			get
			{
				return (StringValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x060141AC RID: 82348 RVA: 0x0030F17C File Offset: 0x0030D37C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ln" == name)
			{
				return new Outline();
			}
			if (10 == namespaceId && "noFill" == name)
			{
				return new NoFill();
			}
			if (10 == namespaceId && "solidFill" == name)
			{
				return new SolidFill();
			}
			if (10 == namespaceId && "gradFill" == name)
			{
				return new GradientFill();
			}
			if (10 == namespaceId && "blipFill" == name)
			{
				return new BlipFill();
			}
			if (10 == namespaceId && "pattFill" == name)
			{
				return new PatternFill();
			}
			if (10 == namespaceId && "grpFill" == name)
			{
				return new GroupFill();
			}
			if (10 == namespaceId && "effectLst" == name)
			{
				return new EffectList();
			}
			if (10 == namespaceId && "effectDag" == name)
			{
				return new EffectDag();
			}
			if (10 == namespaceId && "highlight" == name)
			{
				return new Highlight();
			}
			if (10 == namespaceId && "uLnTx" == name)
			{
				return new UnderlineFollowsText();
			}
			if (10 == namespaceId && "uLn" == name)
			{
				return new Underline();
			}
			if (10 == namespaceId && "uFillTx" == name)
			{
				return new UnderlineFillText();
			}
			if (10 == namespaceId && "uFill" == name)
			{
				return new UnderlineFill();
			}
			if (10 == namespaceId && "latin" == name)
			{
				return new LatinFont();
			}
			if (10 == namespaceId && "ea" == name)
			{
				return new EastAsianFont();
			}
			if (10 == namespaceId && "cs" == name)
			{
				return new ComplexScriptFont();
			}
			if (10 == namespaceId && "sym" == name)
			{
				return new SymbolFont();
			}
			if (10 == namespaceId && "hlinkClick" == name)
			{
				return new HyperlinkOnClick();
			}
			if (10 == namespaceId && "hlinkMouseOver" == name)
			{
				return new HyperlinkOnMouseOver();
			}
			if (10 == namespaceId && "rtl" == name)
			{
				return new RightToLeft();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170065AE RID: 26030
		// (get) Token: 0x060141AD RID: 82349 RVA: 0x0030F39A File Offset: 0x0030D59A
		internal override string[] ElementTagNames
		{
			get
			{
				return TextCharacterPropertiesType.eleTagNames;
			}
		}

		// Token: 0x170065AF RID: 26031
		// (get) Token: 0x060141AE RID: 82350 RVA: 0x0030F3A1 File Offset: 0x0030D5A1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextCharacterPropertiesType.eleNamespaceIds;
			}
		}

		// Token: 0x170065B0 RID: 26032
		// (get) Token: 0x060141AF RID: 82351 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170065B1 RID: 26033
		// (get) Token: 0x060141B0 RID: 82352 RVA: 0x002EF250 File Offset: 0x002ED450
		// (set) Token: 0x060141B1 RID: 82353 RVA: 0x002EF259 File Offset: 0x002ED459
		public Outline Outline
		{
			get
			{
				return base.GetElement<Outline>(0);
			}
			set
			{
				base.SetElement<Outline>(0, value);
			}
		}

		// Token: 0x060141B2 RID: 82354 RVA: 0x0030F3A8 File Offset: 0x0030D5A8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "kumimoji" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "lang" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "altLang" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sz" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "b" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "i" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "u" == name)
			{
				return new EnumValue<TextUnderlineValues>();
			}
			if (namespaceId == 0 && "strike" == name)
			{
				return new EnumValue<TextStrikeValues>();
			}
			if (namespaceId == 0 && "kern" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "cap" == name)
			{
				return new EnumValue<TextCapsValues>();
			}
			if (namespaceId == 0 && "spc" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "normalizeH" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "baseline" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "noProof" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dirty" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "err" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "smtClean" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "smtId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "bmk" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060141B3 RID: 82355 RVA: 0x00293ECF File Offset: 0x002920CF
		protected TextCharacterPropertiesType()
		{
		}

		// Token: 0x060141B4 RID: 82356 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected TextCharacterPropertiesType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060141B5 RID: 82357 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected TextCharacterPropertiesType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060141B6 RID: 82358 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected TextCharacterPropertiesType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060141B7 RID: 82359 RVA: 0x0030F560 File Offset: 0x0030D760
		// Note: this type is marked as 'beforefieldinit'.
		static TextCharacterPropertiesType()
		{
			byte[] array = new byte[19];
			TextCharacterPropertiesType.attributeNamespaceIds = array;
			TextCharacterPropertiesType.eleTagNames = new string[]
			{
				"ln", "noFill", "solidFill", "gradFill", "blipFill", "pattFill", "grpFill", "effectLst", "effectDag", "highlight",
				"uLnTx", "uLn", "uFillTx", "uFill", "latin", "ea", "cs", "sym", "hlinkClick", "hlinkMouseOver",
				"rtl", "extLst"
			};
			TextCharacterPropertiesType.eleNamespaceIds = new byte[]
			{
				10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
				10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
				10, 10
			};
		}

		// Token: 0x04008904 RID: 35076
		private static string[] attributeTagNames = new string[]
		{
			"kumimoji", "lang", "altLang", "sz", "b", "i", "u", "strike", "kern", "cap",
			"spc", "normalizeH", "baseline", "noProof", "dirty", "err", "smtClean", "smtId", "bmk"
		};

		// Token: 0x04008905 RID: 35077
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008906 RID: 35078
		private static readonly string[] eleTagNames;

		// Token: 0x04008907 RID: 35079
		private static readonly byte[] eleNamespaceIds;
	}
}
