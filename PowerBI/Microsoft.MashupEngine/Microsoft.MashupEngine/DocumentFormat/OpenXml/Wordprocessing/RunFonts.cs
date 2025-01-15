using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E8D RID: 11917
	[GeneratedCode("DomGen", "2.0")]
	internal class RunFonts : OpenXmlLeafElement
	{
		// Token: 0x17008B10 RID: 35600
		// (get) Token: 0x06019510 RID: 103696 RVA: 0x00348730 File Offset: 0x00346930
		public override string LocalName
		{
			get
			{
				return "rFonts";
			}
		}

		// Token: 0x17008B11 RID: 35601
		// (get) Token: 0x06019511 RID: 103697 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B12 RID: 35602
		// (get) Token: 0x06019512 RID: 103698 RVA: 0x00348737 File Offset: 0x00346937
		internal override int ElementTypeId
		{
			get
			{
				return 11576;
			}
		}

		// Token: 0x06019513 RID: 103699 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008B13 RID: 35603
		// (get) Token: 0x06019514 RID: 103700 RVA: 0x0034873E File Offset: 0x0034693E
		internal override string[] AttributeTagNames
		{
			get
			{
				return RunFonts.attributeTagNames;
			}
		}

		// Token: 0x17008B14 RID: 35604
		// (get) Token: 0x06019515 RID: 103701 RVA: 0x00348745 File Offset: 0x00346945
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RunFonts.attributeNamespaceIds;
			}
		}

		// Token: 0x17008B15 RID: 35605
		// (get) Token: 0x06019516 RID: 103702 RVA: 0x0034874C File Offset: 0x0034694C
		// (set) Token: 0x06019517 RID: 103703 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "hint")]
		public EnumValue<FontTypeHintValues> Hint
		{
			get
			{
				return (EnumValue<FontTypeHintValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008B16 RID: 35606
		// (get) Token: 0x06019518 RID: 103704 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06019519 RID: 103705 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "ascii")]
		public StringValue Ascii
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

		// Token: 0x17008B17 RID: 35607
		// (get) Token: 0x0601951A RID: 103706 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601951B RID: 103707 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "hAnsi")]
		public StringValue HighAnsi
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

		// Token: 0x17008B18 RID: 35608
		// (get) Token: 0x0601951C RID: 103708 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601951D RID: 103709 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "eastAsia")]
		public StringValue EastAsia
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17008B19 RID: 35609
		// (get) Token: 0x0601951E RID: 103710 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0601951F RID: 103711 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "cs")]
		public StringValue ComplexScript
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17008B1A RID: 35610
		// (get) Token: 0x06019520 RID: 103712 RVA: 0x0034875B File Offset: 0x0034695B
		// (set) Token: 0x06019521 RID: 103713 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "asciiTheme")]
		public EnumValue<ThemeFontValues> AsciiTheme
		{
			get
			{
				return (EnumValue<ThemeFontValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17008B1B RID: 35611
		// (get) Token: 0x06019522 RID: 103714 RVA: 0x0034876A File Offset: 0x0034696A
		// (set) Token: 0x06019523 RID: 103715 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(23, "hAnsiTheme")]
		public EnumValue<ThemeFontValues> HighAnsiTheme
		{
			get
			{
				return (EnumValue<ThemeFontValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17008B1C RID: 35612
		// (get) Token: 0x06019524 RID: 103716 RVA: 0x00348779 File Offset: 0x00346979
		// (set) Token: 0x06019525 RID: 103717 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(23, "eastAsiaTheme")]
		public EnumValue<ThemeFontValues> EastAsiaTheme
		{
			get
			{
				return (EnumValue<ThemeFontValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17008B1D RID: 35613
		// (get) Token: 0x06019526 RID: 103718 RVA: 0x00348788 File Offset: 0x00346988
		// (set) Token: 0x06019527 RID: 103719 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(23, "cstheme")]
		public EnumValue<ThemeFontValues> ComplexScriptTheme
		{
			get
			{
				return (EnumValue<ThemeFontValues>)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x06019529 RID: 103721 RVA: 0x00348798 File Offset: 0x00346998
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "hint" == name)
			{
				return new EnumValue<FontTypeHintValues>();
			}
			if (23 == namespaceId && "ascii" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "hAnsi" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "eastAsia" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "cs" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "asciiTheme" == name)
			{
				return new EnumValue<ThemeFontValues>();
			}
			if (23 == namespaceId && "hAnsiTheme" == name)
			{
				return new EnumValue<ThemeFontValues>();
			}
			if (23 == namespaceId && "eastAsiaTheme" == name)
			{
				return new EnumValue<ThemeFontValues>();
			}
			if (23 == namespaceId && "cstheme" == name)
			{
				return new EnumValue<ThemeFontValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601952A RID: 103722 RVA: 0x00348885 File Offset: 0x00346A85
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RunFonts>(deep);
		}

		// Token: 0x0400A84D RID: 43085
		private const string tagName = "rFonts";

		// Token: 0x0400A84E RID: 43086
		private const byte tagNsId = 23;

		// Token: 0x0400A84F RID: 43087
		internal const int ElementTypeIdConst = 11576;

		// Token: 0x0400A850 RID: 43088
		private static string[] attributeTagNames = new string[] { "hint", "ascii", "hAnsi", "eastAsia", "cs", "asciiTheme", "hAnsiTheme", "eastAsiaTheme", "cstheme" };

		// Token: 0x0400A851 RID: 43089
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23, 23, 23 };
	}
}
